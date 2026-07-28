using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp
{
    internal class Program
    {
        // Shared DbContext - created ONCE, here, so every function below reuses
        // the exact same instance instead of each function opening its own.
        static AppDbContext context = new AppDbContext();

        // Shared login state - 0 means "nobody is logged in".
        // Set by Login(), read by any function that requires a logged-in user,
        // reset back to 0 by Logout().
        static int loggedInUserId = 0;

        static void Main(string[] args)
        {
            bool exitApp = false;
            while (!exitApp)
            {
                Console.WriteLine("\n===== E-Commerce Console App =====");
                Console.WriteLine(" 1. Register New User");
                Console.WriteLine(" 2. Login");
                Console.WriteLine(" 3. Add New Category");
                Console.WriteLine(" 4. Add New Product");
                Console.WriteLine(" 5. View All Products");
                Console.WriteLine(" 6. Place an Order");
                Console.WriteLine(" 7. View My Orders");
                Console.WriteLine(" 8. View Order Details");
                Console.WriteLine(" 9. Add a Review for an Order");
                Console.WriteLine("10. View All Reviews for a Product");
                Console.WriteLine("11. Logout");
                Console.WriteLine(" 0. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: RegisterUser(); break;
                    case 2: Login(); break;
                    case 3: AddCategory(); break;
                    case 4: AddProduct(); break;
                    case 5: ViewAllProducts(); break;
                    case 6: PlaceOrder(); break;
                    case 7: ViewMyOrders(); break;
                    case 8: ViewOrderDetails(); break;
                    case 9: AddReview(); break;
                    case 10: ViewReviewsForProduct(); break;
                    case 11: Logout(); break;
                    case 0:
                        exitApp = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // ===================== FUNCTIONS =====================

        static void RegisterUser()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            if (context.Users.Any(u => u.Email == email))
            {
                Console.WriteLine("A user with that email already exists.");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine()!;

            var user = new User { Name = name, Email = email, Password = password };
            context.Users.Add(user);
            context.SaveChanges();

            Console.WriteLine($"User '{name}' registered successfully with Id {user.Id}.");
        }

        static void Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            Console.Write("Password: ");
            string password = Console.ReadLine()!;

            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Invalid email or password.");
                return;
            }

            loggedInUserId = user.Id;
            Console.WriteLine($"Welcome back, {user.Name}!");
        }

        static void AddCategory()
        {
            Console.Write("Category name: ");
            string name = Console.ReadLine()!;

            Console.Write("Description (optional): ");
            string description = Console.ReadLine()!;

            var category = new Category { Name = name, Description = description };
            context.Categories.Add(category);
            context.SaveChanges();

            Console.WriteLine($"Category '{name}' added with Id {category.Id}.");
        }

        static void AddProduct()
        {
            var categories = context.Categories.ToList();
            if (!categories.Any())
            {
                Console.WriteLine("No categories exist yet. Please add a category first.");
                return;
            }

            Console.Write("Product name: ");
            string name = Console.ReadLine()!;

            Console.Write("Description (optional): ");
            string description = Console.ReadLine()!;

            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.WriteLine("Available categories:");
            foreach (var c in categories)
                Console.WriteLine($"  {c.Id}. {c.Name}");

            Console.Write("Choose a category Id: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId) ||
                !categories.Any(c => c.Id == categoryId))
            {
                Console.WriteLine("Invalid category Id.");
                return;
            }

            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                CategoryId = categoryId
            };

            context.Products.Add(product);
            context.SaveChanges();

            Console.WriteLine($"Product '{name}' added with Id {product.Id}.");
        }

        static void ViewAllProducts()
        {
            var categories = context.Categories.ToList();
            Console.Write("Filter by category Id (leave blank for all): ");
            string input = Console.ReadLine();

            var query = context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int categoryId))
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            var products = query.ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("\nId   Name                 Price      Category");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id,-4} {p.Name,-20} {p.Price,-10:C} {p.Category?.Name}");
            }
        }

        static void PlaceOrder()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine("You must be logged in to place an order.");
                return;
            }

            var order = new Order { UserId = loggedInUserId, OrderDate = DateTime.Now };

            bool addingMore = true;
            while (addingMore)
            {
                var products = context.Products.ToList();
                if (!products.Any())
                {
                    Console.WriteLine("No products available.");
                    return;
                }

                Console.WriteLine("\nAvailable products:");
                foreach (var p in products)
                    Console.WriteLine($"  {p.Id}. {p.Name} - {p.Price:C}");

                Console.Write("Enter product Id to add: ");
                if (!int.TryParse(Console.ReadLine(), out int productId) ||
                    !products.Any(p => p.Id == productId))
                {
                    Console.WriteLine("Invalid product Id.");
                }
                else
                {
                    Console.Write("Quantity: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                    }
                    else
                    {
                        order.OrderProducts.Add(new OrderProduct
                        {
                            ProductId = productId,
                            Quantity = quantity
                        });
                        Console.WriteLine("Product added to the order.");
                    }
                }

                Console.Write("Add another product to this order? (y/n): ");
                addingMore = Console.ReadLine()?.Trim().ToLower() == "y";
            }

            if (!order.OrderProducts.Any())
            {
                Console.WriteLine("Order cancelled - no products were added.");
                return;
            }

            context.Orders.Add(order);
            context.SaveChanges();

            Console.WriteLine($"Order placed successfully with Id {order.Id}.");
        }

        static void ViewMyOrders()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine("You must be logged in to view your orders.");
                return;
            }

            var orders = context.Orders
                .Where(o => o.UserId == loggedInUserId)
                .Include(o => o.OrderProducts)
                .ToList();

            if (!orders.Any())
            {
                Console.WriteLine("You have no orders yet.");
                return;
            }

            Console.WriteLine("\nId   Date                 Items");
            foreach (var o in orders)
            {
                Console.WriteLine($"{o.Id,-4} {o.OrderDate,-20} {o.OrderProducts.Count} item(s)");
            }
        }

        static void ViewOrderDetails()
        {
            Console.Write("Enter Order Id: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid Order Id.");
                return;
            }

            var order = context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Include(o => o.Review)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            Console.WriteLine($"\nOrder #{order.Id} - {order.OrderDate}");
            Console.WriteLine("Products:");

            decimal total = 0;
            foreach (var op in order.OrderProducts)
            {
                decimal lineTotal = op.Product.Price * op.Quantity;
                total += lineTotal;
                Console.WriteLine($"  - {op.Product.Name} x{op.Quantity} = {lineTotal:C}");
            }

            Console.WriteLine($"Order Total: {total:C}");

            if (order.Review != null)
            {
                Console.WriteLine($"Review: {order.Review.Rating}/5 - \"{order.Review.Comment}\"");
            }
            else
            {
                Console.WriteLine("Review: none yet.");
            }
        }

        static void AddReview()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine("You must be logged in to add a review.");
                return;
            }

            Console.Write("Enter Order Id to review: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Invalid Order Id.");
                return;
            }

            var order = context.Orders
                .Include(o => o.Review)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (order.UserId != loggedInUserId)
            {
                Console.WriteLine("This order does not belong to you.");
                return;
            }

            if (order.Review != null)
            {
                Console.WriteLine("This order already has a review.");
                return;
            }

            Console.Write("Rating (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Invalid rating.");
                return;
            }

            Console.Write("Comment: ");
            string comment = Console.ReadLine()!;

            var review = new Review { OrderId = order.Id, Rating = rating, Comment = comment };
            context.Reviews.Add(review);
            context.SaveChanges();

            Console.WriteLine("Review added successfully.");
        }
        static void ViewReviewsForProduct()
        {
            Console.Write("Enter Product Id: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid Product Id.");
                return;
            }

            var product = context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            // Touches all three relationship types:
            // Product -> OrderProduct (M:N) -> Order -> Review (1:1)
            var reviews = context.OrderProducts
                .Where(op => op.ProductId == productId)
                .Include(op => op.Order)
                    .ThenInclude(o => o.Review)
                .Select(op => op.Order.Review)
                .Where(r => r != null)
                .ToList();

            if (!reviews.Any())
            {
                Console.WriteLine($"No reviews found for '{product.Name}'.");
                return;
            }

            Console.WriteLine($"\nReviews for '{product.Name}':");
            foreach (var r in reviews)
            {
                Console.WriteLine($"  Order #{r.OrderId}: {r.Rating}/5 - \"{r.Comment}\"");
            }
        }

        static void Logout()
        {
            if (loggedInUserId == 0)
            {
                Console.WriteLine("No user is currently logged in.");
                return;
            }

            loggedInUserId = 0;
            Console.WriteLine("Logged out successfully.");
        }
    }
}
