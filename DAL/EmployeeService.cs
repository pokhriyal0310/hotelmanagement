using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmployeeService
    {
        private HotelManagementContext db = new HotelManagementContext();

        public IEnumerable<Employee1> GetRoom()
        {
            IEnumerable<Employee1> elist;
            try
            {
                elist = db.Employee1s.ToList();
                return elist;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Employee1 GetEmployeeById(int Id)
        {
            Employee1? employee;
            try
            {
                employee = db.Employee1s.Find(Id);
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new Exception("Record not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Employee1 UpdateEmployee(Employee1 EmployeeRec)
        {
            try
            {
                if (EmployeeRec != null)
                {
                    db.Entry(EmployeeRec).State = EntityState.Modified;
                    db.SaveChanges();
                    return EmployeeRec;
                }
                else
                {
                    throw new Exception("Ui Error");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> AddEmployee(Employee1 employee)
        {
            var Emp1 = new Employee1()
            {
                EmpId = employee.EmpId,
                EmpName = employee.EmpName,
                EmpGrade = employee.EmpGrade,
                HotelId = employee.HotelId

            };
            db.Employee1s.Add(Emp1);
            await db.SaveChangesAsync();
            return (int)Emp1.EmpId;
        }
        public async Task RemoveEmployee(int EmpId)
        {
            Employee1 emp = db.Employee1s.Where((x) => x.EmpId == EmpId).FirstOrDefault();
            db.Employee1s.Remove(emp);
            await db.SaveChangesAsync();
        }
    }
}
