
namespace MnogodetLiteDB {
    partial class FormExportFNS {
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
            this.editMessages = new System.Windows.Forms.TextBox();
            this.buttonExceptions = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // editMessages
            // 
            this.editMessages.Location = new System.Drawing.Point(12, 123);
            this.editMessages.Multiline = true;
            this.editMessages.Name = "editMessages";
            this.editMessages.Size = new System.Drawing.Size(420, 289);
            this.editMessages.TabIndex = 0;
            // 
            // buttonExceptions
            // 
            this.buttonExceptions.Location = new System.Drawing.Point(12, 94);
            this.buttonExceptions.Name = "buttonExceptions";
            this.buttonExceptions.Size = new System.Drawing.Size(163, 23);
            this.buttonExceptions.TabIndex = 2;
            this.buttonExceptions.Text = "Исключения";
            this.buttonExceptions.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(102, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(110, 22);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Дата выгрузки";
            // 
            // FormExportFNS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 424);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.buttonExceptions);
            this.Controls.Add(this.editMessages);
            this.Name = "FormExportFNS";
            this.Text = "FormExportFNS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox editMessages;
        private System.Windows.Forms.Button buttonExceptions;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
    }
}