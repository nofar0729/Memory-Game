namespace MemoryGame
{
    partial class MemoryGameForm
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
            this.CurrentPlayerLabel = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player2Label = new System.Windows.Forms.Label();
            this.prototypeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CurrentPlayerLabel
            // 
            this.CurrentPlayerLabel.AutoSize = true;
            this.CurrentPlayerLabel.BackColor = System.Drawing.Color.LightGreen;
            this.CurrentPlayerLabel.Location = new System.Drawing.Point(31, 512);
            this.CurrentPlayerLabel.Name = "CurrentPlayerLabel";
            this.CurrentPlayerLabel.Size = new System.Drawing.Size(110, 18);
            this.CurrentPlayerLabel.TabIndex = 0;
            this.CurrentPlayerLabel.Text = "Current Player: ";
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.BackColor = System.Drawing.Color.LightGreen;
            this.Player1Label.Location = new System.Drawing.Point(31, 549);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(65, 18);
            this.Player1Label.TabIndex = 1;
            this.Player1Label.Text = "Player1: ";
            // 
            // Player2Label
            // 
            this.Player2Label.AutoSize = true;
            this.Player2Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Player2Label.Location = new System.Drawing.Point(31, 584);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(61, 18);
            this.Player2Label.TabIndex = 2;
            this.Player2Label.Text = "Player2:";
            // 
            // prototypeButton
            // 
            this.prototypeButton.BackColor = System.Drawing.Color.Silver;
            this.prototypeButton.FlatAppearance.BorderSize = 5;
            this.prototypeButton.Location = new System.Drawing.Point(34, 11);
            this.prototypeButton.Margin = new System.Windows.Forms.Padding(2);
            this.prototypeButton.Name = "prototypeButton";
            this.prototypeButton.Size = new System.Drawing.Size(80, 80);
            this.prototypeButton.TabIndex = 3;
            this.prototypeButton.UseVisualStyleBackColor = false;
            this.prototypeButton.Visible = false;
            // 
            // MemoryGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(634, 614);
            this.Controls.Add(this.prototypeButton);
            this.Controls.Add(this.Player2Label);
            this.Controls.Add(this.Player1Label);
            this.Controls.Add(this.CurrentPlayerLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MemoryGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CurrentPlayerLabel;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.Button prototypeButton;
    }
}