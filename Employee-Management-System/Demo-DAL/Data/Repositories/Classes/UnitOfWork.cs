using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Data.Context;
using Demo_DAL.Data.Repositories.Interfaces;

namespace Demo_DAL.Data.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly Lazy<IEmployee_Repository> _EmployeeRepository; 
        private readonly Lazy<IDepartment_Repository> _DepartmentRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork( ApplicationDbContext _DBContext)
        {
            _EmployeeRepository = new Lazy<IEmployee_Repository>(() => new Employee_Repository(_DBContext));
             _DepartmentRepository = new Lazy<IDepartment_Repository>(() => new Department_Repository(_DBContext));
            _dbContext = _DBContext;
        }

   
        public IEmployee_Repository Employee_Repository => _EmployeeRepository.Value;
        public IDepartment_Repository Department_Repository => _DepartmentRepository.Value;


        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

    }
}
