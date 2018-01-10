namespace Library.Web.Controllers
{
    using Library.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Catalog;
    using System.Linq;

    public class CatalogController : Controller
    {
        private readonly ILibraryAssetService libraryAssets;

        public CatalogController(ILibraryAssetService libraryAssets)
        {
            this.libraryAssets = libraryAssets;
        }


        public IActionResult Index()
        {
            var result = this.libraryAssets
                .GetAll()
                .Select(la => new AssetIndexListingModel
                {
                    Id = la.Id,
                    ImageUrl = la.ImageUrl,
                    AuthorOrDirector = this.libraryAssets.GetAuthorOrDirector(la.Id),
                    DeweyCallNumber = this.libraryAssets.GetDeweyIndex(la.Id),
                    Title = la.Title,
                    Type = this.libraryAssets.GetType(la.Id),
                    NumberOfCopies = la.NumberOfCopies
                })
                .ToList();

            return View(new AssetIndexModel
            {
                Assets = result
            });
        }

        public IActionResult Detail(int id)
        {
            var asset = this.libraryAssets.GetById(id);

            var model = new AssetDetailModel
            {
                Id = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = this.libraryAssets.GetAuthorOrDirector(id),
                CurrentLocation = asset.LibraryBranch.Name,
                DeweyCallNumber = this.libraryAssets.GetDeweyIndex(id),
                ISBN = this.libraryAssets.GetIsbn(id),
                Type = this.libraryAssets.GetType(id)
            };

            return View(model);
        }
    }
}
