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



namespace CGATest
{

    public partial class Form1 : Form
    {
        static int width = 1500, height = 500;
        volatile Bitmap newframe = new Bitmap(1500, 500);

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
            //textBox4.Text = TB3.ToString();
            screens = 0;
        }


        private async void TShowPic()
        {
            Bitmap? resized = null;

            var t = Task.Run(() =>
            {
                while (true)
                {
                    if (newframe != null)
                        if (originalSize)
                        {
                            pictureBox1.Image = (Bitmap)newframe.Clone();
                        }
                        else
                        {
                            resized = new Bitmap(newframe, new Size(xResize, yResize));
                            pictureBox1.Image = (Bitmap)resized.Clone();
                        }

                    // Wait for a 50Hz refresh
                    Thread.Sleep(20);
                }
            }
            );
        }


        // Read input stream to buffer
        private async void TBufferInput()
        {

            Process sigrok = null;
            int buffSize = 32;
            int maxBuff = 2000000;
            bool loop = true;
            int readchar = 0;
            char[] c = new char[buffSize];



            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1) // do we start sigrok on our own?
            {
                Console.WriteLine("Starting Sigrok");

                // Sigrok process start
                var startSigrok = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\sigrok\sigrok-cli\sigrok-cli",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = @"C:\Program Files\sigrok\sigrok-cli\",
                    Arguments = @"-d fx2lafw -O binary --config ""samplerate=24 MHz"" --continuous",
                    StandardOutputEncoding = Encoding.Latin1 // required to get only one byte per char
                };

                sigrok = Process.Start(startSigrok);

                sigrok.PriorityClass = ProcessPriorityClass.High;

                // Queue Id for later housekeeping
                processIds.Enqueue(sigrok.Id);

                // Attach sigrok to stdout
                vIn = sigrok.StandardOutput;
            }
            else
                // or is there a pipe?
                if (args[1] == "-")
            {
                vIn = new StreamReader(Console.OpenStandardInput(), System.Text.Encoding.Latin1, bufferSize: 131072);
            }
            else
            {
                Console.WriteLine("Only \"-\" is allowed for standard-in. Exiting.");
                Application.Exit();
            }

            while (loop && vIn != null)
            {
                readchar = await vIn.ReadAsync(c, 0, buffSize);
                TB3 = bufferedData.Count;
                // if (TB3<maxBuff) 
                for (int i = 0; i < readchar; i++) bufferedData.Enqueue(c[i]);

            }
        }


        private async void Form1_Shown(object sender, EventArgs e)
        {
            //Start the thing
            await Main(sender, e);
        }

        public async Task Main(object sender, EventArgs e)
        {
            /*
             * 
             * Probably better placed where the data is actuall read
             * 
             * 
             * 
            Process sigrok = null;
            // readm cmd
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1) // do we start sigrok on our own?
            {

                Console.WriteLine("Starting Sigrok");

                // Sigrok process start
                var startSigrok = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\sigrok\sigrok-cli\sigrok-cli",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = @"C:\Program Files\sigrok\sigrok-cli\",
                    Arguments = @"-d fx2lafw -O binary --config ""samplerate=16 MHz"" --continuous",
                    StandardOutputEncoding = Encoding.Latin1
                };

                sigrok = Process.Start(startSigrok);

                sigrok.PriorityClass = ProcessPriorityClass.High;
                processIds.Enqueue(sigrok.Id);

                // Attach stdout
                vIn = sigrok.StandardOutput;

            }
            else
                if (args[1] == "-")
            {
                vIn = new StreamReader(Console.OpenStandardInput(), System.Text.Encoding.Latin1, bufferSize: 131072); // or is there a pipe?
            }
            else
            {
                Console.WriteLine("Only \"-\" is allowed for standard-in. Exiting.");
                Application.Exit();
            }
            */

            Bitmap newpic = null;

            radioButton1.ForeColor = Color.Green;
            radioButton1.Text = "Reading...";
            radioButton1.Checked = true;

            // Starting thread to display frames
            new Thread(this.TShowPic).Start();
            new Thread(this.TBufferInput).Start();

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

                    xposmax_b = xposmax; yposmax_b = yposmax;
                }
                else // No valid picture
                {
                    xposmax_b = xposmax; yposmax_b = yposmax;
                }


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

            // The stream ended
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
            while (loop)
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


                // Extract Color 
                if (invert_color)
                    color = (int)rawdata ^ 0x3F;
                else
                    color = (int)rawdata & 0x3F;

                /*
                // Generic bit shifting to extract color bits
                blue = (byte)(127 * (byte)(color & 1));
                color = color >> 1;
                green = (byte)(127 * (byte)(color & 1));
                color = color >> 1;
                red = (byte)(127 * (byte)(color & 1));

                switch (colorMode)
                {
                    case 0: //RGB is just one intensity level
                        red = (byte)(red * 2);
                        green = (byte)(green * 2);
                        blue = (byte)(blue * 2);
                        break;
                    case 1: // MDA relies on green and intensity on green
                        color = color >> 1;
                        green = (byte)(green * ((byte)(color & 1) + 1));
                        blue = green;
                        red = green;
                        break;
                    case 2: // CGA has intensity as well (on green)
                        color = color >> 1;
                        blue = (byte)(blue * ((byte)(color & 1) + 1));
                        green = (byte)(green * ((byte)(color & 1) + 1));
                        red = (byte)(red * ((byte)(color & 1) + 1));
                        break;
                    case 3: // For EGA
                        color = color >> 1;
                        green = (byte)(green * ((byte)(color & 1) + 1));
                        color = color >> 1;
                        red = (byte)(red * ((byte)(color & 1) + 1));
                        color = color >> 1;
                        blue = (byte)(blue * ((byte)(color & 1) + 1));
                        break;
                }*/

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
                        case 0: //RGB is just one intensity level
                            red = (byte)(redL * 255);
                            green = (byte)(greenL * 255);
                            blue = (byte)(blueL * 255);
                            break;
                        case 1: // MDA relies on green and intensity on green
                            green = (byte)((greenL * 170) + (greenH * 85));
                            blue = green;
                            red = green;
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
                        if (vsyncc && vcompositeflag) // Just coming from a vsync signal, new frame
                        {
                            yposmax = ypos;
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
                                if (xpos < width)
                                {
                                    if (xpos < xposmax_b && ypos < yposmax_b) // Within defined bitmap ?
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
                    }
                }
            });

            // Visualize parameters
            TB1 = xposmax;
            TB2 = yposmax;

            if (xposmax_b < 10 || yposmax_b < 10) return null; // received frame is too small, discard
            else
            {
                //experiment
                bmp.UnlockBits(data);
                //experiment
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
            int p = -1;

            processIds.TryDequeue(out p);
            Console.WriteLine("Closing and shutting down " + p.ToString());

            // Housekeeping, kill sigrok process if present
            try
            {
                Process processes = Process.GetProcessById(p);
                if (p != 0) processes.Kill();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

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
            } catch { textBox5.Text = ""; }
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
    }

}
