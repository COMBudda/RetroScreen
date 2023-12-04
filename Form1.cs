using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Saleae SDK
using System;
using System.Collections.Generic;
using SaleaeDeviceSdkDotNet;


namespace CGATest
{

    public partial class Form1 : Form
    {
        static int width = 1500, height = 500;
        volatile Bitmap newframe = new Bitmap(1500, 500);
        volatile bool newframeisready = false;

        public volatile StreamReader vIn;
        public volatile ConcurrentQueue<int> bufferedData = new ConcurrentQueue<int>();
        static int maxBuffer = 300000;

        bool endofstream = false;
        public volatile int TB1 = 0, TB2 = 0, TB3 = 0, screens = 0;

        int xposmax = 0, yposmax = 0, xposmax_b = 1, yposmax_b = 1, yposA = 0;

        int vexpected = 0, hexpected = 0, vxc = 0, vxm = 0;
        //int csync_min = 5000000, csync_max = 0;
        int hsync_filterbits = 7, vsync_filterbits = 7;
        static System.Windows.Forms.Timer fTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer ftTimer = new System.Windows.Forms.Timer();
        bool invert_sync = false, vb = false, invert_color = false;

        // Color Mode 0-RGB, 1-MDA, 2-CGA, 3-EGA
        int colorMode = 0;

        // size
        bool originalSize = false;
        int xResize = 1024, yResize = 768;

        //Composite Stuff
        int syncpulses = 0, sync_long = 0, sync_short = 0;
        bool composite_sync = false;

        // process
        volatile ConcurrentQueue<int> processIds = new ConcurrentQueue<int>();

        //Saleae
        UInt32 mSampleRateHz = 24000000;
        MLogic mLogic;
        MLogic16 mLogic16;
        byte mWriteValue = 0;
        bool restartRequired = true;

        // vsync_polarity handling
        bool v_sync_auto_handling = true;
        bool v_inverted = false;
        int v_cycles = 0;

        public Form1()
        {

            InitializeComponent();
            pictureBox1.Image = new Bitmap(width, height);
            ftTimer.Tick += new EventHandler(TShowFrames);

            ftTimer.Interval = 1000;
            ftTimer.Enabled = true;
            ftTimer.Start();

            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }


        private void TShowFrames(object myObject, EventArgs myEventArgs)
        {
            textBox3.Text = screens.ToString();

            textBox1.Text = TB1.ToString();
            textBox2.Text = TB2.ToString();
            screens = 0;
        }


        private void TShowPic(object? obj)
        {
            Bitmap? resized = null;
            CancellationToken ct = (CancellationToken) obj;

            var t = Task.Run(() =>
            {
                while (true && ! ct.IsCancellationRequested)
                {
                    if (newframeisready)
                    {
                        if (originalSize) // Do we want to keep the original raw picture
                            {
                                pictureBox1.Image = (Bitmap)newframe.Clone();
                            }
                            else // or do we scale up
                            {
                                resized = new Bitmap((Bitmap)newframe, new Size(xResize, yResize));
                                pictureBox1.Image = (Bitmap)resized.Clone();
                                resized.Dispose();  
                            }
                        newframeisready = false;
                    }
                    // Wait for a 50Hz refresh
                    Thread.Sleep(1);
                }
            }
            );
        }


        // Read input stream to buffer 
        private async void TBufferInput(object? o)
        {

            Process sigrok = null;
            int buffSize = 32;
            int maxBuff = 2000000;
            bool loop = true;
            int readchar = 0;
            char[] c = new char[buffSize];
            CancellationToken ct = (CancellationToken) o;

            vIn = new StreamReader(Console.OpenStandardInput(), System.Text.Encoding.Latin1, bufferSize: 131072);

            while (loop && vIn != null && !ct.IsCancellationRequested)
            {
                readchar = await vIn.ReadAsync(c, 0, buffSize); 
                TB3 = bufferedData.Count;
                int i = 0;
                // if (TB3<maxBuff) 
                try
                {
                    // Delay input since the file is much faster than the drawing
                    while(bufferedData.Count > maxBuff) { Thread.Sleep(1); }
                    for (i = 0; i < readchar; i++) bufferedData.Enqueue(c[i]);
                }
                catch (Exception ex)
                {
                    // In case we are out of memory
                    bufferedData.Clear();
                    vIn.DiscardBufferedData();
                }
            }
        }

