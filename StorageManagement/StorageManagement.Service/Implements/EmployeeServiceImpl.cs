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
        private EmployeeDao employeeDao = new EmployeeDao();

        public List<Employee> GetAll()
        {
            return employeeDao.GetAll();
        }

        public Employee GetByID(int id)
        {
            var list = employeeDao.GetByField("ID", id);
            if (list.Count == 0)
                return null;
            return list[0];
        }

        public void Insert(Employee model)
        {
            employeeDao.Insert(model);
        }

        public void Update(Employee model)
        {
            employeeDao.Update(model);
        }
    }
}
