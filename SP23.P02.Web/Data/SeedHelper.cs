using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.VisualBasic;
using SP23.P02.Web.Features.Entities;
using SP23.P02.Web.Features.TrainStations;
using SP23.P02.Web.User_Account_Authorizations;
using System.Data;


namespace SP23.P02.Web.Data;

public static class SeedHelper
{


    public static async Task Initialize(IServiceProvider services)
    {
        var context = services.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();

        await AddRoles(services);
        await AddUsers(services);

    }
    public static async Task MigrateAndSeed(DataContext dataContext)
    {
        await dataContext.Database.MigrateAsync();
 

        var trainStations = dataContext.Set<TrainStation>();

        if (!await trainStations.AnyAsync())
        {
            for (int i = 0; i < 3; i++)
            {
                var trainstations = new List<TrainStation>
            {
                new TrainStation
                {
                    Name = "Hammond Train",
                    Address = "123 Hammond Dr"
                },
               
            };

            }

            await dataContext.SaveChangesAsync();
        }
    }



    private static async Task AddRoles(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        if (roleManager.Roles.Any())
        {
            return;
        }

        await roleManager.CreateAsync(new Role
        {
            Name = RoleType.Admin
        });

        await roleManager.CreateAsync(new Role
        {
            Name = RoleType.User
        });
    }
    private static async Task AddUsers(IServiceProvider services)
    {
        const string defaultPass = "Password123!";

        var userManager = services.GetRequiredService<UserManager<User>>();
        if (userManager.Users.Any())
        {
            return;
        }

        var adminUser = new User
        {
           UserName = "galkadi"
        };
        await userManager.CreateAsync(adminUser, defaultPass);
        await userManager.AddToRoleAsync(adminUser, RoleType.Admin);

        var bobUser = new User
        {
         //   UserName = "bob"
        };
        await userManager.CreateAsync(bobUser, defaultPass);
        await userManager.AddToRoleAsync(bobUser, RoleType.User);

        var sueUser = new User
        {
           // UserName = "sue"
        };
        await userManager.CreateAsync(sueUser, defaultPass);
        await userManager.AddToRoleAsync(sueUser, RoleType.User);

        await services.GetRequiredService<DataContext>().SaveChangesAsync();
    }





}