﻿ using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Core.Repositories.RestaurantRepository;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly IRestaurantRepository _restaurantRepository = new RestaurantRepository();
        public async Task<string> CreateAsync(string name,double price,int RestaurantId,ProductCategory PrdctCategory)
        {

            Restaurant Restaurant=await _restaurantRepository.GetAsync(x=>x.Id==RestaurantId);


            if (Restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something wrong!Restaurant is not found!");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }  

            Product Product = new Product(name, price, Restaurant, PrdctCategory);
            Restaurant.Products.Add(Product);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return "Succesfully CREATE";
       
        }

        public async Task<List<Product>> GetAllAsync()
        {
           List<Restaurant> Restaurants = await _restaurantRepository.GetAllAsync();
            List<Product> Products=new List<Product>(); 
            foreach (var item in Restaurants)
            {
               Products.AddRange(item.Products);  
            }
            return Products;
        }

        public async Task<Product> GetAsync(int id)
        {
            List<Restaurant> Restaurants = await _restaurantRepository.GetAllAsync();
            List<Product> products = await GetAllAsync();

            if (products.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is not any Product");
             
                return null;
            }

            foreach (var item in Restaurants)
            {
                Product product=item.Products.Find(x=>x.Id==id);
                if (product != null)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<string> RemoveAsync(int id)
        {
            List<Restaurant> Restaurants = await _restaurantRepository.GetAllAsync();
            foreach (var item in Restaurants)
            {
                Product product = item.Products.Find(x => x.Id == id);
                if (product != null)
                {
                   item.Products.Remove(product);

                    Console.ForegroundColor = ConsoleColor.Green;

                    return "Succesfully REMOVE";
              
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            return "Something wrong!Product is not found";
        }

        public async Task<string> UpdateAsync(int id,string name,double price)
        {
            List<Restaurant> Restaurants = await _restaurantRepository.GetAllAsync();

            List<Product> products = await GetAllAsync();

            if(products.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "There is not any Product";
           
            }

            foreach (var item in Restaurants)
            {
                Product product = item.Products.Find(x => x.Id == id);
                if (product != null)
                {
                    product.Name = name;
                    product.Price = price;

                    Console.ForegroundColor = ConsoleColor.Green;
                    return "Succesfully UPDATE";
            
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            return "Something wrong!Product is not found";
        

        }
    }
}
