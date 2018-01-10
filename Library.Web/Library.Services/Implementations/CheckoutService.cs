namespace Library.Services.Implementations
{
    using Library.Data;
    using Library.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckoutService : ICheckoutService
    {
        private readonly LibraryDbContext db;

        public CheckoutService(LibraryDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<Checkout> GetAll()
        {
            return this.db.Checkouts.ToList();
        }

        public Checkout GetById(int checkoutId)
        {
            return this.db
                .Checkouts
                .Where(c => c.Id == checkoutId)
                .FirstOrDefault();
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return this.db
                .Checkouts
                .Where(c => c.LibraryAssetId == assetId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public void Add(Checkout newCheckout)
        {
            this.db.Checkouts.Add(newCheckout);
            this.db.SaveChanges();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
            }

            var now = DateTime.Now;

            var checkOut = new Checkout()
            {
                LibraryCardId = libraryCardId,
                LibraryAssetId = assetId,
                Since = now,
                Until = now.AddDays(10)
            };

            var libraryAsset = this.db.LibraryAssets.FirstOrDefault(l => l.Id == assetId);
            libraryAsset.StatusId = 1;  // 1 checked out

            this.db.Checkouts.Add(checkOut);

            var checkoutHistory = new CheckoutHistory()
            {
                CheckedOut = now,
                LibraryAssetId = assetId,
                LibraryCardId = libraryCardId
            };

            this.db.CheckoutHistories.Add(checkoutHistory);

            this.db.SaveChanges();
        }

        private bool IsCheckedOut(int assetId)
        {
            return this.db.Checkouts.Any(c => c.LibraryAssetId == assetId);
        }

        public void CheckInItem(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;

            var item = this.db.LibraryAssets.FirstOrDefault(a => a.Id == assetId);

            //remove any existing checkouts on the item
            RemoveExistingCheckouts(assetId);

            //close any existing checkout history
            CloseExistingCheckoutHistory(assetId);

            //look for existing holds on the item
            var earliestHold = this.db
                .Holds
                .Where(h => h.LibraryAssetId == assetId)
                .OrderBy(h => h.HoldPlaced)
                .FirstOrDefault();

            //if there are holds, checkout the item to the librarycard with the earliest hold
            if (earliestHold != null)
            {
                var checkOut = new Checkout()
                {
                    LibraryCardId = earliestHold.LibraryCardId,
                    LibraryAssetId = assetId,
                    Since = DateTime.Now
                };

                this.db.Holds.Remove(earliestHold);

                this.db.Checkouts.Add(checkOut);
            }
            else
            {
                item.StatusId = 2;
            }
            //otherwise update the item status to available
            this.db.SaveChanges();

        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId)
        {
            return this.db
                .CheckoutHistories
                .Include(c => c.LibraryCard)
                .Include(c => c.LibraryAsset)
                .Where(c => c.LibraryAssetId == assetId)
                .ToList();
        }

        public string GetCurrentCheckoutPatron(int assetId)
        {
            return this.db
                .Checkouts
                .Where(c => c.LibraryAssetId == assetId)
                .Select(c => $"{c.LibraryCard.Patron.FirstName} {c.LibraryCard.Patron.LastName}")
                .FirstOrDefault() ?? "";
        }


        public void PlaceHold(int assetId, int libraryCardId)
        {
            var asset = this.db.LibraryAssets.Find(assetId);

            if (asset.Status.Name == "Available")
            {
                asset.StatusId = 4;  //4 On hold

            }

            var hold = new Hold()
            {
                LibraryAssetId = assetId,
                LibraryCardId = libraryCardId,
                HoldPlaced = DateTime.Now
            };

            this.db.Holds.Add(hold);
            this.db.SaveChanges();
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            return this.db
                .Holds
                .Where(h => h.Id == holdId)
                .Select(h => $"{h.LibraryCard.Patron.FirstName} {h.LibraryCard.Patron.LastName}")
                .FirstOrDefault();
        }

        public DateTime GetCurrentHoldPlaced(int holdId)
        {
            return this.db
                .Holds
                .Where(h => h.Id == holdId)
                .Select(h => h.HoldPlaced)
                .FirstOrDefault();

        }

        public IEnumerable<Hold> GetCurrentHolds(int assetId)
        {
            return this.db
                .Holds
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryAssetId == assetId)
                .ToList();
        }


        public void MarkLost(int assetId)
        {
            var asset = this.db.LibraryAssets.Find(assetId);

            asset.StatusId = 3;    //3 - "lost"

            this.db.SaveChanges();
        }

        public void MarkFound(int assetId)
        {
            //Change the status to available
            var asset = this.db.LibraryAssets.Find(assetId);

            asset.StatusId = 2;    //2 -  "Avaialable"

            //remove any existing checkouts
            RemoveExistingCheckouts(assetId);


            //close any existing checkout history
            CloseExistingCheckoutHistory(assetId);
            

            this.db.SaveChanges();
        }


        private void CloseExistingCheckoutHistory(int assetId)
        {
            var checkoutHistory = this.db
                .CheckoutHistories
                .FirstOrDefault(c => c.LibraryAssetId == assetId && c.CheckedIn == null);

            if (checkoutHistory != null)
            {
                checkoutHistory.CheckedIn = DateTime.Now;
            }
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            var checkOut = this.db.Checkouts.FirstOrDefault(c => c.Id == assetId);

            if (checkOut != null)
            {
                this.db.Checkouts.Remove(checkOut);
            }
        }
    }
}
