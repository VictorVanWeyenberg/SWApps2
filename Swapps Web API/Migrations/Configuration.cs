namespace Swapps_Web_API.Migrations
{
    using Swapps_Web_API.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Swapps_Web_API.Models.Swapps_Web_APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Swapps_Web_API.Models.Swapps_Web_APIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            List<string> comicsansTags = new List<string>();
            comicsansTags.Add("comics");
            comicsansTags.Add("games");

            Address comicsansAddress = new Address
            {
                Number = 14,
                Street = "Klein Turkije"
            };

            List<TimeInterval> comicsansServiceHours = new List<TimeInterval>();
            for (int i = 0; i < 7; i++)
            {
                comicsansServiceHours.Add(new TimeInterval()
                {
                    DayOfWeek = i,
                    StartHour = 16,
                    StartMinute = 00,
                    EndHour = 4,
                    EndMinute = 00,
                });
            }

            Promotion comicsansPromotion = new Promotion
            {
                Description = "Enkel Arno ddrink Orval. De rest wordt slecht dus we moeten ervan af.",
                EndDate = new DateTime(2019, 1, 12, 16, 00, 00),
                StartDate = new DateTime(2019, 1, 13, 04, 00, 00),
                Name = "Gratis Orval"
            };

            List<Promotion> comicsansPromotions = new List<Promotion>();
            comicsansPromotions.Add(comicsansPromotion);

            EstablishmentEvent comicsansEvent = new EstablishmentEvent
            {
                Description = "Dress code: scary stuff.",
                EndDate = new DateTime(2018, 11, 01, 04, 00, 00),
                Name = "Halloween",
                StartDate = new DateTime(2018, 10, 31, 16, 00, 00)
            };

            List<EstablishmentEvent> comicsansEvents = new List<EstablishmentEvent>();
            comicsansEvents.Add(comicsansEvent);

            Establishment comicsans = new Establishment()
            {
                Tags = comicsansTags,
                Name = "Comic Sans",
                Address = comicsansAddress,
                ServiceHours = comicsansServiceHours,
                Promotions = comicsansPromotions,
                Events = comicsansEvents,
                Type = EstablishmentType.BAR
            };

            for (int i = 0; i < 7; i++)
            {
                comicsansServiceHours[i].Establishment = comicsans;
            }
            comicsansPromotion.Establishment = comicsans;
            comicsansEvent.Establishment = comicsans;

            Establishment[] establishments = new Establishment[] { comicsans };

            context.Establishments.AddOrUpdate(establishments);
        }
    }
}
