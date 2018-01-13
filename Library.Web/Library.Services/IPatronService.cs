namespace Library.Services
{
    using Data.Models;
    using System.Collections.Generic;

    public interface IPatronService
    {
        Patron GetById(int id);

        IEnumerable<Patron> GetAll();

        void AddPatron(Patron newPatron);


        IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId);

        IEnumerable<Checkout> GetCheckouts(int patronId);

        IEnumerable<Hold> GetHolds(int patronId);
    }
}
