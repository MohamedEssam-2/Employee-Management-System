using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DAL.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployee_Repository Employee_Repository { get; }
        public IDepartment_Repository Department_Repository { get; }
        public int SaveChanges();
        

    }
}
