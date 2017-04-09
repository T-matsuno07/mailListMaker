namespace TagsMailListMaker
{
    partial class SortItemsForm
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
            this.guiList_SortItemsList = new System.Windows.Forms.ListBox();
            this.guiBtn_OneMoveDown = new System.Windows.Forms.Button();
            this.guiBtn_OneMoveUp = new System.Windows.Forms.Button();
            this.guiNumUD_MoveNum = new System.Windows.Forms.NumericUpDown();
            this.guiBtn_LoopMoveUp = new System.Windows.Forms.Button();
            this.guiBtn_LoopMoveDown = new System.Windows.Forms.Button();
            this.guiNumUD_DirectLine = new System.Windows.Forms.NumericUpDown();
            this.guiBtn_MoveDirectLine = new System.Windows.Forms.Button();
            this.guiBtn_SortEnter = new System.Windows.Forms.Button();
            this.guiBtn_SortCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.guiLbl_finallineNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.guiNumUD_MoveNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guiNumUD_DirectLine)).BeginInit();
            this.SuspendLayout();
            // 
            // guiList_SortItemsList
            // 
            this.guiList_SortItemsList.FormattingEnabled = true;
            this.guiList_SortItemsList.ItemHeight = 12;
            this.guiList_SortItemsList.Location = new System.Drawing.Point(227, 12);
            this.guiList_SortItemsList.Name = "guiList_SortItemsList";
            this.guiList_SortItemsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.guiList_SortItemsList.Size = new System.Drawing.Size(152, 280);
            this.guiList_SortItemsList.TabIndex = 0;
            // 
            // guiBtn_OneMoveDown
            // 
            this.guiBtn_OneMoveDown.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_OneMoveDown.Location = new System.Drawing.Point(78, 50);
            this.guiBtn_OneMoveDown.Name = "guiBtn_OneMoveDown";
            this.guiBtn_OneMoveDown.Size = new System.Drawing.Size(143, 32);
            this.guiBtn_OneMoveDown.TabIndex = 32;
            this.guiBtn_OneMoveDown.Text = "1行下へ移動";
            this.guiBtn_OneMoveDown.UseVisualStyleBackColor = true;
            this.guiBtn_OneMoveDown.Click += new System.EventHandler(this.guiBtn_OneMoveDown_Click);
            // 
            // guiBtn_OneMoveUp
            // 
            this.guiBtn_OneMoveUp.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_OneMoveUp.Location = new System.Drawing.Point(78, 12);
            this.guiBtn_OneMoveUp.Name = "guiBtn_OneMoveUp";
            this.guiBtn_OneMoveUp.Size = new System.Drawing.Size(143, 32);
            this.guiBtn_OneMoveUp.TabIndex = 33;
            this.guiBtn_OneMoveUp.Text = "1行上へ移動";
            this.guiBtn_OneMoveUp.UseVisualStyleBackColor = true;
            this.guiBtn_OneMoveUp.Click += new System.EventHandler(this.guiBtn_OneMoveUp_Click);
            // 
            // guiNumUD_MoveNum
            // 
            this.guiNumUD_MoveNum.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.guiNumUD_MoveNum.Location = new System.Drawing.Point(12, 129);
            this.guiNumUD_MoveNum.Name = "guiNumUD_MoveNum";
            this.guiNumUD_MoveNum.Size = new System.Drawing.Size(60, 22);
            this.guiNumUD_MoveNum.TabIndex = 34;
            this.guiNumUD_MoveNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // guiBtn_LoopMoveUp
            // 
            this.guiBtn_LoopMoveUp.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_LoopMoveUp.Location = new System.Drawing.Point(78, 106);
            this.guiBtn_LoopMoveUp.Name = "guiBtn_LoopMoveUp";
            this.guiBtn_LoopMoveUp.Size = new System.Drawing.Size(143, 32);
            this.guiBtn_LoopMoveUp.TabIndex = 35;
            this.guiBtn_LoopMoveUp.Text = "指定行分上へ移動";
            this.guiBtn_LoopMoveUp.UseVisualStyleBackColor = true;
            this.guiBtn_LoopMoveUp.Click += new System.EventHandler(this.guiBtn_LoopMoveUp_Click);
            // 
            // guiBtn_LoopMoveDown
            // 
            this.guiBtn_LoopMoveDown.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_LoopMoveDown.Location = new System.Drawing.Point(78, 144);
            this.guiBtn_LoopMoveDown.Name = "guiBtn_LoopMoveDown";
            this.guiBtn_LoopMoveDown.Size = new System.Drawing.Size(143, 32);
            this.guiBtn_LoopMoveDown.TabIndex = 36;
            this.guiBtn_LoopMoveDown.Text = "指定行分下へ移動";
            this.guiBtn_LoopMoveDown.UseVisualStyleBackColor = true;
            this.guiBtn_LoopMoveDown.Click += new System.EventHandler(this.guiBtn_LoopMoveDown_Click);
            // 
            // guiNumUD_DirectLine
            // 
            this.guiNumUD_DirectLine.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.guiNumUD_DirectLine.Location = new System.Drawing.Point(12, 202);
            this.guiNumUD_DirectLine.Name = "guiNumUD_DirectLine";
            this.guiNumUD_DirectLine.Size = new System.Drawing.Size(60, 22);
            this.guiNumUD_DirectLine.TabIndex = 37;
            this.guiNumUD_DirectLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // guiBtn_MoveDirectLine
            // 
            this.guiBtn_MoveDirectLine.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_MoveDirectLine.Location = new System.Drawing.Point(78, 197);
            this.guiBtn_MoveDirectLine.Name = "guiBtn_MoveDirectLine";
            this.guiBtn_MoveDirectLine.Size = new System.Drawing.Size(143, 32);
            this.guiBtn_MoveDirectLine.TabIndex = 38;
            this.guiBtn_MoveDirectLine.Text = "指定行へ移動";
            this.guiBtn_MoveDirectLine.UseVisualStyleBackColor = true;
            this.guiBtn_MoveDirectLine.Click += new System.EventHandler(this.guiBtn_MoveDirectLine_Click);
            // 
            // guiBtn_SortEnter
            // 
            this.guiBtn_SortEnter.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_SortEnter.Location = new System.Drawing.Point(23, 269);
            this.guiBtn_SortEnter.Name = "guiBtn_SortEnter";
            this.guiBtn_SortEnter.Size = new System.Drawing.Size(82, 32);
            this.guiBtn_SortEnter.TabIndex = 39;
            this.guiBtn_SortEnter.Text = "決定";
            this.guiBtn_SortEnter.UseVisualStyleBackColor = true;
            this.guiBtn_SortEnter.Click += new System.EventHandler(this.guiBtn_SortEnter_Click);
            // 
            // guiBtn_SortCancel
            // 
            this.guiBtn_SortCancel.ForeColor = System.Drawing.Color.Black;
            this.guiBtn_SortCancel.Location = new System.Drawing.Point(113, 269);
            this.guiBtn_SortCancel.Name = "guiBtn_SortCancel";
            this.guiBtn_SortCancel.Size = new System.Drawing.Size(84, 32);
            this.guiBtn_SortCancel.TabIndex = 40;
            this.guiBtn_SortCancel.Text = "キャンセル";
            this.guiBtn_SortCancel.UseVisualStyleBackColor = true;
            this.guiBtn_SortCancel.Click += new System.EventHandler(this.guiBtn_SortCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "先頭行:0  末尾行:";
            // 
            // guiLbl_finallineNum
            // 
            this.guiLbl_finallineNum.AutoSize = true;
            this.guiLbl_finallineNum.Location = new System.Drawing.Point(111, 232);
            this.guiLbl_finallineNum.Name = "guiLbl_finallineNum";
            this.guiLbl_finallineNum.Size = new System.Drawing.Size(29, 12);
            this.guiLbl_finallineNum.TabIndex = 42;
            this.guiLbl_finallineNum.Text = "0000";
            // 
            // SortItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 313);
            this.Controls.Add(this.guiLbl_finallineNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guiBtn_SortCancel);
            this.Controls.Add(this.guiBtn_SortEnter);
            this.Controls.Add(this.guiBtn_MoveDirectLine);
            this.Controls.Add(this.guiNumUD_DirectLine);
            this.Controls.Add(this.guiBtn_LoopMoveDown);
            this.Controls.Add(this.guiBtn_LoopMoveUp);
            this.Controls.Add(this.guiNumUD_MoveNum);
            this.Controls.Add(this.guiBtn_OneMoveUp);
            this.Controls.Add(this.guiBtn_OneMoveDown);
            this.Controls.Add(this.guiList_SortItemsList);
            this.Name = "SortItemsForm";
            this.Text = "SortItemsForm";
            ((System.ComponentModel.ISupportInitialize)(this.guiNumUD_MoveNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guiNumUD_DirectLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox guiList_SortItemsList;
        private System.Windows.Forms.Button guiBtn_OneMoveDown;
        private System.Windows.Forms.Button guiBtn_OneMoveUp;
        private System.Windows.Forms.NumericUpDown guiNumUD_MoveNum;
        private System.Windows.Forms.Button guiBtn_LoopMoveUp;
        private System.Windows.Forms.Button guiBtn_LoopMoveDown;
        private System.Windows.Forms.NumericUpDown guiNumUD_DirectLine;
        private System.Windows.Forms.Button guiBtn_MoveDirectLine;
        private System.Windows.Forms.Button guiBtn_SortEnter;
        private System.Windows.Forms.Button guiBtn_SortCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label guiLbl_finallineNum;
    }
}