using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetApi.Data;
using NetApi.Models;
using System;
using System.Linq;

namespace NetApi.Seeding
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MovieContext>>()))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        filmListSeeding(context);
                        houseSeeding(context);
                        broadcastSeeding(context);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void broadcastSeeding(MovieContext context)
        {
            if (context.Broadcasts.Any())
            {
                return;
            }
            context.Broadcasts.AddRange(
                new Broadcast
                {
                    houseId = context.Houses.First(h => h.houseRow == 5).houseId,
                    filmId = context.Films.First(f => f.filmName == "Return Of The Cuckoo").filmId,
                    dates = new DateTime(2015, 11, 16, 12, 10, 00)
                },
                new Broadcast
                {
                    houseId = context.Houses.First(h => h.houseRow == 4).houseId,
                    filmId = context.Films.First(f => f.filmName == "Return Of The Cuckoo").filmId,
                    dates = new DateTime(2015, 11, 16, 13, 10, 00)
                },
                new Broadcast
                {
                    houseId = context.Houses.First(h => h.houseRow == 5).houseId,
                    filmId = context.Films.First(f => f.filmName == "Suffragette").filmId,
                    dates = new DateTime(2015, 11, 16, 12, 10, 50)
                },
                new Broadcast
                {
                    houseId = context.Houses.First(h => h.houseRow == 6).houseId,
                    filmId = context.Films.First(f => f.filmName == "Suffragette").filmId,
                    dates = new DateTime(2015, 11, 16, 13, 20, 00)
                },
                new Broadcast
                {
                    houseId = context.Houses.First(h => h.houseRow == 5).houseId,
                    filmId = context.Films.First(f => f.filmName == "She Remembers, He Forgets").filmId,
                    dates = new DateTime(2015, 11, 16, 15, 20, 00)
                },
                 new Broadcast
                 {
                     houseId = context.Houses.First(h => h.houseRow == 5).houseId,
                     filmId = context.Films.First(f => f.filmName == "Spectre").filmId,
                     dates = new DateTime(2015, 11, 16, 16, 20, 00)
                 }
            );
            context.SaveChanges();
        }

        public static void houseSeeding(MovieContext context)
        {
            if (context.Houses.Any())
            {
                return;
            }
            context.Houses.AddRange(
                new House
                {
                    houseRow = 5,
                    houseColumn = 5
                },
                new House
                {
                    houseRow = 6,
                    houseColumn = 4
                },
                new House
                {
                    houseRow = 4,
                    houseColumn = 7
                }
            );
            context.SaveChanges();
        }

        public static void filmListSeeding(MovieContext context)
        {
            // Look for any movies.
            if (context.Films.Any())
            {
                return;   // DB has been seeded
            }

            context.Films.AddRange(
                new Film
                {
                    filmName = "Return Of The Cuckoo",
                    duration = 103,
                    category = "IIA",
                    language = "Catonese",
                    director = "Patrick Kong",
                    description = "During the day of the handover of Macau in 1999, Man-Cho (Chi Lam Cheung), Kiki (Joe Chen) and a group of neighbors were celebrating with Aunty Q (Nancy Sit) for her birthday. Kwan-Ho migrates to US for…",
                },
                new Film
                {
                    filmName = "Suffragette",
                    duration = 106,
                    category = "IIA",
                    language = "Catonese",
                    director = "Sarah Gavron",
                    description = "The foot soldiers of the early feminist movement, women who were forced underground to pursue a dangerous game of cat and mouse with an increasingly brutal State...",
                },
                new Film
                {
                    filmName = "She Remembers, He Forgets",
                    duration = 110,
                    category = "IIA",
                    language = "Catonese",
                    director = "Adam Wong",
                    description = "Unfulfilled at work and dissatisfied with her marital life, a middle-aged woman attends a high school reunion and finds a floodgate of flashbacks of her salad days open before her mind eyes..",
                },
                new Film
                {
                    filmName = "Spectre",
                    duration = 148,
                    category = "IIB",
                    language = "Catonese",
                    director = "Sam Mendes",
                    description = "A cryptic message from the past sends James Bond on a rogue mission to Mexico City and eventually Rome, where he meets Lucia Sciarra (Monica Bellucci), the beautiful and forbidden widow of an infamous criminal...",
                }
            );
            context.SaveChanges();
        }
    }
}