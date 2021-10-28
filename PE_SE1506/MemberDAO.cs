using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace PE_SE1506
{
    class Member
    {
        public int member_no { get; set; }
        public string fullname { get; set; }
        public string region_name { get; set; }
        public DateTime issue_dt { get; set; }
        public DateTime expr_dt { get; set; }

        public class MemberDAO
        {
            // đối tượng kết nối
            SqlConnection connection;

            // đối tượng thực thi các truy vấn
            SqlCommand command;


            string getConnectionString()
            {
                // khai báo và lấy chuỗi từ appsettings.json
                IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();

                return config["ConnectionString:MyMemberDB"];
            }

            public int updateMember(int mNo, string fName, string lName, DateTime iDate, DateTime eDate)
            {
                int result = 0;

                connection = new SqlConnection(getConnectionString());
                string query = "update member set firstname = @fName, lastname = @lName, issue_dt = @iDate, expr_dt = @eDate where member_no = @mNo";

                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@fName", fName);
                command.Parameters.AddWithValue("@lName", lName);
                command.Parameters.AddWithValue("@iDate", iDate);
                command.Parameters.AddWithValue("@eDate", eDate);
                command.Parameters.AddWithValue("@mNo", mNo);

                try
                {
                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return result;
            }

            public List<Member> getMembers()
            {
                List<Member> members = new List<Member>();

                connection = new SqlConnection(getConnectionString());
                string query = "select member_no, lastname+' '+firstname as fullname,  region_name, issue_dt, expr_dt " +
                        "from member inner join region on member.region_no = region.region_no";

                command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            members.Add(new Member()
                            {
                                member_no = reader.GetInt32("member_no"),
                                fullname = reader.GetString("fullname"),
                                region_name = reader.GetString("region_name"),
                                issue_dt = reader.GetDateTime("issue_dt"),
                                expr_dt = reader.GetDateTime("expr_dt")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                return members;
            }
        }
    }
}

