
namespace MnogodetLiteDB {
    partial class FormFamily {
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
            this.components = new System.ComponentModel.Container();
            this.gridPersons = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.i = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.o = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.labelExpireDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editAddress = new System.Windows.Forms.TextBox();
            this.editComment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAddPerson = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonDeleteFamily = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.listCancelReason = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCancelReasonDate = new System.Windows.Forms.Label();
            this.databaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelCreationDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelUdostDateExpireStatus = new System.Windows.Forms.Label();
            this.labelUdostNumber = new System.Windows.Forms.Label();
            this.labelUdostDateIssue = new System.Windows.Forms.Label();
            this.labelUdostDateExpire = new System.Windows.Forms.Label();
            this.buttonUdostEdit = new System.Windows.Forms.Button();
            this.labelId = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseBindingSource)).BeginInit();
            this.SuspendLayout();
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
            this.id,
            this.type,
            this.f,
            this.i,
            this.o,
            this.birthDate,
            this.status});
            this.gridPersons.Location = new System.Drawing.Point(12, 142);
            this.gridPersons.Name = "gridPersons";
            this.gridPersons.ReadOnly = true;
            this.gridPersons.RowTemplate.Height = 24;
            this.gridPersons.Size = new System.Drawing.Size(788, 343);
            this.gridPersons.TabIndex = 0;
            this.gridPersons.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridPersons_CellMouseDoubleClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // type
            // 
            this.type.HeaderText = "Род";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // f
            // 
            this.f.HeaderText = "Фамилия";
            this.f.Name = "f";
            this.f.ReadOnly = true;
            // 
            // i
            // 
            this.i.HeaderText = "Имя";
            this.i.Name = "i";
            this.i.ReadOnly = true;
            // 
            // o
            // 
            this.o.HeaderText = "Отчество";
            this.o.Name = "o";
            this.o.ReadOnly = true;
            // 
            // birthDate
            // 
            this.birthDate.HeaderText = "Дата рождения";
            this.birthDate.Name = "birthDate";
            this.birthDate.ReadOnly = true;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.status.HeaderText = "Статус";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата истечения статуса в ФНС: ";
            // 
            // labelExpireDate
            // 
            this.labelExpireDate.AutoSize = true;
            this.labelExpireDate.Location = new System.Drawing.Point(444, 9);
            this.labelExpireDate.Name = "labelExpireDate";
            this.labelExpireDate.Size = new System.Drawing.Size(93, 13);
            this.labelExpireDate.TabIndex = 2;
            this.labelExpireDate.Text = "истек 00.00.0000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Адрес:";
            // 
            // editAddress
            // 
            this.editAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editAddress.Location = new System.Drawing.Point(96, 90);
            this.editAddress.Name = "editAddress";
            this.editAddress.Size = new System.Drawing.Size(704, 20);
            this.editAddress.TabIndex = 4;
            this.editAddress.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // editComment
            // 
            this.editComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editComment.Location = new System.Drawing.Point(96, 116);
            this.editComment.Name = "editComment";
            this.editComment.Size = new System.Drawing.Size(704, 20);
            this.editComment.TabIndex = 6;
            this.editComment.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Комментарий:";
            // 
            // buttonAddPerson
            // 
            this.buttonAddPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddPerson.Location = new System.Drawing.Point(147, 493);
            this.buttonAddPerson.Name = "buttonAddPerson";
            this.buttonAddPerson.Size = new System.Drawing.Size(130, 23);
            this.buttonAddPerson.TabIndex = 7;
            this.buttonAddPerson.Text = "Добавить человека";
            this.buttonAddPerson.UseVisualStyleBackColor = true;
            this.buttonAddPerson.Click += new System.EventHandler(this.buttonAddPerson_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveAndClose.Location = new System.Drawing.Point(11, 493);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(130, 23);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "Сохранить и закрыть";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonDeleteFamily
            // 
            this.buttonDeleteFamily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteFamily.Location = new System.Drawing.Point(670, 493);
            this.buttonDeleteFamily.Name = "buttonDeleteFamily";
            this.buttonDeleteFamily.Size = new System.Drawing.Size(130, 23);
            this.buttonDeleteFamily.TabIndex = 9;
            this.buttonDeleteFamily.Text = "Удалить семью";
            this.buttonDeleteFamily.UseVisualStyleBackColor = true;
            this.buttonDeleteFamily.Click += new System.EventHandler(this.buttonDeleteFamily_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(284, 493);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(130, 26);
            this.labelStatus.TabIndex = 10;
            this.labelStatus.Text = "Статус выгрузки в ФНС\r\nДата выгрузки в ФНС:";
            // 
            // listCancelReason
            // 
            this.listCancelReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listCancelReason.FormattingEnabled = true;
            this.listCancelReason.Location = new System.Drawing.Point(147, 33);
            this.listCancelReason.Name = "listCancelReason";
            this.listCancelReason.Size = new System.Drawing.Size(313, 21);
            this.listCancelReason.TabIndex = 11;
            this.listCancelReason.SelectedValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Причина отмены статуса:";
            // 
            // labelCancelReasonDate
            // 
            this.labelCancelReasonDate.AutoSize = true;
            this.labelCancelReasonDate.Location = new System.Drawing.Point(476, 36);
            this.labelCancelReasonDate.Name = "labelCancelReasonDate";
            this.labelCancelReasonDate.Size = new System.Drawing.Size(61, 13);
            this.labelCancelReasonDate.TabIndex = 13;
            this.labelCancelReasonDate.Text = "00.00.0000";
            // 
            // databaseBindingSource
            // 
            this.databaseBindingSource.DataSource = typeof(MnogodetLiteDB.Database);
            // 
            // labelCreationDate
            // 
            this.labelCreationDate.AutoSize = true;
            this.labelCreationDate.Location = new System.Drawing.Point(174, 9);
            this.labelCreationDate.Name = "labelCreationDate";
            this.labelCreationDate.Size = new System.Drawing.Size(61, 13);
            this.labelCreationDate.TabIndex = 15;
            this.labelCreationDate.Text = "00.00.0000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Дата создания:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Удостоверение номер:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Выдано:";
            // 
            // labelUdostDateExpireStatus
            // 
            this.labelUdostDateExpireStatus.AutoSize = true;
            this.labelUdostDateExpireStatus.Location = new System.Drawing.Point(335, 67);
            this.labelUdostDateExpireStatus.Name = "labelUdostDateExpireStatus";
            this.labelUdostDateExpireStatus.Size = new System.Drawing.Size(58, 13);
            this.labelUdostDateExpireStatus.TabIndex = 21;
            this.labelUdostDateExpireStatus.Text = "Истекает:";
            // 
            // labelUdostNumber
            // 
            this.labelUdostNumber.AutoSize = true;
            this.labelUdostNumber.Location = new System.Drawing.Point(140, 67);
            this.labelUdostNumber.Name = "labelUdostNumber";
            this.labelUdostNumber.Size = new System.Drawing.Size(67, 13);
            this.labelUdostNumber.TabIndex = 22;
            this.labelUdostNumber.Text = "0000000000";
            // 
            // labelUdostDateIssue
            // 
            this.labelUdostDateIssue.AutoSize = true;
            this.labelUdostDateIssue.Location = new System.Drawing.Point(268, 67);
            this.labelUdostDateIssue.Name = "labelUdostDateIssue";
            this.labelUdostDateIssue.Size = new System.Drawing.Size(61, 13);
            this.labelUdostDateIssue.TabIndex = 23;
            this.labelUdostDateIssue.Text = "00.00.0000";
            // 
            // labelUdostDateExpire
            // 
            this.labelUdostDateExpire.AutoSize = true;
            this.labelUdostDateExpire.Location = new System.Drawing.Point(399, 67);
            this.labelUdostDateExpire.Name = "labelUdostDateExpire";
            this.labelUdostDateExpire.Size = new System.Drawing.Size(61, 13);
            this.labelUdostDateExpire.TabIndex = 24;
            this.labelUdostDateExpire.Text = "00.00.0000";
            // 
            // buttonUdostEdit
            // 
            this.buttonUdostEdit.Location = new System.Drawing.Point(466, 61);
            this.buttonUdostEdit.Name = "buttonUdostEdit";
            this.buttonUdostEdit.Size = new System.Drawing.Size(71, 23);
            this.buttonUdostEdit.TabIndex = 25;
            this.buttonUdostEdit.Text = "Изменить";
            this.buttonUdostEdit.UseVisualStyleBackColor = true;
            this.buttonUdostEdit.Click += new System.EventHandler(this.buttonUdostEdit_Click);
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(32, 9);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(37, 13);
            this.labelId.TabIndex = 27;
            this.labelId.Text = "00000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "ID:";
            // 
            // FormFamily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 528);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonUdostEdit);
            this.Controls.Add(this.labelUdostDateExpire);
            this.Controls.Add(this.labelUdostDateIssue);
            this.Controls.Add(this.labelUdostNumber);
            this.Controls.Add(this.labelUdostDateExpireStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelCreationDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelCancelReasonDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listCancelReason);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonDeleteFamily);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonAddPerson);
            this.Controls.Add(this.editComment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelExpireDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridPersons);
            this.Name = "FormFamily";
            this.Text = "FormFamily";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFamily_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridPersons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelExpireDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editAddress;
        private System.Windows.Forms.TextBox editComment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAddPerson;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonDeleteFamily;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn f;
        private System.Windows.Forms.DataGridViewTextBoxColumn i;
        private System.Windows.Forms.DataGridViewTextBoxColumn o;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.ComboBox listCancelReason;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCancelReasonDate;
        private System.Windows.Forms.BindingSource databaseBindingSource;
        private System.Windows.Forms.Label labelCreationDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelUdostDateExpireStatus;
        private System.Windows.Forms.Label labelUdostNumber;
        private System.Windows.Forms.Label labelUdostDateIssue;
        private System.Windows.Forms.Label labelUdostDateExpire;
        private System.Windows.Forms.Button buttonUdostEdit;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label label9;
    }
}