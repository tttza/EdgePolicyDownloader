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
            this.EdgeReleasesGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.EdgeReleasesGrid)).BeginInit();
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
            this.EdgeReleasesGrid.RowHeadersVisible = false;
            this.EdgeReleasesGrid.RowHeadersWidth = 51;
            this.EdgeReleasesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EdgeReleasesGrid.Size = new System.Drawing.Size(795, 450);
            this.EdgeReleasesGrid.TabIndex = 0;
            // 
            // PolicyVersionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 450);
            this.Controls.Add(this.EdgeReleasesGrid);
            this.Name = "PolicyVersionSelector";
            this.Text = "Select Policy Version.";
            this.Load += new System.EventHandler(this.PolicyVersionSelector_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.EdgeReleasesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView EdgeReleasesGrid;
    }
}

