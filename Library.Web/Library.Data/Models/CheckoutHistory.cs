namespace Library.Data.Models
{
    using System;

    public class CheckoutHistory
    {
        public int Id { get; set; }

        public DateTime? CheckedIn { get; set; }

        public DateTime CheckedOut { get; set; }


        public LibraryCard LibraryCard { get; set; }

        public int LibraryCardId { get; set; }


        public LibraryAsset LibraryAsset { get; set; }

        public int LibraryAssetId { get; set; }
    }
}
