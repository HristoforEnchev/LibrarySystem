namespace Library.Services.Implementations
{
    using Library.Data;
    using Library.Data.Models;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class PatronService : IPatronService
    {
        private readonly LibraryDbContext db;

        public PatronService(LibraryDbContext db)
        {
            this.db = db;
        }

        public Patron GetById(int id)
        {
            return this.db
                .Patrons
                .Include(p => p.LibraryCard)
                .Include(p => p.LibraryBranch)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return this.db
                .Patrons
                .Include(p => p.LibraryCard)
                .Include(p => p.LibraryBranch)
                .ToList();
        }

        public void AddPatron(Patron newPatron)
        {
            if (newPatron != null)
            {
                this.db.Patrons.Add(newPatron);
                this.db.SaveChanges();
            }
        }


        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId)
        {
            return this.db
                .CheckoutHistories
                .Where(ch => ch.LibraryCard.PatronId == patronId)
                .Include(ch => ch.LibraryCard)
                .Include(ch => ch.LibraryAsset)
                .OrderByDescending(ch => ch.CheckedOut)
                .ToList();
        }

        public IEnumerable<Checkout> GetCheckouts(int patronId)
        {
            return this.db
                .Checkouts
                .Where(ch => ch.LibraryCard.PatronId == patronId)
                .Include(ch => ch.LibraryCard)
                .Include(ch => ch.LibraryAsset)
                .ToList();
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            return this.db
                .Holds
                .Where(ch => ch.LibraryCard.PatronId == patronId)
                .Include(ch => ch.LibraryCard)
                .Include(ch => ch.LibraryAsset)
                .ToList();
        }
    }
}
