using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Model
{
    public class CreateExerciseViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxMark { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Category { get; set; }
        public string EtalonChart { get; set; }
    }
}
