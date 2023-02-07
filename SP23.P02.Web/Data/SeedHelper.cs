using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.TrainStations;

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
                var trainstations = new List<TrainStation>
            {
                new TrainStation
                {
                    Name = "Hammond Train",
                    Address = "123 Hammond Dr"
                },
                new TrainStation
                {
                    Name = "New Orleans Train",
                    Address = "123 New Orleans Dr"
                },
                new TrainStation
                {
                    Name = "Baton Rouge Train",
                    Address = "123 Baton Rouge Dr"
                },
                new TrainStation
                {
                    Name = "Lafayette Train",
                    Address = "123 Lafayette Dr"
                },
            };

                //loops through the seeded data  
                foreach (var trainstation in trainstations)
                {
                    dataContext.TrainStations.Add(trainstation);
                }

            }

            await dataContext.SaveChangesAsync();
        }
    }
}