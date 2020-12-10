namespace BLTC.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Common;
    using BLTC.Data.Models;

    public class ManufacturersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Manufacturers.Any())
            {
                return;
            }

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.PAMPManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "Switzerland").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.PAMPManufacturerName + ".png").Id,
                Overview = "PAMP Suisse, also known as Produits Artistiques Métaux Précieux, are one of the two biggest precious metal specialists in Switzerland. PAMP produces gold, silver and platinum bullion, as well as aiding the Swiss watch industry. The two main brands of PAMP bars are the Lady Fortuna design and the Rosa, but the company also produces an annual Chinese Lunar series to rival products from the Royal Mint and the Perth Mint.",
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.ScottsdaleManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "United States of America").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.ScottsdaleManufacturerName + ".png").Id,
                Overview = "American silver refiners Scottsdale - sometimes known as the Scottsdale Mint or Scottsdale Silver - are a premier silver manufacturer, well known for their varied range of bullion and their impressive recycling work. Based near Phoenix, Arizona,USA, the company makes intricately - crafted minted silver bullion bars and stackers, as well as larger cast ingots for greater investments. All of Scottsdale's silver is .999 purity or better, as per LBMA standards.",
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.PerthMintManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "Australia").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.PerthMintManufacturerName + ".png").Id,
                Overview = "The Perth Mint in Western Australia is the official producer of Australian gold and silver bullion coins on behalf of the Australian government. Operating out of a former Royal Mint refinery, the Perth Mint is one of the forerunners in bullion coin quality, and along with the Canadian Mint were amongst the first to hit .9999 quality silver.",
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.RoyalMintManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "Canada").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.RoyalMintManufacturerName + ".png").Id,
                Overview = "The Royal Mint is responsible for Britain's coinage. Based in Wales, the organisation is one of the oldest mints in Europe and has been producing coins like the Gold Sovereign since 1817. The Britannia is the other major British coin from the mint, with a new coin produced each year. The Royal Mint are also making variants of this coin for collectors, including the Two Dragons and the Royal Arms coins, to attract new customers.",
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.HeraeusManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "Germany").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.HeraeusManufacturerName + ".png").Id,
                Overview = "German refiners Heraeus have been producing gold since the 19th century, but it was only in 1851 the business really began to expand. Specialising in gold and platinum, Heraeus are one of the oldest bullion refiners in Europe.",
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.UmicoreManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "Belgium").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.UmicoreManufacturerName + ".png").Id,
                Overview = "Belgian bullion refiners Umicore are one of Europe's leading manufacturers of gold and silver bars. Producing minted and cast ingots, the company boasts a strong track record of fine quality metal and recycles metals as well, maximising supply and helping lower premiums.",
            });

            await dbContext.Manufacturers.AddAsync(new Manufacturer
            {
                Name = GlobalConstants.MetalorManufacturerName,
                CountryId = dbContext.Countries.FirstOrDefault(x => x.Name == "Switzerland").Id,
                LogoId = dbContext.Images.FirstOrDefault(x => x.Name == GlobalConstants.MetalorManufacturerName + ".png").Id,
                Overview = "Formerly known as Métaux Précieux SA Metalor, the Swiss refiners Metalor have been operating at a site near Neuchâtel since 1852. The company is owned by Tanaka Kikinzoku Group from Japan and produces a variety of large and small bullion bars, in cast or minted styles.",
            });
        }
    }
}
