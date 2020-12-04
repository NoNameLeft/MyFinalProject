namespace BLTC.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            await dbContext.Countries.AddAsync(new Country { Name = "Switzerland", IsoCode = "CHE", });
            await dbContext.Countries.AddAsync(new Country { Name = "United States of America", IsoCode = "USA", });
            await dbContext.Countries.AddAsync(new Country { Name = "Australia", IsoCode = "AUS", });
            await dbContext.Countries.AddAsync(new Country { Name = "Canada", IsoCode = "CAN", });
            await dbContext.Countries.AddAsync(new Country { Name = "Germany", IsoCode = "DEU", });
            await dbContext.Countries.AddAsync(new Country { Name = "Belgium", IsoCode = "BEL", });
            await dbContext.Countries.AddAsync(new Country { Name = "Japan", IsoCode = "JPN", });
            await dbContext.Countries.AddAsync(new Country { Name = "Russia", IsoCode = "RUS", });
            await dbContext.Countries.AddAsync(new Country { Name = "United Kingdom", IsoCode = "GB", });
            await dbContext.Countries.AddAsync(new Country { Name = "France", IsoCode = "FR", });
            await dbContext.Countries.AddAsync(new Country { Name = "Italy", IsoCode = "IT", });
        }
    }
}
