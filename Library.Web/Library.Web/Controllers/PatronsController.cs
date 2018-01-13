namespace Library.Web.Controllers
{
    using Library.Data.Models;
    using Library.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models.Patrons;
    using System.Collections.Generic;
    using System.Linq;

    public class PatronsController : Controller
    {
        private readonly IPatronService patrons;

        public PatronsController(IPatronService patrons)
        {
            this.patrons = patrons;
        }

        public IActionResult Index()
        {
            var patronsModel = this.patrons
                .GetAll()
                .Select(p => new PatronDetailModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    LibraryCardId = p.LibraryCard.Id,
                    OverdueFees = p.LibraryCard.Fees,
                    HomeLibraryBranch = p.LibraryCard.Patron.LibraryBranch.Name,

                })
                .ToList();

            return View(new PatronIndexModel
            {
                Patrons = patronsModel
            });
        }

        public IActionResult Details(int id)
        {
            var model = this.patrons.GetById(id);

            return View(new PatronDetailModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                HomeLibraryBranch = model.LibraryBranch.Name,
                MemberSince = model.LibraryCard.Created,
                OverdueFees = model.LibraryCard.Fees,
                LibraryCardId = model.LibraryCard.Id,
                Telephone = model.TelephoneNumber,
                AssetsCheckedOut = this.patrons.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = this.patrons.GetCheckoutHistory(id),
                Holds = this.patrons.GetHolds(id).ToList() 
            });
        }
    }
}
