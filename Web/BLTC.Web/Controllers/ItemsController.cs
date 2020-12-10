namespace BLTC.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : Controller
    {
        private readonly string pattern = @"^(.*)\.(gif|jpe?g|bmp|png|tiff|svg|webp)$";

        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IItemsService itemsService;
        private readonly IUsersService usersService;
        private readonly IImagesService imagesService;
        private readonly IManufacturersService manufacturersService;
        private readonly IDictionary<string, string> imagesNameAndContentType;

        public ItemsController(
            IWebHostEnvironment hostingEnvironment,
            IItemsService itemsService,
            IUsersService usersService,
            IImagesService imagesService,
            IManufacturersService manufacturersService)
        {
            this.itemsService = itemsService;
            this.hostingEnvironment = hostingEnvironment;
            this.usersService = usersService;
            this.imagesService = imagesService;
            this.manufacturersService = manufacturersService;
            this.imagesNameAndContentType = new Dictionary<string, string>();
        }

        public IReadOnlyDictionary<string, string> Images => (IReadOnlyDictionary<string, string>)this.imagesNameAndContentType;

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddItemlnputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // check if you are register user (will add authonetication later)
            var userId = this.usersService.GetUserIdByUsername(input.Username).Result;

            // adds uploaded images to the folder
            // won't add file with the same name
            this.AddFilesToFolder(input.Files);

            // creates item (product) in the db and get id
            var itemId = await this.itemsService.Add(
                    input.Name,
                    input.Type,
                    input.Shape,
                    input.Weight,
                    input.Purity,
                    input.Fineness,
                    input.Quantity,
                    input.Dimensions,
                    input.Description,
                    input.Manufacturer);

            // make user there is no two items with the same name
            // get the actual Item (db enitity) with the Id
            var item = await this.itemsService.GetItemById(itemId);

            // add info for the uploaded images to db
            var itemImages = await this.imagesService.Add(userId, this.Images, item.GetType(), itemId);

            // makes relation between newly created images and the item
            this.itemsService.AddImagesToItem(itemImages, itemId);

            return this.Redirect("/Items/All"); // decide where to redirect to!!!
        }

        public IActionResult All()
        {
            // gets all items from the db and map them to the view model
            var items = this.itemsService.GetAllItems<AllItemsViewModel>();

            // create new list of all items view model
            var viewModel = new AllItemsListViewModel { Items = items };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int itemId)
        {
            // gets an item by id and map it to the view model
            var item = this.itemsService.GetItem<ItemDetailsViewModel>(itemId).FirstOrDefault();

            // gets manufacturer id by its name
            var manufacturerId = await this.manufacturersService.GetIdByName(item.ManufacturerName);

            // takes manfacturer db enitity by its id
            var manufacturer = await this.manufacturersService.GetManufacturerById(manufacturerId);

            // create new item details page view model
            var viewModel = new ItemDetailsViewModel
            {
                Id = item.Id,
                Name = item.Name,
                ManufacturerId = manufacturerId,
                ManufacturerName = item.ManufacturerName,
                ManufacturerLogo = await this.imagesService.GetImageById(manufacturer.LogoId),
                Weight = item.Weight,
                Purity = item.Purity,
                Fineness = (int)item.Fineness,
                Dimensions = item.Dimensions,
                Description = item.Description,
                Images = item.Images,
            };

            return this.View(viewModel);
        }

        private void AddFilesToDict(IDictionary<string, string> dict, string fileName)
        {
            // if not match throw Error (wrong image type or something)
            if (Regex.IsMatch(fileName, this.pattern))
            {
                var contentType = Regex.Match(fileName, this.pattern).Groups[2].Value.ToString();

                dict.Add(fileName, contentType);
            }
        }

        private void AddFilesToFolder(IFormFileCollection files)
        {
            var uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "images", "items");
            var existingFileNames = this.ExistingFileNames(uploadsFolder);
            foreach (var file in files)
            {
                if (this.ExistingFileNames(uploadsFolder).Any(x => x == file.FileName))
                {
                    continue;
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);
                this.AddFilesToDict(this.imagesNameAndContentType, file.FileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
            }
        }

        private string[] ExistingFileNames(string folder)
        {
            return Directory.GetFiles(folder)
                            .Select(Path.GetFileName)
                            .ToArray();
        }
    }
}
