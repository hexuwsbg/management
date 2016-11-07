using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorageManagement.ViewModels
{
    public class EmployeeListViewModel
    {
        public List<EmployeeViewModel> Employees { set; get; }
        public string UserName { set; get; }
    }
}