        private async void TSaleaeWatchdog(object? o)
        {
            CancellationToken ct = (CancellationToken) o;

            await Task.Run(() =>
            {
                while (true && !ct.IsCancellationRequested)
                {
                    if (restartRequired) mLogic.ReadStart();
                    restartRequired = false;
                    Thread.Sleep(100);
                }
            });
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            // Starting threads for data input, Saleae connection health and picture display

            Thread Handler_TBufferInput = null;
            CancellationTokenSource CTSTBufferInput = new CancellationTokenSource();
            CancellationToken CTTBufferInput = CTSTBufferInput.Token;

            Thread Handler_TSaleaeWatchdog = null;
            CancellationTokenSource CTSTSaleaeWatchdog = new CancellationTokenSource();
            CancellationToken CTTSaleaeWatchdog = CTSTSaleaeWatchdog.Token;


            Thread Handler_TShowPic = null;
            CancellationTokenSource CTSshowpic = new CancellationTokenSource();
            CancellationToken CTShowPic = CTSshowpic.Token;
            Handler_TShowPic = new Thread(this.TShowPic);
            Handler_TShowPic.Start(CTShowPic);

            // User input via console
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                if (args[1] == "-")
                {
                    Handler_TBufferInput = new Thread(this.TBufferInput);
                    Handler_TBufferInput.Start(CTTBufferInput);
                }
                else
                {
                    Console.WriteLine("Only \"-\" is allowed for standard-in. Exiting.");
                    Application.Exit();
                }
            }
            else
            {
                MSaleaeDevices devices = new MSaleaeDevices();
                devices.OnLogicConnect += new MSaleaeDevices.OnLogicConnectDelegate(devices_LogicOnConnect);
                devices.OnLogic16Connect += new MSaleaeDevices.OnLogic16ConnectDelegate(devices_Logic16OnConnect);
                devices.OnDisconnect += new MSaleaeDevices.OnDisconnectDelegate(devices_OnDisconnect);

                devices.BeginConnect();

                while (mLogic == null) { Thread.Sleep(500); Console.WriteLine("."); }
                if (mLogic != null)
                    mLogic.ReadStart();
                restartRequired = false;

                Handler_TSaleaeWatchdog = new Thread(this.TSaleaeWatchdog);
                Handler_TSaleaeWatchdog.Start(CTTSaleaeWatchdog);
            }

            //Start the thing
            await Main(sender, e);

            // Stopping Saleae if necessary
            if (mLogic != null)
                if (mLogic.IsStreaming() == false)
                    Console.WriteLine("Sorry, the device is not currently streaming.");
                else
                    mLogic.Stop();

            // If finished, clean up threads
            if (Handler_TBufferInput != null) CTSTBufferInput.Cancel();
            if (Handler_TSaleaeWatchdog != null) CTSTSaleaeWatchdog.Cancel();
            if(Handler_TShowPic != null) CTSshowpic.Cancel();
        }

        //Saleae
        void devices_OnDisconnect(ulong device_id)
        {
            Console.WriteLine("Device with id {0} disconnected.", device_id);
            if (mLogic != null)
                mLogic = null;
            if (mLogic16 != null)
                mLogic16 = null;
        }

        void devices_LogicOnConnect(ulong device_id, MLogic logic)
        {
            Console.WriteLine("Logic with id {0} connected.", device_id);

            mLogic = logic;
            mLogic.OnReadData += new MLogic.OnReadDataDelegate(mLogic_OnReadData);
            mLogic.OnWriteData += new MLogic.OnWriteDataDelegate(mLogic_OnWriteData);
            mLogic.OnError += new MLogic.OnErrorDelegate(mLogic_OnError);
            mLogic.SampleRateHz = mSampleRateHz;
        }

