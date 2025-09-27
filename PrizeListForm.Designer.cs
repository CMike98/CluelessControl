namespace CluelessControl
{
    partial class PrizeListForm
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
            PrizeListSelectListBox = new ListBox();
            PrizeListPrizeCountLbl = new Label();
            PrizeListCodeLbl = new Label();
            PrizeListCodeTxtBox = new TextBox();
            PrizeListNameTxtBox = new TextBox();
            PrizeListNameLbl = new Label();
            PrizeListRoundingUnitTxtBox = new TextBox();
            PrizeListRoundingUnitLbl = new Label();
            PrizeListRoundingMethodLbl = new Label();
            PrizeListRoundingMethodComboBox = new ComboBox();
            PrizeListAddBtn = new Button();
            PrizeListRemoveBtn = new Button();
            PrizeListRetrieveBtn = new Button();
            PrizeListSaveBtn = new Button();
            SuspendLayout();
            // 
            // PrizeListSelectListBox
            // 
            PrizeListSelectListBox.Font = new Font("Arial", 12F);
            PrizeListSelectListBox.FormattingEnabled = true;
            PrizeListSelectListBox.ItemHeight = 18;
            PrizeListSelectListBox.Location = new Point(12, 55);
            PrizeListSelectListBox.Name = "PrizeListSelectListBox";
            PrizeListSelectListBox.Size = new Size(204, 220);
            PrizeListSelectListBox.TabIndex = 1;
            PrizeListSelectListBox.SelectedIndexChanged += PrizeListSelectListBox_SelectedIndexChanged;
            // 
            // PrizeListPrizeCountLbl
            // 
            PrizeListPrizeCountLbl.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            PrizeListPrizeCountLbl.Location = new Point(12, 9);
            PrizeListPrizeCountLbl.Name = "PrizeListPrizeCountLbl";
            PrizeListPrizeCountLbl.Size = new Size(204, 43);
            PrizeListPrizeCountLbl.TabIndex = 0;
            PrizeListPrizeCountLbl.Text = "Nagród: 0";
            PrizeListPrizeCountLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PrizeListCodeLbl
            // 
            PrizeListCodeLbl.Location = new Point(222, 9);
            PrizeListCodeLbl.Name = "PrizeListCodeLbl";
            PrizeListCodeLbl.Size = new Size(361, 34);
            PrizeListCodeLbl.TabIndex = 10;
            PrizeListCodeLbl.Text = "Kod nagrody (dla systemu):";
            PrizeListCodeLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PrizeListCodeTxtBox
            // 
            PrizeListCodeTxtBox.CharacterCasing = CharacterCasing.Upper;
            PrizeListCodeTxtBox.Enabled = false;
            PrizeListCodeTxtBox.Location = new Point(222, 46);
            PrizeListCodeTxtBox.Name = "PrizeListCodeTxtBox";
            PrizeListCodeTxtBox.Size = new Size(361, 26);
            PrizeListCodeTxtBox.TabIndex = 11;
            PrizeListCodeTxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PrizeListNameTxtBox
            // 
            PrizeListNameTxtBox.CharacterCasing = CharacterCasing.Upper;
            PrizeListNameTxtBox.Enabled = false;
            PrizeListNameTxtBox.Location = new Point(222, 112);
            PrizeListNameTxtBox.Name = "PrizeListNameTxtBox";
            PrizeListNameTxtBox.Size = new Size(361, 26);
            PrizeListNameTxtBox.TabIndex = 13;
            PrizeListNameTxtBox.TextAlign = HorizontalAlignment.Center;
            // 
            // PrizeListNameLbl
            // 
            PrizeListNameLbl.Location = new Point(222, 75);
            PrizeListNameLbl.Name = "PrizeListNameLbl";
            PrizeListNameLbl.Size = new Size(361, 34);
            PrizeListNameLbl.TabIndex = 12;
            PrizeListNameLbl.Text = "Nazwa nagrody (wyświetlana w kopercie):";
            PrizeListNameLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PrizeListRoundingUnitTxtBox
            // 
            PrizeListRoundingUnitTxtBox.CharacterCasing = CharacterCasing.Upper;
            PrizeListRoundingUnitTxtBox.Enabled = false;
            PrizeListRoundingUnitTxtBox.Location = new Point(222, 178);
            PrizeListRoundingUnitTxtBox.Name = "PrizeListRoundingUnitTxtBox";
            PrizeListRoundingUnitTxtBox.Size = new Size(361, 26);
            PrizeListRoundingUnitTxtBox.TabIndex = 15;
            PrizeListRoundingUnitTxtBox.Text = "1";
            PrizeListRoundingUnitTxtBox.TextAlign = HorizontalAlignment.Center;
            PrizeListRoundingUnitTxtBox.TextChanged += PrizeListRoundingUnitTxtBox_TextChanged;
            // 
            // PrizeListRoundingUnitLbl
            // 
            PrizeListRoundingUnitLbl.Location = new Point(222, 141);
            PrizeListRoundingUnitLbl.Name = "PrizeListRoundingUnitLbl";
            PrizeListRoundingUnitLbl.Size = new Size(361, 34);
            PrizeListRoundingUnitLbl.TabIndex = 14;
            PrizeListRoundingUnitLbl.Text = "Do jakiej liczby zaokrąglać ilość danej nagrody:";
            PrizeListRoundingUnitLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PrizeListRoundingMethodLbl
            // 
            PrizeListRoundingMethodLbl.Location = new Point(222, 207);
            PrizeListRoundingMethodLbl.Name = "PrizeListRoundingMethodLbl";
            PrizeListRoundingMethodLbl.Size = new Size(361, 34);
            PrizeListRoundingMethodLbl.TabIndex = 16;
            PrizeListRoundingMethodLbl.Text = "Zaokrąglenie:";
            PrizeListRoundingMethodLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // PrizeListRoundingMethodComboBox
            // 
            PrizeListRoundingMethodComboBox.Enabled = false;
            PrizeListRoundingMethodComboBox.FormattingEnabled = true;
            PrizeListRoundingMethodComboBox.Items.AddRange(new object[] { "MATEMATYCZNIE", "W DÓŁ", "W GÓRĘ" });
            PrizeListRoundingMethodComboBox.Location = new Point(222, 244);
            PrizeListRoundingMethodComboBox.Name = "PrizeListRoundingMethodComboBox";
            PrizeListRoundingMethodComboBox.Size = new Size(361, 26);
            PrizeListRoundingMethodComboBox.TabIndex = 17;
            // 
            // PrizeListAddBtn
            // 
            PrizeListAddBtn.Location = new Point(12, 287);
            PrizeListAddBtn.Name = "PrizeListAddBtn";
            PrizeListAddBtn.Size = new Size(98, 48);
            PrizeListAddBtn.TabIndex = 18;
            PrizeListAddBtn.Text = "Dodaj";
            PrizeListAddBtn.UseVisualStyleBackColor = true;
            PrizeListAddBtn.Click += PrizeListAddBtn_Click;
            // 
            // PrizeListRemoveBtn
            // 
            PrizeListRemoveBtn.Location = new Point(116, 287);
            PrizeListRemoveBtn.Name = "PrizeListRemoveBtn";
            PrizeListRemoveBtn.Size = new Size(100, 48);
            PrizeListRemoveBtn.TabIndex = 19;
            PrizeListRemoveBtn.Text = "Usuń";
            PrizeListRemoveBtn.UseVisualStyleBackColor = true;
            PrizeListRemoveBtn.Click += PrizeListRemoveBtn_Click;
            // 
            // PrizeListRetrieveBtn
            // 
            PrizeListRetrieveBtn.Enabled = false;
            PrizeListRetrieveBtn.Location = new Point(398, 287);
            PrizeListRetrieveBtn.Name = "PrizeListRetrieveBtn";
            PrizeListRetrieveBtn.Size = new Size(100, 48);
            PrizeListRetrieveBtn.TabIndex = 21;
            PrizeListRetrieveBtn.Text = "Przywróć";
            PrizeListRetrieveBtn.UseVisualStyleBackColor = true;
            PrizeListRetrieveBtn.Click += PrizeListRetrieveBtn_Click;
            // 
            // PrizeListSaveBtn
            // 
            PrizeListSaveBtn.Enabled = false;
            PrizeListSaveBtn.Location = new Point(294, 287);
            PrizeListSaveBtn.Name = "PrizeListSaveBtn";
            PrizeListSaveBtn.Size = new Size(98, 48);
            PrizeListSaveBtn.TabIndex = 20;
            PrizeListSaveBtn.Text = "Zapisz";
            PrizeListSaveBtn.UseVisualStyleBackColor = true;
            PrizeListSaveBtn.Click += PrizeListSaveBtn_Click;
            // 
            // PrizeListForm
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 343);
            Controls.Add(PrizeListRetrieveBtn);
            Controls.Add(PrizeListSaveBtn);
            Controls.Add(PrizeListRemoveBtn);
            Controls.Add(PrizeListAddBtn);
            Controls.Add(PrizeListRoundingMethodComboBox);
            Controls.Add(PrizeListRoundingMethodLbl);
            Controls.Add(PrizeListRoundingUnitTxtBox);
            Controls.Add(PrizeListRoundingUnitLbl);
            Controls.Add(PrizeListNameTxtBox);
            Controls.Add(PrizeListNameLbl);
            Controls.Add(PrizeListCodeTxtBox);
            Controls.Add(PrizeListCodeLbl);
            Controls.Add(PrizeListSelectListBox);
            Controls.Add(PrizeListPrizeCountLbl);
            Font = new Font("Arial", 12F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PrizeListForm";
            Text = "Lista nagród";
            Load += PrizeListForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox PrizeListSelectListBox;
        private Label PrizeListPrizeCountLbl;
        private Label PrizeListCodeLbl;
        private TextBox PrizeListCodeTxtBox;
        private TextBox PrizeListNameTxtBox;
        private Label PrizeListNameLbl;
        private TextBox PrizeListRoundingUnitTxtBox;
        private Label PrizeListRoundingUnitLbl;
        private Label PrizeListRoundingMethodLbl;
        private ComboBox PrizeListRoundingMethodComboBox;
        private Button PrizeListAddBtn;
        private Button PrizeListRemoveBtn;
        private Button PrizeListRetrieveBtn;
        private Button PrizeListSaveBtn;
    }
}