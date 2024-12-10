using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DatabaseInterfaces
{
    public partial class ShipmentPage : ContentPage
    {
        private readonly GameStopDbContext _context;

        public ShipmentPage()
        {
            InitializeComponent();
            _context = new GameStopDbContext();
        }

        private async void OnRecordShipmentClicked(object sender, EventArgs e)
        {
            try
            {
                //retrieve UPC and Quantity from the input fields
                var upc = UPCEntry.Text?.Trim();
                if (string.IsNullOrWhiteSpace(upc))
                {
                    await DisplayAlert("Error", "Please enter a valid UPC.", "OK");
                    return;
                }

                if (!int.TryParse(QuantityEntry.Text, out var quantity) || quantity <= 0)
                {
                    await DisplayAlert("Error", "Please enter a valid positive quantity.", "OK");
                    return;
                }

                //fetch the inventory record for the online store (StoreID = 5)
                const int storeId = 5;
                var inventory = await _context.inventory
                    .FirstOrDefaultAsync(i => i.UPC == upc && i.StoreID == storeId);

                if (inventory != null)
                {
                    //update the stock quantity
                    inventory.StockQuantity += quantity;
                }
                else
                {
                    //if the item doesn't exist in inventory, create a new record
                    inventory = new Inventory
                    {
                        StoreID = storeId,
                        UPC = upc,
                        StockQuantity = quantity
                    };
                    _context.inventory.Add(inventory);
                }

                //save changes to the database
                await _context.SaveChangesAsync();

                //notify the user
                await DisplayAlert("Success", "Shipment recorded successfully!", "OK");

                //clear the input fields
                UPCEntry.Text = string.Empty;
                QuantityEntry.Text = string.Empty;
            }
            catch (Exception ex)
            {
                //handle errors (e.g., database issues)
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
