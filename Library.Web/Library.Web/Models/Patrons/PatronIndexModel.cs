using System.Collections.Generic;

namespace Library.Web.Models.Patrons
{
    public class PatronIndexModel
    {
        public IEnumerable<PatronDetailModel> Patrons { get; set; }
    }
}
