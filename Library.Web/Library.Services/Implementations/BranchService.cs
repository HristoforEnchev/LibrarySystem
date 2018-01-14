namespace Library.Services.Implementations
{
    using System.Collections.Generic;
    using Library.Data;
    using Library.Data.Models;
    using Library.Services;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class BranchService : IBranchService
    {
        private readonly LibraryDbContext db;

        public BranchService(LibraryDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return this.db
                .LibraryBranches
                .Include(lb => lb.Patrons)
                .Include(lb => lb.LibraryAssets)
                .Include(lb => lb.BranchHours)
                .ToList();
        }

        public IEnumerable<Patron> GetPatronsBranch(int branchId)
        {
            return this.db
                .Patrons
                .Include(p => p.LibraryBranch)
                .Where(p => p.LibraryBranchId == branchId)
                .ToList();
        }

        public IEnumerable<LibraryAsset> GetAssetsBranch(int branchId)
        {
            return this.db
                .LibraryAssets
                .Where(la => la.LibraryBranchId == branchId)
                .ToList();
        }

        public LibraryBranch GetById(int branchId)
        {
            return this.db
                .LibraryBranches
                .Include(lb => lb.Patrons)
                .Include(lb => lb.LibraryAssets)
                .FirstOrDefault(lb => lb.Id == branchId);
        }

        public void Add(LibraryBranch newBranch)
        {
            this.db.LibraryBranches.Add(newBranch);
            this.db.SaveChanges();
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            return this.db
                .BranchHours
                .Where(bh => bh.LibraryBranchId == branchId)
                .OrderBy(bh => bh.DayOfWeek)
                .Select(bh => $"{ConvertIntToDay(bh.DayOfWeek)} {bh.OpenTime}:00 to {bh.CloseItem}:00")
                .ToList();

            
                
        }

        public bool IsBranchOpen(int branchId)
        {
            var workingHours = this.db.BranchHours.Where(bh => bh.LibraryBranchId == branchId).ToList();

            return IsBranchWorking(workingHours);
        }

        private bool IsBranchWorking(List<BranchHours> workingHours)
        {
            var dayNow = DateTime.Now.DayOfWeek;
            var hourNow = DateTime.Now.Hour;

            return workingHours.Any(w => ConvertIntToDay(w.DayOfWeek - 1) == dayNow.ToString()
                                                        && w.OpenTime <= hourNow
                                                        && w.CloseItem > hourNow);
        }

        public static string ConvertIntToDay(int num)
        {
            switch (num)
            {
                case 1:
                    return "Sunday";
                case 2:
                    return "Monday";
                case 3:
                    return "Tuesday";
                case 4:
                    return "Wednesday";
                case 5:
                    return "Thursday";
                case 6:
                    return "Friday";
                case 7:
                    return "Saturday";
                default:
                    return "No day";
            }
        }
    }
}
