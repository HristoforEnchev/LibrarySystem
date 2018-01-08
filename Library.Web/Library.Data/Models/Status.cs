namespace Library.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Status
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<LibraryAsset> LibraryAssets { get; set; } = new List<LibraryAsset>();
    }
}
