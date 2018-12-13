namespace Swapps_Web_API.Migrations
{
    using Swapps_Web_API.Models;
    using SWApps2.Converters;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Swapps_Web_APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Swapps_Web_APIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            string estName1 = "Comic Sans";
            string estName2 = "Kinky Star";
            string estName3 = "World's End";

            List<string> tags1 = new List<string>() { "bar", "comics", "games", "geek" };
            List<string> tags2 = new List<string>() { "bar", "music", "metal", "concert", "band", "open", "podium", "jam" };
            List<string> tags3 = new List<string>() { "bar", "dnd", "games", "board", "shop", "comics" };

            Address address1 = new Address
            {
                Street = "Klein Turkije",
                Number = 14
            };

            Address address2 = new Address
            {
                Street = "Vlasmarkt",
                Number = 9
            };

            Address address3 = new Address
            {
                Street = "Ketelvest",
                Number = 51
            };

            EstablishmentType type1 = EstablishmentType.BAR;
            EstablishmentType type2 = EstablishmentType.CULTURE;
            EstablishmentType type3 = EstablishmentType.BAR;

            List<int> timeData1 = new List<int>() { 16, 0, 0, 0, 0, 0, 0, 0, 14, 30, 1, 0, 14, 30, 1, 0, 14, 30, 1, 0, 14, 30, 3, 0, 14, 30, 3, 0 };
            List<int> timeData2 = new List<int>() { 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 1, 0, 20, 0, 3, 0, 20, 0, 3, 0, 20, 0, 3, 0 };
            List<int> timeData3 = new List<int>() { 11, 30, 23, 55, 0, 0, 0, 0, 10, 30, 23, 55, 10, 30, 23, 55, 10, 30, 23, 55, 10, 30, 1, 0, 10, 30, 1, 0 };

            List<TimeInterval> serviceHours1 = new List<TimeInterval>();
            List<TimeInterval> serviceHours2 = new List<TimeInterval>();
            List<TimeInterval> serviceHours3 = new List<TimeInterval>();

            for (int i = 0; i < 7 * 4; i+=4)
            {
                TimeInterval ti1 = new TimeInterval()
                {
                    DayOfWeek = i / 4,
                    StartHour = timeData1[i],
                    StartMinute = timeData1[i + 1],
                    EndHour = timeData1[i + 2],
                    EndMinute = timeData1[i + 3]
                };
                TimeInterval ti2 = new TimeInterval()
                {
                    DayOfWeek = i / 4,
                    StartHour = timeData2[i],
                    StartMinute = timeData2[i + 1],
                    EndHour = timeData2[i + 2],
                    EndMinute = timeData2[i + 3]
                };
                TimeInterval ti3 = new TimeInterval()
                {
                    DayOfWeek = i / 4,
                    StartHour = timeData3[i],
                    StartMinute = timeData3[i + 1],
                    EndHour = timeData3[i + 2],
                    EndMinute = timeData3[i + 3]
                };
                serviceHours1.Add(ti1);
                serviceHours2.Add(ti2);
                serviceHours3.Add(ti3);
            }

            EstablishmentEvent event1 = new EstablishmentEvent
            {
                Name = "Metal Karaoke",
                Description = "Brul die longen eruit. Geen studenten met mondeling examens toegelaten.",
                StartDate = new DateTime(2019, 02, 12, 19, 0, 0),
                EndDate = new DateTime(2019, 02, 13, 1, 0, 0)
            };

            EstablishmentEvent event2 = new EstablishmentEvent
            {
                Name = "Modular Synth Jam Session",
                Description = "Alle hens aan dek, alle lunchboxes en patchkabels in de aanslag. We maken het grootste orkest van modulaire synthesizers.",
                StartDate = new DateTime(2019, 03, 9, 19, 0, 0),
                EndDate = new DateTime(2019, 03, 10, 1, 0, 0)
            };

            EstablishmentEvent event3 = new EstablishmentEvent
            {
                Name = "D&D Adventurers' League",
                Description = "Nieuweling of veteraan. Iedereen is welkom in de campaign.",
                StartDate = new DateTime(2019, 04, 14, 19, 0, 0),
                EndDate = new DateTime(2019, 04, 15, 1, 0, 0)
            };

            Promotion promotion1 = new Promotion
            {
                Name = "Gratis Orval",
                Description = "Enkel Arno drinkt Orval, de flessen moeten dringend op. Bij deze gaan we ze dus gratis uitgeven.",
                StartDate = new DateTime(2019, 05, 14, 19, 0, 0),
                EndDate = new DateTime(2019, 05, 15, 1, 0, 0)
            };

            Promotion promotion2 = new Promotion
            {
                Name = "Afprijzing voor aanprijzing",
                Description = "BYO Instrument en wordt voor een avond een jukebox. Speel improviserend aangevraagde liedjes en krijg hiervoor goedkoper bier.",
                StartDate = new DateTime(2019, 07, 02, 19, 0, 0),
                EndDate = new DateTime(2019, 07, 03, 1, 0, 0)
            };

            Promotion promotion3 = new Promotion
            {
                Name = "Dice Fest",
                Description = "Je kan nooit genoeg dobbelstenen hebben. Deze zaterdag regent het dobbelstenen, kom ze maar halen!",
                StartDate = new DateTime(2019, 06, 22, 19, 0, 0),
                EndDate = new DateTime(2019, 06, 23, 1, 0, 0)
            };

            Establishment est1 = new Establishment
            {
                Name = estName1,
                Address = address1,
                Tags = tags1.Select(t => new Tag { Value = t }).ToList(),
                Type = type1,
                ServiceHours = serviceHours1,
                Promotions = new List<Promotion>() { promotion1 },
                Events = new List<EstablishmentEvent>() { event1 }
            };

            Establishment est2 = new Establishment
            {
                Name = estName2,
                Address = address2,
                Tags = tags2.Select(t => new Tag { Value = t }).ToList(),
                Type = type2,
                ServiceHours = serviceHours2,
                Promotions = new List<Promotion>() { promotion2 },
                Events = new List<EstablishmentEvent>() { event2 }
            };

            Establishment est3 = new Establishment
            {
                Name = estName3,
                Address = address3,
                Tags = tags3.Select(t => new Tag { Value = t }).ToList(),
                Type = type3,
                ServiceHours = serviceHours3,
                Promotions = new List<Promotion>() { promotion3 },
                Events = new List<EstablishmentEvent>() { event3 }
            };

            event1.Establishment = est1;
            event2.Establishment = est2;
            event3.Establishment = est3;

            promotion1.Establishment = est1;
            promotion2.Establishment = est2;
            promotion3.Establishment = est3;

            byte[] user1salt = SecurePassword.GetSalt();
            User user1 = new User
            {
                AbstractUser = new AbstractUser
                {
                    Email = "victorvanweyenb@gmail.com",
                    FirstName = "Victor",
                    LastName = "Van Weyenberg",
                    Salt = Convert.ToBase64String(user1salt),
                    Hash = SecurePassword.Hash("pokemon123", user1salt)
                },
                Subscriptions = new List<Establishment>() { est2, est3 },
            };

            byte[] user5salt = SecurePassword.GetSalt();
            User user2 = new User
            {
                AbstractUser = new AbstractUser
                {
                    Email = "brackenavaron@gmail.com",
                    FirstName = "Navaron",
                    LastName = "Bracke",
                    Salt = Convert.ToBase64String(user5salt),
                    Hash = SecurePassword.Hash("pokemon123", user5salt)
                },
                Subscriptions = new List<Establishment>() { est1, est3 },
            };

            byte[] user2salt = SecurePassword.GetSalt();
            Entrepreneur entre1 = new Entrepreneur
            {
                User = new AbstractUser
                {
                    Email = "jeroendellaert@gmail.com",
                    FirstName = "Jeroen",
                    LastName = "Dellaert",
                    Salt = Convert.ToBase64String(user2salt),
                    Hash = SecurePassword.Hash("pokemon123", user2salt)
                },
                Establishment = est1
            };

            byte[] user3salt = SecurePassword.GetSalt();
            Entrepreneur entre2 = new Entrepreneur
            {
                User = new AbstractUser
                {
                    Email = "loicvanweyenb@gmail.com",
                    FirstName = "Loïc",
                    LastName = "Van Weyenberg",
                    Salt = Convert.ToBase64String(user3salt),
                    Hash = SecurePassword.Hash("pokemon123", user3salt)
                },
                Establishment = est2
            };

            byte[] user4salt = SecurePassword.GetSalt();
            Entrepreneur entre3 = new Entrepreneur
            {
                User = new AbstractUser
                {
                    Email = "basilevanweyenb@gmail.com",
                    FirstName = "Basile",
                    LastName = "Van Weyenberg",
                    Salt = Convert.ToBase64String(user4salt),
                    Hash = SecurePassword.Hash("pokemon123", user4salt)
                },
                Establishment = est3
            };

            context.Establishments.AddOrUpdate(est1);
            context.Establishments.AddOrUpdate(est2);
            context.Establishments.AddOrUpdate(est3);

            context.Users.AddOrUpdate(user1);
            context.Users.AddOrUpdate(user2);

            context.Entrepreneurs.AddOrUpdate(entre1);
            context.Entrepreneurs.AddOrUpdate(entre2);
            context.Entrepreneurs.AddOrUpdate(entre3);

        }
    }
}
