using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZxVentures.Backend.Model.Utils
{
    public class GeoJSONUtils
    {
        public static bool IsPointInMultiPolygon(Position testPoint, MultiPolygon coverageArea)
        {
            foreach (Polygon polygon in coverageArea.Coordinates)
            {
                var points = new List<Point>();
                foreach (LineString linestr in polygon.Coordinates)
                {
                    foreach (var position in linestr.Coordinates)
                    {
                        points.Add(new Point(position));
                    }
                }
                if (IsPointInPolygon(points.ToArray(), testPoint))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPointInPolygon(Point[] polygon, Position testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Coordinates.Latitude < testPoint.Latitude && polygon[j].Coordinates.Latitude >= testPoint.Latitude
                    || polygon[j].Coordinates.Latitude < testPoint.Latitude && polygon[i].Coordinates.Latitude >= testPoint.Latitude)
                {
                    if (polygon[i].Coordinates.Longitude + (testPoint.Latitude - polygon[i].Coordinates.Latitude)
                        / (polygon[j].Coordinates.Latitude - polygon[i].Coordinates.Latitude) *
                            (polygon[j].Coordinates.Longitude - polygon[i].Coordinates.Longitude) < testPoint.Longitude)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            double rx1 = Math.PI * x1 / 180;
            double rx2 = Math.PI * x2 / 180;
            double theta = y1 - y2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rx1) * Math.Sin(rx2) + Math.Cos(rx1) *
                Math.Cos(rx2) * Math.Cos(rtheta);
            dist = Math.Acos(dist) * 180 / Math.PI * 60 * 1000;
            return dist; //em metros
        }
    }
}
