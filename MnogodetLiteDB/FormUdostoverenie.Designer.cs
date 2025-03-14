
namespace MnogodetLiteDB {
    partial class FormUdostoverenie {
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
            this.editNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.editDateIssue = new System.Windows.Forms.DateTimePicker();
            this.editDateExpire = new System.Windows.Forms.DateTimePicker();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // editNumber
            // 
            this.editNumber.Location = new System.Drawing.Point(89, 12);
            this.editNumber.Name = "editNumber";
            this.editNumber.Size = new System.Drawing.Size(141, 20);
            this.editNumber.TabIndex = 0;
            this.editNumber.TextChanged += new System.EventHandler(this.DataChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дата выдачи";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Истекает";
            // 
            // editDateIssue
            // 
            this.editDateIssue.Location = new System.Drawing.Point(89, 38);
            this.editDateIssue.Name = "editDateIssue";
            this.editDateIssue.Size = new System.Drawing.Size(141, 20);
            this.editDateIssue.TabIndex = 21;
            this.editDateIssue.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // editDateExpire
            // 
            this.editDateExpire.Location = new System.Drawing.Point(89, 64);
            this.editDateExpire.Name = "editDateExpire";
            this.editDateExpire.Size = new System.Drawing.Size(141, 20);
            this.editDateExpire.TabIndex = 22;
            this.editDateExpire.ValueChanged += new System.EventHandler(this.DataChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(11, 90);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(89, 23);
            this.buttonSave.TabIndex = 23;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(141, 90);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(89, 23);
            this.buttonCancel.TabIndex = 24;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormUdostoverenie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 119);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.editDateExpire);
            this.Controls.Add(this.editDateIssue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editNumber);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUdostoverenie";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FormUdostoverenie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUdostoverenie_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox editNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker editDateIssue;
        private System.Windows.Forms.DateTimePicker editDateExpire;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}