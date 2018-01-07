namespace Library.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class LibraryAsset
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Year { get; set; }

        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public int NumberOfCopies { get; set; }


        public Status Status { get; set; }

        public int StatusId { get; set; }


        public LibraryBranch LibraryBranch { get; set; }

        public int LibraryBranchId { get; set; }


        public List<Checkout> Checkouts { get; set; } = new List<Checkout>();


        public List<CheckoutHistory> CheckoutHistory { get; set; } = new List<CheckoutHistory>();
    }
}
