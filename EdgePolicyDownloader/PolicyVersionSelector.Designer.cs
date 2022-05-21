namespace EdgePolicyDownloader
{
    partial class PolicyVersionSelector
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EdgeReleasesGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeployButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeReleasesGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EdgeReleasesGrid
            // 
            this.EdgeReleasesGrid.AllowUserToOrderColumns = true;
            this.EdgeReleasesGrid.AllowUserToResizeRows = false;
            this.EdgeReleasesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.EdgeReleasesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("游ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EdgeReleasesGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.EdgeReleasesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EdgeReleasesGrid.Location = new System.Drawing.Point(0, 0);
            this.EdgeReleasesGrid.MultiSelect = false;
            this.EdgeReleasesGrid.Name = "EdgeReleasesGrid";
            this.EdgeReleasesGrid.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("游ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EdgeReleasesGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.EdgeReleasesGrid.RowHeadersVisible = false;
            this.EdgeReleasesGrid.RowHeadersWidth = 51;
            this.EdgeReleasesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EdgeReleasesGrid.Size = new System.Drawing.Size(795, 450);
            this.EdgeReleasesGrid.TabIndex = 0;
            this.EdgeReleasesGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.EdgeReleasesGrid_CellFormatting);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DeployButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.SaveAsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 412);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(795, 38);
            this.panel1.TabIndex = 1;
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Location = new System.Drawing.Point(565, 5);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(96, 30);
            this.SaveAsButton.TabIndex = 0;
            this.SaveAsButton.Text = "Save as...";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(463, 5);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(96, 30);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // DeployButton
            // 
            this.DeployButton.Location = new System.Drawing.Point(687, 5);
            this.DeployButton.Name = "DeployButton";
            this.DeployButton.Size = new System.Drawing.Size(96, 30);
            this.DeployButton.TabIndex = 2;
            this.DeployButton.Text = "Deploy";
            this.DeployButton.UseVisualStyleBackColor = true;
            // 
            // PolicyVersionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EdgeReleasesGrid);
            this.Name = "PolicyVersionSelector";
            this.Text = "Select Policy Version.";
            this.Load += new System.EventHandler(this.PolicyVersionSelector_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.EdgeReleasesGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView EdgeReleasesGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button DeployButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAsButton;
    }
}

