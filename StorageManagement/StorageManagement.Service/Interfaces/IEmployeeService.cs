﻿using StorageManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.Service.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
    }
}
