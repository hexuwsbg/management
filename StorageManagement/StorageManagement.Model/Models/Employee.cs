using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Models
{
    public class Employee : BaseModel
    {
        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public int Salary { set; get; }
    }
}
