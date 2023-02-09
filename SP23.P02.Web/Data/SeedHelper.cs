using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.VisualBasic;
using SP23.P02.Web.Features.Entities;
using SP23.P02.Web.Features.TrainStations;
using System.Runtime.CompilerServices;

namespace SP23.P02.Web.Data;

public static class SeedHelper
{
    

    public static async Task MigrateAndSeed(DataContext dataContext) 
    {
        await dataContext.Database.MigrateAsync();

        var trainStations = dataContext.Set<TrainStation>();

        if (!await trainStations.AnyAsync())
        {
            for (int i = 0; i < 3; i++)
            {
                dataContext.Set<TrainStation>()
                    .Add(new TrainStation
                    {
                        Name = "Hammond",
                        Address = "1234 Place st"
                    });
            }

            await dataContext.SaveChangesAsync();
        }
    }

    public static async Task Initilize(IServiceProvider services)
    {
        

        await AddRoles(services);
        await AddUsers(services);
    }








    private static async Task AddRoles(IServiceProvider services)
    {
        
        var roleManager = services.GetRequiredService<RoleManager<Role>>();

        if (!await roleManager.RoleExistsAsync(RoleType.Admin))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = RoleType.Admin
            });
        }

        if (!await roleManager.RoleExistsAsync(RoleType.User))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = RoleType.User
            });
        }
    }


   

  
    public static async Task AddUsers(IServiceProvider services)
    {
        const string defaultPassword = "Password123!";

        
        

        var userManager = services.GetRequiredService<UserManager<User>>();
        if (userManager.Users.Any())
        {
            return;
        }


        var adminUser = new User
        {
            UserName = "galkadi"
        };
        await userManager.CreateAsync(adminUser, defaultPassword);
        await userManager.AddToRoleAsync(adminUser, RoleType.Admin);

        var bobUser = new User
        {
            UserName = "bob"
        };
        await userManager.CreateAsync(bobUser, defaultPassword);
        await userManager.AddToRoleAsync(bobUser, RoleType.User);

        var sueUser = new User
        {
            UserName = "sue"
        };
        await userManager.CreateAsync(sueUser, defaultPassword);
        await userManager.AddToRoleAsync(sueUser, RoleType.User);


       
    }
    
}