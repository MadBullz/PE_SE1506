using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static PE_SE1506.Member;

namespace PE_SE1506
{
    public partial class frmEdit : Form
    {
        public int member_no { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime issue_dt { get; set; }
        public DateTime expr_dt { get; set; }

        public frmEdit(int member_no, string firstname, string lastname, DateTime issue_dt, DateTime expr_dt)
        {
            InitializeComponent();
            this.member_no = member_no;
            this.firstname = firstname;
            this.lastname = lastname;
            this.issue_dt = issue_dt;
            this.expr_dt = expr_dt;
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            txtFname.Text = firstname;
            txtLname.Text = lastname;
            dtpIssue.Value = issue_dt;
            dtpExpired.Value = expr_dt;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool flag1 = (txtFname.Text != null);
            bool flag2 = (txtLname.Text != null);
            bool flag3 = (dtpIssue.Value.AddDays(90) < dtpExpired.Value);
            if (flag1 && flag2 && flag3)
            {
                MemberDAO mdb = new MemberDAO();
                if(mdb.updateMember(member_no, txtFname.Text, txtLname.Text, dtpIssue.Value, dtpExpired.Value) > 0)
                {
                    MessageBox.Show("Updated");
                    this.Close();
                }
            }
            else MessageBox.Show("Invalid data");
        }
    }
}
