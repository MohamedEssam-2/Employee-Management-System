using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Data.Context;
using Demo_DAL.Data.Repositories.Interfaces;
using Demo_DAL.Models.Employee;

namespace Demo_DAL.Data.Repositories.Classes
{
    public class Employee_Repository(ApplicationDbContext _context) :Generic_Repository<Employee>(_context),IEmployee_Repository 
    {
    }
}
