
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
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();

            DataTable StudentStatus = new DataTable();
            StudentStatus.Columns.Add("Id");
            StudentStatus.Columns.Add("Name");

            StudentStatus.AcceptChanges();
            dr = StudentStatus.NewRow();

            dr["Id"] = 2;
            dr["Name"] = "Disable";
            StudentStatus.Rows.Add(dr);
            dr = StudentStatus.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Active";
            StudentStatus.Rows.Add(dr);
            StudentStatus.AcceptChanges();

            comboStatus.DataSource = null;
            comboStatus.DisplayMember = "Name";
            comboStatus.ValueMember = "Id";
            comboStatus.DataSource = StudentStatus;
            comboStatus.SelectedIndex = 1;
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            SetDataInGridView();
            getDepartment();
        }

        string img_location = String.Empty;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                Image imageFile;
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "png files(*.png)|*.png|jpg files(*.jpg)" +
                   "|*.jpg|All files(*.*)|*.*"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageFile = Image.FromFile(dialog.FileName);
                    int imgHeight = imageFile.Height;
                    if (imgHeight > 350)
                        MessageBox.Show("Maximum Image can be 350x350" +
                            " Image", "Image size is too large..!!"
                            , MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    else
                    {
                        img_location = dialog.FileName.ToString();
                        picStudent.ImageLocation = img_location;
                    }
                }
                dialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        DataRow dr;
        Student student = new Student();
        long stdId = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] image = null;
                if (img_location != string.Empty)
                {

                    FileStream stream = new FileStream(img_location, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    image = brs.ReadBytes((int)stream.Length);

                }
                else
                {
                    Image img = picStudent.Image;
                    ImageConverter converter = new ImageConverter();

                    image = (byte[])converter.ConvertTo(img, typeof(byte[]));
                }

                student.StudentName = txtName.Text.Trim();
                student.StudentAge = int.Parse(comboDpt.SelectedValue.ToString());
                student.StudentCity = txtCity.Text.Trim();
                student.StudentAddress = txtAddress.Text.Trim();


                student.DepartmentID = int.Parse(comboDpt.SelectedValue.ToString());
                student.StudentStatus = short.Parse(comboStatus.SelectedValue.ToString());
                student.StudentImage = image;


                using (var db = new SCDEntities())
                {
                    db.Students.Add(student);
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
                byte[] image = null;
                if (img_location != string.Empty)
                {

                    FileStream stream = new FileStream(img_location, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    image = brs.ReadBytes((int)stream.Length);

                }
                else
                {
                    Image img = picStudent.Image;
                    ImageConverter converter = new ImageConverter();

                    image = (byte[])converter.ConvertTo(img, typeof(byte[]));
                }

                student.StudentName = txtName.Text.Trim();
                student.StudentAge = int.Parse(comboDpt.SelectedValue.ToString());
                student.StudentCity = txtCity.Text.Trim();
                student.StudentAddress = txtAddress.Text.Trim();


                student.DepartmentID = int.Parse(comboDpt.SelectedValue.ToString());
                student.StudentStatus = short.Parse(comboStatus.SelectedValue.ToString());
                student.StudentImage = image;


                using (var db = new SCDEntities())
                {
                    var entry = db.Entry(student);

                    if (entry.State == EntityState.Detached)
                        db.Students.Attach(student);

                    db.Entry(student).State = EntityState.Modified;
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
            txtName.Enabled = true;


            comboDpt.Enabled = true;
            picStudent.Enabled = true;


            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnClear.Enabled = true;
            btnBrowse.Enabled = true;

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
                        var entry = db.Entry(student);

                        if (entry.State == EntityState.Detached)
                            db.Students.Attach(student);

                        db.Students.Remove(student);
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

        public void getDepartment()
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();

            DataRow dr;

            dataTable.Columns.Add("DptID");
            dataTable.Columns.Add("DptName");

            using (var db = new SCDEntities())
            {
                var departments = db.Departments.ToList<Department>();

                foreach (Department dpt in departments)
                {

                    dr = dataTable.NewRow();

                    dr["DptID"] = dpt.DptID;
                    dr["DptName"] = dpt.DptName;
                    dataTable.Rows.Add(dr);
                }
            }

            dataTable.AcceptChanges();

            comboDpt.DataSource = null;

            comboDpt.ValueMember = "DptID";
            comboDpt.DisplayMember = "DptName";
            comboDpt.DataSource = dataTable;


        }
        public void SetDataInGridView()
        {
            var db = new SCDEntities();
            var studentt = (from std in db.Students

                            join dpt in db.Departments

                            on std.DepartmentID equals dpt.DptID


                            select new
                            {
                                StudentId = std.StudentID,
                                StudentName = std.StudentName,
                                StudentAge = std.StudentAge,
                                StudentCity = std.StudentCity,
                                StudentAddress = std.StudentAddress,
                                StudentStatus = std.StudentStatus,

                                DptID = dpt.DptID,
                                DptName = dpt.DptName,


                            }).ToList();

            dgvStudent.Rows.Clear();
            dgvStudent.DataSource = null;

            foreach (var std in studentt)
            {
                int RowIndex = dgvStudent.Rows.Add();

                dgvStudent.Rows[RowIndex].Cells["StudentId"].Value = std.StudentId;
                dgvStudent.Rows[RowIndex].Cells["StudentName"].Value = std.StudentName;
                dgvStudent.Rows[RowIndex].Cells["StudentAge"].Value = std.StudentAge;
                dgvStudent.Rows[RowIndex].Cells["StudentAddress"].Value = std.StudentAddress;
                dgvStudent.Rows[RowIndex].Cells["StudentCity"].Value = std.StudentCity;
                dgvStudent.Rows[RowIndex].Cells["DptID"].Value = std.DptID;
                dgvStudent.Rows[RowIndex].Cells["DptName"].Value = std.DptName;


                if (std.StudentStatus == 1)
                    dgvStudent.Rows[RowIndex].Cells["StudentStatus"].Value = "Active";
                else
                    dgvStudent.Rows[RowIndex].Cells["StudentStatus"].Value = "Disable";

            }

            dgvStudent.Update();
            dgvStudent.Refresh();
        }

        private void ClearData()
        {
            txtName.Text = "";

            picStudent.Image = Properties.Resources.icons8_no_image_100;
            img_location = string.Empty;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            comboStatus.SelectedIndex = 0;

            comboDpt.SelectedIndex = 0;

            comboStatus.Enabled = true;
            comboDpt.Enabled = true;
            txtName.Enabled = true;


            picStudent.Enabled = true;

            btnClear.Enabled = true;
            btnBrowse.Enabled = true;
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();

            try
            {
                if (e.RowIndex >= 0)
                {
                    stdId = long.Parse(dgvStudent.Rows[e.RowIndex].Cells["StudentId"].Value.ToString());

                    using (var db = new SCDEntities())
                    {
                        student = db.Students.Where(x => x.StudentID == stdId).FirstOrDefault();
                    }

                    txtName.Text = dgvStudent.Rows[e.RowIndex].Cells["StudentName"].Value.ToString();
                    txtAge.Text = dgvStudent.Rows[e.RowIndex].Cells["StudentAge"].Value.ToString();

                    comboDpt.SelectedValue =
                        int.Parse(dgvStudent.Rows[e.RowIndex].Cells["DptID"].Value.ToString());
                    //comboStatus.SelectedValue =
                    //int.Parse(dgvStudent.Rows[e.RowIndex].Cells["StudentStatus"].Value.ToString());



                    if (dgvStudent.Rows[e.RowIndex].Cells["StudentStatus"].Value.ToString() == "Active")
                        comboStatus.SelectedIndex = 1;

                    else
                        comboStatus.SelectedIndex = 0;


                    comboStatus.Enabled = false;
                    txtName.Enabled = false;


                    comboDpt.Enabled = false;
                    picStudent.Enabled = false;
                    comboStatus.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnBrowse.Enabled = false;
                    btnAdd.Enabled = false;
                    btnClear.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    Image img = Properties.Resources.icons8_no_image_100;
                    MemoryStream ms = new MemoryStream(student.StudentImage);
                    img = Image.FromStream(ms);
                    picStudent.Image = img;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click1(object sender, EventArgs e)
        {
            DptForm dptForm = new DptForm();
            this.Hide();
            dptForm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
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

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void picStudent_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