        void devices_Logic16OnConnect(ulong device_id, MLogic16 logic_16)
        {
            Console.WriteLine("Logic16 with id {0} connected.", device_id);

            mLogic16 = logic_16;
            logic_16.OnReadData += new MLogic16.OnReadDataDelegate(mLogic16_OnReadData);
            logic_16.OnError += new MLogic16.OnErrorDelegate(mLogic_OnError);
            logic_16.SampleRateHz = mSampleRateHz;
        }

        void mLogic_OnReadData(ulong device_id, byte[] data)
        {
            //Console.WriteLine("Logic: Read {0} bytes, starting with 0x{1:X}", data.Length, (ushort)data[0]);
            var t = new Task(() =>
            {
                for (int datum = 0; datum < data.Length; datum++) bufferedData.Enqueue(data[datum]);
            });
            t.Start();
            t.Wait();

        }
        void mLogic16_OnReadData(ulong device_id, byte[] data)
        {
            Console.WriteLine("Logic16: Read {0} bytes, starting with 0x{1:X}", data.Length, data[0]);
        }

        void mLogic_OnWriteData(ulong device_id, byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = mWriteValue;
                mWriteValue++;
            }

            Console.WriteLine("Wrote {0} bytes of data", data.Length);
        }

        void mLogic_OnError(ulong device_id)
        {
            Console.WriteLine("Logic Reported an Error.  A restart will be attempted.");
            restartRequired = true;
        }
        public async Task Main(object sender, EventArgs e)
        {
            Bitmap newpic = null;
            //Thread Handler_TShowPic = null;

            radioButton1.ForeColor = Color.Green;
            radioButton1.Text = "Reading...";
            radioButton1.Checked = true;

            // Starting stream read
            while (!endofstream)
            {
                Task<Bitmap?> CP = CreatePicture(sender, e);
                await Task.Run(async () =>
                {
                    newpic = await CP;
                    CP.Dispose();

                });

                if (newpic != null) // Valid picture received, extract only the area which has data in it
                {
                    RectangleF cloneRect = new RectangleF(0, 0, xposmax_b, yposmax_b);
                    if (newpic.Height > 5) newframe = (Bitmap)newpic.Clone(cloneRect, PixelFormat.DontCare);
                    newpic.Dispose();
                }

                //Remember bitmap size to use small bitmaps later. This speeds up things.
                xposmax_b = xposmax; yposmax_b = yposmax;

                // Count received frames
                screens++;

                // calculate running average for number of expected lines per frame
                vxc++;
                yposA = (yposA + yposmax) / 2;
                if (vxc > 60)
                {
                    vxc = 0;
                    yposA = yposmax;
                }
            }

            radioButton1.ForeColor = Color.Red;
            radioButton1.Checked = false;

            radioButton1.Text = "Stream ended";
            
        }

        private async Task<Bitmap?> CreatePicture(object sender, EventArgs e)
        {
            //Bitmap bmp = new Bitmap(width, (yposmax > 200 ? yposmax : 200));
            Bitmap bmp = new Bitmap((xposmax_b > 10 ? xposmax : 10), (yposmax_b > 10 ? yposmax_b : 10));
            int readchar, readFail;
            int rawdata = 0;
            bool hsync_raw = false, vsync_raw = false;
            bool hsync = false, vsync = false;
            bool vsyncc = false, hsyncc = false;
            int xpos = 0, ypos = 0;
            int color;
            int hsync_filter = 0, vsync_filter = 0;
            // Vsync inverted auto handling
            int vsync_n = 0, vsync_p = 0;
            // Vsync inverted auto handling

            // Colors incl. intensity bits
            byte redL = 0, greenL = 0, blueL = 0, redH = 0, greenH = 0, blueH = 0;
            byte red = 0, green = 0, blue = 0;

            bool loop = true;
            bool vcompositeflag = false, armed = false;


            //experiment
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = data.Stride;
            //experiment

            await Task.Run(() =>
            {
            while (loop && !endofstream)
            {
                // A timeout in case no data is added to the queue. Multibyte read-async can get stuck then
                readFail = 0;

                while (!bufferedData.TryDequeue(out readchar))
                {
                    readFail++;
                    if (readFail > 100)
                    {
                        readchar = -1;
                        break;
                    }
                    // Give it some time
                    Thread.Sleep(50);
                }

                // Catch standard case of file end
                if (readchar == -1)
                {
                    loop = false;
                    endofstream = true;
                    bmp.Dispose();
                    break;
                }
                else
                {
                    rawdata = readchar;
                }

                // Safe former sync state
                vsyncc = vsync; hsyncc = hsync;

                // Extract Sync Signal
                vsync_raw = (rawdata & 128) > 0;
                hsync_raw = (rawdata & 64) > 0;

                //invert sync signal if required
                if (invert_sync)
                {
                    hsync_raw = !hsync_raw;
                    vsync_raw = !vsync_raw;
                }

                if (v_inverted)
                {
                    vsync_raw = !vsync_raw;
                }


                // Extract Color 
                if (invert_color)
                    color = (int)rawdata ^ 0x3F;
                else
                    color = (int)rawdata & 0x3F;

                //Low bits
                blueL = (byte)(color & 1);
                color = color >> 1;
                greenL = (byte)(color & 1);
                color = color >> 1;
                redL = (byte)(color & 1);
                //High bits (intensity)
                color = color >> 1;
                greenH = (byte)(color & 1);
                color = color >> 1;
                redH = (byte)(color & 1);
                color = color >> 1;
                blueH = (byte)(color & 1);

                    switch (colorMode)
                    {
                        case 0: //Basic RGB is just one intensity level, fails for MDA
                            red = (byte)(redL * 255);
                            green = (byte)(greenL * 255);
                            blue = (byte)(blueL * 255);
                            break;
                        case 1: // MDA relies on signal and intensity (See pinout)
                            green = (byte)((blueH * 170) + (greenH * 85));
                            break;
                        case 2: // CGA has intensity as well (on green)
                            red = (byte)((redL * 170) + (greenH * 85));
                            green = (byte)((greenL * 170) + (greenH * 85));
                            blue = (byte)((blueL * 170) + (greenH * 85));
                            break;
                        case 3: // For EGA
                            green = (byte)(85 * ((greenL) | (greenH << 1)));
                            blue = (byte)(85 * ((blueL) | (blueH << 1)));
                            red = (byte)(85 * ((redL) | (redH << 1)));
                            break;
                    }


                    // Remove Sync Noise by scanning 3 subsequent signals (filterbits set to b111 dec7
                    vsync_filter = vsync_filter << 1;
                    hsync_filter = hsync_filter << 1;
                    vsync_filter = (vsync_filter | (vsync_raw ? 1 : 0));
                    hsync_filter = (hsync_filter | (hsync_raw ? 1 : 0));
                    if ((vsync_filter & vsync_filterbits) == vsync_filterbits) vsync = true; else if ((vsync_filter & vsync_filterbits) == 0) vsync = false;
                    if ((hsync_filter & hsync_filterbits) == hsync_filterbits) hsync = true; else if ((hsync_filter & hsync_filterbits) == 0) hsync = false;


                    //composite handling
                    if (composite_sync)
                    {
                        // Count the time the sync signal is present
                        if (vsync) syncpulses++;
                        else
                        {
                            // We have no signal now....

                            // Get a high watermark for vsync
                            if (syncpulses > sync_long)
                            {
                                sync_long = syncpulses;
                            }

                            // If we just got a smal sync, arm the system for an upcoming hsync
                            if (syncpulses > 0 && syncpulses < (sync_long / 10))
                            {
                                armed = true;
                            }

                            // Armed and a long sync pulse -> vsync 
                            if (armed && (syncpulses > (sync_long * 0.9)))
                            {
                                vcompositeflag = true;
                                armed = false;
                            }
                            else // Special case inverted hysnc during vsync, keep sync signal active to fill the gap
                                if (!armed) vsync = true;

                            syncpulses = 0;
                        }
                        //only one sync on Vsync input
                        hsync = vsync;
                    }
                    else vcompositeflag = true;


                    if (!vsync) // Outside vsync cyle
                    {
                        // Vsync inverted auto handling
                        vsync_n++;
                        // Vsync inverted auto handling

                        if (vsyncc && vcompositeflag) // Just coming from a vsync signal, new frame
                        {
                            yposmax = ypos;

                            // Vsync inverted auto handling
                            if (v_sync_auto_handling)
                            {
                                if (vsync_p > vsync_n && v_cycles > 1)
                                {
                                    if (!v_inverted) v_inverted = true; else v_inverted = false;
                                    v_cycles = 0;
                                }
                                else v_cycles++;
                            }
                            else v_inverted = false;
                            vsync_p = 0; vsync_n = 0;
                            // Vsync inverted auto handling

                            break;
                        }

                        if (!hsync) // Outside hsync cyle
                        {
                            if (hsyncc) // Just coming from a hsync signal, new line
                            {
                                // expectation setting and limiter
                                xposmax = xpos; xpos = 1;
                                if (ypos < height) ypos++;
                            }
                            else // No sync and no recent changes: Set horizontal pixels
                            {

                                if (xpos < width && xpos < xposmax_b && ypos < yposmax_b) // Within defined bitmap ?
                                    // Fast setpixel 
                                    unsafe
                                    {
                                        byte* ptr = (byte*)data.Scan0;
                                        ptr[(xpos * 3) + ypos * stride] = blue;
                                        ptr[(xpos * 3) + ypos * stride + 1] = green;
                                        ptr[(xpos * 3) + ypos * stride + 2] = red;
                                        xpos++;
                                    }
                                else xpos++; // If no pixel are set, we are still interested in the high value

                            }
                        }
                    }
                    else
                    {//Vsync inverted auto handling
                        vsync_p++;
                    }//Vsync inverted auto handling
                }
            });

            // Visualize parameters
            TB1 = xposmax;
            TB2 = yposmax;

            if (xposmax_b < 10 || yposmax_b < 10 || endofstream) return null; // received frame is too small or stream stopped, discard
            else
            {
                //experiment
                bmp.UnlockBits(data);
                //experiment
                newframeisready = true;
                return bmp;
            }

        }

        private void reset()
        {
            // Reset and Flush
            syncpulses = 0; sync_long = 0; sync_short = 0;
            endofstream = false;
            TB1 = 0; TB2 = 0; screens = 0;
            xposmax = 0; yposmax = 0; xposmax_b = 1; yposmax_b = 1; yposA = 0;
            vexpected = 0; hexpected = 0; vxc = 0; vxm = 0;
            //vIn.DiscardBufferedData();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mLogic != null)
                if (mLogic.IsStreaming() == false)
                    Console.WriteLine("Sorry, the device is not currently streaming.");
                else
                    mLogic.Stop();

        }


        // Inverted signal changes
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            invert_sync = !invert_sync;
            checkBox1.Checked = invert_sync;
            reset();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            invert_color = !invert_color;
            checkBox2.Checked = invert_color;
            reset();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            composite_sync = !composite_sync;
            checkBox3.Checked = composite_sync;
            reset();
        }

        // Color mode changes
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            colorMode = 0;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            colorMode = 1;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            colorMode = 2;
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            colorMode = 3;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            originalSize = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            originalSize = false;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int xr = Convert.ToInt16(textBox5.Text);

                if (xr > 0)
                {
                    originalSize = false;
                    radioButton6.Checked = false;
                    radioButton7.Checked = true;
                    xResize = xr;
                }
            }
            catch { textBox5.Text = ""; }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int yr = Convert.ToInt16(textBox6.Text);

                if (yr > 0)
                {
                    originalSize = false;
                    radioButton6.Checked = false;
                    radioButton7.Checked = true;
                    yResize = yr;
                }
            }
            catch { textBox6.Text = ""; }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            v_sync_auto_handling = !v_sync_auto_handling;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            endofstream = true;
        }
    }

}
