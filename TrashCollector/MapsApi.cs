using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector
{
    public class MapsApi
    {
        public class GeoLocation
        {
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }

        public class GeoGeometry
        {
            public GeoLocation Location { get; set; }
        }

        public class GeoResult
        {
            public GeoGeometry Geometry { get; set; }
        }

        public class GeoResponse
        {
            public string Status { get; set; }
            public GeoResult[] Results { get; set; }
        }

        static void FetchData()
        {

        }
    }
}