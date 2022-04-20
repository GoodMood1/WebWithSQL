using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebWithSql.Models;

namespace WebWithSql.Validators
{
    public class UserDtoValidator
    {
        public static bool IsAddUserDtoValid(AddUserDto addUserDto)
        {
            DateTime dateOfBirth;

            if(addUserDto == null)
                return false;

            if(string.IsNullOrWhiteSpace(addUserDto.UserName.Trim()))
                return false;

            if (string.IsNullOrWhiteSpace(addUserDto.Email.Trim()))
                return false;

            if (!DateTime.TryParse(addUserDto.DateOfBirth, out dateOfBirth))
                return false;

            return true;
        }
        public static bool IsUpdateUserDtoValid(UpdateUserDto updateUserDto)
        {
            DateTime dateOfBirth;

            if (updateUserDto == null)
                return false;

            if (string.IsNullOrWhiteSpace(updateUserDto.UserName.Trim()))
                return false;

            if (string.IsNullOrWhiteSpace(updateUserDto.Email.Trim()))
                return false;

            if (!DateTime.TryParse(updateUserDto.DateOfBirth, out dateOfBirth))
                return false;

            return true;
        }
    }
}