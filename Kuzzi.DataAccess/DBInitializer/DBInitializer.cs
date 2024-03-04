using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Data;
using Kuzzi.Models.Auth;
using Kuzzi.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kuzzi.DataAccess.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DBInitializer(UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public void Initialize()
        {
            // migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

            }
            catch (Exception ex)
            {

            }

            // create role if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                // if role are not create, then we will create a new user as well


                CreateUser(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Admin",
                    PhoneNumber = "000000000",
                }, "Gaumoe@2", SD.Role_Admin);
                CreateUser(new ApplicationUser
                {
                    UserName = "user1@gmail.com",
                    Email = "user1@gmail.com",
                    Name = "user1",
                    PhoneNumber = "000000000",
                }, "Gaumoe@2", SD.Role_Admin);
                CreateUser(new ApplicationUser
                {
                    UserName = "user2@gmail.com",
                    Email = "user2@gmail.com",
                    Name = "user2",
                    PhoneNumber = "000000000",
                }, "Gaumoe@2", SD.Role_Admin);
            }
            return;
        }

        private void CreateUser(ApplicationUser newUser, string password, string role)
        {
            _userManager.CreateAsync(newUser, password).GetAwaiter().GetResult();
            ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
        }


    }
}