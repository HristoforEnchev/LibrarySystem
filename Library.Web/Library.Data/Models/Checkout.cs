using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        public DateTime Since { get; set; }

        public DateTime Until { get; set; }


        public LibraryCard LibraryCard { get; set; }

        public int LibraryCardId { get; set; }


        public LibraryAsset LibraryAsset { get; set; }

        public int LibraryAssetId { get; set; }


    }
}
