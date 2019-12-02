namespace BuhOblic
{
    partial class RecipeList
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
            this.recipesDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.recipesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // recipesDataGridView
            // 
            this.recipesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipesDataGridView.Location = new System.Drawing.Point(37, 30);
            this.recipesDataGridView.Name = "recipesDataGridView";
            this.recipesDataGridView.RowTemplate.Height = 24;
            this.recipesDataGridView.Size = new System.Drawing.Size(739, 378);
            this.recipesDataGridView.TabIndex = 0;
            // 
            // RecipeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 524);
            this.Controls.Add(this.recipesDataGridView);
            this.Name = "RecipeList";
            this.Text = "Перелік рецептур";
            ((System.ComponentModel.ISupportInitialize)(this.recipesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView recipesDataGridView;
    }
}