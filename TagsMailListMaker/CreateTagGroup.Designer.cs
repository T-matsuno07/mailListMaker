namespace TagsMailListMaker
{
    partial class CreateTagGroup
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
            this.label1 = new System.Windows.Forms.Label();
            this.guiTexB_StrNewTagName = new System.Windows.Forms.TextBox();
            this.guiBut_EnterOK = new System.Windows.Forms.Button();
            this.guiBut_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "新規登録タグ名称";
            // 
            // guiTexB_StrNewTagName
            // 
            this.guiTexB_StrNewTagName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.guiTexB_StrNewTagName.Location = new System.Drawing.Point(10, 38);
            this.guiTexB_StrNewTagName.Name = "guiTexB_StrNewTagName";
            this.guiTexB_StrNewTagName.Size = new System.Drawing.Size(262, 23);
            this.guiTexB_StrNewTagName.TabIndex = 1;
            // 
            // guiBut_EnterOK
            // 
            this.guiBut_EnterOK.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.guiBut_EnterOK.Location = new System.Drawing.Point(116, 67);
            this.guiBut_EnterOK.Name = "guiBut_EnterOK";
            this.guiBut_EnterOK.Size = new System.Drawing.Size(75, 33);
            this.guiBut_EnterOK.TabIndex = 2;
            this.guiBut_EnterOK.Text = "登録";
            this.guiBut_EnterOK.UseVisualStyleBackColor = true;
            this.guiBut_EnterOK.Click += new System.EventHandler(this.guiBut_EnterOK_Click);
            // 
            // guiBut_Cancel
            // 
            this.guiBut_Cancel.Location = new System.Drawing.Point(197, 67);
            this.guiBut_Cancel.Name = "guiBut_Cancel";
            this.guiBut_Cancel.Size = new System.Drawing.Size(75, 33);
            this.guiBut_Cancel.TabIndex = 3;
            this.guiBut_Cancel.Text = "キャンセル";
            this.guiBut_Cancel.UseVisualStyleBackColor = true;
            this.guiBut_Cancel.Click += new System.EventHandler(this.guiBut_Cancel_Click);
            // 
            // CreateTagGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 112);
            this.Controls.Add(this.guiBut_Cancel);
            this.Controls.Add(this.guiBut_EnterOK);
            this.Controls.Add(this.guiTexB_StrNewTagName);
            this.Controls.Add(this.label1);
            this.Name = "CreateTagGroup";
            this.Text = "CreateTagGroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox guiTexB_StrNewTagName;
        private System.Windows.Forms.Button guiBut_EnterOK;
        private System.Windows.Forms.Button guiBut_Cancel;
    }
}