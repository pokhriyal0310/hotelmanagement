using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdminService
    {
        private HotelManagementContext db = new HotelManagementContext();

        public IEnumerable<Admin> GetAdmin()
        {
            IEnumerable<Admin> alist;
            try
            {
                alist = db.Admins.ToList();
                return alist;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Admin GetAdminById(int Id)
        {
            Admin? user;
            try
            {
                user = db.Admins.Find(Id);
                if(user != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("Record not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Admin UpdateAdmin(Admin AdminRec)
        {
            try
            {
                if (AdminRec != null)
                {
                    db.Entry(AdminRec).State = EntityState.Modified;
                    db.SaveChanges();
                    return AdminRec;
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
        public async Task<int> AddAdmin(Admin user)
        {
            var User = new Admin()
            {
                AdminId = user.AdminId,
                AdminName = user.AdminName,
                AdminType = user.AdminType

            };
            db.Admins.Add(User);
            await db.SaveChangesAsync();
            return (int)User.AdminId;
        }
        public async Task RemoveAdmin(int AdminId)
        {
            Admin adm = db.Admins.Where((x) => x.AdminId == AdminId).FirstOrDefault();
            db.Admins.Remove(adm);
            await db.SaveChangesAsync();
        }
    }
}
