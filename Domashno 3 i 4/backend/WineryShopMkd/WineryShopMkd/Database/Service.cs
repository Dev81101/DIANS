using WineryShopMkd.Models;

namespace WineryShopMkd.Database
{
    using System;
    using System.Collections.Generic;

    public class WineryService
    {
        private List<Winery> wineryShops;

        public WineryService(List<Winery> wineryShops)
        {
            this.wineryShops = wineryShops;
        }

        public Winery GetClosest(Winery inputWinery)
        {
            double inputLat = inputWinery.lat ?? 0;
            double inputLon = inputWinery.lon ?? 0;

            double minDistance = double.MaxValue;
            Winery closestWinery = null;

            foreach (var winery in wineryShops)
            {
                double wineryLat = winery.lat ?? 0;
                double wineryLon = winery.lon ?? 0;

                double distance = CalculateDistance(inputLat, inputLon, wineryLat, wineryLon);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestWinery = winery;
                }
            }

            return closestWinery;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {

            double radiusEarth = 6371;
            double dLat = DegreeToRadian(lat2 - lat1);
            double dLon = DegreeToRadian(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreeToRadian(lat1)) * Math.Cos(DegreeToRadian(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = radiusEarth * c;
            return distance;
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }

}
