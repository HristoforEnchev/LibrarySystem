namespace Library.Services
{
    using Data.Models;
    using Library.Services.Models;
    using System;
    using System.Collections.Generic;

    public interface ICheckoutService
    {
        IEnumerable<Checkout> GetAll();

        Checkout GetById(int checkoutId);

        Checkout GetLatestCheckout(int assetId);

        void Add(Checkout newCheckout);

        void CheckOutItem(int assetId, int libraryCardId);

        bool IsCheckedOut(int assetId);

        void CheckInItem(int assetId);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId);

        string GetCurrentCheckoutPatron(int assetId);


        void PlaceHold(int assetId, int libraryCardId);

        string GetCurrentHoldPatronName(int holdId);

        DateTime GetCurrentHoldPlaced(int holdId);

        IEnumerable<HoldServiceModel> GetCurrentHolds(int assetId);


        void MarkLost(int assetId);

        void MarkFound(int assetId);
    }
}
