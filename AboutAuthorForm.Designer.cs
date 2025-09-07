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
            GraphicsName1 = new Label();
            GraphicsTitle = new Label();
            GraphicsName2 = new Label();
            SuspendLayout();
            // 
            // SoftwareTitle
            // 
            SoftwareTitle.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            SoftwareTitle.Location = new Point(12, 8);
            SoftwareTitle.Name = "SoftwareTitle";
            SoftwareTitle.Size = new Size(469, 45);
            SoftwareTitle.TabIndex = 0;
            SoftwareTitle.Text = "GRA W CIEMNO - OPROGRAMOWANIE STERUJĄCE";
            SoftwareTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProgrammingTitle
            // 
            ProgrammingTitle.Font = new Font("Arial", 12F, FontStyle.Italic);
            ProgrammingTitle.Location = new Point(12, 53);
            ProgrammingTitle.Name = "ProgrammingTitle";
            ProgrammingTitle.Size = new Size(469, 30);
            ProgrammingTitle.TabIndex = 1;
            ProgrammingTitle.Text = "Programowanie:";
            ProgrammingTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ProgrammerName
            // 
            ProgrammerName.Font = new Font("Arial", 14.25F);
            ProgrammerName.Location = new Point(12, 83);
            ProgrammerName.Name = "ProgrammerName";
            ProgrammerName.Size = new Size(469, 53);
            ProgrammerName.TabIndex = 2;
            ProgrammerName.Text = "CMike98\r\nDiscord: cmike98";
            ProgrammerName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MusicSoundName
            // 
            MusicSoundName.Font = new Font("Arial", 14.25F);
            MusicSoundName.Location = new Point(12, 166);
            MusicSoundName.Name = "MusicSoundName";
            MusicSoundName.Size = new Size(469, 34);
            MusicSoundName.TabIndex = 5;
            MusicSoundName.Text = "patrijk42333";
            MusicSoundName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MusicSoundTitle
            // 
            MusicSoundTitle.Font = new Font("Arial", 12F, FontStyle.Italic);
            MusicSoundTitle.Location = new Point(12, 136);
            MusicSoundTitle.Name = "MusicSoundTitle";
            MusicSoundTitle.Size = new Size(469, 30);
            MusicSoundTitle.TabIndex = 4;
            MusicSoundTitle.Text = "Udźwiękowienie:";
            MusicSoundTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GraphicsName1
            // 
            GraphicsName1.Font = new Font("Arial", 14.25F);
            GraphicsName1.Location = new Point(12, 230);
            GraphicsName1.Name = "GraphicsName1";
            GraphicsName1.Size = new Size(469, 75);
            GraphicsName1.TabIndex = 7;
            GraphicsName1.Text = "Iwan111\r\nDiscord: iwan111\r\nhttps://www.youtube.com/@TheIwan1111";
            GraphicsName1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GraphicsTitle
            // 
            GraphicsTitle.Font = new Font("Arial", 12F, FontStyle.Italic);
            GraphicsTitle.Location = new Point(12, 200);
            GraphicsTitle.Name = "GraphicsTitle";
            GraphicsTitle.Size = new Size(469, 30);
            GraphicsTitle.TabIndex = 6;
            GraphicsTitle.Text = "Grafika:";
            GraphicsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // GraphicsName2
            // 
            GraphicsName2.Font = new Font("Arial", 14.25F);
            GraphicsName2.Location = new Point(12, 305);
            GraphicsName2.Name = "GraphicsName2";
            GraphicsName2.Size = new Size(469, 75);
            GraphicsName2.TabIndex = 8;
            GraphicsName2.Text = "HoSt QL\r\nDiscord: hostql\r\nhttps://www.youtube.com/@InternetowiMilionerzy";
            GraphicsName2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AboutAuthorForm
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 389);
            Controls.Add(GraphicsName2);
            Controls.Add(GraphicsName1);
            Controls.Add(GraphicsTitle);
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
        private Label GraphicsName1;
        private Label GraphicsTitle;
        private Label GraphicsName2;
    }
}