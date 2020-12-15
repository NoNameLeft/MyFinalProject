namespace BLTC.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using BLTC.Common;
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Add(ItemAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // else if (await this.itemsService.GetIdByName(input.Name) <= 0)
            // {
            //     return this.StatusCode(409, $"Item with name '{input.Name}' already exists.");
            // }

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
                    input.ManufacturerId);

            // make user there is no two items with the same name
            // get the actual Item (db enitity) with the Id
            var item = await this.itemsService.GetItemById(itemId);

            // add info for the uploaded images to db
            var itemImages = await this.imagesService.Add(userId, this.Images, item.GetType(), itemId);

            // makes relation between newly created images and the item
            await this.itemsService.AddImagesToItem(itemImages, itemId);

            return this.RedirectToAction("Details", new { itemId = item.Id });
        }

        [Authorize]
        public IActionResult Edit(int itemId)
        {
            var item = this.itemsService.GetItem<ItemEditInputModel>(itemId).SingleOrDefault();

            var viewModel = new ItemEditInputModel
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Type = item.Type,
                Shape = item.Shape,
                Weight = item.Weight,
                Purity = item.Purity,
                Fineness = item.Fineness,
                Quantity = item.Quantity,
                Dimensions = item.Dimensions,
                Description = item.Description,
                ManufacturerId = item.ManufacturerId,
                Images = item.Images,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(ItemEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            this.AddFilesToFolder(input.Files); // adds files to the images folder
            var userId = await this.usersService.GetUserIdByUsername(input.Username); // finds the current admin
            var item = await this.itemsService.GetItemById(input.ItemId); // gets the item by its identity
            var itemImages = await this.imagesService.Add(userId, this.Images, item.GetType(), item.Id); // adds the files as images in the db

            // change the item's values
            item.Name = input.Name;
            item.Type = (ItemType)input.Type;
            item.Shape = (ItemShape)input.Shape;
            item.Weight = input.Weight;
            item.Purity = input.Purity;
            item.Quantity = input.Quantity;
            item.Dimensions = input.Dimensions;
            item.Description = input.Description;
            item.ManufacturerId = input.ManufacturerId;

            await this.itemsService.Edit(item); // update item in the db
            await this.itemsService.AddImagesToItem(itemImages, item.Id); // adds the newly created images to the current item

            return this.RedirectToAction("Details", new { itemId = item.Id });
        }

        public IActionResult All()
        {
            // gets all approved by the admin items from the db and map them to the view model
            var approvedItems = this.itemsService.GetAllApprovedItems<ItemsAllViewModel>();

            // create new list of all items view model
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = approvedItems };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Pending()
        {
            var pendingItems = this.itemsService.GetAllPendingItems<ItemsAllViewModel>();
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = pendingItems };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int itemId)
        {
            // gets an item by id and map it to the view model
            var item = this.itemsService.GetItem<ItemDetailsViewModel>(itemId).SingleOrDefault();

            // gets manufacturer id by its name
            var manufacturerId = await this.manufacturersService.GetIdByName(item.ManufacturerName);

            // takes manfacturer db enitity by its id
            var manufacturer = await this.manufacturersService.GetById(manufacturerId);

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
                Quantity = item.Quantity,
                Dimensions = item.Dimensions,
                Description = item.Description,
                IsApproved = item.IsApproved,
                Images = item.Images,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Approve(int itemId)
        {
            var item = await this.itemsService.GetItemById(itemId);
            item.IsApproved = true;
            await this.itemsService.Edit(item);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Disprove(int itemId)
        {
            var item = await this.itemsService.GetItemById(itemId);
            item.IsApproved = false;
            await this.itemsService.Edit(item);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int itemId)
        {
            await this.itemsService.Delete(itemId);

            return this.RedirectToAction("All");
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
                    continue; // it should stop the file from adding!
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
