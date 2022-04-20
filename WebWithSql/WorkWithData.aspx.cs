using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebWithSql.Models;
using WebWithSql.Validators;

namespace WebWithSql
{
    public partial class WorkWithData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserDto newUserDto = createAddUserDto();

            if (UserDtoValidator.IsAddUserDtoValid(newUserDto))
            {
                dataSourceUsers.InsertParameters["UserName"].DefaultValue = newUserDto.UserName;
                dataSourceUsers.InsertParameters["Email"].DefaultValue = newUserDto.Email;
                dataSourceUsers.InsertParameters["BirthData"].DefaultValue = newUserDto.DateOfBirth;

                try
                {
                    dataSourceUsers.Insert();

                    gridUsers.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = $"Ошибка добавления пользователя в БД. {ex.Message}";
                }

            }
            else
            {
                lblError.Text = "New user data invalid";
            }
        }

        private AddUserDto createAddUserDto()
        {
            return new AddUserDto()
            {
                UserName = txtUserName.Text,
                Email = txtEmail.Text,
                DateOfBirth = txtDateOfBirth.Text
            };
        }
        private UpdateUserDto updateUserDto(string s1,string s2,string s3)
        {
            return new UpdateUserDto()
            {
                UserName = s1,
                Email = s2,
                DateOfBirth = s3
            };
        }
        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            panelConfirmDelete.Visible = true;

            Cache.Insert(Constants.Cache.USER_TO_DELETE_KEY, e.Keys["UserID"]);
        }

        protected void btnSureToDeleteUser_Click(object sender, EventArgs e)
        {
            string userToDelete = Cache.Get(Constants.Cache.USER_TO_DELETE_KEY).ToString();

            if (!string.IsNullOrEmpty(userToDelete))
            {
                dataSourceUsers.DeleteParameters["UserID"].DefaultValue = userToDelete;

                try
                {
                    dataSourceUsers.Delete();
                    removeUserToDeleteFromCache();

                    //Повторно считать данные из источника (data refresh)
                    gridUsers.DataBind();

                    //Hide user delete confirmation panel
                    panelConfirmDelete.Visible = false;
                }
                catch (Exception ex)
                {
                    lblError.Text = $"Ошибка удаления пользователя из БД. {ex.Message}";
                }
            }
        }

        private void removeUserToDeleteFromCache()
        {
            Cache.Remove(Constants.Cache.USER_TO_DELETE_KEY);
        }

        protected void btnCancelDeleteUser_Click(object sender, EventArgs e)
        {
            removeUserToDeleteFromCache();
            panelConfirmDelete.Visible = false;
        }

        protected void gridUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var a1 = e.OldValues[0]; var a2 = e.OldValues[1]; var a3 = e.OldValues[2];
            e.NewValues[0] += "";
            e.NewValues[1] += "";
            e.NewValues[2] += "";

            UpdateUserDto newUserDto = updateUserDto(e.NewValues[0].ToString(), e.NewValues[1].ToString(), e.NewValues[2].ToString());

            if (UserDtoValidator.IsUpdateUserDtoValid(newUserDto))
            {
                dataSourceUsers.UpdateParameters["UserName"].DefaultValue = newUserDto.UserName;
                dataSourceUsers.UpdateParameters["Email"].DefaultValue = newUserDto.Email;
                dataSourceUsers.UpdateParameters["BirthData"].DefaultValue = newUserDto.DateOfBirth;

                try
                {
                    dataSourceUsers.Update();

                    gridUsers.DataBind();
                }
                catch (Exception ex)
                {
                    lblError.Text = $"Ошибка добавления пользователя в БД. {ex.Message} ";
                }

            }
            else
            {
                lblError.Text = "New user data invalid";
                e.NewValues[0] = e.OldValues[0];
                e.NewValues[1] = e.OldValues[1];
                e.NewValues[2] = e.OldValues[2];
            }
        }
    }
}