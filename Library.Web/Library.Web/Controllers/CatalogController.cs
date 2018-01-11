namespace Library.Web.Controllers
{
    using Library.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Catalog;
    using Models.Checkout;
    using System.Linq;

    public class CatalogController : Controller
    {
        private readonly ILibraryAssetService libraryAssets;
        private readonly ICheckoutService checkOuts;

        public CatalogController(ILibraryAssetService libraryAssets, ICheckoutService checkOuts)
        {
            this.libraryAssets = libraryAssets;
            this.checkOuts = checkOuts;
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
                Type = this.libraryAssets.GetType(id),
                CheckoutHistory = this.checkOuts.GetCheckoutHistory(id),
                LatestCheckout = this.checkOuts.GetLatestCheckout(id),
                PatronName = this.checkOuts.GetCurrentCheckoutPatron(id),
                CurrentHolds = this.checkOuts.GetCurrentHolds(id)
            };

            return View(model);
        }

        public IActionResult Checkin(int assetId, int cardId)
        {
            this.checkOuts.CheckInItem(assetId);

            return RedirectToAction(nameof(Detail), new { id = assetId });
        }

        public IActionResult Checkout(int id)
        {
            var asset = this.libraryAssets.GetById(id);

            var model = new CheckoutModel()
            {
                AssetId = id,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                LibraryCardId = "",
                IsCheckedOut = this.checkOuts.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult Hold(int id)
        {
            var asset = this.libraryAssets.GetById(id);

            var model = new CheckoutModel()
            {
                AssetId = id,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                LibraryCardId = "",
                IsCheckedOut = this.checkOuts.IsCheckedOut(id),
                HoldCount = this.checkOuts.GetCurrentHolds(id).Count()
            };

            return View(model);
        }

        public IActionResult MarkLost(int assetId)
        {
            this.checkOuts.MarkLost(assetId);

            return RedirectToAction(nameof(Detail), new { id = assetId });
        }

        public IActionResult MarkFound(int assetId)
        {
            this.checkOuts.MarkFound(assetId);

            return RedirectToAction(nameof(Detail), new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            var isCheckedOut = this.checkOuts.IsCheckedOut(assetId);

            if (!isCheckedOut)
            {
                this.checkOuts.CheckOutItem(assetId, libraryCardId);
            }
            else
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Detail), new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            this.checkOuts.PlaceHold(assetId, libraryCardId);

            return RedirectToAction(nameof(Detail), new { id = assetId });
        }
    }
}
