namespace Tubes3_ResmiTamatStima
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
            JudulProgram = new Label();
            picBoxInput = new PictureBox();
            picBoxMatched = new PictureBox();
            picBoxResult = new PictureBox();
            btnPilihCitra = new Button();
            radioBM = new RadioButton();
            groupBox1 = new GroupBox();
            radioKMP = new RadioButton();
            btnSearch = new Button();
            lblWaktuPencarian = new Label();
            lblPersentaseKecocokan = new Label();
            ((System.ComponentModel.ISupportInitialize)picBoxInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxMatched).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxResult).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // JudulProgram
            // 
            JudulProgram.AutoSize = true;
            JudulProgram.Font = new Font("Roboto", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            JudulProgram.Location = new Point(84, 81);
            JudulProgram.Name = "JudulProgram";
            JudulProgram.Size = new Size(895, 34);
            JudulProgram.TabIndex = 0;
            JudulProgram.Text = "Sistem Deteksi Individu Berbasis Biometrik Melalui Citra Sidik Jari";
            JudulProgram.TextAlign = ContentAlignment.TopCenter;
            // 
            // picBoxInput
            // 
            picBoxInput.BackColor = SystemColors.ActiveBorder;
            picBoxInput.Location = new Point(81, 168);
            picBoxInput.Margin = new Padding(3, 4, 3, 4);
            picBoxInput.Name = "picBoxInput";
            picBoxInput.Size = new Size(216, 406);
            picBoxInput.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxInput.TabIndex = 2;
            picBoxInput.TabStop = false;
            // 
            // picBoxMatched
            // 
            picBoxMatched.BackColor = SystemColors.ActiveBorder;
            picBoxMatched.Location = new Point(428, 168);
            picBoxMatched.Margin = new Padding(3, 4, 3, 4);
            picBoxMatched.Name = "picBoxMatched";
            picBoxMatched.Size = new Size(216, 406);
            picBoxMatched.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxMatched.TabIndex = 3;
            picBoxMatched.TabStop = false;
            // 
            // picBoxResult
            // 
            picBoxResult.BackColor = SystemColors.ActiveBorder;
            picBoxResult.Location = new Point(763, 168);
            picBoxResult.Margin = new Padding(3, 4, 3, 4);
            picBoxResult.Name = "picBoxResult";
            picBoxResult.Size = new Size(216, 406);
            picBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxResult.TabIndex = 4;
            picBoxResult.TabStop = false;
            // 
            // btnPilihCitra
            // 
            btnPilihCitra.BackColor = Color.White;
            btnPilihCitra.Font = new Font("Roboto", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPilihCitra.Location = new Point(81, 639);
            btnPilihCitra.Margin = new Padding(3, 4, 3, 4);
            btnPilihCitra.Name = "btnPilihCitra";
            btnPilihCitra.Size = new Size(154, 81);
            btnPilihCitra.TabIndex = 5;
            btnPilihCitra.Text = "Pilih Citra";
            btnPilihCitra.UseVisualStyleBackColor = false;
            btnPilihCitra.Click += btnPilihCitra_Click;
            // 
            // radioBM
            // 
            radioBM.AutoSize = true;
            radioBM.Font = new Font("Roboto", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioBM.Location = new Point(6, 11);
            radioBM.Margin = new Padding(3, 4, 3, 4);
            radioBM.Name = "radioBM";
            radioBM.Size = new Size(58, 24);
            radioBM.TabIndex = 6;
            radioBM.TabStop = true;
            radioBM.Text = "BM";
            radioBM.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ButtonHighlight;
            groupBox1.Controls.Add(radioKMP);
            groupBox1.Controls.Add(radioBM);
            groupBox1.Location = new Point(320, 639);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(145, 81);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            // 
            // radioKMP
            // 
            radioKMP.AutoSize = true;
            radioKMP.Font = new Font("Roboto", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioKMP.Location = new Point(6, 49);
            radioKMP.Margin = new Padding(3, 4, 3, 4);
            radioKMP.Name = "radioKMP";
            radioKMP.Size = new Size(70, 24);
            radioKMP.TabIndex = 7;
            radioKMP.TabStop = true;
            radioKMP.Text = "KMP";
            radioKMP.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Roboto", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(546, 645);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(144, 68);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += button2_Click;
            // 
            // lblWaktuPencarian
            // 
            lblWaktuPencarian.AutoSize = true;
            lblWaktuPencarian.Font = new Font("Roboto", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWaktuPencarian.Location = new Point(745, 655);
            lblWaktuPencarian.Name = "lblWaktuPencarian";
            lblWaktuPencarian.Size = new Size(161, 20);
            lblWaktuPencarian.TabIndex = 10;
            lblWaktuPencarian.Text = "Waktu Pencarian: ";
            // 
            // lblPersentaseKecocokan
            // 
            lblPersentaseKecocokan.AutoSize = true;
            lblPersentaseKecocokan.Font = new Font("Roboto", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPersentaseKecocokan.Location = new Point(745, 688);
            lblPersentaseKecocokan.Name = "lblPersentaseKecocokan";
            lblPersentaseKecocokan.Size = new Size(209, 20);
            lblPersentaseKecocokan.TabIndex = 12;
            lblPersentaseKecocokan.Text = "Persentase Kecocokan:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 784);
            Controls.Add(lblPersentaseKecocokan);
            Controls.Add(lblWaktuPencarian);
            Controls.Add(btnSearch);
            Controls.Add(groupBox1);
            Controls.Add(btnPilihCitra);
            Controls.Add(picBoxResult);
            Controls.Add(picBoxMatched);
            Controls.Add(picBoxInput);
            Controls.Add(JudulProgram);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "ResmiTamatStima";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picBoxInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxMatched).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxResult).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label JudulProgram;
        private System.Windows.Forms.PictureBox picBoxInput;
        private System.Windows.Forms.PictureBox picBoxMatched;
        private System.Windows.Forms.PictureBox picBoxResult;
        private System.Windows.Forms.Button btnPilihCitra;
        private System.Windows.Forms.RadioButton radioBM;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioKMP;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblWaktuPencarian;
        private System.Windows.Forms.Label lblPersentaseKecocokan;
    }
}
