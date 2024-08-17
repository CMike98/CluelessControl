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
            CorrectAnswerLabel = new Label();
            ContestantTextLabel = new Label();
            HostTextLabel = new Label();
            ExplanationLabel = new Label();
            CashTextLabel = new Label();
            OfferTextLabel = new Label();
            OfferLabel = new Label();
            CashLabel = new Label();
            ContestantEnvelope4Picture = new PictureBox();
            ContestantEnvelope1Picture = new PictureBox();
            ContestantEnvelope2Picture = new PictureBox();
            ContestantEnvelope3Picture = new PictureBox();
            ContestantEnvelope0Picture = new PictureBox();
            HostEnvelope4Picture = new PictureBox();
            HostEnvelope3Picture = new PictureBox();
            HostEnvelope2Picture = new PictureBox();
            HostEnvelope1Picture = new PictureBox();
            HostEnvelope0Picture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope4Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope1Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope2Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope3Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope0Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope4Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope3Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope2Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope1Picture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope0Picture).BeginInit();
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
            AnswerALabel.Size = new Size(900, 95);
            AnswerALabel.TabIndex = 1;
            AnswerALabel.Text = "A: ?";
            AnswerALabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AnswerBLabel
            // 
            AnswerBLabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerBLabel.Font = new Font("Arial", 24F);
            AnswerBLabel.ForeColor = Color.White;
            AnswerBLabel.Location = new Point(1012, 801);
            AnswerBLabel.Name = "AnswerBLabel";
            AnswerBLabel.Size = new Size(900, 95);
            AnswerBLabel.TabIndex = 2;
            AnswerBLabel.Text = "B: ?";
            AnswerBLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AnswerDLabel
            // 
            AnswerDLabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerDLabel.Font = new Font("Arial", 24F);
            AnswerDLabel.ForeColor = Color.White;
            AnswerDLabel.Location = new Point(1012, 904);
            AnswerDLabel.Name = "AnswerDLabel";
            AnswerDLabel.Size = new Size(900, 95);
            AnswerDLabel.TabIndex = 4;
            AnswerDLabel.Text = "D: ?";
            AnswerDLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AnswerCLabel
            // 
            AnswerCLabel.BorderStyle = BorderStyle.FixedSingle;
            AnswerCLabel.Font = new Font("Arial", 24F);
            AnswerCLabel.ForeColor = Color.White;
            AnswerCLabel.Location = new Point(12, 904);
            AnswerCLabel.Name = "AnswerCLabel";
            AnswerCLabel.Size = new Size(900, 95);
            AnswerCLabel.TabIndex = 3;
            AnswerCLabel.Text = "C: ?";
            AnswerCLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CorrectAnswerLabel
            // 
            CorrectAnswerLabel.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 238);
            CorrectAnswerLabel.ForeColor = Color.White;
            CorrectAnswerLabel.Location = new Point(918, 801);
            CorrectAnswerLabel.Name = "CorrectAnswerLabel";
            CorrectAnswerLabel.Size = new Size(88, 198);
            CorrectAnswerLabel.TabIndex = 5;
            CorrectAnswerLabel.Tag = "";
            CorrectAnswerLabel.Text = "A";
            CorrectAnswerLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ContestantTextLabel
            // 
            ContestantTextLabel.Font = new Font("Arial", 20.25F);
            ContestantTextLabel.ForeColor = Color.White;
            ContestantTextLabel.Location = new Point(12, 9);
            ContestantTextLabel.Name = "ContestantTextLabel";
            ContestantTextLabel.Size = new Size(268, 87);
            ContestantTextLabel.TabIndex = 12;
            ContestantTextLabel.Text = "ZAWODNIK";
            ContestantTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HostTextLabel
            // 
            HostTextLabel.Font = new Font("Arial", 20.25F);
            HostTextLabel.ForeColor = Color.White;
            HostTextLabel.Location = new Point(1644, 9);
            HostTextLabel.Name = "HostTextLabel";
            HostTextLabel.Size = new Size(268, 87);
            HostTextLabel.TabIndex = 18;
            HostTextLabel.Text = "PROWADZĄCY";
            HostTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ExplanationLabel
            // 
            ExplanationLabel.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 238);
            ExplanationLabel.ForeColor = Color.White;
            ExplanationLabel.Location = new Point(560, 459);
            ExplanationLabel.Name = "ExplanationLabel";
            ExplanationLabel.Size = new Size(804, 172);
            ExplanationLabel.TabIndex = 6;
            ExplanationLabel.Tag = "";
            ExplanationLabel.Text = "Wyjaśnienie";
            ExplanationLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CashTextLabel
            // 
            CashTextLabel.BorderStyle = BorderStyle.FixedSingle;
            CashTextLabel.Font = new Font("Arial", 20.25F);
            CashTextLabel.ForeColor = Color.White;
            CashTextLabel.Location = new Point(413, 116);
            CashTextLabel.Name = "CashTextLabel";
            CashTextLabel.Size = new Size(524, 48);
            CashTextLabel.TabIndex = 19;
            CashTextLabel.Text = "GOTÓWKA";
            CashTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OfferTextLabel
            // 
            OfferTextLabel.BorderStyle = BorderStyle.FixedSingle;
            OfferTextLabel.Font = new Font("Arial", 20.25F);
            OfferTextLabel.ForeColor = Color.White;
            OfferTextLabel.Location = new Point(987, 115);
            OfferTextLabel.Name = "OfferTextLabel";
            OfferTextLabel.Size = new Size(524, 48);
            OfferTextLabel.TabIndex = 20;
            OfferTextLabel.Text = "OFERTA";
            OfferTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OfferLabel
            // 
            OfferLabel.BorderStyle = BorderStyle.FixedSingle;
            OfferLabel.Font = new Font("Arial", 24F);
            OfferLabel.ForeColor = Color.White;
            OfferLabel.Location = new Point(987, 163);
            OfferLabel.Name = "OfferLabel";
            OfferLabel.Size = new Size(524, 104);
            OfferLabel.TabIndex = 22;
            OfferLabel.Text = "0";
            OfferLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CashLabel
            // 
            CashLabel.BorderStyle = BorderStyle.FixedSingle;
            CashLabel.Font = new Font("Arial", 24F);
            CashLabel.ForeColor = Color.White;
            CashLabel.Location = new Point(413, 163);
            CashLabel.Name = "CashLabel";
            CashLabel.Size = new Size(524, 104);
            CashLabel.TabIndex = 21;
            CashLabel.Text = "0";
            CashLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ContestantEnvelope4Picture
            // 
            ContestantEnvelope4Picture.BackColor = Color.White;
            ContestantEnvelope4Picture.BackgroundImageLayout = ImageLayout.Center;
            ContestantEnvelope4Picture.Location = new Point(12, 544);
            ContestantEnvelope4Picture.Name = "ContestantEnvelope4Picture";
            ContestantEnvelope4Picture.Size = new Size(268, 87);
            ContestantEnvelope4Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            ContestantEnvelope4Picture.TabIndex = 23;
            ContestantEnvelope4Picture.TabStop = false;
            ContestantEnvelope4Picture.Tag = "C4";
            ContestantEnvelope4Picture.Paint += EnvelopePicture_Paint;
            // 
            // ContestantEnvelope1Picture
            // 
            ContestantEnvelope1Picture.BackColor = Color.White;
            ContestantEnvelope1Picture.BackgroundImageLayout = ImageLayout.Center;
            ContestantEnvelope1Picture.Location = new Point(12, 223);
            ContestantEnvelope1Picture.Name = "ContestantEnvelope1Picture";
            ContestantEnvelope1Picture.Size = new Size(268, 87);
            ContestantEnvelope1Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            ContestantEnvelope1Picture.TabIndex = 24;
            ContestantEnvelope1Picture.TabStop = false;
            ContestantEnvelope1Picture.Tag = "C1";
            ContestantEnvelope1Picture.Paint += EnvelopePicture_Paint;
            // 
            // ContestantEnvelope2Picture
            // 
            ContestantEnvelope2Picture.BackColor = Color.White;
            ContestantEnvelope2Picture.BackgroundImageLayout = ImageLayout.Center;
            ContestantEnvelope2Picture.Location = new Point(12, 330);
            ContestantEnvelope2Picture.Name = "ContestantEnvelope2Picture";
            ContestantEnvelope2Picture.Size = new Size(268, 87);
            ContestantEnvelope2Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            ContestantEnvelope2Picture.TabIndex = 25;
            ContestantEnvelope2Picture.TabStop = false;
            ContestantEnvelope2Picture.Tag = "C2";
            ContestantEnvelope2Picture.Paint += EnvelopePicture_Paint;
            // 
            // ContestantEnvelope3Picture
            // 
            ContestantEnvelope3Picture.BackColor = Color.White;
            ContestantEnvelope3Picture.BackgroundImageLayout = ImageLayout.Center;
            ContestantEnvelope3Picture.Location = new Point(12, 437);
            ContestantEnvelope3Picture.Name = "ContestantEnvelope3Picture";
            ContestantEnvelope3Picture.Size = new Size(268, 87);
            ContestantEnvelope3Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            ContestantEnvelope3Picture.TabIndex = 26;
            ContestantEnvelope3Picture.TabStop = false;
            ContestantEnvelope3Picture.Tag = "C3";
            ContestantEnvelope3Picture.Paint += EnvelopePicture_Paint;
            // 
            // ContestantEnvelope0Picture
            // 
            ContestantEnvelope0Picture.BackColor = Color.White;
            ContestantEnvelope0Picture.BackgroundImageLayout = ImageLayout.Center;
            ContestantEnvelope0Picture.Location = new Point(12, 116);
            ContestantEnvelope0Picture.Name = "ContestantEnvelope0Picture";
            ContestantEnvelope0Picture.Size = new Size(268, 87);
            ContestantEnvelope0Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            ContestantEnvelope0Picture.TabIndex = 27;
            ContestantEnvelope0Picture.TabStop = false;
            ContestantEnvelope0Picture.Tag = "C0";
            ContestantEnvelope0Picture.Paint += EnvelopePicture_Paint;
            // 
            // HostEnvelope4Picture
            // 
            HostEnvelope4Picture.BackColor = Color.White;
            HostEnvelope4Picture.BackgroundImageLayout = ImageLayout.Center;
            HostEnvelope4Picture.Location = new Point(1644, 544);
            HostEnvelope4Picture.Name = "HostEnvelope4Picture";
            HostEnvelope4Picture.Size = new Size(268, 87);
            HostEnvelope4Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            HostEnvelope4Picture.TabIndex = 32;
            HostEnvelope4Picture.TabStop = false;
            HostEnvelope4Picture.Tag = "H4";
            HostEnvelope4Picture.Paint += EnvelopePicture_Paint;
            // 
            // HostEnvelope3Picture
            // 
            HostEnvelope3Picture.BackColor = Color.White;
            HostEnvelope3Picture.BackgroundImageLayout = ImageLayout.Center;
            HostEnvelope3Picture.Location = new Point(1644, 437);
            HostEnvelope3Picture.Name = "HostEnvelope3Picture";
            HostEnvelope3Picture.Size = new Size(268, 87);
            HostEnvelope3Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            HostEnvelope3Picture.TabIndex = 31;
            HostEnvelope3Picture.TabStop = false;
            HostEnvelope3Picture.Tag = "H3";
            HostEnvelope3Picture.Paint += EnvelopePicture_Paint;
            // 
            // HostEnvelope2Picture
            // 
            HostEnvelope2Picture.BackColor = Color.White;
            HostEnvelope2Picture.BackgroundImageLayout = ImageLayout.Center;
            HostEnvelope2Picture.Location = new Point(1644, 330);
            HostEnvelope2Picture.Name = "HostEnvelope2Picture";
            HostEnvelope2Picture.Size = new Size(268, 87);
            HostEnvelope2Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            HostEnvelope2Picture.TabIndex = 30;
            HostEnvelope2Picture.TabStop = false;
            HostEnvelope2Picture.Tag = "H2";
            HostEnvelope2Picture.Paint += EnvelopePicture_Paint;
            // 
            // HostEnvelope1Picture
            // 
            HostEnvelope1Picture.BackColor = Color.White;
            HostEnvelope1Picture.BackgroundImageLayout = ImageLayout.Center;
            HostEnvelope1Picture.Location = new Point(1644, 223);
            HostEnvelope1Picture.Name = "HostEnvelope1Picture";
            HostEnvelope1Picture.Size = new Size(268, 87);
            HostEnvelope1Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            HostEnvelope1Picture.TabIndex = 29;
            HostEnvelope1Picture.TabStop = false;
            HostEnvelope1Picture.Tag = "H1";
            HostEnvelope1Picture.Paint += EnvelopePicture_Paint;
            // 
            // HostEnvelope0Picture
            // 
            HostEnvelope0Picture.BackColor = Color.White;
            HostEnvelope0Picture.BackgroundImageLayout = ImageLayout.Center;
            HostEnvelope0Picture.Location = new Point(1644, 116);
            HostEnvelope0Picture.Name = "HostEnvelope0Picture";
            HostEnvelope0Picture.Size = new Size(268, 87);
            HostEnvelope0Picture.SizeMode = PictureBoxSizeMode.CenterImage;
            HostEnvelope0Picture.TabIndex = 28;
            HostEnvelope0Picture.TabStop = false;
            HostEnvelope0Picture.Tag = "H0";
            HostEnvelope0Picture.Paint += EnvelopePicture_Paint;
            // 
            // HostScreenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1924, 1011);
            Controls.Add(HostEnvelope4Picture);
            Controls.Add(HostEnvelope3Picture);
            Controls.Add(HostEnvelope2Picture);
            Controls.Add(HostEnvelope1Picture);
            Controls.Add(HostEnvelope0Picture);
            Controls.Add(ContestantEnvelope0Picture);
            Controls.Add(ContestantEnvelope3Picture);
            Controls.Add(ContestantEnvelope2Picture);
            Controls.Add(ContestantEnvelope1Picture);
            Controls.Add(ContestantEnvelope4Picture);
            Controls.Add(OfferLabel);
            Controls.Add(CashLabel);
            Controls.Add(OfferTextLabel);
            Controls.Add(CashTextLabel);
            Controls.Add(ExplanationLabel);
            Controls.Add(HostTextLabel);
            Controls.Add(ContestantTextLabel);
            Controls.Add(CorrectAnswerLabel);
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
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope4Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope1Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope2Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope3Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)ContestantEnvelope0Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope4Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope3Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope2Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope1Picture).EndInit();
            ((System.ComponentModel.ISupportInitialize)HostEnvelope0Picture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label QuestionLabel;
        private Label AnswerALabel;
        private Label AnswerBLabel;
        private Label AnswerDLabel;
        private Label AnswerCLabel;
        private Label CorrectAnswerLabel;
        private Label ContestantTextLabel;
        private Label HostTextLabel;
        private Label ExplanationLabel;
        private Label CashTextLabel;
        private Label OfferTextLabel;
        private Label OfferLabel;
        private Label CashLabel;
        private PictureBox HostEnvelope0Picture;
        private PictureBox HostEnvelope1Picture;
        private PictureBox HostEnvelope2Picture;
        private PictureBox HostEnvelope3Picture;
        private PictureBox HostEnvelope4Picture;
        private PictureBox ContestantEnvelope0Picture;
        private PictureBox ContestantEnvelope1Picture;
        private PictureBox ContestantEnvelope2Picture;
        private PictureBox ContestantEnvelope3Picture;
        private PictureBox ContestantEnvelope4Picture;
    }
}