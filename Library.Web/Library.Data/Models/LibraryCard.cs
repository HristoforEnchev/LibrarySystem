using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Models
{
    public class LibraryCard
    {
        public int Id { get; set; }


        public List<Checkout> Checkouts { get; set; } = new List<Checkout>();


        public List<CheckoutHistory> CheckoutHistory { get; set; } = new List<Models.CheckoutHistory>();


        public Patron Patron { get; set; }

        public int PatronId { get; set; }
    }
}
