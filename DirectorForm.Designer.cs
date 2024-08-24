﻿namespace CluelessControl
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
            PreShowWarningLbl = new Label();
            PreShowQuestionEditorBtn = new Button();
            PreShowEnvelopeSettingsBtn = new Button();
            PreShowGameSettingsBtn = new Button();
            PreShowStartGameBtn = new Button();
            PreShowIntroBtn = new Button();
            PreShowTitleLbl = new Label();
            GameSettingsTab = new TabPage();
            SettingsEnvelopeStartCountGroup = new GroupBox();
            SettingsEnvelopeStartCountNumeric = new NumericUpDown();
            SettingsEnvelopeStartCountLbl = new Label();
            SettingsRoundingGroup = new GroupBox();
            SettingsDecimalPlacesLbl = new Label();
            SettingsDecimalPlacesTxtBox = new TextBox();
            SettingsSaveToFileBtn = new Button();
            SettingsLoadFromFileBtn = new Button();
            SettingsSaveToMemoryBtn = new Button();
            SettingsLoadFromMemoryBtn = new Button();
            SettingsPlusPercentGroup = new GroupBox();
            SettingsPlusPercentBestRadio = new RadioButton();
            SettingsPlusPercentAllRadio = new RadioButton();
            SettingsMinusPercentGroup = new GroupBox();
            SettingsMinusPercentWorstRadio = new RadioButton();
            SettingsMinusPercentAllRadio = new RadioButton();
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
            QuestionEditorCommentTxtBox = new TextBox();
            QuestionEditorCommentLbl = new Label();
            QuestionEditorAnsDTxtBox = new TextBox();
            QuestionEditorAnsDRadio = new RadioButton();
            QuestionEditorAnsCTxtBox = new TextBox();
            QuestionEditorAnsCRadio = new RadioButton();
            QuestionEditorAnsBTxtBox = new TextBox();
            QuestionEditorAnsBRadio = new RadioButton();
            QuestionEditorAnsATxtBox = new TextBox();
            QuestionEditorAnsARadio = new RadioButton();
            QuestionEditorTextBox = new TextBox();
            QuestionEditorTextLbl = new Label();
            QuestionEditorMoveDownBtn = new Button();
            QuestionEditorMoveUpBtn = new Button();
            QuestionEditorDeleteBtn = new Button();
            QuestionEditorNewBtn = new Button();
            QuestionEditorListBox = new ListBox();
            QuestionEditorCountLbl = new Label();
            QuestionEditorSaveToFileBtn = new Button();
            QuestionEditorSaveBtn = new Button();
            QuestionEditorLoadFromFileBtn = new Button();
            QuestionEditorClearBtn = new Button();
            GamePickEnvelopesTab = new TabPage();
            GameQuestionsTab = new TabPage();
            GameTradingTab = new TabPage();
            GameOverTab = new TabPage();
            EnvelopeSettingsOpen = new OpenFileDialog();
            EnvelopeSettingsSave = new SaveFileDialog();
            SettingsOpen = new OpenFileDialog();
            SettingsSave = new SaveFileDialog();
            QuestionSetSave = new SaveFileDialog();
            QuestionSetOpen = new OpenFileDialog();
            DirectorTabControl.SuspendLayout();
            BeforeTheShowTab.SuspendLayout();
            GameSettingsTab.SuspendLayout();
            SettingsEnvelopeStartCountGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SettingsEnvelopeStartCountNumeric).BeginInit();
            SettingsRoundingGroup.SuspendLayout();
            SettingsPlusPercentGroup.SuspendLayout();
            SettingsMinusPercentGroup.SuspendLayout();
            GameSettingsEnvelopesTab.SuspendLayout();
            QuestionEditorTab.SuspendLayout();
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
            BeforeTheShowTab.Controls.Add(PreShowWarningLbl);
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
            // PreShowWarningLbl
            // 
            PreShowWarningLbl.Font = new Font("Arial", 18F, FontStyle.Italic, GraphicsUnit.Point, 238);
            PreShowWarningLbl.ForeColor = Color.Red;
            PreShowWarningLbl.Location = new Point(6, 259);
            PreShowWarningLbl.Name = "PreShowWarningLbl";
            PreShowWarningLbl.Size = new Size(1069, 183);
            PreShowWarningLbl.TabIndex = 6;
            PreShowWarningLbl.Text = "Pamiętaj o załadowaniu ustawień gry, ustawień kopert i zestawu pytań przed grą!";
            PreShowWarningLbl.TextAlign = ContentAlignment.MiddleCenter;
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
            GameSettingsTab.Controls.Add(SettingsEnvelopeStartCountGroup);
            GameSettingsTab.Controls.Add(SettingsRoundingGroup);
            GameSettingsTab.Controls.Add(SettingsSaveToFileBtn);
            GameSettingsTab.Controls.Add(SettingsLoadFromFileBtn);
            GameSettingsTab.Controls.Add(SettingsSaveToMemoryBtn);
            GameSettingsTab.Controls.Add(SettingsLoadFromMemoryBtn);
            GameSettingsTab.Controls.Add(SettingsPlusPercentGroup);
            GameSettingsTab.Controls.Add(SettingsMinusPercentGroup);
            GameSettingsTab.Location = new Point(4, 24);
            GameSettingsTab.Name = "GameSettingsTab";
            GameSettingsTab.Size = new Size(1084, 445);
            GameSettingsTab.TabIndex = 6;
            GameSettingsTab.Text = "Ustawienia";
            // 
            // SettingsEnvelopeStartCountGroup
            // 
            SettingsEnvelopeStartCountGroup.Controls.Add(SettingsEnvelopeStartCountNumeric);
            SettingsEnvelopeStartCountGroup.Controls.Add(SettingsEnvelopeStartCountLbl);
            SettingsEnvelopeStartCountGroup.Location = new Point(21, 15);
            SettingsEnvelopeStartCountGroup.Name = "SettingsEnvelopeStartCountGroup";
            SettingsEnvelopeStartCountGroup.Size = new Size(491, 119);
            SettingsEnvelopeStartCountGroup.TabIndex = 25;
            SettingsEnvelopeStartCountGroup.TabStop = false;
            SettingsEnvelopeStartCountGroup.Text = "Ilość kopert";
            // 
            // SettingsEnvelopeStartCountNumeric
            // 
            SettingsEnvelopeStartCountNumeric.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsEnvelopeStartCountNumeric.Location = new Point(353, 50);
            SettingsEnvelopeStartCountNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            SettingsEnvelopeStartCountNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SettingsEnvelopeStartCountNumeric.Name = "SettingsEnvelopeStartCountNumeric";
            SettingsEnvelopeStartCountNumeric.Size = new Size(120, 29);
            SettingsEnvelopeStartCountNumeric.TabIndex = 1;
            SettingsEnvelopeStartCountNumeric.TextAlign = HorizontalAlignment.Center;
            SettingsEnvelopeStartCountNumeric.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // SettingsEnvelopeStartCountLbl
            // 
            SettingsEnvelopeStartCountLbl.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsEnvelopeStartCountLbl.Location = new Point(6, 17);
            SettingsEnvelopeStartCountLbl.Name = "SettingsEnvelopeStartCountLbl";
            SettingsEnvelopeStartCountLbl.Size = new Size(342, 92);
            SettingsEnvelopeStartCountLbl.TabIndex = 0;
            SettingsEnvelopeStartCountLbl.Text = "Ile kopert zawodnik wybiera na początku?";
            SettingsEnvelopeStartCountLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SettingsRoundingGroup
            // 
            SettingsRoundingGroup.Controls.Add(SettingsDecimalPlacesLbl);
            SettingsRoundingGroup.Controls.Add(SettingsDecimalPlacesTxtBox);
            SettingsRoundingGroup.Location = new Point(569, 15);
            SettingsRoundingGroup.Name = "SettingsRoundingGroup";
            SettingsRoundingGroup.Size = new Size(491, 119);
            SettingsRoundingGroup.TabIndex = 24;
            SettingsRoundingGroup.TabStop = false;
            SettingsRoundingGroup.Text = "Zaokrąglanie";
            // 
            // SettingsDecimalPlacesLbl
            // 
            SettingsDecimalPlacesLbl.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsDecimalPlacesLbl.Location = new Point(6, 17);
            SettingsDecimalPlacesLbl.Name = "SettingsDecimalPlacesLbl";
            SettingsDecimalPlacesLbl.Size = new Size(479, 47);
            SettingsDecimalPlacesLbl.TabIndex = 0;
            SettingsDecimalPlacesLbl.Text = "Do ilu miejsc po przecinku zaokrąglać nagrody (ujemna liczba = przed przecinkiem) ?";
            SettingsDecimalPlacesLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SettingsDecimalPlacesTxtBox
            // 
            SettingsDecimalPlacesTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsDecimalPlacesTxtBox.Location = new Point(6, 78);
            SettingsDecimalPlacesTxtBox.Name = "SettingsDecimalPlacesTxtBox";
            SettingsDecimalPlacesTxtBox.Size = new Size(479, 35);
            SettingsDecimalPlacesTxtBox.TabIndex = 1;
            SettingsDecimalPlacesTxtBox.Text = "0";
            SettingsDecimalPlacesTxtBox.TextAlign = HorizontalAlignment.Center;
            SettingsDecimalPlacesTxtBox.TextChanged += SettingsDecimalPlacesTxtBox_TextChanged;
            // 
            // SettingsSaveToFileBtn
            // 
            SettingsSaveToFileBtn.Font = new Font("Arial", 14.25F);
            SettingsSaveToFileBtn.Location = new Point(835, 354);
            SettingsSaveToFileBtn.Name = "SettingsSaveToFileBtn";
            SettingsSaveToFileBtn.Size = new Size(225, 75);
            SettingsSaveToFileBtn.TabIndex = 23;
            SettingsSaveToFileBtn.Text = "Zapisz do pliku";
            SettingsSaveToFileBtn.UseVisualStyleBackColor = true;
            SettingsSaveToFileBtn.Click += SettingsSaveToFileBtn_Click;
            // 
            // SettingsLoadFromFileBtn
            // 
            SettingsLoadFromFileBtn.Font = new Font("Arial", 14.25F);
            SettingsLoadFromFileBtn.Location = new Point(604, 354);
            SettingsLoadFromFileBtn.Name = "SettingsLoadFromFileBtn";
            SettingsLoadFromFileBtn.Size = new Size(225, 75);
            SettingsLoadFromFileBtn.TabIndex = 22;
            SettingsLoadFromFileBtn.Text = "Wczytaj z pliku";
            SettingsLoadFromFileBtn.UseVisualStyleBackColor = true;
            SettingsLoadFromFileBtn.Click += SettingsLoadFromFileBtn_Click;
            // 
            // SettingsSaveToMemoryBtn
            // 
            SettingsSaveToMemoryBtn.Font = new Font("Arial", 14.25F);
            SettingsSaveToMemoryBtn.Location = new Point(252, 354);
            SettingsSaveToMemoryBtn.Name = "SettingsSaveToMemoryBtn";
            SettingsSaveToMemoryBtn.Size = new Size(225, 75);
            SettingsSaveToMemoryBtn.TabIndex = 21;
            SettingsSaveToMemoryBtn.Text = "Zapisz do pamięci";
            SettingsSaveToMemoryBtn.UseVisualStyleBackColor = true;
            SettingsSaveToMemoryBtn.Click += SettingsSaveToMemoryBtn_Click;
            // 
            // SettingsLoadFromMemoryBtn
            // 
            SettingsLoadFromMemoryBtn.Font = new Font("Arial", 14.25F);
            SettingsLoadFromMemoryBtn.Location = new Point(21, 354);
            SettingsLoadFromMemoryBtn.Name = "SettingsLoadFromMemoryBtn";
            SettingsLoadFromMemoryBtn.Size = new Size(225, 75);
            SettingsLoadFromMemoryBtn.TabIndex = 20;
            SettingsLoadFromMemoryBtn.Text = "Wczytaj z pamięci";
            SettingsLoadFromMemoryBtn.UseVisualStyleBackColor = true;
            SettingsLoadFromMemoryBtn.Click += SettingsLoadFromMemoryBtn_Click;
            // 
            // SettingsPlusPercentGroup
            // 
            SettingsPlusPercentGroup.Controls.Add(SettingsPlusPercentBestRadio);
            SettingsPlusPercentGroup.Controls.Add(SettingsPlusPercentAllRadio);
            SettingsPlusPercentGroup.Location = new Point(569, 140);
            SettingsPlusPercentGroup.Name = "SettingsPlusPercentGroup";
            SettingsPlusPercentGroup.Size = new Size(491, 119);
            SettingsPlusPercentGroup.TabIndex = 3;
            SettingsPlusPercentGroup.TabStop = false;
            SettingsPlusPercentGroup.Text = "Plusy procentowe";
            // 
            // SettingsPlusPercentBestRadio
            // 
            SettingsPlusPercentBestRadio.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsPlusPercentBestRadio.Location = new Point(25, 63);
            SettingsPlusPercentBestRadio.Name = "SettingsPlusPercentBestRadio";
            SettingsPlusPercentBestRadio.Size = new Size(448, 37);
            SettingsPlusPercentBestRadio.TabIndex = 6;
            SettingsPlusPercentBestRadio.TabStop = true;
            SettingsPlusPercentBestRadio.Text = "Liczy się jeden, najlepszy plus";
            SettingsPlusPercentBestRadio.UseVisualStyleBackColor = true;
            // 
            // SettingsPlusPercentAllRadio
            // 
            SettingsPlusPercentAllRadio.Checked = true;
            SettingsPlusPercentAllRadio.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsPlusPercentAllRadio.Location = new Point(25, 20);
            SettingsPlusPercentAllRadio.Name = "SettingsPlusPercentAllRadio";
            SettingsPlusPercentAllRadio.Size = new Size(448, 37);
            SettingsPlusPercentAllRadio.TabIndex = 5;
            SettingsPlusPercentAllRadio.TabStop = true;
            SettingsPlusPercentAllRadio.Text = "Liczą się wszystkie plusy";
            SettingsPlusPercentAllRadio.UseVisualStyleBackColor = true;
            // 
            // SettingsMinusPercentGroup
            // 
            SettingsMinusPercentGroup.Controls.Add(SettingsMinusPercentWorstRadio);
            SettingsMinusPercentGroup.Controls.Add(SettingsMinusPercentAllRadio);
            SettingsMinusPercentGroup.Location = new Point(21, 140);
            SettingsMinusPercentGroup.Name = "SettingsMinusPercentGroup";
            SettingsMinusPercentGroup.Size = new Size(491, 119);
            SettingsMinusPercentGroup.TabIndex = 2;
            SettingsMinusPercentGroup.TabStop = false;
            SettingsMinusPercentGroup.Text = "Minusy procentowe";
            // 
            // SettingsMinusPercentWorstRadio
            // 
            SettingsMinusPercentWorstRadio.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsMinusPercentWorstRadio.Location = new Point(25, 63);
            SettingsMinusPercentWorstRadio.Name = "SettingsMinusPercentWorstRadio";
            SettingsMinusPercentWorstRadio.Size = new Size(448, 37);
            SettingsMinusPercentWorstRadio.TabIndex = 4;
            SettingsMinusPercentWorstRadio.TabStop = true;
            SettingsMinusPercentWorstRadio.Text = "Liczy się jeden, najgorszy minus";
            SettingsMinusPercentWorstRadio.UseVisualStyleBackColor = true;
            // 
            // SettingsMinusPercentAllRadio
            // 
            SettingsMinusPercentAllRadio.Checked = true;
            SettingsMinusPercentAllRadio.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            SettingsMinusPercentAllRadio.Location = new Point(25, 20);
            SettingsMinusPercentAllRadio.Name = "SettingsMinusPercentAllRadio";
            SettingsMinusPercentAllRadio.Size = new Size(448, 37);
            SettingsMinusPercentAllRadio.TabIndex = 3;
            SettingsMinusPercentAllRadio.TabStop = true;
            SettingsMinusPercentAllRadio.Text = "Liczą się wszystkie minusy";
            SettingsMinusPercentAllRadio.UseVisualStyleBackColor = true;
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
            QuestionEditorTab.Controls.Add(QuestionEditorCommentTxtBox);
            QuestionEditorTab.Controls.Add(QuestionEditorCommentLbl);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsDTxtBox);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsDRadio);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsCTxtBox);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsCRadio);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsBTxtBox);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsBRadio);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsATxtBox);
            QuestionEditorTab.Controls.Add(QuestionEditorAnsARadio);
            QuestionEditorTab.Controls.Add(QuestionEditorTextBox);
            QuestionEditorTab.Controls.Add(QuestionEditorTextLbl);
            QuestionEditorTab.Controls.Add(QuestionEditorMoveDownBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorMoveUpBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorDeleteBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorNewBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorListBox);
            QuestionEditorTab.Controls.Add(QuestionEditorCountLbl);
            QuestionEditorTab.Controls.Add(QuestionEditorSaveToFileBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorSaveBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorLoadFromFileBtn);
            QuestionEditorTab.Controls.Add(QuestionEditorClearBtn);
            QuestionEditorTab.Location = new Point(4, 24);
            QuestionEditorTab.Name = "QuestionEditorTab";
            QuestionEditorTab.Size = new Size(1084, 445);
            QuestionEditorTab.TabIndex = 7;
            QuestionEditorTab.Text = "Edytor pytań";
            // 
            // QuestionEditorCommentTxtBox
            // 
            QuestionEditorCommentTxtBox.Enabled = false;
            QuestionEditorCommentTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorCommentTxtBox.Location = new Point(286, 362);
            QuestionEditorCommentTxtBox.Multiline = true;
            QuestionEditorCommentTxtBox.Name = "QuestionEditorCommentTxtBox";
            QuestionEditorCommentTxtBox.Size = new Size(776, 70);
            QuestionEditorCommentTxtBox.TabIndex = 21;
            QuestionEditorCommentTxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuestionEditorCommentLbl
            // 
            QuestionEditorCommentLbl.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorCommentLbl.Location = new Point(286, 320);
            QuestionEditorCommentLbl.Name = "QuestionEditorCommentLbl";
            QuestionEditorCommentLbl.Size = new Size(776, 35);
            QuestionEditorCommentLbl.TabIndex = 20;
            QuestionEditorCommentLbl.Text = "Komentarz:";
            QuestionEditorCommentLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // QuestionEditorAnsDTxtBox
            // 
            QuestionEditorAnsDTxtBox.Enabled = false;
            QuestionEditorAnsDTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsDTxtBox.Location = new Point(346, 274);
            QuestionEditorAnsDTxtBox.Name = "QuestionEditorAnsDTxtBox";
            QuestionEditorAnsDTxtBox.Size = new Size(716, 35);
            QuestionEditorAnsDTxtBox.TabIndex = 19;
            QuestionEditorAnsDTxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuestionEditorAnsDRadio
            // 
            QuestionEditorAnsDRadio.AutoSize = true;
            QuestionEditorAnsDRadio.Enabled = false;
            QuestionEditorAnsDRadio.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsDRadio.Location = new Point(286, 274);
            QuestionEditorAnsDRadio.Name = "QuestionEditorAnsDRadio";
            QuestionEditorAnsDRadio.Size = new Size(54, 31);
            QuestionEditorAnsDRadio.TabIndex = 18;
            QuestionEditorAnsDRadio.TabStop = true;
            QuestionEditorAnsDRadio.Text = "D:";
            QuestionEditorAnsDRadio.UseVisualStyleBackColor = true;
            // 
            // QuestionEditorAnsCTxtBox
            // 
            QuestionEditorAnsCTxtBox.Enabled = false;
            QuestionEditorAnsCTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsCTxtBox.Location = new Point(346, 227);
            QuestionEditorAnsCTxtBox.Name = "QuestionEditorAnsCTxtBox";
            QuestionEditorAnsCTxtBox.Size = new Size(716, 35);
            QuestionEditorAnsCTxtBox.TabIndex = 17;
            QuestionEditorAnsCTxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuestionEditorAnsCRadio
            // 
            QuestionEditorAnsCRadio.AutoSize = true;
            QuestionEditorAnsCRadio.Enabled = false;
            QuestionEditorAnsCRadio.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsCRadio.Location = new Point(286, 227);
            QuestionEditorAnsCRadio.Name = "QuestionEditorAnsCRadio";
            QuestionEditorAnsCRadio.Size = new Size(54, 31);
            QuestionEditorAnsCRadio.TabIndex = 16;
            QuestionEditorAnsCRadio.TabStop = true;
            QuestionEditorAnsCRadio.Text = "C:";
            QuestionEditorAnsCRadio.UseVisualStyleBackColor = true;
            // 
            // QuestionEditorAnsBTxtBox
            // 
            QuestionEditorAnsBTxtBox.Enabled = false;
            QuestionEditorAnsBTxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsBTxtBox.Location = new Point(345, 180);
            QuestionEditorAnsBTxtBox.Name = "QuestionEditorAnsBTxtBox";
            QuestionEditorAnsBTxtBox.Size = new Size(717, 35);
            QuestionEditorAnsBTxtBox.TabIndex = 15;
            QuestionEditorAnsBTxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuestionEditorAnsBRadio
            // 
            QuestionEditorAnsBRadio.AutoSize = true;
            QuestionEditorAnsBRadio.Enabled = false;
            QuestionEditorAnsBRadio.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsBRadio.Location = new Point(286, 180);
            QuestionEditorAnsBRadio.Name = "QuestionEditorAnsBRadio";
            QuestionEditorAnsBRadio.Size = new Size(53, 31);
            QuestionEditorAnsBRadio.TabIndex = 14;
            QuestionEditorAnsBRadio.TabStop = true;
            QuestionEditorAnsBRadio.Text = "B:";
            QuestionEditorAnsBRadio.UseVisualStyleBackColor = true;
            // 
            // QuestionEditorAnsATxtBox
            // 
            QuestionEditorAnsATxtBox.Enabled = false;
            QuestionEditorAnsATxtBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsATxtBox.Location = new Point(344, 133);
            QuestionEditorAnsATxtBox.Name = "QuestionEditorAnsATxtBox";
            QuestionEditorAnsATxtBox.Size = new Size(718, 35);
            QuestionEditorAnsATxtBox.TabIndex = 13;
            QuestionEditorAnsATxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuestionEditorAnsARadio
            // 
            QuestionEditorAnsARadio.AutoSize = true;
            QuestionEditorAnsARadio.Enabled = false;
            QuestionEditorAnsARadio.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorAnsARadio.Location = new Point(286, 133);
            QuestionEditorAnsARadio.Name = "QuestionEditorAnsARadio";
            QuestionEditorAnsARadio.Size = new Size(52, 31);
            QuestionEditorAnsARadio.TabIndex = 12;
            QuestionEditorAnsARadio.TabStop = true;
            QuestionEditorAnsARadio.Text = "A:";
            QuestionEditorAnsARadio.UseVisualStyleBackColor = true;
            // 
            // QuestionEditorTextBox
            // 
            QuestionEditorTextBox.Enabled = false;
            QuestionEditorTextBox.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorTextBox.Location = new Point(286, 50);
            QuestionEditorTextBox.Multiline = true;
            QuestionEditorTextBox.Name = "QuestionEditorTextBox";
            QuestionEditorTextBox.Size = new Size(776, 70);
            QuestionEditorTextBox.TabIndex = 11;
            QuestionEditorTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // QuestionEditorTextLbl
            // 
            QuestionEditorTextLbl.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            QuestionEditorTextLbl.Location = new Point(286, 12);
            QuestionEditorTextLbl.Name = "QuestionEditorTextLbl";
            QuestionEditorTextLbl.Size = new Size(776, 35);
            QuestionEditorTextLbl.TabIndex = 10;
            QuestionEditorTextLbl.Text = "Pytanie:";
            QuestionEditorTextLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // QuestionEditorMoveDownBtn
            // 
            QuestionEditorMoveDownBtn.Enabled = false;
            QuestionEditorMoveDownBtn.Font = new Font("Arial", 12F);
            QuestionEditorMoveDownBtn.Location = new Point(152, 214);
            QuestionEditorMoveDownBtn.Name = "QuestionEditorMoveDownBtn";
            QuestionEditorMoveDownBtn.Size = new Size(128, 50);
            QuestionEditorMoveDownBtn.TabIndex = 3;
            QuestionEditorMoveDownBtn.Text = "W dół";
            QuestionEditorMoveDownBtn.UseVisualStyleBackColor = true;
            QuestionEditorMoveDownBtn.Click += QuestionEditorMoveDownBtn_Click;
            // 
            // QuestionEditorMoveUpBtn
            // 
            QuestionEditorMoveUpBtn.Enabled = false;
            QuestionEditorMoveUpBtn.Font = new Font("Arial", 12F);
            QuestionEditorMoveUpBtn.Location = new Point(12, 214);
            QuestionEditorMoveUpBtn.Name = "QuestionEditorMoveUpBtn";
            QuestionEditorMoveUpBtn.Size = new Size(128, 50);
            QuestionEditorMoveUpBtn.TabIndex = 2;
            QuestionEditorMoveUpBtn.Text = "W górę";
            QuestionEditorMoveUpBtn.UseVisualStyleBackColor = true;
            QuestionEditorMoveUpBtn.Click += QuestionEditorMoveUpBtn_Click;
            // 
            // QuestionEditorDeleteBtn
            // 
            QuestionEditorDeleteBtn.Enabled = false;
            QuestionEditorDeleteBtn.Font = new Font("Arial", 12F);
            QuestionEditorDeleteBtn.Location = new Point(152, 270);
            QuestionEditorDeleteBtn.Name = "QuestionEditorDeleteBtn";
            QuestionEditorDeleteBtn.Size = new Size(128, 50);
            QuestionEditorDeleteBtn.TabIndex = 5;
            QuestionEditorDeleteBtn.Text = "Usuń pytanie";
            QuestionEditorDeleteBtn.UseVisualStyleBackColor = true;
            QuestionEditorDeleteBtn.Click += QuestionEditorDeleteBtn_Click;
            // 
            // QuestionEditorNewBtn
            // 
            QuestionEditorNewBtn.Font = new Font("Arial", 12F);
            QuestionEditorNewBtn.Location = new Point(12, 270);
            QuestionEditorNewBtn.Name = "QuestionEditorNewBtn";
            QuestionEditorNewBtn.Size = new Size(128, 50);
            QuestionEditorNewBtn.TabIndex = 4;
            QuestionEditorNewBtn.Text = "Nowe pytanie";
            QuestionEditorNewBtn.UseVisualStyleBackColor = true;
            QuestionEditorNewBtn.Click += QuestionEditorNewBtn_Click;
            // 
            // QuestionEditorListBox
            // 
            QuestionEditorListBox.Font = new Font("Arial", 14.25F);
            QuestionEditorListBox.FormattingEnabled = true;
            QuestionEditorListBox.ItemHeight = 22;
            QuestionEditorListBox.Location = new Point(12, 50);
            QuestionEditorListBox.Name = "QuestionEditorListBox";
            QuestionEditorListBox.Size = new Size(268, 158);
            QuestionEditorListBox.TabIndex = 1;
            QuestionEditorListBox.SelectedIndexChanged += QuestionEditorListBox_SelectedIndexChanged;
            // 
            // QuestionEditorCountLbl
            // 
            QuestionEditorCountLbl.Font = new Font("Arial", 14.25F);
            QuestionEditorCountLbl.Location = new Point(12, 12);
            QuestionEditorCountLbl.Name = "QuestionEditorCountLbl";
            QuestionEditorCountLbl.Size = new Size(268, 35);
            QuestionEditorCountLbl.TabIndex = 0;
            QuestionEditorCountLbl.Text = "Pytań: 0";
            QuestionEditorCountLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // QuestionEditorSaveToFileBtn
            // 
            QuestionEditorSaveToFileBtn.Enabled = false;
            QuestionEditorSaveToFileBtn.Font = new Font("Arial", 12F);
            QuestionEditorSaveToFileBtn.Location = new Point(152, 382);
            QuestionEditorSaveToFileBtn.Name = "QuestionEditorSaveToFileBtn";
            QuestionEditorSaveToFileBtn.Size = new Size(128, 50);
            QuestionEditorSaveToFileBtn.TabIndex = 9;
            QuestionEditorSaveToFileBtn.Text = "Zapisz zestaw do pliku";
            QuestionEditorSaveToFileBtn.UseVisualStyleBackColor = true;
            QuestionEditorSaveToFileBtn.Click += QuestionEditorSaveToFileBtn_Click;
            // 
            // QuestionEditorSaveBtn
            // 
            QuestionEditorSaveBtn.Enabled = false;
            QuestionEditorSaveBtn.Font = new Font("Arial", 12F);
            QuestionEditorSaveBtn.Location = new Point(152, 326);
            QuestionEditorSaveBtn.Name = "QuestionEditorSaveBtn";
            QuestionEditorSaveBtn.Size = new Size(128, 50);
            QuestionEditorSaveBtn.TabIndex = 7;
            QuestionEditorSaveBtn.Text = "Zapisz zestaw";
            QuestionEditorSaveBtn.UseVisualStyleBackColor = true;
            QuestionEditorSaveBtn.Click += QuestionEditorSaveBtn_Click;
            // 
            // QuestionEditorLoadFromFileBtn
            // 
            QuestionEditorLoadFromFileBtn.Font = new Font("Arial", 12F);
            QuestionEditorLoadFromFileBtn.Location = new Point(12, 382);
            QuestionEditorLoadFromFileBtn.Name = "QuestionEditorLoadFromFileBtn";
            QuestionEditorLoadFromFileBtn.Size = new Size(128, 50);
            QuestionEditorLoadFromFileBtn.TabIndex = 8;
            QuestionEditorLoadFromFileBtn.Text = "Wczytaj z pliku";
            QuestionEditorLoadFromFileBtn.UseVisualStyleBackColor = true;
            QuestionEditorLoadFromFileBtn.Click += QuestionEditorLoadFromFileBtn_Click;
            // 
            // QuestionEditorClearBtn
            // 
            QuestionEditorClearBtn.Enabled = false;
            QuestionEditorClearBtn.Font = new Font("Arial", 12F);
            QuestionEditorClearBtn.Location = new Point(12, 326);
            QuestionEditorClearBtn.Name = "QuestionEditorClearBtn";
            QuestionEditorClearBtn.Size = new Size(128, 50);
            QuestionEditorClearBtn.TabIndex = 6;
            QuestionEditorClearBtn.Text = "Czyść zestaw pytań";
            QuestionEditorClearBtn.UseVisualStyleBackColor = true;
            QuestionEditorClearBtn.Click += QuestionEditorClearBtn_Click;
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
            // SettingsOpen
            // 
            SettingsOpen.Filter = "Pliki *.json|*.json";
            SettingsOpen.Title = "Ładowanie ustawień";
            // 
            // SettingsSave
            // 
            SettingsSave.Filter = "Pliki *.json|*.json";
            SettingsSave.Title = "Zapisywanie ustawień";
            // 
            // QuestionSetSave
            // 
            QuestionSetSave.Filter = "Pliki *.json|*.json";
            QuestionSetSave.Title = "Zapisywanie zestawu pytań";
            // 
            // QuestionSetOpen
            // 
            QuestionSetOpen.Filter = "Pliki *.json|*.json";
            QuestionSetOpen.Title = "Wczytywanie zestawu pytań";
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
            FormClosing += DirectorForm_FormClosing;
            Load += DirectorForm_Load;
            DirectorTabControl.ResumeLayout(false);
            BeforeTheShowTab.ResumeLayout(false);
            GameSettingsTab.ResumeLayout(false);
            SettingsEnvelopeStartCountGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SettingsEnvelopeStartCountNumeric).EndInit();
            SettingsRoundingGroup.ResumeLayout(false);
            SettingsRoundingGroup.PerformLayout();
            SettingsPlusPercentGroup.ResumeLayout(false);
            SettingsMinusPercentGroup.ResumeLayout(false);
            GameSettingsEnvelopesTab.ResumeLayout(false);
            GameSettingsEnvelopesTab.PerformLayout();
            QuestionEditorTab.ResumeLayout(false);
            QuestionEditorTab.PerformLayout();
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
        private Label SettingsDecimalPlacesLbl;
        private TextBox SettingsDecimalPlacesTxtBox;
        private GroupBox SettingsPlusPercentGroup;
        private RadioButton SettingsPlusPercentBestRadio;
        private RadioButton SettingsPlusPercentAllRadio;
        private GroupBox SettingsMinusPercentGroup;
        private RadioButton SettingsMinusPercentWorstRadio;
        private RadioButton SettingsMinusPercentAllRadio;
        private Button SettingsSaveToFileBtn;
        private Button SettingsLoadFromFileBtn;
        private Button SettingsSaveToMemoryBtn;
        private Button SettingsLoadFromMemoryBtn;
        private Label PreShowWarningLbl;
        private OpenFileDialog SettingsOpen;
        private SaveFileDialog SettingsSave;
        private TextBox QuestionEditorTextBox;
        private Label QuestionEditorTextLbl;
        private Button QuestionEditorMoveDownBtn;
        private Button QuestionEditorMoveUpBtn;
        private Button QuestionEditorDeleteBtn;
        private Button QuestionEditorNewBtn;
        private ListBox QuestionEditorListBox;
        private Label QuestionEditorCountLbl;
        private Button QuestionEditorSaveToFileBtn;
        private Button QuestionEditorSaveBtn;
        private Button QuestionEditorLoadFromFileBtn;
        private Button QuestionEditorClearBtn;
        private TextBox QuestionEditorAnsDTxtBox;
        private RadioButton QuestionEditorAnsDRadio;
        private TextBox QuestionEditorAnsCTxtBox;
        private RadioButton QuestionEditorAnsCRadio;
        private TextBox QuestionEditorAnsBTxtBox;
        private RadioButton QuestionEditorAnsBRadio;
        private TextBox QuestionEditorAnsATxtBox;
        private RadioButton QuestionEditorAnsARadio;
        private TextBox QuestionEditorCommentTxtBox;
        private Label QuestionEditorCommentLbl;
        private SaveFileDialog QuestionSetSave;
        private OpenFileDialog QuestionSetOpen;
        private GroupBox SettingsRoundingGroup;
        private GroupBox SettingsEnvelopeStartCountGroup;
        private NumericUpDown SettingsEnvelopeStartCountNumeric;
        private Label SettingsEnvelopeStartCountLbl;
    }
}
