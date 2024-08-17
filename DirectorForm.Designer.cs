namespace CluelessControl
{
    partial class DirectorForm
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
            DirectorTabControl = new TabControl();
            BeforeTheShowTab = new TabPage();
            GamePickEnvelopesTab = new TabPage();
            GameQuestionsTab = new TabPage();
            GameTradingTab = new TabPage();
            GameOverTab = new TabPage();
            GameSettingsEnvelopesTab = new TabPage();
            GameSettingsTab = new TabPage();
            QuestionEditorTab = new TabPage();
            DirectorTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // DirectorTabControl
            // 
            DirectorTabControl.Controls.Add(BeforeTheShowTab);
            DirectorTabControl.Controls.Add(GamePickEnvelopesTab);
            DirectorTabControl.Controls.Add(GameQuestionsTab);
            DirectorTabControl.Controls.Add(GameTradingTab);
            DirectorTabControl.Controls.Add(GameOverTab);
            DirectorTabControl.Controls.Add(GameSettingsEnvelopesTab);
            DirectorTabControl.Controls.Add(GameSettingsTab);
            DirectorTabControl.Controls.Add(QuestionEditorTab);
            DirectorTabControl.Location = new Point(12, 66);
            DirectorTabControl.Name = "DirectorTabControl";
            DirectorTabControl.SelectedIndex = 0;
            DirectorTabControl.Size = new Size(776, 372);
            DirectorTabControl.TabIndex = 0;
            // 
            // BeforeTheShowTab
            // 
            BeforeTheShowTab.Location = new Point(4, 24);
            BeforeTheShowTab.Name = "BeforeTheShowTab";
            BeforeTheShowTab.Padding = new Padding(3);
            BeforeTheShowTab.Size = new Size(768, 344);
            BeforeTheShowTab.TabIndex = 0;
            BeforeTheShowTab.Text = "Przed programem";
            BeforeTheShowTab.UseVisualStyleBackColor = true;
            // 
            // GamePickEnvelopesTab
            // 
            GamePickEnvelopesTab.Location = new Point(4, 24);
            GamePickEnvelopesTab.Name = "GamePickEnvelopesTab";
            GamePickEnvelopesTab.Padding = new Padding(3);
            GamePickEnvelopesTab.Size = new Size(768, 344);
            GamePickEnvelopesTab.TabIndex = 1;
            GamePickEnvelopesTab.Text = "Gra - Wybór kopert";
            GamePickEnvelopesTab.UseVisualStyleBackColor = true;
            // 
            // GameQuestionsTab
            // 
            GameQuestionsTab.Location = new Point(4, 24);
            GameQuestionsTab.Name = "GameQuestionsTab";
            GameQuestionsTab.Size = new Size(768, 344);
            GameQuestionsTab.TabIndex = 2;
            GameQuestionsTab.Text = "Gra - Pytania";
            GameQuestionsTab.UseVisualStyleBackColor = true;
            // 
            // GameTradingTab
            // 
            GameTradingTab.Location = new Point(4, 24);
            GameTradingTab.Name = "GameTradingTab";
            GameTradingTab.Size = new Size(768, 344);
            GameTradingTab.TabIndex = 3;
            GameTradingTab.Text = "Gra - Licytacja";
            GameTradingTab.UseVisualStyleBackColor = true;
            // 
            // GameOverTab
            // 
            GameOverTab.Location = new Point(4, 24);
            GameOverTab.Name = "GameOverTab";
            GameOverTab.Size = new Size(768, 344);
            GameOverTab.TabIndex = 4;
            GameOverTab.Text = "Gra - Koniec";
            GameOverTab.UseVisualStyleBackColor = true;
            // 
            // GameSettingsEnvelopesTab
            // 
            GameSettingsEnvelopesTab.Location = new Point(4, 24);
            GameSettingsEnvelopesTab.Name = "GameSettingsEnvelopesTab";
            GameSettingsEnvelopesTab.Size = new Size(768, 344);
            GameSettingsEnvelopesTab.TabIndex = 5;
            GameSettingsEnvelopesTab.Text = "Ustawienia - Koperty";
            GameSettingsEnvelopesTab.UseVisualStyleBackColor = true;
            // 
            // GameSettingsTab
            // 
            GameSettingsTab.Location = new Point(4, 24);
            GameSettingsTab.Name = "GameSettingsTab";
            GameSettingsTab.Size = new Size(768, 344);
            GameSettingsTab.TabIndex = 6;
            GameSettingsTab.Text = "Ustawienia";
            GameSettingsTab.UseVisualStyleBackColor = true;
            // 
            // QuestionEditorTab
            // 
            QuestionEditorTab.Location = new Point(4, 24);
            QuestionEditorTab.Name = "QuestionEditorTab";
            QuestionEditorTab.Size = new Size(768, 344);
            QuestionEditorTab.TabIndex = 7;
            QuestionEditorTab.Text = "Edytor pytań";
            QuestionEditorTab.UseVisualStyleBackColor = true;
            // 
            // DirectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(800, 450);
            Controls.Add(DirectorTabControl);
            DoubleBuffered = true;
            Name = "DirectorForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gra w Ciemno - Reżyserka";
            Load += DirectorForm_Load;
            DirectorTabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl DirectorTabControl;
        private TabPage BeforeTheShowTab;
        private TabPage GamePickEnvelopesTab;
        private TabPage GameQuestionsTab;
        private TabPage GameTradingTab;
        private TabPage GameOverTab;
        private TabPage GameSettingsEnvelopesTab;
        private TabPage GameSettingsTab;
        private TabPage QuestionEditorTab;
    }
}
