namespace CluelessControl
{
    partial class AboutAuthorForm
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
            SoftwareTitle = new Label();
            ProgrammingTitle = new Label();
            ProgrammerName = new Label();
            MusicSoundName = new Label();
            MusicSoundTitle = new Label();
            SuspendLayout();
            // 
            // SoftwareTitle
            // 
            SoftwareTitle.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            SoftwareTitle.Location = new Point(12, 8);
            SoftwareTitle.Name = "SoftwareTitle";
            SoftwareTitle.Size = new Size(339, 45);
            SoftwareTitle.TabIndex = 0;
            SoftwareTitle.Text = "GRA W CIEMNO - KONTROLKA";
            SoftwareTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProgrammingTitle
            // 
            ProgrammingTitle.Location = new Point(12, 53);
            ProgrammingTitle.Name = "ProgrammingTitle";
            ProgrammingTitle.Size = new Size(339, 30);
            ProgrammingTitle.TabIndex = 1;
            ProgrammingTitle.Text = "Programowanie:";
            ProgrammingTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ProgrammerName
            // 
            ProgrammerName.Font = new Font("Arial", 14.25F);
            ProgrammerName.Location = new Point(12, 83);
            ProgrammerName.Name = "ProgrammerName";
            ProgrammerName.Size = new Size(339, 75);
            ProgrammerName.TabIndex = 2;
            ProgrammerName.Text = "CMike98\r\nDiscord: CMike98";
            ProgrammerName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MusicSoundName
            // 
            MusicSoundName.Font = new Font("Arial", 14.25F);
            MusicSoundName.Location = new Point(12, 188);
            MusicSoundName.Name = "MusicSoundName";
            MusicSoundName.Size = new Size(339, 46);
            MusicSoundName.TabIndex = 5;
            MusicSoundName.Text = "patrijk42333";
            MusicSoundName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MusicSoundTitle
            // 
            MusicSoundTitle.Location = new Point(12, 158);
            MusicSoundTitle.Name = "MusicSoundTitle";
            MusicSoundTitle.Size = new Size(339, 30);
            MusicSoundTitle.TabIndex = 4;
            MusicSoundTitle.Text = "Oryginalne udźwiękowienie:";
            MusicSoundTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AboutAuthorForm
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(363, 243);
            Controls.Add(MusicSoundName);
            Controls.Add(MusicSoundTitle);
            Controls.Add(ProgrammerName);
            Controls.Add(ProgrammingTitle);
            Controls.Add(SoftwareTitle);
            Font = new Font("Arial", 12F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutAuthorForm";
            Text = "O autorze";
            ResumeLayout(false);
        }

        #endregion

        private Label SoftwareTitle;
        private Label ProgrammingTitle;
        private Label ProgrammerName;
        private Label MusicSoundName;
        private Label MusicSoundTitle;
    }
}