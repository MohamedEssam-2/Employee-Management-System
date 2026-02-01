using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Data.Context;
using Demo_DAL.Data.Repositories.Interfaces;
using Demo_DAL.Models.Department;

namespace Demo_DAL.Data.Repositories.Classes
{
    public class Department_Repository(ApplicationDbContext _context) :Generic_Repository<Department>(_context),IDepartment_Repository
    {


        //private readonly ApplicationDbContext _context = context;
        //Priamry ctor
        //Cant use parameterless ctor because we make it primary



        //Repository Patterns
        //CRUD Operations
        //GetALL
        //GetById
        //Add
        //Update
        //Delete






        //inject Object Method
        //Ask clr to create object of ApplicationDbContext and pass it to this constructor

        //public Department_Repository(ApplicationDbContext context) 
        //{
        //    _context = context;
        //}



        //public Department? GetById(int id)
        //{

        //    var department = _context.Departments.Find(id);
        //    return department;
        //}
        ////OR Using lamda  EXPRESSION 
        ////public Department? GetById(int id) => _context.Departments.Find(id);

        //public IEnumerable<Department> GetAll(bool withtracking = false)
        //{
        //    if (withtracking)
        //        return _context.Departments.ToList();
        //    else
        //        return _context.Departments.AsNoTracking().ToList();
        //}

        //public int Add(Department department)
        //{
        //    _context.Departments.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    _context.Departments.Update(department);
        //    return _context.SaveChanges();
        //}
        //public int Remove(Department department)
        //{
        //    _context.Departments.Remove(department);
        //    return _context.SaveChanges();
        //}
    }
}
