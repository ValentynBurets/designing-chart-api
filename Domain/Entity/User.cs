using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User: EntityBase
    {
        public string Name {  get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
