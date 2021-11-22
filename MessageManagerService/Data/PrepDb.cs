using MessageManagerService.Controllers.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MessageManagerService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Messages.Any())
            {
                Console.WriteLine("Seeding data");

                context.Messages.AddRange(
                    new Message { MessageStr = "I coming home" },
                    new Message { MessageStr = "Today is a great day" },
                    new Message { MessageStr = "It is sunny" },
                    new Message { MessageStr = "Oozy rat in a sanitary zoo" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Date already populated");
            }
        }
    }
}
