namespace Library.Data.Models
{
    using System;

    public class Hold
    {
        public int Id { get; set; }

        public DateTime HoldPlaced { get; set; }


        public LibraryCard LibraryCard { get; set; }

        public int LibraryCardId { get; set; }


        public LibraryAsset LibraryAsset { get; set; }

        public int LibraryAssetId { get; set; }

    }
}
