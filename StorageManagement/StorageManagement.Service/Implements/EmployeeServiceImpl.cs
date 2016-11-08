using StorageManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageManagement.DataAccess.Models;
using StorageManagement.DataAccess.Implements;

namespace StorageManagement.Service.Implements
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        private BaseDao<Employee> employeeDao = new BaseDao<Employee>();

        public List<Employee> GetAll()
        {
            return employeeDao.GetAll();
        }
    }
}
