
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
using System.Xml.Linq;

namespace SCD_CRUD
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();


        }



        
        

       
        Order order = new Order();
        long orid = 0;









        public void SetDataInGridView()
        {
            var db = new SCDEntities();
            var order = (from od in db.Orders





                          select new
                          {
                              OrderID = od.OrderID,
                              OrderItem = od.OrderItem,
                              OrderPrice = od.OrderPrice,
                              OrderDiscount = od.OrderDiscount,
                              OrderNetPrice = (od.OrderQty * od.OrderPrice )- od.OrderDiscount,
                              OrderQty = od.OrderQty,




                          }).ToList();

            dgvOrder.Rows.Clear();
            dgvOrder.DataSource = null;

            foreach (var od in order)
            {
                int RowIndex = dgvOrder.Rows.Add();

                dgvOrder.Rows[RowIndex].Cells["OrderID"].Value = od.OrderID;
                dgvOrder.Rows[RowIndex].Cells["OrderItem"].Value = od.OrderItem;
                dgvOrder.Rows[RowIndex].Cells["OrderPrice"].Value = od.OrderPrice;
                dgvOrder.Rows[RowIndex].Cells["OrderQty"].Value = od.OrderQty;
                dgvOrder.Rows[RowIndex].Cells["OrderNetPrice"].Value = od.OrderNetPrice;
                dgvOrder.Rows[RowIndex].Cells["OrderDiscount"].Value = od.OrderDiscount;




                dgvOrder.Update();
                dgvOrder.Refresh();
            }



        }

        private void Order_Load_1(object sender, EventArgs e)
        {
            SetDataInGridView();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtitem.Text = "";


            
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtitem.Enabled = true;

            btnClear.Enabled = true;
         
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void ClearData()
        {
            txtitem.Text = "";

           
         
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
          

   

           
            txtitem.Enabled = true;

            btnClear.Enabled = true;
           
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
               

                order.OrderItem = txtitem.Text.Trim();
                order.OrderQty = int.Parse(txtQty.Text.Trim());
                order.OrderDiscount = decimal.Parse( txtDiscount.Text.Trim());
                //order.OrderNetPrice = decimal.Parse(txtNetPrice.Text.Trim());
                order.OrderPrice = decimal.Parse(txtPrice.Text.Trim());
                


                using (var db = new SCDEntities())
                {
                    Model.Order order2 = db.Orders.Add(order);
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
              

                order.OrderItem = (txtitem.Text.Trim());

                order.OrderQty = int.Parse(txtQty.Text.Trim());
                order.OrderDiscount = decimal.Parse(txtDiscount.Text.Trim());
                //order.OrderNetPrice = decimal.Parse(txtNetPrice.Text.Trim());
                order.OrderPrice = decimal.Parse(txtPrice.Text.Trim());


                using (var db = new SCDEntities())
                {
                    var entry = db.Entry(order);

                    if (entry.State == EntityState.Detached)
                        db.Orders.Attach(order);

                    db.Entry(order).State = EntityState.Modified;
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

         
                txtitem.Enabled = true;


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
                        var entry = db.Entry(order);

                        if (entry.State == EntityState.Detached)
                            db.Orders.Attach(order);

                        db.Orders.Remove(order);
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearData();

            try
            {
                if (e.RowIndex >= 0)
                {
                    orid = long.Parse(dgvOrder.Rows[e.RowIndex].Cells["OrderID"].Value.ToString());

                    using (var ob = new SCDEntities())
                    {
                        order = ob.Orders.Where(x => x.OrderID == orid).FirstOrDefault();
                    }

                    txtitem.Text = dgvOrder.Rows[e.RowIndex].Cells["OrderItem"].Value.ToString();
                    txtPrice.Text = dgvOrder.Rows[e.RowIndex].Cells["OrderPrice"].Value.ToString();
                    txtQty.Text = dgvOrder.Rows[e.RowIndex].Cells["OrderQty"].Value.ToString();
                    //txtNetPrice.Text = dgvOrder.Rows[e.RowIndex].Cells["OrderNetPrice"].Value.ToString();
                    txtDiscount.Text = dgvOrder.Rows[e.RowIndex].Cells["OrderDiscount"].Value.ToString();





                    txtitem.Enabled = false;

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

        
    }
    }
