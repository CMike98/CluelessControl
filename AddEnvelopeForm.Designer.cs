namespace CluelessControl
{
    partial class AddEnvelopeForm
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
            AddEnvelopeChooseEnvelopeLbl = new Label();
            AddEnvelopeSelectListBox = new ListBox();
            AddEnvelopeSelectedEnvelopeLbl = new Label();
            AddEnvelopeSelectedTitleLbl = new Label();
            AddEnvelopeConfirmBtn = new Button();
            SuspendLayout();
            // 
            // AddEnvelopeChooseEnvelopeLbl
            // 
            AddEnvelopeChooseEnvelopeLbl.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            AddEnvelopeChooseEnvelopeLbl.Location = new Point(12, 9);
            AddEnvelopeChooseEnvelopeLbl.Name = "AddEnvelopeChooseEnvelopeLbl";
            AddEnvelopeChooseEnvelopeLbl.Size = new Size(204, 43);
            AddEnvelopeChooseEnvelopeLbl.TabIndex = 0;
            AddEnvelopeChooseEnvelopeLbl.Text = "Wybierz kopertę:";
            AddEnvelopeChooseEnvelopeLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AddEnvelopeSelectListBox
            // 
            AddEnvelopeSelectListBox.Font = new Font("Arial", 14.25F);
            AddEnvelopeSelectListBox.FormattingEnabled = true;
            AddEnvelopeSelectListBox.ItemHeight = 22;
            AddEnvelopeSelectListBox.Location = new Point(12, 55);
            AddEnvelopeSelectListBox.Name = "AddEnvelopeSelectListBox";
            AddEnvelopeSelectListBox.Size = new Size(204, 158);
            AddEnvelopeSelectListBox.TabIndex = 56;
            AddEnvelopeSelectListBox.SelectedIndexChanged += AddEnvelopeSelectListBox_SelectedIndexChanged;
            // 
            // AddEnvelopeSelectedEnvelopeLbl
            // 
            AddEnvelopeSelectedEnvelopeLbl.BorderStyle = BorderStyle.FixedSingle;
            AddEnvelopeSelectedEnvelopeLbl.Font = new Font("Arial", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            AddEnvelopeSelectedEnvelopeLbl.Location = new Point(222, 55);
            AddEnvelopeSelectedEnvelopeLbl.Name = "AddEnvelopeSelectedEnvelopeLbl";
            AddEnvelopeSelectedEnvelopeLbl.Size = new Size(268, 87);
            AddEnvelopeSelectedEnvelopeLbl.TabIndex = 58;
            AddEnvelopeSelectedEnvelopeLbl.TextAlign = ContentAlignment.TopCenter;
            // 
            // AddEnvelopeSelectedTitleLbl
            // 
            AddEnvelopeSelectedTitleLbl.Font = new Font("Arial", 12F);
            AddEnvelopeSelectedTitleLbl.Location = new Point(222, 9);
            AddEnvelopeSelectedTitleLbl.Name = "AddEnvelopeSelectedTitleLbl";
            AddEnvelopeSelectedTitleLbl.Size = new Size(268, 46);
            AddEnvelopeSelectedTitleLbl.TabIndex = 57;
            AddEnvelopeSelectedTitleLbl.Text = "Wybrana koperta:";
            AddEnvelopeSelectedTitleLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AddEnvelopeConfirmBtn
            // 
            AddEnvelopeConfirmBtn.Enabled = false;
            AddEnvelopeConfirmBtn.Font = new Font("Arial", 12F);
            AddEnvelopeConfirmBtn.Location = new Point(292, 145);
            AddEnvelopeConfirmBtn.Name = "AddEnvelopeConfirmBtn";
            AddEnvelopeConfirmBtn.Size = new Size(128, 68);
            AddEnvelopeConfirmBtn.TabIndex = 59;
            AddEnvelopeConfirmBtn.Text = "Zatwierdź";
            AddEnvelopeConfirmBtn.UseVisualStyleBackColor = true;
            AddEnvelopeConfirmBtn.Click += AddEnvelopeConfirmBtn_Click;
            // 
            // AddEnvelopeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 225);
            Controls.Add(AddEnvelopeConfirmBtn);
            Controls.Add(AddEnvelopeSelectedEnvelopeLbl);
            Controls.Add(AddEnvelopeSelectedTitleLbl);
            Controls.Add(AddEnvelopeSelectListBox);
            Controls.Add(AddEnvelopeChooseEnvelopeLbl);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddEnvelopeForm";
            Text = "Dodaj kopertę";
            TopMost = true;
            Load += AddEnvelopeForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label AddEnvelopeChooseEnvelopeLbl;
        private ListBox AddEnvelopeSelectListBox;
        private Label AddEnvelopeSelectedEnvelopeLbl;
        private Label AddEnvelopeSelectedTitleLbl;
        private Button AddEnvelopeConfirmBtn;
    }
}