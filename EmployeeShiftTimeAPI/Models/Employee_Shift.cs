using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeShiftTimeAPI.Models;

namespace EmployeeShiftTimeAPI.Models
{
    public class Employee_Shift
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int No_Of_Hours { get; set; }
        public int Per_Month { get; set; }

    }

}