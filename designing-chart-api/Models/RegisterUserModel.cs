using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designing_chart_api.Models
{
    public class RegisterUserModel : LoginUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
