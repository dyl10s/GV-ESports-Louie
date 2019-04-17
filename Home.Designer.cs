namespace Louie_Bot
{
    partial class Home
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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.gbTerminal = new System.Windows.Forms.GroupBox();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.btnCheckForUpdate = new System.Windows.Forms.Button();
            this.gbTerminal.SuspendLayout();
            this.gbControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.Black;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.ForeColor = System.Drawing.Color.White;
            this.txtOutput.Location = new System.Drawing.Point(3, 18);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(887, 488);
            this.txtOutput.TabIndex = 0;
            // 
            // gbTerminal
            // 
            this.gbTerminal.BackColor = System.Drawing.Color.Transparent;
            this.gbTerminal.Controls.Add(this.txtOutput);
            this.gbTerminal.Location = new System.Drawing.Point(13, 125);
            this.gbTerminal.Name = "gbTerminal";
            this.gbTerminal.Size = new System.Drawing.Size(893, 509);
            this.gbTerminal.TabIndex = 3;
            this.gbTerminal.TabStop = false;
            // 
            // gbControls
            // 
            this.gbControls.Controls.Add(this.btnCheckForUpdate);
            this.gbControls.Location = new System.Drawing.Point(13, 12);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(893, 107);
            this.gbControls.TabIndex = 2;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "Controls";
            // 
            // btnCheckForUpdate
            // 
            this.btnCheckForUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnCheckForUpdate.Location = new System.Drawing.Point(6, 31);
            this.btnCheckForUpdate.Name = "btnCheckForUpdate";
            this.btnCheckForUpdate.Size = new System.Drawing.Size(162, 56);
            this.btnCheckForUpdate.TabIndex = 0;
            this.btnCheckForUpdate.Text = "Check for Update";
            this.btnCheckForUpdate.UseVisualStyleBackColor = true;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 647);
            this.Controls.Add(this.gbTerminal);
            this.Controls.Add(this.gbControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Home";
            this.Text = "Louie";
            this.gbTerminal.ResumeLayout(false);
            this.gbTerminal.PerformLayout();
            this.gbControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.GroupBox gbTerminal;
        private System.Windows.Forms.GroupBox gbControls;
        private System.Windows.Forms.Button btnCheckForUpdate;
    }
}

