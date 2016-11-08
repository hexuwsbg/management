using StorageManagement.DataAccess.Models;
using StorageManagement.Models;
using StorageManagement.Service.Implements;
using StorageManagement.Service.Interfaces;
using StorageManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageManagement.Controllers
{
    public class TestController : Controller
    {
        private IEmployeeService employeeService = new EmployeeServiceImpl();
        // GET: Test
        public ViewResult GetView()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            //var employees = employeeService.GetAll();
            var employees = new List<Employee>();
            var employee = employeeService.GetByID(1);
            var modelList = new List<EmployeeViewModel>();
            //var employee = new Employee();
            employee.Salary = 100;
            //employee.FirstName = "Nick";
            employeeService.Update(employee);

            foreach (var emp in employees)
            {
                EmployeeViewModel model = new EmployeeViewModel();
                model.EmployeeName = string.Format("{0} {1}", emp.FirstName, emp.LastName);
                model.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    model.SalaryColor = "yellow";
                }
                else
                {
                    model.SalaryColor = "green";
                }
                modelList.Add(model);
            }
            employeeListViewModel.Employees = modelList;
            employeeListViewModel.UserName = "Admin";

            return View("MyView", employeeListViewModel);
        }
    }
}