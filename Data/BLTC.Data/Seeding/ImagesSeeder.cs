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

            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.PAMPManufacturerName + ".png", Extension = "image/png", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.ScottsdaleManufacturerName + ".png", Extension = "image/png", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.PerthMintManufacturerName + ".png", Extension = "image/png", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.RoyalMintManufacturerName + ".png", Extension = "image/png", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.HeraeusManufacturerName + ".png", Extension = "image/png", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.UmicoreManufacturerName + ".png", Extension = "image/png", });
            await dbContext.Images.AddAsync(new Image { AddedByEmployeeId = this.defaulUserId, Name = GlobalConstants.MetalorManufacturerName + ".png", Extension = "image/png", });
        }
    }
}
