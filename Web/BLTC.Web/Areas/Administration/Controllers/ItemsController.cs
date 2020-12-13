namespace BLTC.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : AdministrationController
    {
        private readonly string pattern = @"^(.*)\.(gif|jpe?g|bmp|png|tiff|svg|webp)$";

        private readonly IItemsService itemsService;
        private readonly IManufacturersService manufacturersService;
        private readonly IImagesService imagesService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IUsersService usersService;
        private readonly IDictionary<string, string> uploadedImageFiles;

        public ItemsController(IItemsService itemsService, IManufacturersService manufacturersService, IImagesService imagesService, IWebHostEnvironment hostingEnvironment, IUsersService usersService)
        {
            this.itemsService = itemsService;
            this.manufacturersService = manufacturersService;
            this.imagesService = imagesService;
            this.hostingEnvironment = hostingEnvironment;
            this.usersService = usersService;
            this.uploadedImageFiles = new Dictionary<string, string>();
        }

        public IReadOnlyDictionary<string, string> Images => (IReadOnlyDictionary<string, string>)this.uploadedImageFiles;

        public IActionResult All()
        {
            // gets all approved by the admin items from the db and map them to the view model
            var pendingItems = this.itemsService.GetAllPendingItems<ItemsAllViewModel>();

            // create new list of all items view model
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = pendingItems };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int itemId)
        {
            var item = this.itemsService.GetItem<ItemDetailsViewModel>(itemId).SingleOrDefault();
            var manufacturerId = await this.manufacturersService.GetIdByName(item.ManufacturerName);
            var manufacturer = await this.manufacturersService.GetById(manufacturerId);

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
                Images = item.Images,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Approve(int itemId)
        {
            var item = await this.itemsService.GetItemById(itemId);
            item.IsApproved = true;
            await this.itemsService.Edit(item);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Disprove(int itemId)
        {
            var item = await this.itemsService.GetItemById(itemId);
            item.IsApproved = false;
            await this.itemsService.Edit(item);

            return this.RedirectToAction("All");
        }

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

        public async Task<IActionResult> Delete(int itemId)
        {
            await this.itemsService.Delete(itemId);

            return this.RedirectToAction("All");
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
                this.AddFilesToDict(this.uploadedImageFiles, file.FileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
            }
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

        private string[] ExistingFileNames(string folder)
        {
            return Directory.GetFiles(folder)
                            .Select(Path.GetFileName)
                            .ToArray();
        }
    }
}
