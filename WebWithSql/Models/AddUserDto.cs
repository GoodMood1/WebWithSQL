using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWithSql.Models
{
    public class AddUserDto
    {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string DateOfBirth { get; set; }
    }
}