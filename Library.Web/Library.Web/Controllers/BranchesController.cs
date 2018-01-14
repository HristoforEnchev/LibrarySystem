namespace Library.Web.Controllers
{
    using Library.Services;
    using Library.Web.Models.Branches;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class BranchesController : Controller
    {
        private readonly IBranchService branches;

        public BranchesController(IBranchService branches)
        {
            this.branches = branches;
        }

        public IActionResult Index()
        {
            var branches = this.branches.GetAll().Select(b => new BranchDetailModel
            {
                Id = b.Id,
                Name = b.Name,
                IsOpen = this.branches.IsBranchOpen(b.Id),
                NumberOfAssets = this.branches.GetAssetsBranch(b.Id).Count(),
                NumberOfPatrons = this.branches.GetPatronsBranch(b.Id).Count()
            });

            return View(new BranchIndexModel
            {
                Branches = branches
            });
        }

        public IActionResult Detail(int id)
        {
            var branch = this.branches.GetById(id);

            return View(new BranchDetailModel
            {
                Id = id,
                Name = branch.Name,
                Address = branch.Address,
                Telephone = branch.Telephone,
                OpenDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfAssets = this.branches.GetAssetsBranch(id).Count(),
                NumberOfPatrons = this.branches.GetPatronsBranch(id).Count(),
                TotalAssetValue = this.branches.GetAssetsBranch(id).Sum(a => a.Cost),
                ImageUrl = branch.ImageUrl,
                HoursOpen = this.branches.GetBranchHours(id)
            });
        }
    }
}
