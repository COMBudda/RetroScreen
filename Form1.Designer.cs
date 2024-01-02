namespace CGATest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            radioButton1 = new RadioButton();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            checkBox5 = new CheckBox();
            label6 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBox1 = new ComboBox();
            checkBox4 = new CheckBox();
            groupBox3 = new GroupBox();
            label5 = new Label();
            label4 = new Label();
            radioButton7 = new RadioButton();
            radioButton6 = new RadioButton();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            groupBox1 = new GroupBox();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel5 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel6 = new ToolStripStatusLabel();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            toolStripStatusLabel7 = new ToolStripStatusLabel();
            toolStripStatusLabel8 = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1644, 1233);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(24, 19);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(142, 29);
            radioButton1.TabIndex = 2;
            radioButton1.TabStop = true;
            radioButton1.Text = "Read Indictor";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.UseWaitCursor = true;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(checkBox5);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(checkBox4);
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(checkBox3);
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(radioButton1);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1640, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(222, 1233);
            panel1.TabIndex = 7;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(152, 324);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(22, 21);
            checkBox5.TabIndex = 27;
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F);
            label6.Location = new Point(20, 303);
            label6.Name = "label6";
            label6.Size = new Size(110, 63);
            label6.TabIndex = 26;
            label6.Text = "Alternate\r\nStabilizer\r\n(Experimental)";
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Location = new Point(11, 62);
            label3.Name = "label3";
            label3.Size = new Size(197, 2);
            label3.TabIndex = 25;
            label3.Text = "label3";
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Location = new Point(12, 238);
            label2.Name = "label2";
            label2.Size = new Size(197, 2);
            label2.TabIndex = 24;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8F);
            label1.Location = new Point(20, 256);
            label1.Name = "label1";
            label1.Size = new Size(87, 42);
            label1.TabIndex = 23;
            label1.Text = "Smoothing\r\nLevel";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.AddRange(new object[] { "0", "1", "2" });
            comboBox1.Location = new Point(127, 260);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(77, 33);
            comboBox1.TabIndex = 22;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new Point(23, 74);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(192, 29);
            checkBox4.TabIndex = 19;
            checkBox4.Text = "Auto VSync Polarity";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.SkyBlue;
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(radioButton7);
            groupBox3.Controls.Add(radioButton6);
            groupBox3.Controls.Add(textBox6);
            groupBox3.Controls.Add(textBox5);
            groupBox3.Location = new Point(0, 570);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(218, 229);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Size";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 139);
            label5.Name = "label5";
            label5.Size = new Size(22, 25);
            label5.TabIndex = 5;
            label5.Text = "Y";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 102);
            label4.Name = "label4";
            label4.Size = new Size(23, 25);
            label4.TabIndex = 4;
            label4.Text = "X";
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Checked = true;
            radioButton7.Location = new Point(22, 64);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(118, 29);
            radioButton7.TabIndex = 3;
            radioButton7.TabStop = true;
            radioButton7.Text = "Resized to";
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += radioButton7_CheckedChanged;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(22, 30);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(99, 29);
            radioButton6.TabIndex = 2;
            radioButton6.Text = "Original";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(85, 136);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(71, 31);
            textBox6.TabIndex = 1;
            textBox6.Text = "768";
            textBox6.TextAlign = HorizontalAlignment.Right;
            textBox6.TextChanged += textBox6_TextChanged;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(85, 99);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(71, 31);
            textBox5.TabIndex = 0;
            textBox5.Text = "1024";
            textBox5.TextAlign = HorizontalAlignment.Right;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Honeydew;
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(0, 376);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(218, 194);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Color Mode";
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(22, 146);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(116, 29);
            radioButton5.TabIndex = 3;
            radioButton5.Text = "EGA (true)";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(22, 111);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(176, 29);
            radioButton4.TabIndex = 2;
            radioButton4.Text = "CGA (compatible)";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(22, 76);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(78, 29);
            radioButton3.TabIndex = 1;
            radioButton3.TabStop = true;
            radioButton3.Text = "MDA";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Location = new Point(23, 41);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(70, 29);
            radioButton2.TabIndex = 0;
            radioButton2.TabStop = true;
            radioButton2.Text = "RGB";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(22, 194);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(166, 29);
            checkBox3.TabIndex = 10;
            checkBox3.Text = "Composite Sync";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(22, 154);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(131, 29);
            checkBox2.TabIndex = 9;
            checkBox2.Text = "Invert Color";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(23, 113);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(124, 29);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Invert Sync";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel4, toolStripStatusLabel2, toolStripStatusLabel5, toolStripStatusLabel3, toolStripStatusLabel6, toolStripDropDownButton1, toolStripStatusLabel7, toolStripStatusLabel8 });
            statusStrip1.Location = new Point(0, 1197);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1640, 36);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(161, 29);
            toolStripStatusLabel1.Text = "Samples/Scanline";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel4.Font = new Font("Segoe UI", 9F);
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(26, 29);
            toolStripStatusLabel4.Text = "0";
            toolStripStatusLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolStripStatusLabel2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(155, 29);
            toolStripStatusLabel2.Text = "Scanlines/Frame";
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new Size(26, 29);
            toolStripStatusLabel5.Text = "0";
            toolStripStatusLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolStripStatusLabel3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(177, 29);
            toolStripStatusLabel3.Text = "Incoming Frames/s";
            // 
            // toolStripStatusLabel6
            // 
            toolStripStatusLabel6.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            toolStripStatusLabel6.Size = new Size(26, 29);
            toolStripStatusLabel6.Text = "0";
            toolStripStatusLabel6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(153, 33);
            toolStripDropDownButton1.Text = "Sampling Rate";
            toolStripDropDownButton1.TextImageRelation = TextImageRelation.Overlay;
            // 
            // toolStripStatusLabel7
            // 
            toolStripStatusLabel7.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolStripStatusLabel7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            toolStripStatusLabel7.Size = new Size(171, 29);
            toolStripStatusLabel7.Text = "Connection Status";
            // 
            // toolStripStatusLabel8
            // 
            toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            toolStripStatusLabel8.Size = new Size(117, 29);
            toolStripStatusLabel8.Text = "---------------";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1862, 1233);
            Controls.Add(statusStrip1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "RetroScreen";
            FormClosing += Form1_FormClosing;
            Shown += Form1_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private RadioButton radioButton1;
        private System.Windows.Forms.Timer timer1;
        private Panel panel1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private GroupBox groupBox3;
        private RadioButton radioButton7;
        private RadioButton radioButton6;
        private TextBox textBox6;
        private TextBox textBox5;
        private Label label5;
        private Label label4;
        private CheckBox checkBox4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripStatusLabel toolStripStatusLabel6;
        private ToolStripStatusLabel toolStripStatusLabel7;
        private ToolStripStatusLabel toolStripStatusLabel8;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label6;
        private CheckBox checkBox5;
    }
}
