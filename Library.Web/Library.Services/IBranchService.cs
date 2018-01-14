namespace Library.Services
{
    using Library.Data.Models;
    using System.Collections.Generic;

    public interface IBranchService
    {
        IEnumerable<LibraryBranch> GetAll();

        IEnumerable<Patron> GetPatronsBranch(int branchId);

        IEnumerable<LibraryAsset> GetAssetsBranch(int branchId);

        LibraryBranch GetById(int branchId);

        void Add(LibraryBranch newBranch);

        IEnumerable<string> GetBranchHours(int branchId);

        bool IsBranchOpen(int branchId);
    }
}
