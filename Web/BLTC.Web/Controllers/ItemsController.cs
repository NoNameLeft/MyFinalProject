namespace BLTC.Web.Controllers
{
    using System.IO;
    using System.Linq;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ItemsController(IItemsService itemsService, IWebHostEnvironment hostingEnvironment)
        {
            this.itemsService = itemsService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddItemlnputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                // TODO: Redirect to all items page
                return this.View(input);
            }

             // var uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "images");

            // var filePath = Path.Combine(uploadsFolder, input.Files.FileName);

            // input.Files.CopyTo(new FileStream(filePath, FileMode.Create));
            this.itemsService.Add(
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

            return this.Redirect("/");
        }
    }
}
