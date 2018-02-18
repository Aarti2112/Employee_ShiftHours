using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeShiftTimeAPI.Models;

namespace EmployeeShiftTimeAPI.DAL
{
    public class EmployeeDAL : DbContext , IEmployeeDAL
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                
                modelBuilder.Entity<Employee>().ToTable("Employee");
                modelBuilder.Entity<Shifts>().ToTable("Shifts");

                modelBuilder.Entity<Employee>()
                    .HasMany<Shifts>(s => s.Shift_coll)
                    .WithMany(e => e.Employee_coll)
                    .Map(m =>
                        {
                            m.ToTable("Employee_Works_Shift");
                            m.MapLeftKey("Employee_ID");
                            m.MapRightKey("Shift_ID");
                            
                        });
               

                base.OnModelCreating(modelBuilder);

               // modelBuilder.Entity<Employee_Works_Shift>()
               //.HasKey(es => new { es.Employee_ID, es.Shift_ID });

               // modelBuilder.Entity<Employee_Works_Shift>()
               //.HasRequired(es => es.employee)
               //.WithMany(e => e.empShift)
               //.HasForeignKey(es => es.Employee_ID);

               // modelBuilder.Entity<Employee_Works_Shift>()
               // .HasRequired(es => es.shifts)
               // .WithMany(s => s.empShift)
               // .HasForeignKey(es => es.Shift_ID);

                //modelBuilder.Entity<Employee_Works_Shift>()
                //.Property(p => p.Employee_ID).IsOptional();
                //modelBuilder.Entity<Employee_Works_Shift>()
                //  .Property(p => p.Shift_ID).IsOptional();

        }

        public virtual DbSet<Employee> employees { get; set; }
        public virtual DbSet<Shifts> shifts { get; set; }
    }
}