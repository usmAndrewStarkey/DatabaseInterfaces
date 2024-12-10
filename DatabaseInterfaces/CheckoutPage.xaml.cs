using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public partial class CheckoutPage : ContentPage
    {
        private readonly GameStopDbContext _context;
        private readonly List<Product> _cart;
        private readonly decimal _totalAmount;

        public CheckoutPage(List<Product> cart, decimal totalAmount)
        {
            InitializeComponent();
            _context = new GameStopDbContext();
            _cart = cart;
            _totalAmount = totalAmount;

            //sets up the UI with the cart data
            CartCollectionView.ItemsSource = _cart.Select(product => new
            {
                ProductName = product.ProductName,
                Quantity = 1,
                PriceAtPurchase = product.Price
            }).ToList();

            TotalAmountLabel.Text = $"Total: ${_totalAmount:F2}";
        }

        private async void OnConfirmPurchaseClicked(object sender, EventArgs e)
        {
            //simulates the online customer and store information
            var customerId = 5; //online anon buyer
            var storeId = 5; //online store

            //create a new transaction
            var transaction = new Transaction
            {
                CustomerID = customerId,
                StoreID = storeId,
                Timestamp = DateTime.Now
            };

            _context.transaction.Add(transaction);

            //save the transaction to generate a TransactionID
            await _context.SaveChangesAsync();

            //add the transaction items (products in the cart)
            foreach (var product in _cart)
            {
                var transactionItem = new TransactionItem
                {
                    TransactionID = transaction.TransactionID,  //use the generated TransactionID
                    UPC = product.UPC,
                    Quantity = 1,
                    PriceAtPurchase = product.Price
                };

                _context.transactionItem.Add(transactionItem); //add the item to the list

                //update the inventory (decrease stock for the online store)
                var inventory = await _context.inventory
                             .FirstOrDefaultAsync(i => i.UPC == product.UPC && i.StoreID == storeId);

                if (inventory != null && inventory.StockQuantity >= transactionItem.Quantity)
                {
                    inventory.StockQuantity -= transactionItem.Quantity; //deduct stock
                }
                else
                {
                    //handle insufficient stock
                    await DisplayAlert("Insufficient Stock", $"The product {product.ProductName} is out of stock or has insufficient quantity.", "OK");
                    return;  // Stop the checkout process
                }
            }

            //save the transaction items to the database
            await _context.SaveChangesAsync();

            //show confirmation and navigate back to the previous page
            await DisplayAlert("Success", "Your order has been placed successfully!", "OK");
            _cart.Clear();

            //navigate back to the main page or any other page
            await Navigation.PopAsync();
        }
        private async void OnShipmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShipmentPage());
        }
    }
}
