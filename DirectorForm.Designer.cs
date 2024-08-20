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
            PreShowQuestionEditorBtn = new Button();
            PreShowEnvelopeSettingsBtn = new Button();
            PreShowGameSettingsBtn = new Button();
            PreShowStartGameBtn = new Button();
            PreShowIntroBtn = new Button();
            PreShowTitleLbl = new Label();
            GameSettingsTab = new TabPage();
            GameSettingsEnvelopesTab = new TabPage();
            EnvelopeSettingsPercentageTxtBox = new TextBox();
            EnvelopeSettingsPercentageRadio = new RadioButton();
            EnvelopeSettingsCashTxtBox = new TextBox();
            EnvelopeSettingsCashRadio = new RadioButton();
            EnvelopeSettingsHintLbl = new Label();
            EnvelopeSettingsMoveDownBtn = new Button();
            EnvelopeSettingsMoveUpBtn = new Button();
            EnvelopeSettingsDeleteBtn = new Button();
            EnvelopeSettingsNewBtn = new Button();
            EnvelopeSettingsListBox = new ListBox();
            EnvelopeSettingsCountLbl = new Label();
            EnvelopeSettingsSaveToFileBtn = new Button();
            EnvelopeSettingsSaveBtn = new Button();
            EnvelopeSettingsLoadFromFileBtn = new Button();
            EnvelopeSettingsRandomiseBtn = new Button();
            QuestionEditorTab = new TabPage();
            GamePickEnvelopesTab = new TabPage();
            GameQuestionsTab = new TabPage();
            GameTradingTab = new TabPage();
            GameOverTab = new TabPage();
            EnvelopeSettingsOpen = new OpenFileDialog();
            EnvelopeSettingsSave = new SaveFileDialog();
            DirectorTabControl.SuspendLayout();
            BeforeTheShowTab.SuspendLayout();
            GameSettingsEnvelopesTab.SuspendLayout();
            SuspendLayout();
            // 
            // DirectorTabControl
            // 
            DirectorTabControl.Controls.Add(BeforeTheShowTab);
            DirectorTabControl.Controls.Add(GameSettingsTab);
            DirectorTabControl.Controls.Add(GameSettingsEnvelopesTab);
            DirectorTabControl.Controls.Add(QuestionEditorTab);
            DirectorTabControl.Controls.Add(GamePickEnvelopesTab);
            DirectorTabControl.Controls.Add(GameQuestionsTab);
            DirectorTabControl.Controls.Add(GameTradingTab);
            DirectorTabControl.Controls.Add(GameOverTab);
            DirectorTabControl.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            DirectorTabControl.Location = new Point(12, 66);
            DirectorTabControl.Name = "DirectorTabControl";
            DirectorTabControl.SelectedIndex = 0;
            DirectorTabControl.Size = new Size(1092, 473);
            DirectorTabControl.TabIndex = 0;
            // 
            // BeforeTheShowTab
            // 
            BeforeTheShowTab.BackColor = SystemColors.Control;
            BeforeTheShowTab.Controls.Add(PreShowQuestionEditorBtn);
            BeforeTheShowTab.Controls.Add(PreShowEnvelopeSettingsBtn);
            BeforeTheShowTab.Controls.Add(PreShowGameSettingsBtn);
            BeforeTheShowTab.Controls.Add(PreShowStartGameBtn);
            BeforeTheShowTab.Controls.Add(PreShowIntroBtn);
            BeforeTheShowTab.Controls.Add(PreShowTitleLbl);
            BeforeTheShowTab.Location = new Point(4, 24);
            BeforeTheShowTab.Name = "BeforeTheShowTab";
            BeforeTheShowTab.Padding = new Padding(3);
            BeforeTheShowTab.Size = new Size(1084, 445);
            BeforeTheShowTab.TabIndex = 0;
            BeforeTheShowTab.Text = "Przed programem";
            // 
            // PreShowQuestionEditorBtn
            // 
            PreShowQuestionEditorBtn.Font = new Font("Arial", 14.25F);
            PreShowQuestionEditorBtn.Location = new Point(651, 188);
            PreShowQuestionEditorBtn.Name = "PreShowQuestionEditorBtn";
            PreShowQuestionEditorBtn.Size = new Size(211, 68);
            PreShowQuestionEditorBtn.TabIndex = 5;
            PreShowQuestionEditorBtn.Text = "Edytor pytań";
            PreShowQuestionEditorBtn.UseVisualStyleBackColor = true;
            PreShowQuestionEditorBtn.Click += PreShowQuestionEditorBtn_Click;
            // 
            // PreShowEnvelopeSettingsBtn
            // 
            PreShowEnvelopeSettingsBtn.Font = new Font("Arial", 14.25F);
            PreShowEnvelopeSettingsBtn.Location = new Point(434, 188);
            PreShowEnvelopeSettingsBtn.Name = "PreShowEnvelopeSettingsBtn";
            PreShowEnvelopeSettingsBtn.Size = new Size(211, 68);
            PreShowEnvelopeSettingsBtn.TabIndex = 4;
            PreShowEnvelopeSettingsBtn.Text = "Ustawienia kopert";
            PreShowEnvelopeSettingsBtn.UseVisualStyleBackColor = true;
            PreShowEnvelopeSettingsBtn.Click += PreShowEnvelopeSettingsBtn_Click;
            // 
            // PreShowGameSettingsBtn
            // 
            PreShowGameSettingsBtn.Font = new Font("Arial", 14.25F);
            PreShowGameSettingsBtn.Location = new Point(217, 188);
            PreShowGameSettingsBtn.Name = "PreShowGameSettingsBtn";
            PreShowGameSettingsBtn.Size = new Size(211, 68);
            PreShowGameSettingsBtn.TabIndex = 3;
            PreShowGameSettingsBtn.Text = "Ustawienia gry";
            PreShowGameSettingsBtn.UseVisualStyleBackColor = true;
            PreShowGameSettingsBtn.Click += PreShowGameSettingsBtn_Click;
            // 
            // PreShowStartGameBtn
            // 
            PreShowStartGameBtn.Font = new Font("Arial", 14.25F);
            PreShowStartGameBtn.Location = new Point(553, 114);
            PreShowStartGameBtn.Name = "PreShowStartGameBtn";
            PreShowStartGameBtn.Size = new Size(211, 68);
            PreShowStartGameBtn.TabIndex = 2;
            PreShowStartGameBtn.Text = "Rozpocznij grę";
            PreShowStartGameBtn.UseVisualStyleBackColor = true;
            PreShowStartGameBtn.Click += PreShowStartGameBtn_Click;
            // 
            // PreShowIntroBtn
            // 
            PreShowIntroBtn.Font = new Font("Arial", 14.25F);
            PreShowIntroBtn.Location = new Point(336, 114);
            PreShowIntroBtn.Name = "PreShowIntroBtn";
            PreShowIntroBtn.Size = new Size(211, 68);
            PreShowIntroBtn.TabIndex = 1;
            PreShowIntroBtn.Text = "Intro";
            PreShowIntroBtn.UseVisualStyleBackColor = true;
            PreShowIntroBtn.Click += PreShowIntroBtn_Click;
            // 
            // PreShowTitleLbl
            // 
            PreShowTitleLbl.Font = new Font("Arial", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            PreShowTitleLbl.Location = new Point(6, 16);
            PreShowTitleLbl.Name = "PreShowTitleLbl";
            PreShowTitleLbl.Size = new Size(1072, 95);
            PreShowTitleLbl.TabIndex = 0;
            PreShowTitleLbl.Text = "GRA W CIEMNO - REŻYSERKA";
            PreShowTitleLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GameSettingsTab
            // 
            GameSettingsTab.BackColor = SystemColors.Control;
            GameSettingsTab.Location = new Point(4, 24);
            GameSettingsTab.Name = "GameSettingsTab";
            GameSettingsTab.Size = new Size(1084, 445);
            GameSettingsTab.TabIndex = 6;
            GameSettingsTab.Text = "Ustawienia";
            // 
            // GameSettingsEnvelopesTab
            // 
            GameSettingsEnvelopesTab.BackColor = SystemColors.Control;
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsPercentageTxtBox);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsPercentageRadio);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsCashTxtBox);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsCashRadio);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsHintLbl);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsMoveDownBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsMoveUpBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsDeleteBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsNewBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsListBox);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsCountLbl);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsSaveToFileBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsSaveBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsLoadFromFileBtn);
            GameSettingsEnvelopesTab.Controls.Add(EnvelopeSettingsRandomiseBtn);
            GameSettingsEnvelopesTab.Location = new Point(4, 24);
            GameSettingsEnvelopesTab.Name = "GameSettingsEnvelopesTab";
            GameSettingsEnvelopesTab.Size = new Size(1084, 445);
            GameSettingsEnvelopesTab.TabIndex = 5;
            GameSettingsEnvelopesTab.Text = "Ustawienia - Koperty";
            // 
            // EnvelopeSettingsPercentageTxtBox
            // 
            EnvelopeSettingsPercentageTxtBox.Enabled = false;
            EnvelopeSettingsPercentageTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            EnvelopeSettingsPercentageTxtBox.Location = new Point(309, 190);
            EnvelopeSettingsPercentageTxtBox.Name = "EnvelopeSettingsPercentageTxtBox";
            EnvelopeSettingsPercentageTxtBox.Size = new Size(743, 35);
            EnvelopeSettingsPercentageTxtBox.TabIndex = 64;
            EnvelopeSettingsPercentageTxtBox.Text = "0";
            EnvelopeSettingsPercentageTxtBox.TextAlign = HorizontalAlignment.Center;
            EnvelopeSettingsPercentageTxtBox.TextChanged += EnvelopeSettingsPercentageTxtBox_TextChanged;
            // 
            // EnvelopeSettingsPercentageRadio
            // 
            EnvelopeSettingsPercentageRadio.Enabled = false;
            EnvelopeSettingsPercentageRadio.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            EnvelopeSettingsPercentageRadio.Location = new Point(309, 146);
            EnvelopeSettingsPercentageRadio.Name = "EnvelopeSettingsPercentageRadio";
            EnvelopeSettingsPercentageRadio.Size = new Size(743, 31);
            EnvelopeSettingsPercentageRadio.TabIndex = 63;
            EnvelopeSettingsPercentageRadio.TabStop = true;
            EnvelopeSettingsPercentageRadio.Text = "Procent (wpisz liczbę, np. +100% => 100, -100% => -100)";
            EnvelopeSettingsPercentageRadio.UseVisualStyleBackColor = true;
            EnvelopeSettingsPercentageRadio.CheckedChanged += EnvelopeSettingsPercentageRadio_CheckedChanged;
            // 
            // EnvelopeSettingsCashTxtBox
            // 
            EnvelopeSettingsCashTxtBox.Enabled = false;
            EnvelopeSettingsCashTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            EnvelopeSettingsCashTxtBox.Location = new Point(309, 96);
            EnvelopeSettingsCashTxtBox.Name = "EnvelopeSettingsCashTxtBox";
            EnvelopeSettingsCashTxtBox.Size = new Size(743, 35);
            EnvelopeSettingsCashTxtBox.TabIndex = 62;
            EnvelopeSettingsCashTxtBox.Text = "0";
            EnvelopeSettingsCashTxtBox.TextAlign = HorizontalAlignment.Center;
            EnvelopeSettingsCashTxtBox.TextChanged += EnvelopeSettingsCashTxtBox_TextChanged;
            // 
            // EnvelopeSettingsCashRadio
            // 
            EnvelopeSettingsCashRadio.Enabled = false;
            EnvelopeSettingsCashRadio.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            EnvelopeSettingsCashRadio.Location = new Point(309, 52);
            EnvelopeSettingsCashRadio.Name = "EnvelopeSettingsCashRadio";
            EnvelopeSettingsCashRadio.Size = new Size(743, 31);
            EnvelopeSettingsCashRadio.TabIndex = 61;
            EnvelopeSettingsCashRadio.TabStop = true;
            EnvelopeSettingsCashRadio.Text = "Pieniądze (wpisz liczbę, np. 100 000 => 100000)";
            EnvelopeSettingsCashRadio.UseVisualStyleBackColor = true;
            EnvelopeSettingsCashRadio.CheckedChanged += EnvelopeSettingsCashRadio_CheckedChanged;
            // 
            // EnvelopeSettingsHintLbl
            // 
            EnvelopeSettingsHintLbl.Font = new Font("Arial", 18F, FontStyle.Italic, GraphicsUnit.Point, 238);
            EnvelopeSettingsHintLbl.Location = new Point(309, 328);
            EnvelopeSettingsHintLbl.Name = "EnvelopeSettingsHintLbl";
            EnvelopeSettingsHintLbl.Size = new Size(743, 106);
            EnvelopeSettingsHintLbl.TabIndex = 60;
            EnvelopeSettingsHintLbl.Text = "Podpowiedź: jeżeli chcesz zero w kopercie, lepiej zrób kopertę z kwotą 0, niż +0%.";
            EnvelopeSettingsHintLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EnvelopeSettingsMoveDownBtn
            // 
            EnvelopeSettingsMoveDownBtn.Enabled = false;
            EnvelopeSettingsMoveDownBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsMoveDownBtn.Location = new Point(152, 216);
            EnvelopeSettingsMoveDownBtn.Name = "EnvelopeSettingsMoveDownBtn";
            EnvelopeSettingsMoveDownBtn.Size = new Size(128, 50);
            EnvelopeSettingsMoveDownBtn.TabIndex = 59;
            EnvelopeSettingsMoveDownBtn.Text = "W dół";
            EnvelopeSettingsMoveDownBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsMoveDownBtn.Click += EnvelopeSettingsMoveDownBtn_Click;
            // 
            // EnvelopeSettingsMoveUpBtn
            // 
            EnvelopeSettingsMoveUpBtn.Enabled = false;
            EnvelopeSettingsMoveUpBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsMoveUpBtn.Location = new Point(12, 216);
            EnvelopeSettingsMoveUpBtn.Name = "EnvelopeSettingsMoveUpBtn";
            EnvelopeSettingsMoveUpBtn.Size = new Size(128, 50);
            EnvelopeSettingsMoveUpBtn.TabIndex = 58;
            EnvelopeSettingsMoveUpBtn.Text = "W górę";
            EnvelopeSettingsMoveUpBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsMoveUpBtn.Click += EnvelopeSettingsMoveUpBtn_Click;
            // 
            // EnvelopeSettingsDeleteBtn
            // 
            EnvelopeSettingsDeleteBtn.Enabled = false;
            EnvelopeSettingsDeleteBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsDeleteBtn.Location = new Point(152, 272);
            EnvelopeSettingsDeleteBtn.Name = "EnvelopeSettingsDeleteBtn";
            EnvelopeSettingsDeleteBtn.Size = new Size(128, 50);
            EnvelopeSettingsDeleteBtn.TabIndex = 57;
            EnvelopeSettingsDeleteBtn.Text = "Usuń";
            EnvelopeSettingsDeleteBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsDeleteBtn.Click += EnvelopeSettingsDeleteBtn_Click;
            // 
            // EnvelopeSettingsNewBtn
            // 
            EnvelopeSettingsNewBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsNewBtn.Location = new Point(12, 272);
            EnvelopeSettingsNewBtn.Name = "EnvelopeSettingsNewBtn";
            EnvelopeSettingsNewBtn.Size = new Size(128, 50);
            EnvelopeSettingsNewBtn.TabIndex = 56;
            EnvelopeSettingsNewBtn.Text = "Nowa";
            EnvelopeSettingsNewBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsNewBtn.Click += EnvelopeSettingsNewBtn_Click;
            // 
            // EnvelopeSettingsListBox
            // 
            EnvelopeSettingsListBox.Font = new Font("Arial", 14.25F);
            EnvelopeSettingsListBox.FormattingEnabled = true;
            EnvelopeSettingsListBox.ItemHeight = 22;
            EnvelopeSettingsListBox.Location = new Point(12, 52);
            EnvelopeSettingsListBox.Name = "EnvelopeSettingsListBox";
            EnvelopeSettingsListBox.Size = new Size(268, 158);
            EnvelopeSettingsListBox.TabIndex = 55;
            EnvelopeSettingsListBox.SelectedIndexChanged += EnvelopeSettingsListBox_SelectedIndexChanged;
            // 
            // EnvelopeSettingsCountLbl
            // 
            EnvelopeSettingsCountLbl.Font = new Font("Arial", 14.25F);
            EnvelopeSettingsCountLbl.Location = new Point(12, 14);
            EnvelopeSettingsCountLbl.Name = "EnvelopeSettingsCountLbl";
            EnvelopeSettingsCountLbl.Size = new Size(268, 35);
            EnvelopeSettingsCountLbl.TabIndex = 54;
            EnvelopeSettingsCountLbl.Text = "Kopert: 0";
            EnvelopeSettingsCountLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EnvelopeSettingsSaveToFileBtn
            // 
            EnvelopeSettingsSaveToFileBtn.Enabled = false;
            EnvelopeSettingsSaveToFileBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsSaveToFileBtn.Location = new Point(152, 384);
            EnvelopeSettingsSaveToFileBtn.Name = "EnvelopeSettingsSaveToFileBtn";
            EnvelopeSettingsSaveToFileBtn.Size = new Size(128, 50);
            EnvelopeSettingsSaveToFileBtn.TabIndex = 53;
            EnvelopeSettingsSaveToFileBtn.Text = "Zapisz zestaw do pliku";
            EnvelopeSettingsSaveToFileBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsSaveToFileBtn.Click += EnvelopeSettingsSaveToFileBtn_Click;
            // 
            // EnvelopeSettingsSaveBtn
            // 
            EnvelopeSettingsSaveBtn.Enabled = false;
            EnvelopeSettingsSaveBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsSaveBtn.Location = new Point(152, 328);
            EnvelopeSettingsSaveBtn.Name = "EnvelopeSettingsSaveBtn";
            EnvelopeSettingsSaveBtn.Size = new Size(128, 50);
            EnvelopeSettingsSaveBtn.TabIndex = 52;
            EnvelopeSettingsSaveBtn.Text = "Zapisz zestaw";
            EnvelopeSettingsSaveBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsSaveBtn.Click += EnvelopeSettingsSaveBtn_Click;
            // 
            // EnvelopeSettingsLoadFromFileBtn
            // 
            EnvelopeSettingsLoadFromFileBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsLoadFromFileBtn.Location = new Point(12, 384);
            EnvelopeSettingsLoadFromFileBtn.Name = "EnvelopeSettingsLoadFromFileBtn";
            EnvelopeSettingsLoadFromFileBtn.Size = new Size(128, 50);
            EnvelopeSettingsLoadFromFileBtn.TabIndex = 51;
            EnvelopeSettingsLoadFromFileBtn.Text = "Wczytaj z pliku";
            EnvelopeSettingsLoadFromFileBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsLoadFromFileBtn.Click += EnvelopeSettingsLoadFromFileBtn_Click;
            // 
            // EnvelopeSettingsRandomiseBtn
            // 
            EnvelopeSettingsRandomiseBtn.Enabled = false;
            EnvelopeSettingsRandomiseBtn.Font = new Font("Arial", 12F);
            EnvelopeSettingsRandomiseBtn.Location = new Point(12, 328);
            EnvelopeSettingsRandomiseBtn.Name = "EnvelopeSettingsRandomiseBtn";
            EnvelopeSettingsRandomiseBtn.Size = new Size(128, 50);
            EnvelopeSettingsRandomiseBtn.TabIndex = 50;
            EnvelopeSettingsRandomiseBtn.Text = "Losuj kolejność";
            EnvelopeSettingsRandomiseBtn.UseVisualStyleBackColor = true;
            EnvelopeSettingsRandomiseBtn.Click += EnvelopeSettingsRandomiseBtn_Click;
            // 
            // QuestionEditorTab
            // 
            QuestionEditorTab.BackColor = SystemColors.Control;
            QuestionEditorTab.Location = new Point(4, 24);
            QuestionEditorTab.Name = "QuestionEditorTab";
            QuestionEditorTab.Size = new Size(1084, 445);
            QuestionEditorTab.TabIndex = 7;
            QuestionEditorTab.Text = "Edytor pytań";
            // 
            // GamePickEnvelopesTab
            // 
            GamePickEnvelopesTab.BackColor = SystemColors.Control;
            GamePickEnvelopesTab.Location = new Point(4, 24);
            GamePickEnvelopesTab.Name = "GamePickEnvelopesTab";
            GamePickEnvelopesTab.Padding = new Padding(3);
            GamePickEnvelopesTab.Size = new Size(1084, 445);
            GamePickEnvelopesTab.TabIndex = 1;
            GamePickEnvelopesTab.Text = "Gra - Wybór kopert";
            // 
            // GameQuestionsTab
            // 
            GameQuestionsTab.BackColor = SystemColors.Control;
            GameQuestionsTab.Location = new Point(4, 24);
            GameQuestionsTab.Name = "GameQuestionsTab";
            GameQuestionsTab.Size = new Size(1084, 445);
            GameQuestionsTab.TabIndex = 2;
            GameQuestionsTab.Text = "Gra - Pytania";
            // 
            // GameTradingTab
            // 
            GameTradingTab.BackColor = SystemColors.Control;
            GameTradingTab.Location = new Point(4, 24);
            GameTradingTab.Name = "GameTradingTab";
            GameTradingTab.Size = new Size(1084, 445);
            GameTradingTab.TabIndex = 3;
            GameTradingTab.Text = "Gra - Licytacja";
            // 
            // GameOverTab
            // 
            GameOverTab.BackColor = SystemColors.Control;
            GameOverTab.Location = new Point(4, 24);
            GameOverTab.Name = "GameOverTab";
            GameOverTab.Size = new Size(1084, 445);
            GameOverTab.TabIndex = 4;
            GameOverTab.Text = "Gra - Koniec";
            // 
            // EnvelopeSettingsOpen
            // 
            EnvelopeSettingsOpen.Filter = "Pliki *.json|*.json";
            EnvelopeSettingsOpen.Title = "Ładowanie zestawu kopert";
            // 
            // EnvelopeSettingsSave
            // 
            EnvelopeSettingsSave.Filter = "Pliki *.json|*.json";
            EnvelopeSettingsSave.Title = "Zapisywanie zestawu kopert";
            // 
            // DirectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1116, 542);
            Controls.Add(DirectorTabControl);
            DoubleBuffered = true;
            Name = "DirectorForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gra w Ciemno - Reżyserka";
            Load += DirectorForm_Load;
            DirectorTabControl.ResumeLayout(false);
            BeforeTheShowTab.ResumeLayout(false);
            GameSettingsEnvelopesTab.ResumeLayout(false);
            GameSettingsEnvelopesTab.PerformLayout();
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
        private Label PreShowTitleLbl;
        private Button PreShowIntroBtn;
        private Button PreShowStartGameBtn;
        private Button PreShowQuestionEditorBtn;
        private Button PreShowEnvelopeSettingsBtn;
        private Button PreShowGameSettingsBtn;
        private Button EnvelopeSettingsRandomiseBtn;
        private Button EnvelopeSettingsSaveToFileBtn;
        private Button EnvelopeSettingsSaveBtn;
        private Button EnvelopeSettingsLoadFromFileBtn;
        private Button EnvelopeSettingsNewBtn;
        private ListBox EnvelopeSettingsListBox;
        private Label EnvelopeSettingsCountLbl;
        private Button EnvelopeSettingsMoveDownBtn;
        private Button EnvelopeSettingsMoveUpBtn;
        private Button EnvelopeSettingsDeleteBtn;
        private Label EnvelopeSettingsHintLbl;
        private TextBox EnvelopeSettingsCashTxtBox;
        private RadioButton EnvelopeSettingsCashRadio;
        private TextBox EnvelopeSettingsPercentageTxtBox;
        private RadioButton EnvelopeSettingsPercentageRadio;
        private OpenFileDialog EnvelopeSettingsOpen;
        private SaveFileDialog EnvelopeSettingsSave;
    }
}
