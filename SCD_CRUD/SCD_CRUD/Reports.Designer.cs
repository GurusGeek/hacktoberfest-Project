namespace SCD_CRUD
{
    partial class Reports
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnOrderReport = new System.Windows.Forms.Button();
            this.BtnEmployeeReport = new System.Windows.Forms.Button();
            this.BtnDepartmentReport = new System.Windows.Forms.Button();
            this.BtnStudentReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.00419F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.99581F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.41791F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.58209F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1193, 737);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.flowLayoutPanel1.Controls.Add(this.BtnOrderReport);
            this.flowLayoutPanel1.Controls.Add(this.BtnEmployeeReport);
            this.flowLayoutPanel1.Controls.Add(this.BtnDepartmentReport);
            this.flowLayoutPanel1.Controls.Add(this.BtnStudentReport);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 123);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(173, 611);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // BtnOrderReport
            // 
            this.BtnOrderReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOrderReport.Location = new System.Drawing.Point(3, 3);
            this.BtnOrderReport.Name = "BtnOrderReport";
            this.BtnOrderReport.Size = new System.Drawing.Size(170, 55);
            this.BtnOrderReport.TabIndex = 1;
            this.BtnOrderReport.Text = "Order";
            this.BtnOrderReport.UseVisualStyleBackColor = true;
            // 
            // BtnEmployeeReport
            // 
            this.BtnEmployeeReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmployeeReport.Location = new System.Drawing.Point(3, 64);
            this.BtnEmployeeReport.Name = "BtnEmployeeReport";
            this.BtnEmployeeReport.Size = new System.Drawing.Size(170, 55);
            this.BtnEmployeeReport.TabIndex = 2;
            this.BtnEmployeeReport.Text = "Employee";
            this.BtnEmployeeReport.UseVisualStyleBackColor = true;
            // 
            // BtnDepartmentReport
            // 
            this.BtnDepartmentReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDepartmentReport.Location = new System.Drawing.Point(3, 125);
            this.BtnDepartmentReport.Name = "BtnDepartmentReport";
            this.BtnDepartmentReport.Size = new System.Drawing.Size(170, 55);
            this.BtnDepartmentReport.TabIndex = 3;
            this.BtnDepartmentReport.Text = "Department";
            this.BtnDepartmentReport.UseVisualStyleBackColor = true;
            // 
            // BtnStudentReport
            // 
            this.BtnStudentReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStudentReport.Location = new System.Drawing.Point(3, 186);
            this.BtnStudentReport.Name = "BtnStudentReport";
            this.BtnStudentReport.Size = new System.Drawing.Size(170, 55);
            this.BtnStudentReport.TabIndex = 4;
            this.BtnStudentReport.Text = "Student";
            this.BtnStudentReport.UseVisualStyleBackColor = true;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 737);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Reports";
            this.Text = "Reports";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BtnOrderReport;
        private System.Windows.Forms.Button BtnEmployeeReport;
        private System.Windows.Forms.Button BtnDepartmentReport;
        private System.Windows.Forms.Button BtnStudentReport;
    }
}