﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
namespace DockSample
{
    public partial class Form_SHGB : DockContent
    {
        SqlConnection con;
        public static int hwnd = 0;
        //接收信息事件委托
        public delegate void DataArrivalEventHandler(string msg);
        //事件对象
        public event DataArrivalEventHandler DataArrivalEvent;
        public Form_SHGB()
        {
            InitializeComponent();
            dateTimeInput1.Value = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
            dateTimeInput2.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString());
            con = new SqlConnection(MainForm.connetstring);
        }

        private void Form_JCBJ_Load(object sender, EventArgs e)
        {
            hwnd = (int)this.Handle;
            Queue_data();
        }
        private void Queue_data()
        {

            try
            {
                string d1 = dateTimeInput1.Value.ToString();
                string d2 = dateTimeInput2.Value.ToString();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string str = "select 维修编号,接修日期,修品大类,修品小类,修品型号,修品品牌,修品SN1,规格参数,故障描述,外观,优先级,技术员,预计积分,预付款,维修报价,维修状态,维修情况,客户编号,业务员,预约日期,完工日期,返修次数,送修标志 from J_维修处理表 where  接修日期 between '" + d1 + "' and '" + d2 + "' and 当前状态=5 ";

                SqlDataAdapter da = new SqlDataAdapter(str, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewX1.DataSource = dt;
                //this.dataGridViewX1.Columns["序号"].Visible = false;
                this.dataGridViewX1.Columns["接修日期"].DefaultCellStyle.Format = "yyyy-MM-dd";
                //this.dataGridViewX1.Columns["入库编号"].Width = 110;
                //this.dataGridViewX1.Columns["配件类别"].Width = 70;
                //this.dataGridViewX1.Columns["配件编号"].Width = 70;
                //this.dataGridViewX1.Columns["存储位置"].Width = 100;
                //this.dataGridViewX1.Columns["规格型号"].Width = 100;
                //this.dataGridViewX1.Columns["入库日期"].Width = 110;
                //this.dataGridViewX1.Columns["入库数量"].Width = 60;
                //this.dataGridViewX1.Columns["计量单位"].Width = 40;
                //this.dataGridViewX1.Columns["购买单价"].Width = 60;
                //this.dataGridViewX1.Columns["销售单价"].Width = 60;
                //this.dataGridViewX1.Columns["供货商家"].Width = 100;
                //this.dataGridViewX1.Columns["入库人员"].Width = 40;


                //如果父窗体已注册了自定义事件
                if (DataArrivalEvent != null)
                {
                    DataArrivalEvent(dt.Rows.Count.ToString());
                }
            }
            catch
            {
            }
            con.Close();
        }
        

        private void buttonX1_Click(object sender, EventArgs e)
        {
            dateTimeInput1.Value = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
            dateTimeInput2.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString());
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Queue_data();
        }

        private void Form_XPZJ_FormClosed(object sender, FormClosedEventArgs e)
        {
            hwnd = 0;
        }

        private void dataGridViewX1_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
            this.dataGridViewX1.ClearSelection();
            this.dataGridViewX1.Rows[e.RowIndex].Selected = true;
        }

        private void 质检通过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认通过审核吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (this.dataGridViewX1.SelectedRows.Count > 0)
                {
                    try
                    {
                        string str2 = this.dataGridViewX1.SelectedRows[0].Cells["维修编号"].Value.ToString();
                        //string sxbz = this.dataGridViewX1.SelectedRows[0].Cells["送修标志"].Value.ToString();
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        //string str3 = this.dataGridViewX1.SelectedRows[0].Cells["挂账标志"].Value.ToString();
                        //if (sxbz != "1")
                        //{
                        //    Form_JFSH frm = new Form_JFSH(str2);
                        //    if (frm.ShowDialog() == DialogResult.OK)
                        //    {


                        //        int num = Convert.ToInt32(this.dataGridViewX1.SelectedRows[0].Cells["返修次数"].Value.ToString());
                        //        //string gzbz = Convert.ToInt32(this.dataGridViewX1.SelectedRows[0].Cells["挂账标志"].Value.ToString());
                        //        //int jf = 1;
                        //        //int kf = 0;
                        //        if (Convert.ToDecimal(frm.DJ) > 0)
                        //        {
                        //            string str1 = "update J_维修处理表 set 当前状态=6,审核人='" + LoginXT.username + "',预计积分=" + Convert.ToDecimal(frm.DJ) + "  where 维修编号='" + str2 + "'";
                        //            SqlCommand SQL3 = new SqlCommand(str1, con);
                        //            SQL3.ExecuteNonQuery();
                        //            SQL3.Dispose();
                        //        }
                        //        else
                        //        {
                        //            string str1 = "update J_维修处理表 set 当前状态=6,审核人='" + LoginXT.username + "' where 维修编号='" + str2 + "'";
                        //            SqlCommand SQL3 = new SqlCommand(str1, con);
                        //            SQL3.ExecuteNonQuery();
                        //            SQL3.Dispose();
                        //        }

                                //if (num == 0)
                                //{
                                //    jf = 1;
                                //    kf = 0;
                                //}
                                //else
                                //{
                                //    jf = 0;
                                //    kf = num;

                                //}
                                //str1 = "update J_维修积分表 set 审核标志=1,日期='" + DateTime.Now.ToString() + "',合计=工时+难度+价值+新品+加分*" + jf + "-扣分*" + kf + "  where 维修编号='" + str2 + "'";
                                //SqlCommand SQL = new SqlCommand(str1, con);
                                //SQL.ExecuteNonQuery();
                                //SQL.Dispose();
                        //    }
                        //}
                        //else
                        //{
                            string str1 = "update J_维修处理表 set 当前状态=6,审核人='" + LoginXT.username + "' where 维修编号='" + str2 + "'";
                            SqlCommand SQL3 = new SqlCommand(str1, con);
                            SQL3.ExecuteNonQuery();
                            SQL3.Dispose();
                        //}
                    }
                    catch
                    {

                    }
                    con.Close();
                }
                Queue_data();
            }

        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewX1.SelectedRows.Count > 0)
            {
                //string temp = this.dataGridViewX1.SelectedRows[0].Cells["返修次数"].Value.ToString();
                string wxbh = this.dataGridViewX1.SelectedRows[0].Cells["维修编号"].Value.ToString();
                Form_XXXX frm = new Form_XXXX(wxbh);
                frm.ShowDialog();
            }
        }
    }
}