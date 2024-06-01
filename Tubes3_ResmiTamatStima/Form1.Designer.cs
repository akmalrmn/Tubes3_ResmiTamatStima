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
            this.JudulProgram = new System.Windows.Forms.Label();
            this.picBoxInput = new System.Windows.Forms.PictureBox();
            this.picBoxMatched = new System.Windows.Forms.PictureBox();
            this.picBoxResult = new System.Windows.Forms.PictureBox();
            this.btnPilihCitra = new System.Windows.Forms.Button();
            this.radioBM = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioKMP = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblWaktuPencarian = new System.Windows.Forms.Label();
            this.lblPersentaseKecocokan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMatched)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxResult)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // JudulProgram
            // 
            this.JudulProgram.AutoSize = true;
            this.JudulProgram.Font = new System.Drawing.Font("Roboto", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JudulProgram.Location = new System.Drawing.Point(84, 65);
            this.JudulProgram.Name = "JudulProgram";
            this.JudulProgram.Size = new System.Drawing.Size(895, 34);
            this.JudulProgram.TabIndex = 0;
            this.JudulProgram.Text = "Sistem Deteksi Individu Berbasis Biometrik Melalui Citra Sidik Jari";
            this.JudulProgram.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picBoxInput
            // 
            this.picBoxInput.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.picBoxInput.Location = new System.Drawing.Point(81, 134);
            this.picBoxInput.Name = "picBoxInput";
            this.picBoxInput.Size = new System.Drawing.Size(216, 325);
            this.picBoxInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxInput.TabIndex = 2;
            this.picBoxInput.TabStop = false;
            // 
            // picBoxMatched
            // 
            this.picBoxMatched.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.picBoxMatched.Location = new System.Drawing.Point(428, 134);
            this.picBoxMatched.Name = "picBoxMatched";
            this.picBoxMatched.Size = new System.Drawing.Size(216, 325);
            this.picBoxMatched.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxMatched.TabIndex = 3;
            this.picBoxMatched.TabStop = false;
            // 
            // picBoxResult
            // 
            this.picBoxResult.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.picBoxResult.Location = new System.Drawing.Point(763, 134);
            this.picBoxResult.Name = "picBoxResult";
            this.picBoxResult.Size = new System.Drawing.Size(216, 325);
            this.picBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxResult.TabIndex = 4;
            this.picBoxResult.TabStop = false;
            // 
            // btnPilihCitra
            // 
            this.btnPilihCitra.BackColor = System.Drawing.Color.White;
            this.btnPilihCitra.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPilihCitra.Location = new System.Drawing.Point(81, 511);
            this.btnPilihCitra.Name = "btnPilihCitra";
            this.btnPilihCitra.Size = new System.Drawing.Size(154, 65);
            this.btnPilihCitra.TabIndex = 5;
            this.btnPilihCitra.Text = "Pilih Citra";
            this.btnPilihCitra.UseVisualStyleBackColor = false;
            this.btnPilihCitra.Click += new System.EventHandler(this.btnPilihCitra_Click);
            // 
            // radioBM
            // 
            this.radioBM.AutoSize = true;
            this.radioBM.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBM.Location = new System.Drawing.Point(6, 9);
            this.radioBM.Name = "radioBM";
            this.radioBM.Size = new System.Drawing.Size(58, 24);
            this.radioBM.TabIndex = 6;
            this.radioBM.TabStop = true;
            this.radioBM.Text = "BM";
            this.radioBM.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.radioKMP);
            this.groupBox1.Controls.Add(this.radioBM);
            this.groupBox1.Location = new System.Drawing.Point(320, 511);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // radioKMP
            // 
            this.radioKMP.AutoSize = true;
            this.radioKMP.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioKMP.Location = new System.Drawing.Point(6, 39);
            this.radioKMP.Name = "radioKMP";
            this.radioKMP.Size = new System.Drawing.Size(70, 24);
            this.radioKMP.TabIndex = 7;
            this.radioKMP.TabStop = true;
            this.radioKMP.Text = "KMP";
            this.radioKMP.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Roboto", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(546, 516);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(144, 54);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblWaktuPencarian
            // 
            this.lblWaktuPencarian.AutoSize = true;
            this.lblWaktuPencarian.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaktuPencarian.Location = new System.Drawing.Point(745, 524);
            this.lblWaktuPencarian.Name = "lblWaktuPencarian";
            this.lblWaktuPencarian.Size = new System.Drawing.Size(161, 20);
            this.lblWaktuPencarian.TabIndex = 10;
            this.lblWaktuPencarian.Text = "Waktu Pencarian: ";
            // 
            // lblPersentaseKecocokan
            // 
            this.lblPersentaseKecocokan.AutoSize = true;
            this.lblPersentaseKecocokan.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersentaseKecocokan.Location = new System.Drawing.Point(745, 550);
            this.lblPersentaseKecocokan.Name = "lblPersentaseKecocokan";
            this.lblPersentaseKecocokan.Size = new System.Drawing.Size(209, 20);
            this.lblPersentaseKecocokan.TabIndex = 12;
            this.lblPersentaseKecocokan.Text = "Persentase Kecocokan:";
            // 
            // ResmiTamatStima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 627);
            this.Controls.Add(this.lblPersentaseKecocokan);
            this.Controls.Add(this.lblWaktuPencarian);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPilihCitra);
            this.Controls.Add(this.picBoxResult);
            this.Controls.Add(this.picBoxMatched);
            this.Controls.Add(this.picBoxInput);
            this.Controls.Add(this.JudulProgram);
            this.Name = "ResmiTamatStima";
            this.Text = "ResmiTamatStima";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMatched)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
