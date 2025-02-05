using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    // Product properties.
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int QuantityInStock { get; set; }
    public double Price { get; set; }

    // Constructor to initialize a new product.
    public Product(int productId, string name, int quantityInStock, double price)
    {
        ProductId = productId;
        Name = name;
        QuantityInStock = quantityInStock;
        Price = price;
    }

    // Returns a formatted string with product details.
    public string DisplayProductInfo()
    {
        return $"ID: {ProductId}, Name: {Name}, Quantity: {QuantityInStock}, Price: {Price}";
    }
}

class InventoryManager
{
    // List to store inventory products.
    private List<Product> products = new List<Product>();

    // Adds a product to the inventory.
    public void AddProduct(Product product)
    {
        if (IsProductExist(product.ProductId))
        {
            Console.WriteLine("Product ID already exists. Please choose a unique ID.");
            return;
        }
        products.Add(product);
        Console.WriteLine($"Product '{product.Name}' added successfully.");
    }

    // Removes a product from the inventory by ID.
    public void RemoveProduct(int productId)
    {
        var product = products.FirstOrDefault(p => p.ProductId == productId);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine($"Product '{product.Name}' removed.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    // Updates the stock quantity of a product.
    public void UpdateProduct(int productId, int newQuantity)
    {
        var product = products.FirstOrDefault(p => p.ProductId == productId);
        if (product != null)
        {
            product.QuantityInStock = newQuantity;
            Console.WriteLine($"Quantity for product '{product.Name}' updated.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    // Lists all products in the inventory.
    public void ListProducts()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n===============================");
        Console.WriteLine(" Retail Inventory Details ");
        Console.WriteLine("===============================");
        Console.ResetColor();
        foreach (var product in products)
        {
            Console.WriteLine(product.DisplayProductInfo());
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("===============================\n");
        Console.ResetColor();
    }

    // Returns the total value of the inventory.
    public double GetTotalValue()
    {
        return products.Sum(p => p.Price * p.QuantityInStock);
    }

    // Checks if a product exists by ID.
    private bool IsProductExist(int productId)
    {
        return products.Any(p => p.ProductId == productId);
    }
}

class Program
{
    static void Main()
    {
        // Instantiate the inventory manager.
        InventoryManager inventory = new InventoryManager();

        while (true)
        {
            // Display the main menu.
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n====================================");
            Console.WriteLine(" Retail Inventory Management System ");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Product Quantity");
            Console.WriteLine("4. List Products");
            Console.WriteLine("5. Get Total Inventory Value");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        // Add a new product.
                        Console.Write("Enter Product ID: ");
                        int productId = GetValidInteger();
                        Console.Write("Enter Product Name: ");
                        string name = GetValidString();
                        Console.Write("Enter Quantity: ");
                        int quantity = GetValidInteger(minValue: 0);
                        Console.Write("Enter Price: ");
                        double price = GetValidDouble(minValue: 0);

                        Product newProduct = new Product(productId, name, quantity, price);
                        inventory.AddProduct(newProduct);
                        break;

                    case 2:
                        // Remove an existing product.
                        Console.Write("Enter Product ID to remove: ");
                        int removeId = GetValidInteger();
                        inventory.RemoveProduct(removeId);
                        break;

                    case 3:
                        // Update product quantity.
                        Console.Write("Enter Product ID to update: ");
                        int updateId = GetValidInteger();
                        Console.Write("Enter new Quantity: ");
                        int newQuantity = GetValidInteger(minValue: 0);
                        inventory.UpdateProduct(updateId, newQuantity);
                        break;

                    case 4:
                        // List all products.
                        inventory.ListProducts();
                        break;

                    case 5:
                        // Display total inventory value.
                        double totalValue = inventory.GetTotalValue();
                        Console.WriteLine($"Total Inventory Value: {totalValue}");
                        break;

                    case 6:
                        // Exit the program.
                        Console.WriteLine("Exiting program.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    // Helper methods for input validation.
    static int GetValidInteger(int minValue = 1)
    {
        int value;
        while (!int.TryParse(Console.ReadLine(), out value) || value < minValue)
        {
            Console.Write($"Invalid input. Please enter an integer greater than or equal to {minValue}: ");
        }
        return value;
    }

    static string GetValidString()
    {
        string value;
        while (string.IsNullOrWhiteSpace(value = Console.ReadLine()))
        {
            Console.Write("Invalid input. Please enter a valid string: ");
        }
        return value;
    }

    static double GetValidDouble(double minValue = 0)
    {
        double value;
        while (!double.TryParse(Console.ReadLine(), out value) || value < minValue)
        {
            Console.Write($"Invalid input. Please enter a number greater than or equal to {minValue}: ");
        }
        return value;
    }
}