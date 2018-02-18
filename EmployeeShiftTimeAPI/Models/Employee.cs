using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeShiftTimeAPI.Models 
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public IList<Shifts> Shift_coll { get; set; }
        
    }
}