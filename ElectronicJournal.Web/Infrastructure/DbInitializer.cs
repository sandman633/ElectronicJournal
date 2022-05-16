using ElectronicJournal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal.Web
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();


            await context.Database.MigrateAsync();
            var roles = AppData.Roles.ToArray();
            var roleStore = new RoleStore<IdentityRole<Guid>,ApplicationDbContext,Guid>(context);
            if (context.Users.Any())
            {
                return;
            }
            foreach (var role in roles)
            {
                if (!context.Roles.Any(x => x.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole<Guid>(role)
                    {
                        NormalizedName = role.ToUpper()
                    });
                }
            }

            const string username = "sad@mail.ru";

            if (context.Users.Any(x => x.Email == username))
            {
                return;
            }



            var passwordHasher = new PasswordHasher<User>();


           
            var liners = AppData.Liners.ToArray();
            var progs = AppData.Prog.ToArray();
            var testers = AppData.Testetrs.ToArray();
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            var groups = AppData.UserGroups.ToArray(); 

            context.Groups.AddRange(groups);




            foreach (var user in liners)
            {
                var userStore = new UserStore<User, IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                user.PasswordHash = passwordHasher.HashPassword(user, "123qwe");
                userStore.CreateAsync(user);
                foreach (var role in roles)
                {
                    var identityResultRole = await userManager!.AddToRoleAsync(user, role);
                }
            }

            foreach (var prog in progs)
            {
                var userStore = new UserStore<User, IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                prog.PasswordHash = passwordHasher.HashPassword(prog, "123qwe");
                userStore.CreateAsync(prog);
                var identityResultRole = await userManager!.AddToRoleAsync(prog, AppData.UserRoleName);
            }
            foreach (var tester in testers)
            {
                var userStore = new UserStore<User, IdentityRole<Guid>, ApplicationDbContext, Guid>(context);

                tester.PasswordHash = passwordHasher.HashPassword(tester, "123qwe");
                userStore.CreateAsync(tester);
                var identityResultRole = await userManager!.AddToRoleAsync(tester, AppData.UserRoleName);
            }

            List<UserGroup> userGroups = new List<UserGroup>()
            {
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = progs[0].Id,
                    GroupId = groups[1].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = progs[1].Id,
                    GroupId = groups[1].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = progs[2].Id,
                    GroupId = groups[1].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = progs[3].Id,
                    GroupId = groups[1].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = progs[4].Id,
                    GroupId = groups[1].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = testers[0].Id,
                    GroupId = groups[0].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = testers[1].Id,
                    GroupId = groups[0].Id
                },
                new UserGroup
                {
                    Id = new Guid(),
                    UserId = testers[2].Id,
                    GroupId = groups[0].Id
                },
            };

            context.UserGroups.AddRange(userGroups);
            context.CourseTypes.AddRange(AppData.CourseTypes);

            List<Course> courses = new List<Course>()
            {
                new Course()
                {
                    Id= new Guid(),
                    Name = "English for Business. Advanced Level",
                    Description = "Английский язык для бизнеса. Продвинутый уровень",
                    Created = DateTime.Now,
                    TypeId = 1,
                },
                new Course()
                {
                    Id= new Guid(),
                    Name = "Be a BestTester!",
                    Description = "Курс для автоматизации тестирования для тестировщиков всех уровней",
                    Created = DateTime.Now,
                    TypeId = 2,
                },
                new Course()
                {
                    Id= new Guid(),
                    Name = "LoftSofton C# Advanced.",
                    Description = "Курс по языку программирования C# для разработчиков уровня middle и выше, а так же продвинутым Junior",
                    Created = DateTime.Now,
                    TypeId = 2,
                }
            };

            context.CourseStatuses.AddRange(AppData.CourseStatuses);
            context.Courses.AddRange(courses);

            List<UserCourse> userCourses = new List<UserCourse>()
            {
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = liners[0].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = liners[1].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = testers[0].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = testers[1].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = testers[2].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[1].Id,
                    UserId = testers[0].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[1].Id,
                    UserId = testers[1].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[1].Id,
                    UserId = testers[2].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = progs[0].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = progs[1].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = progs[2].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = progs[3].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[0].Id,
                    UserId = progs[4].Id,
                    StatusId = 1,
                },
               new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[2].Id,
                    UserId = progs[0].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[2].Id,
                    UserId = progs[1].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[2].Id,
                    UserId = progs[2].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[2].Id,
                    UserId = progs[3].Id,
                    StatusId = 1,
                },
                new UserCourse
                {
                    Id = new Guid(),
                    CourseId = courses[2].Id,
                    UserId = progs[4].Id,
                    StatusId = 1,
                },
            };



            context.UserCourses.AddRange(userCourses);

            await context.SaveChangesAsync();
        }
    }

    public class AppData
    {
        public const string AdministratorRoleName = "Liner";
        public const string UserRoleName = "User";

        public static IEnumerable<string> Roles
        {
            get
            {
                yield return AdministratorRoleName;
                yield return UserRoleName;
            }
        }

        public static List<Group> UserGroups = new List<Group>()
        {
            new Group
            {
                Id = Guid.NewGuid(),
                Description = "Primark Testers",
                Name = "Primark.TestersBI"
            },
            new Group
            {
                Id = Guid.NewGuid(),
                Description = "Primark Programmers",
                Name = "Primark.ProgrammersMPE"
            }
        };

        public static List<User> Liners = new List<User>()
        {
            new User
            {
                Name = "Насыров Булат",
                Id = Guid.NewGuid(),
                Email = "BNasyrov@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "BNasyrov@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "BNasyrov@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new User
            {
                Name = "Жилина Светлана",
                Id = Guid.NewGuid(),
                Email = "SZilina@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "SZilina@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "SZilina@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
        };
        public static List<User> Testetrs = new List<User>()
        {
            new User
            {
                Name = "Азалия Хасанова",
                Id = Guid.NewGuid(),
                Email = "AKhasanova@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "AKhasanova@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "AKhasanova@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new User
            {
                Name = "Екатерина Чернобровкина",
                Id = Guid.NewGuid(),
                Email = "EChernobrovkina@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "EChernobrovkina@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "EChernobrovkina@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new User
            {
                Name = "Юлия Маркова",
                Id = Guid.NewGuid(),
                Email = "UMarkova@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "UMarkova@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "UMarkova@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            }
        };
        public static List<User> Prog = new List<User>
        {
            new User
            {
                Name = "Шигапов Камиль",
                Id = Guid.NewGuid(),
                Email = "KShigapov@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "KShigapov@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "KShigapov@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
    },

            new User
            {
                Name = "Чибуряева Полина",
                Id = Guid.NewGuid(),
                Email = "PChibyraeva@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "PChibyraeva@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "PChibyraeva@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new User
            {
                Name = "Мухаметзянов Артур",
                Id = Guid.NewGuid(),
                Email = "AMuhametzyanov@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "AMuhametzyanov@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "AMuhametzyanov@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },

           new User
            {
                Name = "Соловьев Сайдаш",
                Id = Guid.NewGuid(),
                Email = "SSaidash@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "SSaidash@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "SSaidash@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            },
            new User
            {
                Name = "Карина Медведева",
                Id = Guid.NewGuid(),
                Email = "KMedvedeva@mail.ru",
                EmailConfirmed = true,
                NormalizedEmail = "sad@mail.ru".ToUpper(),
                PhoneNumber = "+790000000000",
                UserName = "KMedvedeva@mail.ru",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "KMedvedeva@mail.ru".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            }
        };

        public static List<CourseType> CourseTypes = new List<CourseType>()
        {
            new CourseType
            {
                Id = Guid.NewGuid(),
                TypeId = 1,
                Name = "Внешний"
            },
            new CourseType
            {
                Id = Guid.NewGuid(),
                TypeId = 2,
                Name = "Внутренний"
            }
        };

        public static List<CourseStatus> CourseStatuses= new List<CourseStatus>()
        {
            new CourseStatus
            {
                Id = Guid.NewGuid(),
                StatusId = 1,
                StatusName = "Обучается"
            },
            new CourseStatus
            {
                Id = Guid.NewGuid(),
                StatusId = 2,
                StatusName = "Закончил обучаться"
            }
        };


    }
}
