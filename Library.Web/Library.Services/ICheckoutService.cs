namespace Library.Services
{
    using Data.Models;
    using System;
    using System.Collections.Generic;

    public interface ICheckoutService
    {
        IEnumerable<Checkout> GetAll();

        Checkout GetById(int checkoutId);

        Checkout GetLatestCheckout(int assetId);

        void Add(Checkout newCheckout);

        void CheckOutItem(int assetId, int libraryCardId);

        void CheckInItem(int assetId, int libraryCardId);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId);

        string GetCurrentCheckoutPatron(int assetId);


        void PlaceHold(int assetId, int libraryCardId);

        string GetCurrentHoldPatronName(int holdId);

        DateTime GetCurrentHoldPlaced(int holdId);

        IEnumerable<Hold> GetCurrentHolds(int assetId);


        void MarkLost(int assetId);

        void MarkFound(int assetId);
    }
}
