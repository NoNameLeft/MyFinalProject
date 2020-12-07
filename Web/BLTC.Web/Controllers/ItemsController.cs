namespace BLTC.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : Controller
    {
        private readonly string pattern = @"^(.*)\.(gif|jpe?g|bmp|png|tiff|svg|webp)$";

        private readonly IItemsService itemsService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IUsersService usersService;
        private readonly IImagesService imagesService;
        private readonly IDictionary<string, string> imagesNameAndContentType;

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
            this.imagesNameAndContentType = new Dictionary<string, string>();
        }

        public IReadOnlyDictionary<string, string> Images => (IReadOnlyDictionary<string, string>)this.imagesNameAndContentType;

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

            var userId = this.usersService.GetUserIdByUsername(input.Username).Result;

            var uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "images", "items");
            foreach (var file in input.Files)
            {
                var filePath = Path.Combine(uploadsFolder, file.FileName);
                this.AddFilesToDict(this.imagesNameAndContentType, file.FileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            var itemId =
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

            var item = this.itemsService.GetItemById(itemId.Result).Result;
            var itemImages = this.imagesService.Add(userId, this.Images, item.GetType(), itemId.Result);

            this.itemsService.AddImagesToItem(itemImages.Result, itemId.Result);

            return this.Redirect("/");
        }

        private void AddFilesToDict(IDictionary<string, string> dict, string fileName)
        {
            var fileNameAndType = Regex.Match(fileName, this.pattern); // abc.jpg --> (abc)(jpg)

            fileName = fileNameAndType.Groups[1].Value.ToString();
            var contentType = this.ChangeContentType(fileNameAndType.Groups[2].Value.ToString());

            dict.Add(fileName, contentType);
        }

        private string ChangeContentType(string extention)
        {
            switch (extention)
            {
                case "jpg":
                    extention = "image/jpeg";
                    break;
                case "png":
                    extention = "image/png";
                    break;
                case "gif":
                    extention = "image/gif";
                    break;
                case "svg":
                    extention = "image/svg+xml";
                    break;
                case "tiff":
                    extention = "image/tiff";
                    break;
                case "webp":
                    extention = "image/webp";
                    break;
                case "bmp":
                    extention = "image/bmp";
                    break;
            }

            return extention;
        }
    }
}
