using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User 
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public DateTime birthday { get; set; }
        public string gender { get; set; }
        public string tel { get; set; }
        public City city { get; set; }
        public bool message { get; set; }
    }
}
