﻿namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BranchHours
    {
        public int Id { get; set; }

        [Range(0, 6)]
        public int DayOfWeek { get; set; }

        [Range(0, 23)]
        public int OpenTime { get; set; }

        [Range(0, 23)]
        public int CloseItem { get; set; }


        public LibraryBranch LibraryBranch { get; set; }

        public int LibraryBranchId { get; set; }
    }
}
