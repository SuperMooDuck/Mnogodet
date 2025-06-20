
namespace MnogodetLiteDB {
    partial class FormMain {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.gridPersons = new System.Windows.Forms.DataGridView();
            this.familyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.i = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.o = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonFamiliesProblems = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.editF = new System.Windows.Forms.TextBox();
            this.editI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.editDocumentNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.editAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAddFamily = new System.Windows.Forms.Button();
            this.buttonDictionaries = new System.Windows.Forms.Button();
            this.QueryBtn = new System.Windows.Forms.Button();
            this.buttonDoubles = new System.Windows.Forms.Button();
            this.buttonParameters = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).BeginInit();
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
            this.familyId,
            this.f,
            this.i,
            this.o,
            this.birthDate,
            this.address});
            this.gridPersons.Location = new System.Drawing.Point(136, 12);
            this.gridPersons.Name = "gridPersons";
            this.gridPersons.ReadOnly = true;
            this.gridPersons.RowTemplate.Height = 24;
            this.gridPersons.Size = new System.Drawing.Size(621, 585);
            this.gridPersons.TabIndex = 0;
            this.gridPersons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridPersons_MouseDoubleClick);
            // 
            // familyId
            // 
            this.familyId.HeaderText = "familyId";
            this.familyId.Name = "familyId";
            this.familyId.ReadOnly = true;
            this.familyId.Visible = false;
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
            this.birthDate.Width = 150;
            // 
            // address
            // 
            this.address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.address.HeaderText = "Адрес";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // buttonFamiliesProblems
            // 
            this.buttonFamiliesProblems.Location = new System.Drawing.Point(12, 337);
            this.buttonFamiliesProblems.Name = "buttonFamiliesProblems";
            this.buttonFamiliesProblems.Size = new System.Drawing.Size(118, 23);
            this.buttonFamiliesProblems.TabIndex = 4;
            this.buttonFamiliesProblems.Text = "Ошибки в семьях";
            this.buttonFamiliesProblems.UseVisualStyleBackColor = true;
            this.buttonFamiliesProblems.Click += new System.EventHandler(this.buttonFamiliesProblems_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Гражд. без док-ов";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Фамилия:";
            // 
            // editF
            // 
            this.editF.Location = new System.Drawing.Point(12, 26);
            this.editF.Name = "editF";
            this.editF.Size = new System.Drawing.Size(118, 20);
            this.editF.TabIndex = 9;
            this.editF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchEdit_KeyDown);
            // 
            // editI
            // 
            this.editI.Location = new System.Drawing.Point(12, 68);
            this.editI.Name = "editI";
            this.editI.Size = new System.Drawing.Size(118, 20);
            this.editI.TabIndex = 11;
            this.editI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchEdit_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Имя:";
            // 
            // editO
            // 
            this.editO.Location = new System.Drawing.Point(12, 110);
            this.editO.Name = "editO";
            this.editO.Size = new System.Drawing.Size(118, 20);
            this.editO.TabIndex = 13;
            this.editO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchEdit_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Отчество:";
            // 
            // editDocumentNumber
            // 
            this.editDocumentNumber.Location = new System.Drawing.Point(12, 204);
            this.editDocumentNumber.Name = "editDocumentNumber";
            this.editDocumentNumber.Size = new System.Drawing.Size(118, 20);
            this.editDocumentNumber.TabIndex = 15;
            this.editDocumentNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchEdit_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Номер документа:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(12, 237);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(118, 23);
            this.buttonSearch.TabIndex = 16;
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // editAddress
            // 
            this.editAddress.Location = new System.Drawing.Point(12, 156);
            this.editAddress.Name = "editAddress";
            this.editAddress.Size = new System.Drawing.Size(118, 20);
            this.editAddress.TabIndex = 18;
            this.editAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchEdit_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Адрес:";
            // 
            // buttonAddFamily
            // 
            this.buttonAddFamily.Location = new System.Drawing.Point(12, 284);
            this.buttonAddFamily.Name = "buttonAddFamily";
            this.buttonAddFamily.Size = new System.Drawing.Size(118, 23);
            this.buttonAddFamily.TabIndex = 23;
            this.buttonAddFamily.Text = "Добавить семью";
            this.buttonAddFamily.UseVisualStyleBackColor = true;
            this.buttonAddFamily.Click += new System.EventHandler(this.buttonAddFamily_Click);
            // 
            // buttonDictionaries
            // 
            this.buttonDictionaries.Location = new System.Drawing.Point(12, 561);
            this.buttonDictionaries.Name = "buttonDictionaries";
            this.buttonDictionaries.Size = new System.Drawing.Size(118, 37);
            this.buttonDictionaries.TabIndex = 25;
            this.buttonDictionaries.Text = "Редактор справочников";
            this.buttonDictionaries.UseVisualStyleBackColor = true;
            this.buttonDictionaries.Click += new System.EventHandler(this.buttonDictionaries_Click);
            // 
            // QueryBtn
            // 
            this.QueryBtn.Location = new System.Drawing.Point(12, 449);
            this.QueryBtn.Name = "QueryBtn";
            this.QueryBtn.Size = new System.Drawing.Size(118, 39);
            this.QueryBtn.TabIndex = 26;
            this.QueryBtn.Text = "Запросы и выгрузки";
            this.QueryBtn.UseVisualStyleBackColor = true;
            this.QueryBtn.Click += new System.EventHandler(this.QueryBtn_Click);
            // 
            // buttonDoubles
            // 
            this.buttonDoubles.Location = new System.Drawing.Point(12, 395);
            this.buttonDoubles.Name = "buttonDoubles";
            this.buttonDoubles.Size = new System.Drawing.Size(118, 23);
            this.buttonDoubles.TabIndex = 27;
            this.buttonDoubles.Text = "Дубли";
            this.buttonDoubles.UseVisualStyleBackColor = true;
            this.buttonDoubles.Click += new System.EventHandler(this.buttonDoubles_Click);
            // 
            // buttonParameters
            // 
            this.buttonParameters.Location = new System.Drawing.Point(12, 518);
            this.buttonParameters.Name = "buttonParameters";
            this.buttonParameters.Size = new System.Drawing.Size(118, 37);
            this.buttonParameters.TabIndex = 28;
            this.buttonParameters.Text = "Редактор параметров";
            this.buttonParameters.UseVisualStyleBackColor = true;
            this.buttonParameters.Click += new System.EventHandler(this.buttonParameters_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 609);
            this.Controls.Add(this.buttonParameters);
            this.Controls.Add(this.buttonDoubles);
            this.Controls.Add(this.QueryBtn);
            this.Controls.Add(this.buttonDictionaries);
            this.Controls.Add(this.buttonAddFamily);
            this.Controls.Add(this.editAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.editDocumentNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.editO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editF);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonFamiliesProblems);
            this.Controls.Add(this.gridPersons);
            this.MinimumSize = new System.Drawing.Size(785, 648);
            this.Name = "FormMain";
            this.Text = "Многодетные семьи";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonFamiliesProblems;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editF;
        private System.Windows.Forms.TextBox editI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editDocumentNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox editAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAddFamily;
        private System.Windows.Forms.DataGridViewTextBoxColumn familyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn f;
        private System.Windows.Forms.DataGridViewTextBoxColumn i;
        private System.Windows.Forms.DataGridViewTextBoxColumn o;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.Button buttonDictionaries;
        private System.Windows.Forms.Button QueryBtn;
        private System.Windows.Forms.Button buttonDoubles;
        private System.Windows.Forms.Button buttonParameters;
        public System.Windows.Forms.DataGridView gridPersons;
    }
}

