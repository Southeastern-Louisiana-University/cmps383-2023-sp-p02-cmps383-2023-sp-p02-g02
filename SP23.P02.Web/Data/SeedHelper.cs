using Microsoft.AspNetCore.Identity;
using SP23.P02.Web.DTOs;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Entities;
using SP23.P02.Web.Features.TrainStations;
using System.Data;
using System.Runtime.CompilerServices;


namespace SP23.P02.Web.Data;

public static class SeedHelper
{


    public static async Task MigrateAndSeed(IServiceProvider services)
    {
        var dataContext = services.GetRequiredService<DataContext>();
        await dataContext.Database.MigrateAsync();

        await AddStations(dataContext);
        await AddRoles(services);
        await AddUsers(services);
    }
        
    private static async Task AddStations(DataContext dataContext)
    {
        var trainstations = dataContext.Set<TrainStation>();
        if (!await trainstations.AnyAsync())
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



    private static async Task AddRoles(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        if (roleManager.Roles.Any())
        {
            return;
        }

        await roleManager.CreateAsync(new Role
        {   
            Name = "Admin"
        });

        await roleManager.CreateAsync(new Role
        {
            Name = "User"
        });

    }


    private static async Task AddUsers(IServiceProvider services)
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
        await userManager.AddToRoleAsync(adminUser, "Admin");

        var bobUser = new User
        {
            UserName = "bob"
        };
        await userManager.CreateAsync(bobUser, defaultPassword);
        await userManager.AddToRoleAsync(bobUser, "User");

        var sueUser = new User
        {
            UserName = "sue"
        };
        await userManager.CreateAsync(sueUser, defaultPassword);
        await userManager.AddToRoleAsync(sueUser, "User");

    
    }

}

   