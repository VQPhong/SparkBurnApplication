namespace SparkBurnApplication
{
    partial class FlashBurnForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.comboBoxDrives = new System.Windows.Forms.ComboBox();
            this.buttonResetPartitions = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBarBurn = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelProgress = new System.Windows.Forms.Label();
            this.labelSupporter = new System.Windows.Forms.Label();
            this.buttonInputSerial = new System.Windows.Forms.Button();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(877, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "In đĩa";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonBurn_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(18, 19);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(580, 446);
            this.listBoxFiles.TabIndex = 1;
            // 
            // comboBoxDrives
            // 
            this.comboBoxDrives.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDrives.FormattingEnabled = true;
            this.comboBoxDrives.Location = new System.Drawing.Point(16, 23);
            this.comboBoxDrives.Name = "comboBoxDrives";
            this.comboBoxDrives.Size = new System.Drawing.Size(142, 21);
            this.comboBoxDrives.TabIndex = 2;
            // 
            // buttonResetPartitions
            // 
            this.buttonResetPartitions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetPartitions.Location = new System.Drawing.Point(164, 21);
            this.buttonResetPartitions.Name = "buttonResetPartitions";
            this.buttonResetPartitions.Size = new System.Drawing.Size(119, 23);
            this.buttonResetPartitions.TabIndex = 3;
            this.buttonResetPartitions.Text = "Tải lại danh sách";
            this.buttonResetPartitions.UseVisualStyleBackColor = true;
            this.buttonResetPartitions.Click += new System.EventHandler(this.buttonResetPartitions_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxFiles);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 476);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách file in";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxDrives);
            this.groupBox2.Controls.Add(this.buttonResetPartitions);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(653, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 62);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ổ đĩa";
            // 
            // progressBarBurn
            // 
            this.progressBarBurn.Location = new System.Drawing.Point(16, 19);
            this.progressBarBurn.Name = "progressBarBurn";
            this.progressBarBurn.Size = new System.Drawing.Size(267, 23);
            this.progressBarBurn.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxLoading);
            this.groupBox3.Controls.Add(this.labelProgress);
            this.groupBox3.Controls.Add(this.progressBarBurn);
            this.groupBox3.Location = new System.Drawing.Point(653, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 109);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tiến trình";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(13, 60);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(78, 13);
            this.labelProgress.TabIndex = 7;
            this.labelProgress.Text = "LabelTienTrinh";
            // 
            // labelSupporter
            // 
            this.labelSupporter.AutoSize = true;
            this.labelSupporter.Location = new System.Drawing.Point(666, 464);
            this.labelSupporter.Name = "labelSupporter";
            this.labelSupporter.Size = new System.Drawing.Size(79, 13);
            this.labelSupporter.TabIndex = 8;
            this.labelSupporter.Text = "LabelSupporter";
            // 
            // buttonInputSerial
            // 
            this.buttonInputSerial.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInputSerial.Location = new System.Drawing.Point(877, 80);
            this.buttonInputSerial.Name = "buttonInputSerial";
            this.buttonInputSerial.Size = new System.Drawing.Size(75, 35);
            this.buttonInputSerial.TabIndex = 9;
            this.buttonInputSerial.Text = "Nhập Serial";
            this.buttonInputSerial.UseVisualStyleBackColor = true;
            this.buttonInputSerial.Click += new System.EventHandler(this.buttonInputSerial_Click);
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Location = new System.Drawing.Point(235, 48);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(48, 46);
            this.pictureBoxLoading.TabIndex = 8;
            this.pictureBoxLoading.TabStop = false;
            // 
            // FlashBurnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 504);
            this.Controls.Add(this.buttonInputSerial);
            this.Controls.Add(this.labelSupporter);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FlashBurnForm";
            this.Text = "Spark Burn Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.ComboBox comboBoxDrives;
        private System.Windows.Forms.Button buttonResetPartitions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBarBurn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label labelSupporter;
        private System.Windows.Forms.Button buttonInputSerial;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
    }
}

