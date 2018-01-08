namespace Library.Services
{
    using Data.Models;
    using System.Collections.Generic;

    public interface ILibraryAssetService
    {
        IEnumerable<LibraryAsset> GetAll();

        LibraryAsset GetById(int id);

        void Add(LibraryAsset libraryAsset);

        string GetAuthorOrDirector(int id);

        string GetDeweyIndex(int id);

        string GetType(int id);

        string GetTitle(int id);

        string GetIsbn(int id);

        LibraryBranch GetCurrentLocation(int id);
    }
}
