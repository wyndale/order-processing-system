using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingSystem
{
    public enum ItemCategory
    {
        Clothing,
        Groceries,
        Electronics,
        Accessories,
        BodyProduct
    }

    public struct Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ItemCategory Category { get; set; }
    }

    public interface IDiscountCalculator
    {
        decimal CalculateDiscount(Item item);
    }

    public class ClothingDiscount : IDiscountCalculator
    {
        public decimal CalculateDiscount(Item item)
        {
            if (item.Category == ItemCategory.Clothing && DateTime.Now.Month == 12)
            {
                return item.Price * (decimal)0.5;
            }
            if (item.Category == ItemCategory.Clothing && DateTime.Now.Month == 6)
            {
                return item.Price * (decimal)0.3;
            }
            return 0;
        }
    }

    public class GroceriesDiscount : IDiscountCalculator
    {
        public decimal CalculateDiscount(Item item)
        {
            if (item.Category == ItemCategory.Groceries && DateTime.Now.Month == 12)
            {
                return item.Price * (decimal)0.3;
            }
            return 0;
        }
    }

    public class ElectronicsDiscount : IDiscountCalculator
    {
        public decimal CalculateDiscount(Item item)
        {
            if (item.Category == ItemCategory.Electronics && DateTime.Now.Month == 12)
            {
                return item.Price * (decimal)0.1;
            }
            return 0;
        }
    }

    public class AccessorieDiscount : IDiscountCalculator
    {
        public decimal CalculateDiscount(Item item)
        {
            if (item.Category == ItemCategory.Accessories && DateTime.Now.Month == 6)
            {
                return item.Price * (decimal)0.2;
            }
            return 0;
        }
    }

    public class BodyProducts : IDiscountCalculator
    {
        public decimal CalculateDiscount(Item item)
        {
            if (item.Category == ItemCategory.BodyProduct && DateTime.Now.Month == 6)
            {
                return item.Price * (decimal)0.3;
            }
            else if (item.Category == ItemCategory.BodyProduct && DateTime.Now.Month == 12)
            {
                return item.Price * (decimal)0.5;
            }
            return 0;
        }
    }

    class Program
    {
        static void Main()
        {

            Console.ReadKey();
        }
    }
}
