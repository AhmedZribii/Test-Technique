using Library.Context;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class DataSeed
    {
        public static void SeedDatabase()
        {
            using (var context = new MyDbContext())
            {
                if (!context.Books.Any())
                {
                    for (int i = 1; i <= 10000; i++)
                    {
                        var book = new Books
                        {
                            Title = $"Book Title {i}",
                            ISBN = $"ISBN-{i}",
                            PubYear = $"2000 + ({i} % 20)",
                            Author = $"Author Name {i}"
                        };
                        context.Books.Add(book);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
