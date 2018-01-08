namespace Library.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class LibraryCard
    {
        public int Id { get; set; }

        public decimal Fees { get; set; }

        public DateTime Created { get; set; }


        public List<Checkout> Checkouts { get; set; } = new List<Checkout>();


        public List<CheckoutHistory> CheckoutHistory { get; set; } = new List<CheckoutHistory>();


        public List<Hold> Holds { get; set; } = new List<Hold>();


        public Patron Patron { get; set; }

        public int PatronId { get; set; }
    }
}
