namespace Library.Services.Implementations
{
    using Data;
    using Library.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LibraryAssetService : ILibraryAssetService
    {
        private readonly LibraryDbContext db;

        public LibraryAssetService(LibraryDbContext db)
        {
            this.db = db;
        }

        public void Add(LibraryAsset libraryAsset)
        {
            this.db.LibraryAssets.Add(libraryAsset);
            this.db.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return this.db
                .LibraryAssets
                .Include(la => la.LibraryBranch)
                .Include(la => la.Status)
                .ToList();
        }

        public LibraryAsset GetById(int id)
        {
            return this.db
                .LibraryAssets
                .Where(la => la.Id == id)
                .Include(la => la.LibraryBranch)
                .Include(la => la.Status)
                .FirstOrDefault();
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return this.db
                .LibraryAssets
                .Where(la => la.Id == id)
                .Select(la => la.LibraryBranch)
                .FirstOrDefault();
        }

        public string GetDeweyIndex(int id)      // .OfType<Book>()
        {
            if (this.db.Books.Any(b => b.Id == id))   
            {
                return this.db
                    .Books
                    .Where(b => b.Id == id)
                    .Select(b => b.DeweyIndex)
                    .FirstOrDefault();
            }

            return "";
        }

        public string GetIsbn(int id)
        {
            if (this.db.Books.Any(b => b.Id == id))
            {
                return this.db
                    .Books
                    .Where(b => b.Id == id)
                    .Select(b => b.ISBN)
                    .FirstOrDefault();
            }

            return "";
        }

        public string GetTitle(int id)
        {
            return this.db
                .LibraryAssets
                .Where(la => la.Id == id)
                .Select(la => la.Title)
                .FirstOrDefault();
        }

        public string GetType(int id)
        {
            return this.db
                .LibraryAssets
                .Where(la => la.Id == id)
                .FirstOrDefault()
                .GetType()
                .ToString();
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = this.db
                            .LibraryAssets
                            .OfType<Book>()
                            .Where(la => la.Id == id)
                            .Any();

            return isBook ? this.db
                    .Books
                    .Where(la => la.Id == id)
                    .Select(la => la.Author)
                    .FirstOrDefault()
                    :
                    this.db
                    .Videos
                    .Where(la => la.Id == id)
                    .Select(la => la.Director)
                    .FirstOrDefault()
                    ??
                    "Unknown";
        }
    }
}
