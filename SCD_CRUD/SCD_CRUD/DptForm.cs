using SCD_CRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCD_CRUD
{

    public partial class DptForm : Form
    {

        public DptForm()
        {
            InitializeComponent();

            DataTable DptStatus = new DataTable();
            DptStatus.Columns.Add("Id");
            DptStatus.Columns.Add("Name");

            DptStatus.AcceptChanges();
            dr = DptStatus.NewRow();

            dr["Id"] = 2;
            dr["Name"] = "Disable";
            DptStatus.Rows.Add(dr);
            dr = DptStatus.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Active";
            DptStatus.Rows.Add(dr);
            DptStatus.AcceptChanges();

            comboStatus.DataSource = null;
            comboStatus.DisplayMember = "Name";
            comboStatus.ValueMember = "Id";
            comboStatus.DataSource = DptStatus;
            comboStatus.SelectedIndex = 1;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string img_location = String.Empty;


        DataRow dr;
        Department department = new Department();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                department.DptName = textName.Text.Trim();

                department.DptStatus = short.Parse(comboStatus.SelectedValue.ToString());



                using (var db = new SCDEntities())
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                }
                ClearData();
                SetDataInGridView();
                MessageBox.Show("record save successfully");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                department.DptName = textName.Text.Trim();



                department.DptStatus = short.Parse(comboStatus.SelectedValue.ToString());



                using (var db = new SCDEntities())
                {
                    var entry = db.Entry(department);

                    if (entry.State == EntityState.Detached)
                        db.Departments.Attach(department);

                    db.Entry(department).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ClearData();
                SetDataInGridView();
                MessageBox.Show("record save successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            comboStatus.Enabled = true;
            textName.Enabled = true;


            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnClear.Enabled = true;


            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("are you sure you want to delete ?", "Delete ? ", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    using (var db = new SCDEntities())
                    {
                        var entry = db.Entry(department);

                        if (entry.State == EntityState.Detached)
                            db.Departments.Attach(department);

                        db.Departments.Remove(department);
                        db.SaveChanges();
                    }

                    ClearData();
                    SetDataInGridView();

                    MessageBox.Show("Record deleted successfully", "deleted", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SetDataInGridView()
        {

            var db = new SCDEntities();
            var studentt = (from dpt in db.Departments


                            where dpt.DptStatus == 1

                            select new
                            {

                                DptID = dpt.DptID,
                                DptName = dpt.DptName,
                                DptStatus = dpt.DptStatus


                            }).ToList();

            dgvDpt.Rows.Clear();
            dgvDpt.DataSource = null;

            foreach (var dpt in studentt)
            {
                int RowIndex = dgvDpt.Rows.Add();

                dgvDpt.Rows[RowIndex].Cells["DptID"].Value = dpt.DptID;
                dgvDpt.Rows[RowIndex].Cells["DptName"].Value = dpt.DptName;



                if (dpt.DptStatus == 1)
                    dgvDpt.Rows[RowIndex].Cells["DptStatus"].Value = "Active";
                else
                    dgvDpt.Rows[RowIndex].Cells["DptStatus"].Value = "Disable";

            }

            dgvDpt.Update();
            dgvDpt.Refresh();
        }

        private void ClearData()
        {
            txtName.Text = "";

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            comboStatus.SelectedIndex = 0;

            comboStatus.Enabled = true;
            textName.Enabled = true;

            btnClear.Enabled = true;

            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        long dptId = 0;


        private void DptForm_Load(object sender, EventArgs e)
        {
            SetDataInGridView();
        }

        private void dgvDpt_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();

            try
            {
                if (e.RowIndex >= 0)
                {
                    dptId = long.Parse(dgvDpt.Rows[e.RowIndex].Cells["DptID"].Value.ToString());

                    using (var db = new SCDEntities())
                    {
                        department = db.Departments.Where(x => x.DptID == dptId).FirstOrDefault();
                    }

                    txtName.Text = dgvDpt.Rows[e.RowIndex].Cells["DptName"].Value.ToString();


                    //comboStatus.SelectedValue =
                    //int.Parse(dgvStudent.Rows[e.RowIndex].Cells["StudentStatus"].Value.ToString());



                    if (dgvDpt.Rows[e.RowIndex].Cells["DptStatus"].Value.ToString() == "Active")
                        comboStatus.SelectedIndex = 1;

                    else
                        comboStatus.SelectedIndex = 0;


                    comboStatus.Enabled = false;
                    textName.Enabled = false;

                    comboStatus.Enabled = false;
                    btnUpdate.Enabled = false;

                    btnAdd.Enabled = false;
                    btnClear.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textName.Text = "";

        }

        private void textName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            this.Hide();
            studentForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            roleForm roleForm = new roleForm();
            this.Hide();
            roleForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
    }
}
