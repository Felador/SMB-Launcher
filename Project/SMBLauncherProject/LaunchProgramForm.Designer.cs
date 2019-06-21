namespace SMBLauncherProject
{
    partial class LaunchProgramForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchProgramForm));
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.lblDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(64, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 28);
            this.label2.TabIndex = 8;
            this.label2.Text = "NAME:";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.Black;
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Font = new System.Drawing.Font("Consolas", 12F);
            this.tbName.ForeColor = System.Drawing.Color.White;
            this.tbName.Location = new System.Drawing.Point(147, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(682, 36);
            this.tbName.TabIndex = 9;
            this.tbName.Text = "Example: Livesplit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "LOCATION:";
            // 
            // tbLocation
            // 
            this.tbLocation.BackColor = System.Drawing.Color.Black;
            this.tbLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLocation.Font = new System.Drawing.Font("Consolas", 12F);
            this.tbLocation.ForeColor = System.Drawing.Color.White;
            this.tbLocation.Location = new System.Drawing.Point(147, 54);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(682, 36);
            this.tbLocation.TabIndex = 11;
            this.tbLocation.Text = "Example: F:\\Software\\Livesplit\\LiveSplit.exe";
            // 
            // lblDone
            // 
            this.lblDone.BackColor = System.Drawing.Color.Black;
            this.lblDone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDone.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblDone.ForeColor = System.Drawing.Color.White;
            this.lblDone.Location = new System.Drawing.Point(142, 100);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(182, 37);
            this.lblDone.TabIndex = 12;
            this.lblDone.Text = "DONE";
            this.lblDone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDone.Click += new System.EventHandler(this.LblDone_Click);
            // 
            // LaunchProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(841, 146);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.tbLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LaunchProgramForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Launch Program";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.Label lblDone;
    }
}