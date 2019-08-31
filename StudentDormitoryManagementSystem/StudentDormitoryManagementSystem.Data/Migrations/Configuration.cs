using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentDormitoryManagementSystem.Data.Migrations
{
    using StudentDormitoryManagementSystem.Data.Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class
        Configuration : DbMigrationsConfiguration<StudentDormitoryManagementSystem.Data.MsSqlDbContext>
    {
        private const string AdministratorUserName = "administrator@sdms.com";
        private const string AdministratorPassword = "administrator";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(StudentDormitoryManagementSystem.Data.MsSqlDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //this.SeedUsers(context);
            //this.SeedSampleData(context);
            //this.SeedSharedInventory(context);

            base.Seed(context);
        }

        private void SeedSharedInventory(MsSqlDbContext context)
        {
            var kitchenRoom = new Room
            {
                CreatedOn = DateTime.Now,
                Floor = 1,
                Number = StudentDormitoryManagementSystem.Common.Constants.KitchenRoomNumber,
                Type = RoomType.Shared,
            };

            context.Rooms.Add(kitchenRoom);

            var kitchenInventory = new Inventory
            {
                Type = InventoryType.Shared,
                Approved = true,
                LastDateModified = DateTime.Now,
                CreatedOn = DateTime.Now,
                Room = kitchenRoom,
                RoomId = kitchenRoom.Id
            };

            context.Inventories.Add(kitchenInventory);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (context.Roles.Any()) return;
            const string roleName = "Admin";

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = roleName };
            roleManager.Create(role);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var user = new User
            {
                UserName = AdministratorUserName,
                Email = AdministratorUserName,
                EmailConfirmed = true,
            };

            userManager.Create(user, AdministratorPassword);
            userManager.AddToRole(user.Id, roleName);
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (!context.Students.Any())
            {
                var student = new StudentInfo
                {
                    FirstName = "Stoqn",
                    SecondName = "Stoqnov",
                    LastName = "Stoqnov",
                    BirthPlace = "Plovdiv",
                    Address = "Levski str. 1",
                    Course = 1,
                    EGN = "9901010101",
                    PersonalCardNumber = "645491253",
                    Phone = "0888888888",
                    Speciality = "Information Technology",
                    University = "Technical University-Sofia",
                    Years = 21,
                    Nationality = "Bulgarian",
                };

                context.Students.Add(student);
                context.SaveChanges();
            }

            if (!context.Rooms.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var room = new Room
                    {
                        Type = RoomType.Single,
                        Number = (short)(i + 1),
                        Floor = (short)(i + 1),
                        Students = i == 0 ? context.Students.Where(s => s.EGN == "9901010101").ToList() : new List<StudentInfo>(),
                    };

                    context.Rooms.Add(room);
                    context.SaveChanges();
                }
            }

            if (!context.Items.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var item = new Item
                    {
                        Name = "Bed " + i,
                        Description = "This is a bed " + i,
                        Material = "Metal",
                        Model = "F340",
                        State = State.Well,
                        Size = Size.Medium,
                        Room = context.Rooms.FirstOrDefault(),
                        DateRegistered = DateTime.Now,
                        ExpirationDate = DateTime.MaxValue,
                    };

                    context.Items.Add(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
