
namespace MnogodetLiteDB {
    partial class FormFamiliesProblems {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridFamilies = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.editDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridPersons = new System.Windows.Forms.DataGridView();
            this.pId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridFamilies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFamilies
            // 
            this.gridFamilies.AllowUserToAddRows = false;
            this.gridFamilies.AllowUserToDeleteRows = false;
            this.gridFamilies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFamilies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.f,
            this.ExpireDate});
            this.gridFamilies.Location = new System.Drawing.Point(12, 46);
            this.gridFamilies.Name = "gridFamilies";
            this.gridFamilies.ReadOnly = true;
            this.gridFamilies.RowTemplate.Height = 24;
            this.gridFamilies.Size = new System.Drawing.Size(367, 584);
            this.gridFamilies.TabIndex = 0;
            this.gridFamilies.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridFamilies_CellMouseDoubleClick_1);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // f
            // 
            this.f.HeaderText = "Фамилия семьи";
            this.f.Name = "f";
            this.f.ReadOnly = true;
            // 
            // ExpireDate
            // 
            this.ExpireDate.HeaderText = "Дата окончания статуса";
            this.ExpireDate.Name = "ExpireDate";
            this.ExpireDate.ReadOnly = true;
            this.ExpireDate.Width = 200;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // editDate
            // 
            this.editDate.Location = new System.Drawing.Point(197, 12);
            this.editDate.Name = "editDate";
            this.editDate.Size = new System.Drawing.Size(100, 20);
            this.editDate.TabIndex = 2;
            this.editDate.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Дата проверки";
            this.label1.Visible = false;
            // 
            // gridPersons
            // 
            this.gridPersons.AllowUserToAddRows = false;
            this.gridPersons.AllowUserToDeleteRows = false;
            this.gridPersons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPersons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPersons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pId,
            this.pF,
            this.pI,
            this.pO,
            this.pBirthDate,
            this.status});
            this.gridPersons.Location = new System.Drawing.Point(389, 46);
            this.gridPersons.Name = "gridPersons";
            this.gridPersons.ReadOnly = true;
            this.gridPersons.RowTemplate.Height = 24;
            this.gridPersons.Size = new System.Drawing.Size(546, 584);
            this.gridPersons.TabIndex = 4;
            this.gridPersons.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridPersons_CellMouseDoubleClick_1);
            // 
            // pId
            // 
            this.pId.HeaderText = "id";
            this.pId.Name = "pId";
            this.pId.ReadOnly = true;
            this.pId.Visible = false;
            // 
            // pF
            // 
            this.pF.HeaderText = "Фамилия";
            this.pF.Name = "pF";
            this.pF.ReadOnly = true;
            // 
            // pI
            // 
            this.pI.HeaderText = "Имя";
            this.pI.Name = "pI";
            this.pI.ReadOnly = true;
            // 
            // pO
            // 
            this.pO.HeaderText = "Отчество";
            this.pO.Name = "pO";
            this.pO.ReadOnly = true;
            // 
            // pBirthDate
            // 
            this.pBirthDate.HeaderText = "Дата рождения";
            this.pBirthDate.Name = "pBirthDate";
            this.pBirthDate.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Статус";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // FormFamiliesProblems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 635);
            this.Controls.Add(this.gridPersons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editDate);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.gridFamilies);
            this.Name = "FormFamiliesProblems";
            this.Text = "FormFamiliesProblems";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFamiliesProblems_FormClosed);
            this.Shown += new System.EventHandler(this.FormFamiliesProblems_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridFamilies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridFamilies;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox editDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn f;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpireDate;
        private System.Windows.Forms.DataGridView gridPersons;
        private System.Windows.Forms.DataGridViewTextBoxColumn pId;
        private System.Windows.Forms.DataGridViewTextBoxColumn pF;
        private System.Windows.Forms.DataGridViewTextBoxColumn pI;
        private System.Windows.Forms.DataGridViewTextBoxColumn pO;
        private System.Windows.Forms.DataGridViewTextBoxColumn pBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}