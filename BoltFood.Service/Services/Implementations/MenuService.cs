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
        private readonly IProductService _productService=new ProductService();


        public async Task ShowMenuAsync()
        {
            Show();
            int.TryParse(Console.ReadLine(), out int request);

            while(request != 0)
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
                        await UpdateRestaurant();
                        break;
                    case 5:
                        await RemoveRestaurant();
                        break;
                    case 6:
                        await CreateProduct();

                        break;
                    case 7:
                        await ShowAllProducts();
                        break;
                    case 8:
                        await GetProductById();
                        break;
                    case 9:
                        await UpdateProduct();
                        break;
                    case 10:
                        await RemoveProduct();
                        break;
                    case 11:
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
            Console.WriteLine("4.Update Restaurant");
            Console.WriteLine("5.Remove Restaurant");
            Console.WriteLine("6.Create Product");
            Console.WriteLine("7.Show All Products");
            Console.WriteLine("8.Get Product By Id");
            Console.WriteLine("9.Update Product");
            Console.WriteLine("10.Remove Product");
            Console.WriteLine("11.Clear");
            Console.WriteLine("0.Left");
           
        }
        public bool CheckString(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong!Please enter Name correctly!!!");
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
                name=Console.ReadLine();

            }
            while (!CheckString(name));
            

            Console.WriteLine("Choose Restaurant's Category");

            var Enums = Enum.GetValues(typeof(RestaurantCategory));

            foreach(var category in Enums)
            {
                Console.WriteLine((int)category + "." + category);

            }
            int.TryParse(Console.ReadLine(), out int RstCategory);

            try
            {
                Enums.GetValue(RstCategory-1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Enter Right Restaurant's Category");
                return;
                
            }

            string message = await _restaurantService.CreateAsync(name,(RestaurantCategory)RstCategory);
            Console.WriteLine(message);
        }
        private async Task ShowAllRestaurants()
        {
            List<Restaurant> restaurants = await _restaurantService.GetAllAsync();
            Console.ForegroundColor= ConsoleColor.Cyan;
            foreach(var items in restaurants)
            {
                Console.WriteLine(items);
            }
        }
        private async Task GetRestaurantById()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Restaurant's Id:");
            int.TryParse(Console.ReadLine(), out int id);

            Restaurant Restaurant= await _restaurantService.GetAsync(id);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Restaurant);
        }
        private async Task UpdateRestaurant()
        {
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
            Console.ForegroundColor= ConsoleColor.White;

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



            Console.WriteLine("Enter Restaurant's Id:");
           int.TryParse(Console.ReadLine(),out int RestaurantId); 


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
           
            string message = await _productService.CreateAsync(name, price, RestaurantId,(ProductCategory)PrdctCategory);

            Console.WriteLine(message);

        }
        private async Task ShowAllProducts()
        {
            List<Product> products = await _productService.GetAllAsync();
            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach(var items in products)
            {
                Console.WriteLine(items);
            }
        }
        private async Task GetProductById()
        {
            Console.WriteLine("Enter Product's id:");
            int.TryParse(Console.ReadLine(), out int id);

            Product Product=await _productService.GetAsync(id); 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Product);
        }
        private async Task UpdateProduct()
        {
            Console.ForegroundColor= ConsoleColor.White;

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
        }
        private async Task RemoveProduct()
        {

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Product's Id:");
            int.TryParse(Console.ReadLine(), out int id);
           string message= await _productService.RemoveAsync(id);   
            Console.WriteLine(message);
        }   
    }
}
