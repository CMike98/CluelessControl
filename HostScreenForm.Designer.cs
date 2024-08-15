namespace CluelessControl
{
    partial class HostScreenForm
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
            QuestionLabel = new Label();
            AnswerALabel = new Label();
            AnswerBLabel = new Label();
            AnswerDLabel = new Label();
            AnswerCLabel = new Label();
            SuspendLayout();
            // 
            // QuestionLabel
            // 
            QuestionLabel.BorderStyle = BorderStyle.FixedSingle;
            QuestionLabel.Font = new Font("Arial", 24F);
            QuestionLabel.ForeColor = Color.White;
            QuestionLabel.Location = new Point(12, 656);
            QuestionLabel.Name = "QuestionLabel";
            QuestionLabel.Size = new Size(1900, 131);
            QuestionLabel.TabIndex = 0;
            QuestionLabel.Text = "Pytanie testowe";
            QuestionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AnswerALabel
            // 
            AnswerALabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerALabel.Font = new Font("Arial", 24F);
            AnswerALabel.ForeColor = Color.White;
            AnswerALabel.Location = new Point(12, 801);
            AnswerALabel.Name = "AnswerALabel";
            AnswerALabel.Size = new Size(930, 95);
            AnswerALabel.TabIndex = 1;
            AnswerALabel.Text = "A: ?";
            AnswerALabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AnswerBLabel
            // 
            AnswerBLabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerBLabel.Font = new Font("Arial", 24F);
            AnswerBLabel.ForeColor = Color.White;
            AnswerBLabel.Location = new Point(982, 801);
            AnswerBLabel.Name = "AnswerBLabel";
            AnswerBLabel.Size = new Size(930, 95);
            AnswerBLabel.TabIndex = 2;
            AnswerBLabel.Text = "B: ?";
            AnswerBLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AnswerDLabel
            // 
            AnswerDLabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerDLabel.Font = new Font("Arial", 24F);
            AnswerDLabel.ForeColor = Color.White;
            AnswerDLabel.Location = new Point(982, 904);
            AnswerDLabel.Name = "AnswerDLabel";
            AnswerDLabel.Size = new Size(930, 95);
            AnswerDLabel.TabIndex = 4;
            AnswerDLabel.Text = "D: ?";
            AnswerDLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AnswerCLabel
            // 
            AnswerCLabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerCLabel.Font = new Font("Arial", 24F);
            AnswerCLabel.ForeColor = Color.White;
            AnswerCLabel.Location = new Point(12, 904);
            AnswerCLabel.Name = "AnswerCLabel";
            AnswerCLabel.Size = new Size(930, 95);
            AnswerCLabel.TabIndex = 3;
            AnswerCLabel.Text = "C: ?";
            AnswerCLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HostScreenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1924, 1011);
            Controls.Add(AnswerDLabel);
            Controls.Add(AnswerCLabel);
            Controls.Add(AnswerBLabel);
            Controls.Add(AnswerALabel);
            Controls.Add(QuestionLabel);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "HostScreenForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Prowadzący";
            WindowState = FormWindowState.Maximized;
            FormClosing += HostScreenForm_FormClosing;
            Load += HostScreenForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label QuestionLabel;
        private Label AnswerALabel;
        private Label AnswerBLabel;
        private Label AnswerDLabel;
        private Label AnswerCLabel;
    }
}