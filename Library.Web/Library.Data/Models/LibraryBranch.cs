namespace Library.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LibraryBranch
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Limit branch name to 30 characters!")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Description { get; set; }

        public DateTime OpenDate { get; set; }

        public string ImageUrl { get; set; }


        public List<BranchHours> BranchHours { get; set; } = new List<BranchHours>();


        public List<LibraryAsset> LibraryAssets { get; set; } = new List<LibraryAsset>();


        public List<Patron> Patrons { get; set; } = new List<Patron>();
    }
}
