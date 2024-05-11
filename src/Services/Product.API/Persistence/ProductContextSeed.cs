using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
    {
        if (!productContext.Products.Any())
        {
            productContext.AddRange(getCatalogProducts());
            await productContext.SaveChangesAsync();
            logger.Information("Seeded data for Product DB associated with context {DbContextName}",
                nameof(ProductContext));
        }
    }

    private static IEnumerable<CatalogProduct> getCatalogProducts()
    {
        return new List<CatalogProduct>
        {
            new()
            {
                No = "PizzaMargherita",
                Name = "Pizza Margherita",
                Summary = "Classic Italian pizza with tomatoes, mozzarella cheese, and basil leaves.",
                Description =
                    "Pizza Margherita is a classic Italian pizza topped with tomatoes, mozzarella cheese, and basil leaves. It's a simple yet delicious dish loved by many.",
                Price = 12.99m,
                ImageUrl = "https://images.unsplash.com/photo-1598023696416-0193a0bcd302?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8UGl6emElMjBNYXJnaGVyaXRhfGVufDB8fDB8fHww"
            },
            new()
            {
                No = "ChickenParmesan",
                Name = "Chicken Parmesan",
                Summary = "Breaded chicken breast topped with marinara sauce and melted cheese.",
                Description =
                    "Chicken Parmesan is a popular Italian-American dish featuring breaded chicken breast topped with marinara sauce and melted cheese, typically served with pasta.",
                Price = 15.49m,
                ImageUrl =
                    "https://images.unsplash.com/photo-1632778149955-e80f8ceca2e8?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q2hpY2tlbiUyMFBhcm1lc2FufGVufDB8fDB8fHww"
            },
            new()
            {
                No = "CaesarSalad",
                Name = "Caesar Salad",
                Summary = "Fresh romaine lettuce, croutons, Parmesan cheese, and Caesar dressing.",
                Description =
                    "Caesar Salad is a classic salad made with fresh romaine lettuce, croutons, Parmesan cheese, and Caesar dressing. It's a refreshing and flavorful choice.",
                Price = 9.99m,
                ImageUrl =
                    "https://images.unsplash.com/photo-1551248429-40975aa4de74?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8Q2Flc2FyJTIwU2FsYWR8ZW58MHx8MHx8fDA%3D"
            },
            new()
            {
                No = "SushiRoll",
                Name = "Sushi Roll",
                Summary = "Assorted sushi rolls with fresh fish, avocado, and rice.",
                Description =
                    "Sushi Roll is a Japanese delicacy made with assorted fresh fish, avocado, and rice rolled in seaweed. It's a popular choice for sushi lovers.",
                Price = 18.99m,
                ImageUrl =
                    "https://images.unsplash.com/photo-1579871494447-9811cf80d66c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8U3VzaGklMjBSb2xsfGVufDB8fDB8fHww"
            },
            new()
            {
                No = "PastaCarbonara",
                Name = "Pasta Carbonara",
                Summary = "Creamy pasta with bacon, eggs, Parmesan cheese, and black pepper.",
                Description =
                    "Pasta Carbonara is a creamy Italian pasta dish made with bacon, eggs, Parmesan cheese, and black pepper. It's rich in flavor and a comfort food favorite.",
                Price = 14.99m,
                ImageUrl =
                    "https://images.unsplash.com/photo-1598866594230-a7c12756260f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fFBhc3RhJTIwQ2FyYm9uYXJhfGVufDB8fDB8fHww"
            },
            new()
            {
                No = "ChocolateCake",
                Name = "Chocolate Cake",
                Summary = "Decadent chocolate cake with layers of rich frosting.",
                Description =
                    "Chocolate Cake is a decadent dessert featuring layers of rich chocolate cake with creamy frosting. It's a must-try for chocolate lovers.",
                Price = 20.99m,
                ImageUrl =
                    "https://images.unsplash.com/photo-1626263468007-a9e0cf83f1ac?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fENob2NvbGF0ZSUyMENha2V8ZW58MHx8MHx8fDA%3D"
            }
        };
    }
}