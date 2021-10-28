using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PE_SE1506.Member;

namespace PE_SE1506
{
    public partial class Form1 : Form
    {
        MemberDAO mdb = new MemberDAO();

        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                int member_no = Int32.Parse(dgvMember.CurrentRow.Cells[1].Value.ToString());
                string fullname = dgvMember.CurrentRow.Cells[2].Value.ToString();
                DateTime issue_dt = DateTime.Parse(dgvMember.CurrentRow.Cells[4].Value.ToString());
                DateTime expr_dt = DateTime.Parse(dgvMember.CurrentRow.Cells[5].Value.ToString());
                string firstname = fullname.Split(' ')[1];
                string lastname = fullname.Split(' ')[0];
                frmEdit fEdit = new frmEdit(member_no, firstname, lastname, issue_dt, expr_dt);
                fEdit.ShowDialog();
                reload();
            }
        }

        void reload()
        {
            dgvMember.DataSource = mdb.getMembers();
            dgvMember.Columns[4].Visible = false;
            dgvMember.Columns[5].Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reload();
        }
    }
}
