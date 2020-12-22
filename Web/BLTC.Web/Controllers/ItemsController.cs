namespace BLTC.Web.Controllers
{
    using System.Collections.Generic;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Common;
    using BLTC.Web.ViewModels.Items;
    using BLTC.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : Controller
    {
        private readonly int defaultPageSize = 8;

        private readonly IItemsService itemsService;
        private readonly IManufacturersService manufacturersService;
        private readonly IDictionary<string, string> imagesNameAndContentType;

        public ItemsController(
            IItemsService itemsService,
            IManufacturersService manufacturersService)
        {
            this.itemsService = itemsService;
            this.manufacturersService = manufacturersService;
            this.imagesNameAndContentType = new Dictionary<string, string>();
        }

        public IReadOnlyDictionary<string, string> Images => (IReadOnlyDictionary<string, string>)this.imagesNameAndContentType;

        [HttpGet]
        [AllowAnonymous]
        [Route("products/available/everything")]
        public IActionResult All(int pageNumber = 1)
        {
            var approvedItems = this.itemsService.GetAllApprovedItems<ItemsAllViewModel>();
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = approvedItems };

            return this.View(PaginatedList<ItemsAllViewModel>.Create(viewModel.Items, pageNumber, this.defaultPageSize));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("products/everything/customfilter/{type?}/{shape?}/{weight?}")]
        public IActionResult Filter(string type, string shape, decimal? weight, int pageNumber = 1)
        {
            var filteredItems = this.itemsService.GetItemsByFilter<ItemsAllViewModel>(type);
            var title = $"{type} Items";
            if (!string.IsNullOrEmpty(shape) && (weight is null || weight <= 0))
            {
                filteredItems = this.itemsService.GetItemsByFilter<ItemsAllViewModel>(type, shape);
                title = $"{type} {shape}";
            }
            else if (!string.IsNullOrEmpty(shape) && weight > 0)
            {
                filteredItems = this.itemsService.GetItemsByFilter<ItemsAllViewModel>(type, shape, weight);
                title = $"{weight} Gram {type} {shape}";
            }

            if (filteredItems is null)
            {
                return this.NotFound();
            }

            var listItems = new ItemsAllListViewModel<ItemsAllViewModel> { Items = filteredItems };
            var viewModel = PaginatedList<ItemsAllViewModel>.Create(listItems.Items, pageNumber, this.defaultPageSize);
            viewModel.Title = title;

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("products/everything/details_for/{itemId?}")]
        public IActionResult Details(int itemId)
        {
            if (itemId <= 0)
            {
                return this.NotFound();
            }

            var item = this.itemsService.GetItemById<ItemDetailsViewModel>(itemId);
            if (item is null)
            {
                return this.NotFound();
            }

            var manufacturer = this.manufacturersService.GetByName<ManufacturersInfoViewModel>(item.ManufacturerName);
            var viewModel = new ItemDetailsViewModel
            {
                Id = item.Id,
                Name = item.Name,
                ManufacturerId = manufacturer.Id,
                ManufacturerName = manufacturer.Name,
                ManufacturerLogo = manufacturer.Logo,
                Weight = item.Weight,
                Purity = item.Purity,
                Fineness = item.Fineness,
                Quantity = item.Quantity,
                Dimensions = item.Dimensions,
                Description = item.Description,
                IsApproved = item.IsApproved,
                Images = item.Images,
            };

            return this.View(viewModel);
        }
    }
}
