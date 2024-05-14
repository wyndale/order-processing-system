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

    public class BodyProductsDiscount : IDiscountCalculator
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

    public delegate void OrderEventHandler(string message);

    public class Order
    {
        public List<Item> Items { get; set; }
        public event OrderEventHandler OrderPlaced;

        public Order()
        {
            Items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public decimal GetTotalCost()
        {
            var discountCalculators = new List<IDiscountCalculator>()
            {
                new ClothingDiscount(),
                new GroceriesDiscount(),
                new ElectronicsDiscount(),
                new AccessorieDiscount(),
                new BodyProductsDiscount()
            };
            var totalDiscount = Items.SelectMany(item => discountCalculators.Select(calculator => calculator.CalculateDiscount(item))).Sum();

            return Items.Sum(item => item.Price) - totalDiscount;
        }

        public void PrintReceipt()
        {
            Console.Write("Order Receipt:");
            foreach (var item in Items)
            {
                Console.WriteLine($"- {item.Name} ({item.Category}): {item.Price}");
            }

            decimal totalCost = GetTotalCost();
            Console.WriteLine($"Total: {totalCost}");

            OrderPlaced?.Invoke($"Order placed successfully! Total cost: {totalCost}");
        }
    }

    class Program
    {
        static void Main()
        {
            var order = new Order();

            // Add items to the order
            order.AddItem(new Item { Name = "Laptop", Price = 800, Category = ItemCategory.Electronics });
            order.AddItem(new Item { Name = "Shirt", Price = 20, Category = ItemCategory.Clothing });

            // Calculate and print the total cost with discounts
            Console.WriteLine("Order Details:");
            order.PrintReceipt();
            Console.WriteLine("\n");
            // Example of event handling (optional)
            order.OrderPlaced += OnOrderPlaced;
            order.PrintReceipt(); // Trigger the event again
        }

        static void OnOrderPlaced(string message)
        {
            Console.WriteLine(message);
        

            Console.ReadKey();
        }
    }
}
