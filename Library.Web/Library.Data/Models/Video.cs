namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Video : LibraryAsset
    {
        [Required]
        public string Director { get; set; }
    }
}
