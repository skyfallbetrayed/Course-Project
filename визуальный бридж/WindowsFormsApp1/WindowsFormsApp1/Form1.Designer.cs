namespace StreetBridgeWindowsApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button nextTurnButton;
        private System.Windows.Forms.Button drawCardButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Label[] playerLabels;
        private System.Windows.Forms.Label lastPlayedCardLabel;
        private System.Windows.Forms.Label currentPlayerLabel;
        private System.Windows.Forms.RadioButton[][] handRadioButtonGroups;
        private System.Windows.Forms.ListBox eventLogListBox;

        private System.Windows.Forms.Button heartsButton;
        private System.Windows.Forms.Button diamondsButton;
        private System.Windows.Forms.Button clubsButton;
        private System.Windows.Forms.Button spadesButton;
        private System.Windows.Forms.Form suitSelectionForm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.nextTurnButton = new System.Windows.Forms.Button();
            this.drawCardButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.lastPlayedCardLabel = new System.Windows.Forms.Label();
            this.currentPlayerLabel = new System.Windows.Forms.Label();
            this.eventLogListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // nextTurnButton
            this.nextTurnButton.Location = new System.Drawing.Point(12, 400);
            this.nextTurnButton.Name = "nextTurnButton";
            this.nextTurnButton.Size = new System.Drawing.Size(100, 23);
            this.nextTurnButton.TabIndex = 0;
            this.nextTurnButton.Text = "Наступний Хід";
            this.nextTurnButton.UseVisualStyleBackColor = true;
            this.nextTurnButton.Click += new System.EventHandler(this.nextTurnButton_Click);

            // drawCardButton
            this.drawCardButton.Location = new System.Drawing.Point(120, 400);
            this.drawCardButton.Name = "drawCardButton";
            this.drawCardButton.Size = new System.Drawing.Size(100, 23);
            this.drawCardButton.TabIndex = 1;
            this.drawCardButton.Text = "Взяти Карту";
            this.drawCardButton.UseVisualStyleBackColor = true;
            this.drawCardButton.Click += new System.EventHandler(this.drawCardButton_Click);

            // inputTextBox
            this.inputTextBox.Location = new System.Drawing.Point(12, 370);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(300, 20);
            this.inputTextBox.TabIndex = 2;

            // lastPlayedCardLabel
            this.lastPlayedCardLabel.AutoSize = true;
            this.lastPlayedCardLabel.Location = new System.Drawing.Point(12, 340);
            this.lastPlayedCardLabel.Name = "lastPlayedCardLabel";
            this.lastPlayedCardLabel.Size = new System.Drawing.Size(117, 13);
            this.lastPlayedCardLabel.TabIndex = 3;
            this.lastPlayedCardLabel.Text = "Остання грана карта:";

            // currentPlayerLabel
            this.currentPlayerLabel.AutoSize = true;
            this.currentPlayerLabel.Location = new System.Drawing.Point(12, 320);
            this.currentPlayerLabel.Name = "currentPlayerLabel";
            this.currentPlayerLabel.Size = new System.Drawing.Size(102, 13);
            this.currentPlayerLabel.TabIndex = 4;
            this.currentPlayerLabel.Text = "Поточний гравець:";

            // eventLogListBox
            this.eventLogListBox.FormattingEnabled = true;
            this.eventLogListBox.Location = new System.Drawing.Point(434, 330);
            this.eventLogListBox.Name = "eventLogListBox";
            this.eventLogListBox.Size = new System.Drawing.Size(622, 238);
            this.eventLogListBox.TabIndex = 5;

            // Form1
            this.ClientSize = new System.Drawing.Size(1472, 655);
            this.Controls.Add(this.eventLogListBox);
            this.Controls.Add(this.currentPlayerLabel);
            this.Controls.Add(this.nextTurnButton);
            this.Controls.Add(this.drawCardButton);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.lastPlayedCardLabel);
            this.Name = "Form1";
            this.Text = "Гра Вуличний Бридж";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
