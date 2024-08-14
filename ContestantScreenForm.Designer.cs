namespace CluelessControl
{
    partial class ContestantScreenForm
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
            SuspendLayout();
            // 
            // ContestantScreenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1924, 1061);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "ContestantScreenForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zawodnik";
            WindowState = FormWindowState.Maximized;
            FormClosing += ContestantScreenForm_FormClosing;
            Load += ContestantScreenForm_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}