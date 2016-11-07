using StorageManagement.Models;
using StorageManagement.Services;
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
        // GET: Test
        public ViewResult GetView()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer service = new EmployeeBusinessLayer();
            var employees = service.GetEmployees();
            var modelList = new List<EmployeeViewModel>();

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