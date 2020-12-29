namespace BLTC.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Services.Data.Models;
    using BLTC.Web.ViewModels.Common;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : AdministrationController
    {
        private readonly int defaultPageSize = 8;

        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IItemsService itemsService;
        private readonly IUsersService usersService;
        private readonly IImagesService imagesService;

        public ItemsController(
            IWebHostEnvironment hostingEnvironment,
            IItemsService itemsService,
            IUsersService usersService,
            IImagesService imagesService)
        {
            this.itemsService = itemsService;
            this.hostingEnvironment = hostingEnvironment;
            this.usersService = usersService;
            this.imagesService = imagesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ItemAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var dto = new CreateItemDto()
            {
                Name = input.Name,
                Type = input.Type,
                Shape = input.Shape,
                Weight = input.Weight,
                Purity = input.Purity,
                Fineness = input.Fineness,
                Quantity = input.Quantity,
                Dimensions = input.Dimensions,
                Description = input.Description,
                ManufacturerId = input.ManufacturerId,
            };

            var userId = await this.usersService.GetIdAsync(input.Username);
            await this.itemsService.CreateAsync(dto, userId, input.Files, Path.Combine(this.hostingEnvironment.WebRootPath, "images", "items"));

            return this.RedirectToAction("Pending");
        }

        public IActionResult Edit(int itemId)
        {
            var item = this.itemsService.GetItemById<ItemEditInputModel>(itemId);
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

            var dto = new UpdateItemDto()
            {
                Name = input.Name,
                Type = input.Type,
                Shape = input.Shape,
                Weight = input.Weight,
                Purity = input.Purity,
                ItemId = input.ItemId,
                Images = input.Files,
                Quantity = input.Quantity,
                Dimensions = input.Dimensions,
                Description = input.Description,
                ManufacturerId = input.ManufacturerId,
            };

            await this.itemsService.UpdateAsync(dto, await this.usersService.GetIdAsync(input.Username), Path.Combine(this.hostingEnvironment.WebRootPath, "images", "items"));

            return this.RedirectToAction("Details", new { itemId = input.ItemId });
        }

        [Route("products/pending/everything")]
        public IActionResult Pending(int pageNumber = 1)
        {
            var pendingItems = this.itemsService.GetAllPendingItems<ItemsAllViewModel>();
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = pendingItems };

            return this.View(PaginatedList<ItemsAllViewModel>.Create(viewModel.Items, pageNumber, this.defaultPageSize));
        }

        public async Task<IActionResult> Approve(int itemId)
        {
            await this.itemsService.ChangeStatus(itemId);

            return this.RedirectToAction("Pending");
        }

        public async Task<IActionResult> Disprove(int itemId)
        {
            await this.itemsService.ChangeStatus(itemId);

            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> Delete(int itemId)
        {
            await this.itemsService.DeleteAsync(itemId);

            return this.RedirectToAction("Pending");
        }
    }
}
