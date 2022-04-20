using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebWithSql.Models;
using WebWithSql.Validators;

namespace WebWithSql
{
    public partial class Repeater : System.Web.UI.Page
    {
        static string rupd = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void repeatUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string userID = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "delete":
                    {
                        deleteUser(userID);
                        break;
                    }
                case "edit":
                    {
                        editUser(userID);
                        rupd = userID;
                        break;
                    }
            }

        }

        private void editUser(string userID)
        {
            panelEditUser.Visible = true;
        }


        private void deleteUser(string userID)
        {
            dataSourceUsers.DeleteParameters["UserID"].DefaultValue = userID;

            try
            {
                dataSourceUsers.Delete();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Ошибка удаления пользователя из БД. {ex.Message}";
            }

            //Refresh user list
            repeatUsers.DataBind();
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CompanyConnectionString"].ToString());
            try
            {
                string query = $"UPDATE Users SET UserName = '{txtUserName.Text}', Email = '{txtEmail.Text}', BirthData = '{txtDateOfBirth.Text}' WHERE UserId = {rupd}";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                con.Open();
                da.SelectCommand.ExecuteNonQuery();
                con.Close();
                repeatUsers.DataBind();
            }
            catch
            {
                con.Close();
            }
        }
    }
}