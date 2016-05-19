namespace XiboClientWatchdog
{
    partial class Tray
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tray));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.processLabel = new System.Windows.Forms.Label();
            this.libraryLabel = new System.Windows.Forms.Label();
            this.lastAccessedLabel = new System.Windows.Forms.Label();
            this.lastRestartLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.errorTextBox = new System.Windows.Forms.TextBox();
            this.notifyContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyContextMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Watch Dog";
            this.notifyIcon1.Visible = true;
            // 
            // notifyContextMenu
            // 
            this.notifyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem1});
            this.notifyContextMenu.Name = "notifyContextMenu";
            this.notifyContextMenu.Size = new System.Drawing.Size(114, 48);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem2.Text = "Restore";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem1.Text = "Exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // processLabel
            // 
            this.processLabel.AutoSize = true;
            this.processLabel.Location = new System.Drawing.Point(12, 19);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(70, 13);
            this.processLabel.TabIndex = 1;
            this.processLabel.Text = "processLabel";
            // 
            // libraryLabel
            // 
            this.libraryLabel.AutoSize = true;
            this.libraryLabel.Location = new System.Drawing.Point(12, 46);
            this.libraryLabel.Name = "libraryLabel";
            this.libraryLabel.Size = new System.Drawing.Size(60, 13);
            this.libraryLabel.TabIndex = 2;
            this.libraryLabel.Text = "libraryLabel";
            // 
            // lastAccessedLabel
            // 
            this.lastAccessedLabel.AutoSize = true;
            this.lastAccessedLabel.Location = new System.Drawing.Point(12, 72);
            this.lastAccessedLabel.Name = "lastAccessedLabel";
            this.lastAccessedLabel.Size = new System.Drawing.Size(76, 13);
            this.lastAccessedLabel.TabIndex = 3;
            this.lastAccessedLabel.Text = "Last Scanned:";
            // 
            // lastRestartLabel
            // 
            this.lastRestartLabel.AutoSize = true;
            this.lastRestartLabel.Location = new System.Drawing.Point(12, 101);
            this.lastRestartLabel.Name = "lastRestartLabel";
            this.lastRestartLabel.Size = new System.Drawing.Size(64, 13);
            this.lastRestartLabel.TabIndex = 4;
            this.lastRestartLabel.Text = "Last Restart";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(15, 216);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(571, 34);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // errorTextBox
            // 
            this.errorTextBox.Location = new System.Drawing.Point(15, 128);
            this.errorTextBox.Multiline = true;
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.Size = new System.Drawing.Size(571, 82);
            this.errorTextBox.TabIndex = 7;
            // 
            // Tray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 262);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.lastRestartLabel);
            this.Controls.Add(this.lastAccessedLabel);
            this.Controls.Add(this.libraryLabel);
            this.Controls.Add(this.processLabel);
            this.Name = "Tray";
            this.Text = "Watch Dog";
            this.notifyContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label processLabel;
        private System.Windows.Forms.Label libraryLabel;
        private System.Windows.Forms.Label lastAccessedLabel;
        private System.Windows.Forms.Label lastRestartLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox errorTextBox;
    }
}

