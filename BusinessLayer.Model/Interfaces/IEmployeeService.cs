﻿using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string siteId, string companyCode, string employeeCode);
        Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo);
        Task<bool> DeleteEmployeeAsync(string siteId, string companyCode, string employeeCode); 
    }
}
