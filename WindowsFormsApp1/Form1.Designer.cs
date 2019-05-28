namespace WindowsFormsApp1
{
   
        partial class Form1
        {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
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
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.sourceimg = new System.Windows.Forms.PictureBox();
                this.showmosaic = new System.Windows.Forms.PictureBox();
                this.uploadimg = new System.Windows.Forms.Button();
                this.saveimg = new System.Windows.Forms.Button();
                this.begin = new System.Windows.Forms.Button();
                this.radioButton1 = new System.Windows.Forms.RadioButton();
                this.radioButton2 = new System.Windows.Forms.RadioButton();
                this.radioButton3 = new System.Windows.Forms.RadioButton();
                this.progressBar1 = new System.Windows.Forms.ProgressBar();
                this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
                this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.textBox2 = new System.Windows.Forms.TextBox();
                this.textBox3 = new System.Windows.Forms.TextBox();
                ((System.ComponentModel.ISupportInitialize)(this.sourceimg)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.showmosaic)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
                this.SuspendLayout();
                // 
                // sourceimg
                // 
                this.sourceimg.BackColor = System.Drawing.SystemColors.InactiveBorder;
                this.sourceimg.Location = new System.Drawing.Point(943, 12);
                this.sourceimg.Name = "sourceimg";
                this.sourceimg.Size = new System.Drawing.Size(415, 298);
                this.sourceimg.TabIndex = 0;
                this.sourceimg.TabStop = false;
                // 
                // showmosaic
                // 
                this.showmosaic.BackColor = System.Drawing.SystemColors.InactiveBorder;
                this.showmosaic.Location = new System.Drawing.Point(12, 12);
                this.showmosaic.Name = "showmosaic";
                this.showmosaic.Size = new System.Drawing.Size(925, 673);
                this.showmosaic.TabIndex = 1;
                this.showmosaic.TabStop = false;
                // 
                // uploadimg
                // 
                this.uploadimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.uploadimg.Location = new System.Drawing.Point(1090, 335);
                this.uploadimg.Name = "uploadimg";
                this.uploadimg.Size = new System.Drawing.Size(128, 34);
                this.uploadimg.TabIndex = 2;
                this.uploadimg.Text = "Upload Image";
                this.uploadimg.UseVisualStyleBackColor = true;
                this.uploadimg.Click += new System.EventHandler(this.uploadimg_Click);
                // 
                // saveimg
                // 
                this.saveimg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.saveimg.Location = new System.Drawing.Point(10, 709);
                this.saveimg.Name = "saveimg";
                this.saveimg.Size = new System.Drawing.Size(104, 30);
                this.saveimg.TabIndex = 3;
                this.saveimg.Text = "Save";
                this.saveimg.UseVisualStyleBackColor = true;
                this.saveimg.Click += new System.EventHandler(this.saveimg_Click);
                // 
                // begin
                // 
                this.begin.Enabled = false;
                this.begin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.begin.Location = new System.Drawing.Point(120, 709);
                this.begin.Name = "begin";
                this.begin.Size = new System.Drawing.Size(136, 30);
                this.begin.TabIndex = 4;
                this.begin.Text = "Create Mosaic";
                this.begin.UseVisualStyleBackColor = true;
                this.begin.Click += new System.EventHandler(this.begin_Click);
                // 
                // radioButton1
                // 
                this.radioButton1.AutoSize = true;
                this.radioButton1.Checked = true;
                this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radioButton1.Location = new System.Drawing.Point(1102, 423);
                this.radioButton1.Name = "radioButton1";
                this.radioButton1.Size = new System.Drawing.Size(65, 30);
                this.radioButton1.TabIndex = 5;
                this.radioButton1.TabStop = true;
                this.radioButton1.Text = "4x4";
                this.radioButton1.UseVisualStyleBackColor = true;
                this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
                // 
                // radioButton2
                // 
                this.radioButton2.AutoSize = true;
                this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radioButton2.Location = new System.Drawing.Point(1102, 447);
                this.radioButton2.Name = "radioButton2";
                this.radioButton2.Size = new System.Drawing.Size(89, 30);
                this.radioButton2.TabIndex = 6;
                this.radioButton2.Text = "16x16";
                this.radioButton2.UseVisualStyleBackColor = true;
                this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
                // 
                // radioButton3
                // 
                this.radioButton3.AutoSize = true;
                this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radioButton3.Location = new System.Drawing.Point(1102, 471);
                this.radioButton3.Name = "radioButton3";
                this.radioButton3.Size = new System.Drawing.Size(89, 30);
                this.radioButton3.TabIndex = 7;
                this.radioButton3.Text = "64x64";
                this.radioButton3.UseVisualStyleBackColor = true;
                this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
                // 
                // progressBar1
                // 
                this.progressBar1.Location = new System.Drawing.Point(272, 709);
                this.progressBar1.Name = "progressBar1";
                this.progressBar1.Size = new System.Drawing.Size(665, 30);
                this.progressBar1.TabIndex = 8;
                // 
                // numericUpDown1
                // 
                this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numericUpDown1.Location = new System.Drawing.Point(962, 558);
                this.numericUpDown1.Name = "numericUpDown1";
                this.numericUpDown1.Size = new System.Drawing.Size(153, 26);
                this.numericUpDown1.TabIndex = 14;
                this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
                // 
                // numericUpDown2
                // 
                this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numericUpDown2.Location = new System.Drawing.Point(1174, 558);
                this.numericUpDown2.Name = "numericUpDown2";
                this.numericUpDown2.Size = new System.Drawing.Size(175, 26);
                this.numericUpDown2.TabIndex = 15;
                this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
                // 
                // textBox1
                // 
                this.textBox1.BackColor = System.Drawing.SystemColors.Info;
                this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.textBox1.Location = new System.Drawing.Point(962, 590);
                this.textBox1.Multiline = true;
                this.textBox1.Name = "textBox1";
                this.textBox1.ReadOnly = true;
                this.textBox1.Size = new System.Drawing.Size(153, 48);
                this.textBox1.TabIndex = 16;
                this.textBox1.Text = "Enter Elitism value between 10 and 90.";
                this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                // 
                // textBox2
                // 
                this.textBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.PageTabList;
                this.textBox2.BackColor = System.Drawing.SystemColors.Info;
                this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.textBox2.Location = new System.Drawing.Point(1174, 590);
                this.textBox2.Multiline = true;
                this.textBox2.Name = "textBox2";
                this.textBox2.ReadOnly = true;
                this.textBox2.Size = new System.Drawing.Size(175, 95);
                this.textBox2.TabIndex = 17;
                this.textBox2.Text = "Enter amount of generations. (Values above 1000 give better results)";
                // 
                // textBox3
                // 
                this.textBox3.BackColor = System.Drawing.SystemColors.Info;
                this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.textBox3.Location = new System.Drawing.Point(962, 705);
                this.textBox3.Multiline = true;
                this.textBox3.Name = "textBox3";
                this.textBox3.ReadOnly = true;
                this.textBox3.Size = new System.Drawing.Size(387, 34);
                this.textBox3.TabIndex = 18;
                this.textBox3.Text = "Default Elitism is 10. Default Generations is 1000.";
                this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                // 
                // Form1
                // 
                this.BackColor = System.Drawing.SystemColors.Info;
                this.ClientSize = new System.Drawing.Size(1370, 765);
                this.Controls.Add(this.textBox3);
                this.Controls.Add(this.textBox2);
                this.Controls.Add(this.textBox1);
                this.Controls.Add(this.numericUpDown2);
                this.Controls.Add(this.numericUpDown1);
                this.Controls.Add(this.progressBar1);
                this.Controls.Add(this.radioButton3);
                this.Controls.Add(this.radioButton2);
                this.Controls.Add(this.radioButton1);
                this.Controls.Add(this.begin);
                this.Controls.Add(this.saveimg);
                this.Controls.Add(this.uploadimg);
                this.Controls.Add(this.showmosaic);
                this.Controls.Add(this.sourceimg);
                this.ForeColor = System.Drawing.SystemColors.WindowText;
                this.Name = "Form1";
                this.Text = "Mosaic";
                ((System.ComponentModel.ISupportInitialize)(this.sourceimg)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.showmosaic)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.PictureBox sourceimg;
            private System.Windows.Forms.PictureBox showmosaic;
            private System.Windows.Forms.Button uploadimg;
            private System.Windows.Forms.Button saveimg;
            private System.Windows.Forms.Button begin;
            private System.Windows.Forms.RadioButton radioButton1;
            private System.Windows.Forms.RadioButton radioButton2;
            private System.Windows.Forms.RadioButton radioButton3;
            private System.Windows.Forms.ProgressBar progressBar1;
            private System.Windows.Forms.NumericUpDown numericUpDown1;
            private System.Windows.Forms.NumericUpDown numericUpDown2;
            private System.Windows.Forms.TextBox textBox1;
            private System.Windows.Forms.TextBox textBox2;
            private System.Windows.Forms.TextBox textBox3;
        }
    }




