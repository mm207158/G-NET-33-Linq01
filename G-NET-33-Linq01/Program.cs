using LINQ.DataSources;

namespace G_NET_33_Linq01
{
    internal class Program
    {
        static void PrintHeader(int num, string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{'─',-60}");
            Console.WriteLine($"  Exercise {num}: {title}");
            Console.WriteLine($"{'─',-60}");
            Console.ResetColor();
        }
        static void Main(string[] args)

        {
           

            var products = Source.ProductList;
            var customers = Source.CustomerList;

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 1 – Seafood products (name + price)
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(1, "All Seafood products (name & price)");

            var seafood = products.Where(p => p.Category == "Seafood");

            foreach (var p in seafood)
                Console.WriteLine($"  {p.ProductName,-40} ${p.UnitPrice,8:F2}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 2 – Only product names
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(2, "All product names");

            var names = products.Select(p => p.ProductName);

            foreach (var name in names)
                Console.WriteLine($"  {name}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 3 – Sorted by UnitPrice ascending
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(3, "All products sorted by UnitPrice ascending");

            var sortedByPrice = products.OrderBy(p => p.UnitPrice);

            foreach (var p in sortedByPrice)
                Console.WriteLine($"  {p.ProductName,-40} ${p.UnitPrice,8:F2}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 4 – UnitPrice between 10 and 30 (inclusive)
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(4, "Products with UnitPrice between $10 and $30");

            var midRange = products.Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);

            foreach (var p in midRange)
                Console.WriteLine($"  {p.ProductName,-40} ${p.UnitPrice,8:F2}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 5 – In-stock Condiments
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(5, "In-stock Condiments products");

            var inStockCondiments = products
                .Where(p => p.UnitsInStock > 0 && p.Category == "Condiments");

            foreach (var p in inStockCondiments)
                Console.WriteLine($"  {p.ProductName,-40} Stock: {p.UnitsInStock}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 6 – Anonymous type: Name, Price, StockStatus
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(6, "Anonymous type with StockStatus");

            var productInfo = products.Select(p => new
            {
                Name = p.ProductName,
                Price = p.UnitPrice,
                StockStatus = p.UnitsInStock > 0 ? "Available" : "Out of Stock"
            });

            foreach (var item in productInfo)
                Console.WriteLine($"  {item.Name,-40} ${item.Price,8:F2}  [{item.StockStatus}]");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 7 – Products with 1-based position index
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(7, "Products with 1-based position");

            // Select overload that provides the index
            var indexedProducts = products.Select((p, i) => $"{i + 1}. {p.ProductName}");

            Console.WriteLine("  " + string.Join(", ", indexedProducts));

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 8 – Sort by Category asc, then UnitPrice desc within category
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(8, "Sorted by Category (asc) then UnitPrice (desc)");

            var sortedCategoryPrice = products
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);

            string currentCategory = "";
            foreach (var p in sortedCategoryPrice)
            {
                if (p.Category != currentCategory)
                {
                    currentCategory = p.Category;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n  [{currentCategory}]");
                    Console.ResetColor();
                }
                Console.WriteLine($"    {p.ProductName,-40} ${p.UnitPrice,8:F2}");
            }

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 9 – Beverages sorted by UnitsInStock descending
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(9, "Beverages sorted by stock (descending)");

            var beveragesByStock = products
                .Where(p => p.Category == "Beverages")
                .OrderByDescending(p => p.UnitsInStock);

            foreach (var p in beveragesByStock)
                Console.WriteLine($"  {p.ProductName,-40} Stock: {p.UnitsInStock}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 10 – Query syntax + compound from: orders placed in 1997 or later
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(10, "Orders placed in 1997 or later (query syntax + compound from)");

            var lateOrders =
                from c in customers
                from o in c.Orders                      // compound from clause
                where o.OrderDate.Year >= 1997
                select new { c.CustomerID, o.OrderDate };

            foreach (var item in lateOrders)
                Console.WriteLine($"  Customer: {item.CustomerID,-8}  OrderDate: {item.OrderDate:yyyy-MM-dd}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 11 – Position number alongside ProductName (same as Ex.7, explicit)
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(11, "Position number alongside ProductName");

            // Using Select with index overload – verbose tabular form
            var numberedProducts = products.Select((p, i) => new { Position = i + 1, p.ProductName });

            foreach (var item in numberedProducts)
                Console.WriteLine($"  {item.Position,3}. {item.ProductName}");

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 12 – Sort by word-length, then case-insensitive alphabetically
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(12, "Sort words by length, then case-insensitively");

            string[] arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = arr
                .OrderBy(w => w.Length)
                .ThenBy(w => w, StringComparer.OrdinalIgnoreCase);

            Console.WriteLine("  Original : " + string.Join(", ", arr));
            Console.WriteLine("  Sorted   : " + string.Join(", ", sortedWords));

            // ─────────────────────────────────────────────────────────────────────────────
            //  Exercise 13 – Digits whose second letter is 'i', reversed from original order
            // ─────────────────────────────────────────────────────────────────────────────
            PrintHeader(13, "Digits whose 2nd letter is 'i', reversed from original order");

            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            // Keep only words where index[1] == 'i', then reverse the relative order
            var result = digits
                .Where(d => d.Length >= 2 && d[1] == 'i')
                .Reverse();

            Console.WriteLine("  All digits : " + string.Join(", ", digits));
            Console.WriteLine("  Filtered   : " + string.Join(", ", result));

            Console.WriteLine();
        }
    }
}
