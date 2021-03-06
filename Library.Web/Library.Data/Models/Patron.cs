﻿namespace Library.Data.Models
{
    using System;

    public class Patron
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string TelephoneNumber { get; set; }


        public LibraryCard LibraryCard { get; set; }


        public LibraryBranch LibraryBranch { get; set; }

        public int LibraryBranchId { get; set; }
    }
}
