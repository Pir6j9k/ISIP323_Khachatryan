using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopInventory
{
    public enum Category
    {
        Electronics,
        Cloths,
        Food,
        Books,
        Sports
    }

    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock => Quantity > 0;    
        public Category Category { get; set; }
        public override string ToString()
        {
            return $"Код: {Code}\n Название: {Name}\nЦена:{Price}\nКоличество: {Quantity}\nВ наличии: {(InStock ? "Да" : "Нет")}\nКатегория: {Category}";
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            
        }

        static void AddProduct()
        {
            
        }

        static void RemoveProduct()
        {
            
        }

        static void SupplyProduct()
        {
            
        }

        static void SellProduct()
        {

        }

        static void SearchProducts()
        {

        }

        static void SearchByCode()
        {

        }

        static void SearchByName()
        {
        
        }

        static void SearchByCategory()
        {
            
        }

        static void ShowSearchResults()
        {
         
        }

        static void ShowAllProducts()
        {
           
        }
    }
}