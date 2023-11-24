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
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            radioButton1 = new RadioButton();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(2027, 1261);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Enabled = false;
            textBox1.Location = new Point(24, 78);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 31);
            textBox1.TabIndex = 1;
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(33, 31);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(142, 29);
            radioButton1.TabIndex = 2;
            radioButton1.TabStop = true;
            radioButton1.Text = "Read Indictor";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.UseWaitCursor = true;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Enabled = false;
            textBox2.Location = new Point(24, 161);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(151, 31);
            textBox2.TabIndex = 4;
            textBox2.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(24, 248);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(115, 31);
            textBox3.TabIndex = 5;
            textBox3.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 220);
            label1.Name = "label1";
            label1.Size = new Size(137, 25);
            label1.TabIndex = 6;
            label1.Text = "Intake Frames/s";
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(checkBox3);
            panel1.Controls.Add(checkBox2);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(groupBox2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(2019, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(218, 1261);
            panel1.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Honeydew;
            groupBox1.Controls.Add(radioButton5);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Location = new Point(0, 241);
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
            radioButton5.Size = new Size(70, 29);
            radioButton5.TabIndex = 3;
            radioButton5.Text = "EGA";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(22, 111);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(72, 29);
            radioButton4.TabIndex = 2;
            radioButton4.Text = "CGA";
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
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
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
            checkBox2.Location = new Point(22, 144);
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
            checkBox1.Location = new Point(23, 95);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(124, 29);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Invert Sync";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.AntiqueWhite;
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(label1);
            groupBox2.Dock = DockStyle.Bottom;
            groupBox2.Location = new Point(0, 952);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(218, 309);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Details";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 133);
            label3.Name = "label3";
            label3.Size = new Size(140, 25);
            label3.TabIndex = 15;
            label3.Text = "Scanlines/Frame";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 50);
            label2.Name = "label2";
            label2.Size = new Size(150, 25);
            label2.TabIndex = 14;
            label2.Text = "Samples/Scanline";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2237, 1261);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Shown += Form1_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textBox1;
        private RadioButton radioButton1;
        private TextBox textBox2;
        private TextBox textBox3;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Panel panel1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckedListBox checkedListBox1;
        private GroupBox groupBox1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private Label label3;
        private Label label2;
        private GroupBox groupBox2;
    }
}
