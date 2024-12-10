using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DatabaseInterfaces
{
    public partial class MainPage : ContentPage
    {
        private readonly GameStopDbContext _context;
        private List<Product> _cart;

        public MainPage()
        {
            InitializeComponent();
            _context = new GameStopDbContext();
            _cart = new List<Product>();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                //fetch products from the database
                var products = await _context.product.ToListAsync();
                ProductCollectionView.ItemsSource = products;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"failed: {ex.Message}");
            }
        }

        private void OnAddToCartClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;
            if (product != null)
            {
                _cart.Add(product);
                DisplayAlert("Product Added", $"{product.ProductName} has been added to your cart.", "OK");
            }
        }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            if (_cart.Any())
            {
                //calculate total price
                var totalAmount = _cart.Sum(p => p.Price);

                //navigate to the checkout page and pass the cart and total amount
                await Navigation.PushAsync(new CheckoutPage(_cart, totalAmount));
            }
            else
            {
                DisplayAlert("Empty Cart", "Your cart is empty. Please add items to your cart.", "OK");
            }
        }
        private async void OnShipmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShipmentPage());
        }

    }
}