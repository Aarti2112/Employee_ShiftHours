using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeShiftTimeAPI;
using EmployeeShiftTimeAPI.Controllers;
using EmployeeShiftTimeAPI.DAL;
using EmployeeShiftTimeAPI.Models;
using System.Data.Entity;
using Moq;

namespace EmployeeShiftTimeAPI.Tests.Controllers
{
    [TestClass]
    public class EmployeeAPIControllerTest
    {
        [TestMethod]
        public void GetEmployeeShiftHours()
        {
            // Arrange
            IList<Shifts> shiftcoll = new List<Shifts>
            {
                new Shifts { Shift_ID = 1 , Shift_Start = Convert.ToDateTime("2016-11-11 09:00:00.000") , Shift_End = Convert.ToDateTime("2016-11-11 17:00:00.000") , Shift_Name = "Morning 9-17" },
                new Shifts { Shift_ID = 4 , Shift_Start = Convert.ToDateTime("2016-11-13 09:00:00.000") , Shift_End = Convert.ToDateTime("2016-11-13 17:00:00.000") , Shift_Name = "Morning 9-17" },
                new Shifts { Shift_ID = 5 , Shift_Start = Convert.ToDateTime("2016-12-14 10:00:00.000") , Shift_End = Convert.ToDateTime("2016-12-14 14:00:00.000") , Shift_Name = "Morning 10-14" }
            };
            var data = new List<Employee> {
                new Employee {Employee_ID = 4 , FirstName = "Joe", Surname = "Mellor", Shift_coll = shiftcoll }
               
            };

            // Convert the IEnumerable list to an IQueryable list
            IQueryable<Employee> queryableList = data.AsQueryable();

            //mock the Employee DBSet
            var mockSet = new Mock<DbSet<Employee>>();

            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            //mock DataContext class
            var mockContext = new Mock<EmployeeDAL>();

            //set up mocked Employee data to for the DBContext class 
            mockContext.Setup(m => m.employees).Returns(mockSet.Object);

            //call the EmployeeAPI constructor with the mockcontext object
            var EmpAPIController = new EmployeeAPIController(mockContext.Object);

            //Act
            var result = EmpAPIController.Get();

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(16, result[0].No_Of_Hours);
            Assert.AreEqual(4, result[1].No_Of_Hours);
            Assert.AreEqual(11, result[0].Per_Month);
            Assert.AreEqual(12, result[1].Per_Month);
          
        }
        

    }
}
