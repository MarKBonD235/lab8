namespace Практика_Зад5
{
    partial class ProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileForm));
            this.label5 = new System.Windows.Forms.Label();
            this.surnameBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.patronomicBox = new System.Windows.Forms.TextBox();
            this.roleBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.continueBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.OliveDrab;
            this.label5.Location = new System.Drawing.Point(107, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 31);
            this.label5.TabIndex = 4;
            this.label5.Text = "Добро пожаловать!";
            // 
            // surnameBox
            // 
            this.surnameBox.Enabled = false;
            this.surnameBox.Location = new System.Drawing.Point(67, 111);
            this.surnameBox.Name = "surnameBox";
            this.surnameBox.Size = new System.Drawing.Size(125, 27);
            this.surnameBox.TabIndex = 5;
            // 
            // nameBox
            // 
            this.nameBox.Enabled = false;
            this.nameBox.Location = new System.Drawing.Point(67, 156);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(125, 27);
            this.nameBox.TabIndex = 6;
            // 
            // patronomicBox
            // 
            this.patronomicBox.Enabled = false;
            this.patronomicBox.Location = new System.Drawing.Point(67, 207);
            this.patronomicBox.Name = "patronomicBox";
            this.patronomicBox.Size = new System.Drawing.Size(125, 27);
            this.patronomicBox.TabIndex = 7;
            // 
            // roleBox
            // 
            this.roleBox.Enabled = false;
            this.roleBox.Location = new System.Drawing.Point(67, 258);
            this.roleBox.Name = "roleBox";
            this.roleBox.Size = new System.Drawing.Size(125, 27);
            this.roleBox.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(255, 135);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 130);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // continueBtn
            // 
            this.continueBtn.BackColor = System.Drawing.Color.IndianRed;
            this.continueBtn.Location = new System.Drawing.Point(147, 344);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(150, 49);
            this.continueBtn.TabIndex = 10;
            this.continueBtn.Text = "Продолжить";
            this.continueBtn.UseVisualStyleBackColor = false;
            this.continueBtn.Click += new System.EventHandler(this.continueBtn_Click);
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(452, 403);
            this.ControlBox = false;
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.roleBox);
            this.Controls.Add(this.patronomicBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.surnameBox);
            this.Controls.Add(this.label5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 450);
            this.Name = "ProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Профиль";
            this.Load += new System.EventHandler(this.ProfileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label5;
        private PictureBox pictureBox1;
        public Button continueBtn;
        public TextBox surnameBox;
        public TextBox nameBox;
        public TextBox patronomicBox;
        public TextBox roleBox;
    }
}