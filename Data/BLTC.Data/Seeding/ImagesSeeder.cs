namespace BLTC.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Common;
    using BLTC.Data.Models;

    public class ImagesSeeder : ISeeder
    {
        private string defaulUserId = string.Empty;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            this.defaulUserId = dbContext.Users.FirstOrDefault(x => x.UserName == GlobalConstants.DefaultOwner).Id;

            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.PAMPManufacturerName, Extension = ".pgn", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.ScottsdaleManufacturerName, Extension = ".pgn", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.PerthMintManufacturerName, Extension = ".pgn", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.RoyalMintManufacturerName, Extension = ".pgn", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.HeraeusManufacturerName, Extension = ".pgn", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.UmicoreManufacturerName, Extension = ".pgn", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.MetalorManufacturerName, Extension = ".pgn", });
        }
    }
}
