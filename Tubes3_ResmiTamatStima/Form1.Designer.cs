﻿namespace Tubes3_ResmiTamatStima
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            picBoxInput = new PictureBox();
            picBoxMatched = new PictureBox();
            btnPilihCitra = new Button();
            radioBM = new RadioButton();
            groupBox1 = new GroupBox();
            radioKMP = new RadioButton();
            btnSearch = new Button();
            lblWaktuPencarian = new Label();
            lblPersentaseKecocokan = new Label();
            BiodataText = new TextBox();
            ((System.ComponentModel.ISupportInitialize)picBoxInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBoxMatched).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // picBoxInput
            // 
            picBoxInput.BackColor = SystemColors.ActiveBorder;
            picBoxInput.BackgroundImage = (Image)resources.GetObject("picBoxInput.BackgroundImage");
            picBoxInput.BackgroundImageLayout = ImageLayout.Stretch;
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
            picBoxMatched.BackgroundImage = src.Properties.Resources._16061;
            picBoxMatched.BackgroundImageLayout = ImageLayout.Stretch;
            picBoxMatched.Location = new Point(428, 168);
            picBoxMatched.Margin = new Padding(3, 4, 3, 4);
            picBoxMatched.Name = "picBoxMatched";
            picBoxMatched.Size = new Size(216, 406);
            picBoxMatched.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxMatched.TabIndex = 3;
            picBoxMatched.TabStop = false;
            // 
            // btnPilihCitra
            // 
            btnPilihCitra.BackColor = Color.FromArgb(240, 88, 135);
            btnPilihCitra.FlatAppearance.BorderSize = 0;
            btnPilihCitra.FlatStyle = FlatStyle.Flat;
            btnPilihCitra.Font = new Font("Connection II", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
            radioBM.Font = new Font("Connection II", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioBM.Location = new Point(6, 11);
            radioBM.Margin = new Padding(0);
            radioBM.Name = "radioBM";
            radioBM.Size = new Size(61, 26);
            radioBM.TabIndex = 6;
            radioBM.TabStop = true;
            radioBM.Text = "BM";
            radioBM.UseVisualStyleBackColor = true;
            radioBM.CheckedChanged += radioBM_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(240, 88, 135);
            groupBox1.Controls.Add(radioKMP);
            groupBox1.Controls.Add(radioBM);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Press Start 2P", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(294, 629);
            groupBox1.Margin = new Padding(0);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(0);
            groupBox1.Size = new Size(145, 81);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            // 
            // radioKMP
            // 
            radioKMP.AutoSize = true;
            radioKMP.Font = new Font("Connection II", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioKMP.Location = new Point(6, 49);
            radioKMP.Margin = new Padding(0);
            radioKMP.Name = "radioKMP";
            radioKMP.Size = new Size(74, 26);
            radioKMP.TabIndex = 7;
            radioKMP.TabStop = true;
            radioKMP.Text = "KMP";
            radioKMP.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(240, 88, 135);
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Connection II", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(483, 643);
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
            lblWaktuPencarian.BackColor = Color.FromArgb(240, 88, 135);
            lblWaktuPencarian.Font = new Font("Connection II", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWaktuPencarian.Location = new Point(705, 655);
            lblWaktuPencarian.Name = "lblWaktuPencarian";
            lblWaktuPencarian.Size = new Size(211, 22);
            lblWaktuPencarian.TabIndex = 10;
            lblWaktuPencarian.Text = "Waktu Pencarian: ";
            lblWaktuPencarian.Click += lblWaktuPencarian_Click_1;
            // 
            // lblPersentaseKecocokan
            // 
            lblPersentaseKecocokan.AutoSize = true;
            lblPersentaseKecocokan.BackColor = Color.FromArgb(240, 88, 135);
            lblPersentaseKecocokan.Font = new Font("Connection II", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPersentaseKecocokan.Location = new Point(705, 688);
            lblPersentaseKecocokan.Name = "lblPersentaseKecocokan";
            lblPersentaseKecocokan.Size = new Size(265, 22);
            lblPersentaseKecocokan.TabIndex = 12;
            lblPersentaseKecocokan.Text = "Persentase Kecocokan:";
            lblPersentaseKecocokan.Click += lblPersentaseKecocokan_Click;
            // 
            // BiodataText
            // 
            BiodataText.BackColor = Color.White;
            BiodataText.BorderStyle = BorderStyle.FixedSingle;
            BiodataText.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold);
            BiodataText.Location = new Point(712, 168);
            BiodataText.Multiline = true;
            BiodataText.Name = "BiodataText";
            BiodataText.Size = new Size(338, 406);
            BiodataText.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1092, 784);
            Controls.Add(BiodataText);
            Controls.Add(lblPersentaseKecocokan);
            Controls.Add(lblWaktuPencarian);
            Controls.Add(btnSearch);
            Controls.Add(groupBox1);
            Controls.Add(btnPilihCitra);
            Controls.Add(picBoxMatched);
            Controls.Add(picBoxInput);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "ResmiTamatStima";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picBoxInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBoxMatched).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox picBoxInput;
        private System.Windows.Forms.PictureBox picBoxMatched;
        private System.Windows.Forms.Button btnPilihCitra;
        private System.Windows.Forms.RadioButton radioBM;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioKMP;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblWaktuPencarian;
        private System.Windows.Forms.Label lblPersentaseKecocokan;
        private TextBox BiodataText;
    }
}
