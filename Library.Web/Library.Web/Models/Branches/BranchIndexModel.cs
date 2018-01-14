namespace Library.Web.Models.Branches
{
    using System.Collections.Generic;

    public class BranchIndexModel
    {
        public IEnumerable<BranchDetailModel> Branches { get; set; }
    }
}
