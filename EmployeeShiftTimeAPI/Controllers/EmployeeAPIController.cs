using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeShiftTimeAPI.Models;
using EmployeeShiftTimeAPI.DAL;



namespace EmployeeShiftTimeAPI.Controllers
{
    public class EmployeeAPIController : ApiController 
    {
        EmployeeDAL ObjEmpDal = new EmployeeDAL();

        private IEmployeeDAL db = new EmployeeDAL();

        public EmployeeAPIController() { }

        //constructor of an EmployeeAPIController 
        public EmployeeAPIController(IEmployeeDAL context)
        {
            db = context;
        } 
      
        // GET api/values
        public List<Employee_Shift> Get()
        {

            var emp_Data = from emp in db.employees
                           from shift in emp.Shift_coll
                           select new
                           {
                               Employee_ID = emp.Employee_ID,
                               Shift_Start = shift.Shift_Start,
                               No_Of_Hours = shift.Shift_End.Hour - shift.Shift_Start.Hour
                           };

            var emp_GroupBy = from emp in emp_Data
                              group emp by new { emp.Employee_ID, emp.Shift_Start.Month } into g
                              select new
                              {
                                  empID = g.Key.Employee_ID,
                                  data = g.Sum(x => x.No_Of_Hours),
                                  month = g.Key.Month
                              };


            List<Employee_Shift> Emp_Shift_list = (from eg in emp_GroupBy
                                                   join emp in db.employees on eg.empID equals emp.Employee_ID
                                                   orderby emp.FirstName ascending
                                                   select new Employee_Shift()
                                                   {
                                                       Firstname = emp.FirstName,
                                                       Lastname = emp.Surname,
                                                       No_Of_Hours = eg.data,
                                                       Per_Month = eg.month
                                                   }).ToList();


            return Emp_Shift_list;
        }

       
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}