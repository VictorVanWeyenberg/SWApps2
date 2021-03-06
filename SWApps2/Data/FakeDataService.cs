﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWApps2.Model;

namespace SWApps2.Data
{
    class FakeDataService
    {
        public List<Establishment> Establishments { get; }

        public List<Promotion> Promotions { get; }

        public List<EstablishmentEvent> EstablishmentEvents { get; }

        public FakeDataService() {
                Establishments = new List<Establishment>();
            Promotions = new List<Promotion>();
            EstablishmentEvents = new List<EstablishmentEvent>();

                Address address1 = new Address("Klein Turkije", 8);

                TimeInterval[] ti = new TimeInterval[7];
                NodaTime.LocalTime openHour = new NodaTime.LocalTime(16, 0);
                NodaTime.LocalTime closeHour = new NodaTime.LocalTime(4, 0);
                for (int i = 0; i < 7; i++)
                {
                    ti[i] = new TimeInterval(openHour, closeHour);
                }

                // Establishment establishment1 = new Establishment("Comic Sans", address1, serviceHours1, EstablishmentType.BAR, new Uri("/Assets/comicsans.jpg"));
                Establishment establishment1 = new Establishment("Comic Sans", address1, new ServiceHours(ti), EstablishmentType.BAR,null);

            DateTime start = new DateTime(2018, 11, 24, 19, 0, 0);
            DateTime end = new DateTime(2018, 11, 25, 2, 0, 0);
            Promotion promotion1 = new Promotion(establishment1, "Karaoke", "Sing yer lungs out", start, end);
            EstablishmentEvent event1 = new EstablishmentEvent(establishment1, "Karaoke", "Sing yer lungs out", start, end);

            establishment1.Promotions.Add(promotion1);
            establishment1.EstablishmentEvents.Add(event1);

            Promotions.Add(promotion1);
            Establishments.Add(establishment1);
            EstablishmentEvents.Add(event1);
        }
    }
}
