
namespace MnogodetLiteDB {
    partial class FormPerson {
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
            this.label1 = new System.Windows.Forms.Label();
            this.editF = new System.Windows.Forms.TextBox();
            this.editI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.editBirthDate = new System.Windows.Forms.DateTimePicker();
            this.gridDocuments = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuedPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuedCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.editType = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.editGender = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // editF
            // 
            this.editF.Location = new System.Drawing.Point(70, 44);
            this.editF.Name = "editF";
            this.editF.Size = new System.Drawing.Size(128, 20);
            this.editF.TabIndex = 1;
            this.editF.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // editI
            // 
            this.editI.Location = new System.Drawing.Point(70, 72);
            this.editI.Name = "editI";
            this.editI.Size = new System.Drawing.Size(128, 20);
            this.editI.TabIndex = 3;
            this.editI.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Фамилия";
            // 
            // editO
            // 
            this.editO.Location = new System.Drawing.Point(70, 100);
            this.editO.Name = "editO";
            this.editO.Size = new System.Drawing.Size(128, 20);
            this.editO.TabIndex = 5;
            this.editO.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отчество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Дата";
            // 
            // editBirthDate
            // 
            this.editBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.editBirthDate.Location = new System.Drawing.Point(71, 155);
            this.editBirthDate.Name = "editBirthDate";
            this.editBirthDate.Size = new System.Drawing.Size(128, 20);
            this.editBirthDate.TabIndex = 8;
            this.editBirthDate.Value = new System.DateTime(2021, 10, 11, 0, 0, 0, 0);
            this.editBirthDate.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // gridDocuments
            // 
            this.gridDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDocuments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.number,
            this.issuedDate,
            this.issuedPlace,
            this.issuedCode});
            this.gridDocuments.Location = new System.Drawing.Point(204, 12);
            this.gridDocuments.Name = "gridDocuments";
            this.gridDocuments.RowTemplate.Height = 24;
            this.gridDocuments.Size = new System.Drawing.Size(931, 274);
            this.gridDocuments.TabIndex = 9;
            this.gridDocuments.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridDocuments_CellBeginEdit);
            this.gridDocuments.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDocuments_CellValueChanged);
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.type.HeaderText = "Вид";
            this.type.Name = "type";
            // 
            // number
            // 
            this.number.HeaderText = "Номер";
            this.number.Name = "number";
            this.number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // issuedDate
            // 
            this.issuedDate.HeaderText = "Дата выдачи";
            this.issuedDate.Name = "issuedDate";
            this.issuedDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.issuedDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // issuedPlace
            // 
            this.issuedPlace.HeaderText = "Место выдачи";
            this.issuedPlace.Name = "issuedPlace";
            this.issuedPlace.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.issuedPlace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // issuedCode
            // 
            this.issuedCode.HeaderText = "Код места выдачи";
            this.issuedCode.Name = "issuedCode";
            this.issuedCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.issuedCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "рождения";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(5, 234);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(193, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(5, 205);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(193, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить и закрыть";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Род";
            // 
            // editType
            // 
            this.editType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editType.FormattingEnabled = true;
            this.editType.Items.AddRange(new object[] {
            "Мать",
            "Отец",
            "Ребенок"});
            this.editType.Location = new System.Drawing.Point(70, 12);
            this.editType.Name = "editType";
            this.editType.Size = new System.Drawing.Size(128, 21);
            this.editType.TabIndex = 14;
            this.editType.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(5, 263);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(193, 23);
            this.buttonDelete.TabIndex = 15;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Пол";
            // 
            // editGender
            // 
            this.editGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editGender.FormattingEnabled = true;
            this.editGender.Items.AddRange(new object[] {
            "Женский",
            "Мужской"});
            this.editGender.Location = new System.Drawing.Point(70, 128);
            this.editGender.Name = "editGender";
            this.editGender.Size = new System.Drawing.Size(128, 21);
            this.editGender.TabIndex = 17;
            this.editGender.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // FormPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 298);
            this.Controls.Add(this.editGender);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.editType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridDocuments);
            this.Controls.Add(this.editBirthDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.editO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editF);
            this.Controls.Add(this.label1);
            this.Name = "FormPerson";
            this.Text = "FormPerson";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPerson_FormClosing);
            this.Load += new System.EventHandler(this.FormPerson_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editF;
        private System.Windows.Forms.TextBox editI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker editBirthDate;
        private System.Windows.Forms.DataGridView gridDocuments;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox editType;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridViewComboBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuedPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuedCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox editGender;
    }
}