using SCD_CRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SCD_CRUD
{
    public partial class roleForm : Form
    {
        public roleForm()
        {
            InitializeComponent();

            DataTable RoleStatus = new DataTable();
            RoleStatus.Columns.Add("Id");
            RoleStatus.Columns.Add("Name");

            RoleStatus.AcceptChanges();
            dr = RoleStatus.NewRow();

            dr["Id"] = 2;
            dr["Name"] = "Disable";
            RoleStatus.Rows.Add(dr);
            dr = RoleStatus.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Active";
            RoleStatus.Rows.Add(dr);
            RoleStatus.AcceptChanges();

            comboStatus.DataSource = null;
            comboStatus.DisplayMember = "Name";
            comboStatus.ValueMember = "Id";
            comboStatus.DataSource = RoleStatus;
            comboStatus.SelectedIndex = 1;

        }

        DataRow dr;
        Role role = new Role();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                role.RoleName = txtRname.Text.Trim();

                role.RoleStatus = short.Parse(comboStatus.SelectedValue.ToString());



                using (var db = new SCDEntities())
                {
                    db.Roles.Add(role);
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

                role.RoleName = txtRname.Text.Trim();

                role.RoleStatus = short.Parse(comboStatus.SelectedValue.ToString());



                using (var db = new SCDEntities())
                {
                    var entry = db.Entry(role);

                    if (entry.State == EntityState.Detached)
                        db.Roles.Attach(role);

                    db.Entry(role).State = EntityState.Modified;
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
            txtRname.Enabled = true;


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
                        var entry = db.Entry(role);

                        if (entry.State == EntityState.Detached)
                            db.Roles.Attach(role);

                        db.Roles.Remove(role);
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
            var studentt = (from rs in db.Roles


                                // where dpt.DptStatus == 1

                            select new
                            {

                                RoleID = rs.RoleID,
                                RoleName = rs.RoleName,
                                RoleStatus = rs.RoleStatus


                            }).ToList();

            dgvRole.Rows.Clear();
            dgvRole.DataSource = null;

            foreach (var rs in studentt)
            {
                int RowIndex = dgvRole.Rows.Add();

                dgvRole.Rows[RowIndex].Cells["RoleID"].Value = rs.RoleID;
                dgvRole.Rows[RowIndex].Cells["RoleName"].Value = rs.RoleName;



                if (rs.RoleStatus == 1)
                    dgvRole.Rows[RowIndex].Cells["RoleStatus"].Value = "Active";
                else
                    dgvRole.Rows[RowIndex].Cells["RoleStatus"].Value = "Disable";

            }
            dgvRole.Refresh();
            dgvRole.Update();

            dgvRole.Refresh();
        }

        private void ClearData()
        {
            txtRname.Text = "";

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            comboStatus.SelectedIndex = 0;

            comboStatus.Enabled = true;
            txtRname.Enabled = true;

            btnClear.Enabled = true;

            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }


        private void roleForm_Click(object sender, EventArgs e)
        {
            SetDataInGridView();
        }

        long RsId = 0;
        private void dgvRole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    RsId = long.Parse(dgvRole.Rows[e.RowIndex].Cells["RoleID"].Value.ToString());

                    using (var db = new SCDEntities())
                    {
                        role = db.Roles.Where(x => x.RoleID == RsId).FirstOrDefault();
                    }

                    txtRname.Text = dgvRole.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();


                    if (dgvRole.Rows[e.RowIndex].Cells["RoleStatus"].Value.ToString() == "Active")
                        comboStatus.SelectedIndex = 1;

                    else
                        comboStatus.SelectedIndex = 0;


                    comboStatus.Enabled = false;
                    txtRname.Enabled = false;

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
            txtRname.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            this.Hide();
            studentForm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DptForm dptForm = new DptForm();
            this.Hide();
            dptForm.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void roleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
