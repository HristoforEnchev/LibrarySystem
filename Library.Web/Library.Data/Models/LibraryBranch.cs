namespace Library.Data.Models
{
    using System.Collections.Generic;

    public class LibraryBranch
    {
        public int Id { get; set; }


        public List<BranchHours> BranchHours { get; set; } = new List<BranchHours>();
    }
}
