using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoltFood.Service.Services.Implementations
{
    public class MenuService : IMenuService
    {

        private readonly IRestaurantService _restaurantService = new RestaurantService();
        private readonly IProductService _productService = new ProductService();

        public async Task ShowMenuAsync()
        {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                         BOLT FOOD                           ");
                Console.ForegroundColor = ConsoleColor.White;
            Show();
            int.TryParse(Console.ReadLine(), out int request);

            while (request != 0)
            {
                switch (request)
                {
                    case 1:
                        await CreateRestaurant();
                        break;
                    case 2:
                        await ShowAllRestaurants();
                        break;
                    case 3:
                        await GetRestaurantById();
                        break;
                    case 4:
                        await SortRestaurantByRating();
                        break;
                    case 5:
                        await UpdateRestaurant();
                        break;
                    case 6:
                        await RemoveRestaurant();
                        break;
                    case 7:
                        await CreateProduct();

                        break;
                    case 8:
                        await ShowAllProducts();
                        break;
                    case 9:
                        await GetProductById();
                        break;
                    case 10:
                        await UpdateProduct();
                        break;
                    case 11:
                        await RemoveProduct();
                        break;
                    case 12:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("You Must Enter The Right Step");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Show();
                int.TryParse(Console.ReadLine(), out request);
            }
        }
        private void Show()
        {
            Console.WriteLine("1.Create Restaurant");
            Console.WriteLine("2.Show All Restaurants");
            Console.WriteLine("3.Get Restaurant By Id");
            Console.WriteLine("4.Sort Restaurant By Rating");
            Console.WriteLine("5.Update Restaurant");
            Console.WriteLine("6.Remove Restaurant");
            Console.WriteLine("7.Create Product");
            Console.WriteLine("8.Show All Products");
            Console.WriteLine("9.Get Product By Id");
            Console.WriteLine("10.Update Product");
            Console.WriteLine("11.Remove Product");
            Console.WriteLine("12.Clear");
            Console.WriteLine("0.Left");
        }
        private async Task<bool> CheckRestaraunt()
        {
            List<Restaurant> restaraunts =await _restaurantService.GetAllAsync();
            if(restaraunts.Count == 0)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("We have not any Restaurants!");
                return false;
            }
            return true;
        }
        private async Task<bool> CheckProduct()
        {
            List<Product> products = await _productService.GetAllAsync();
            if (products.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("We have not any Products!");
                return false;
            }
            return true;

        }
        private bool CheckString(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong!Please enter Name correctly!!!");
                return false;
            }
            return true;
        }
        private bool CheckPrice(double price)
        {
            if (price <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong!Please Enter Price correctly!!!");
                return false;
            }
            return true;
        }
        private bool CheckRating(double rating)
        {
            if (rating <= 0 || rating > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong!Please Enter Rating correctly!!!");
                return false;
            }
            return true;
        }
        private async Task CreateRestaurant()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string name;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Restaurant's Name");
                name = Console.ReadLine();

            }
            while (!CheckString(name));

            Console.WriteLine("Choose Restaurant's Category");
            var Enums = Enum.GetValues(typeof(RestaurantCategory));
            foreach (var category in Enums)
            {
                Console.WriteLine((int)category + "." + category);

            }
            int.TryParse(Console.ReadLine(), out int RstCategory);

            try
            {
                Enums.GetValue(RstCategory - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Enter Right Restaurant's Category");
                return;

            }

            double rating;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Restaurant's Rating:");
                double.TryParse(Console.ReadLine(), out rating);

            }
            while (!CheckRating(rating));

            string message = await _restaurantService.CreateAsync(name, rating, (RestaurantCategory)RstCategory);
            Console.WriteLine(message);
        }
        private async Task ShowAllRestaurants()
        {
            List<Restaurant> restaurants = await _restaurantService.GetAllAsync();
            if(restaurants.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("We have not any Restaraunts!");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var items in restaurants)
            {
                Console.WriteLine(items);
            }
        }
        private async Task GetRestaurantById()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Restaurant's Id:");
            int.TryParse(Console.ReadLine(), out int id);

            Restaurant Restaurant = await _restaurantService.GetAsync(id);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Restaurant);
        }
        private async Task SortRestaurantByRating()
        {
            List<Restaurant> restaurants = await _restaurantService.SortRestaurantByRating();
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var items in restaurants)
            {
                Console.WriteLine(items);
            }
        }
        private async Task UpdateRestaurant()
        {
             if( !await CheckRestaraunt())
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Restaurant's Id:");
            int.TryParse(Console.ReadLine(), out int id);

            string name;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Restaurant's Name");
                name = Console.ReadLine();
            }
            while (!CheckString(name));

            string message = await _restaurantService.UpdateAsync(id, name);
            Console.WriteLine(message);
        }
        private async Task RemoveRestaurant()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Restaurant's Id:");
            int.TryParse(Console.ReadLine(), out int id);

            string message = await _restaurantService.RemoveAsync(id);
            Console.WriteLine(message);

        }
        private async Task CreateProduct()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string name;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Product's Name");
                name = Console.ReadLine();

            }
            while (!CheckString(name));

            double price;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Product's Price:");
                double.TryParse(Console.ReadLine(),out price);

            }
            while (!CheckPrice(price));

            Console.WriteLine("Enter Restaurant's Id:");
            int.TryParse(Console.ReadLine(), out int RestaurantId);

            Console.WriteLine("Choose Restaurant's Category");
            var Enums = Enum.GetValues(typeof(ProductCategory));

            foreach (var category in Enums)
            {
                Console.WriteLine((int)category + "." + category);

            }
            int.TryParse(Console.ReadLine(), out int PrdctCategory);

            try
            {
                Enums.GetValue(PrdctCategory - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Enter Right Product Category");
                return;

            }

            string message = await _productService.CreateAsync(name, price, RestaurantId, (ProductCategory)PrdctCategory);
            Console.WriteLine(message);

        }
        private async Task ShowAllProducts()
        {
            List<Product> products = await _productService.GetAllAsync();
            if(products.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("We have not any Products!");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach (var items in products)
            {
                Console.WriteLine(items);
            }
        }
        private async Task GetProductById()
        {
            Console.WriteLine("Enter Product's id:");
            int.TryParse(Console.ReadLine(), out int id);

            Product Product = await _productService.GetAsync(id);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Product);
        }
        private async Task UpdateProduct()
        {
            if (!await CheckRestaraunt())
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Product's Id:");
            int.TryParse(Console.ReadLine(), out int id);

            string name;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Product's Name");
                name = Console.ReadLine();

            }
            while (!CheckString(name));

            Console.WriteLine("Enter Product's Price:");
            double.TryParse(Console.ReadLine(), out double price);

            string message = await _productService.UpdateAsync(id, name, price);
            Console.WriteLine(message);
        }
        private async Task RemoveProduct()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Product's Id:");
            int.TryParse(Console.ReadLine(), out int id);

            string message = await _productService.RemoveAsync(id);
            Console.WriteLine(message);
        }
    }
}
