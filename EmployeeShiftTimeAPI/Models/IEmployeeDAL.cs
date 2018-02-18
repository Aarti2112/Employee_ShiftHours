using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeShiftTimeAPI.Models
{
    public interface IEmployeeDAL : IDisposable
    {
        DbSet<Employee> employees { get; set; }
        DbSet<Shifts> shifts { get; set; }
    }
}
