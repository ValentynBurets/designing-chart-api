using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public abstract class User: EntityBase
    {
        protected User(Guid idLink)
        {
            IdLink = idLink;
        }

        public Guid IdLink { get; set; }
        public string Name {  get; set; }
        public string SurName { get; set; }

    }
}
