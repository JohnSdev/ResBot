using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReseBot
{
    public class ServiceDay
    {
        public string planningPeriodBegin { get; set; }
        public string planningPeriodEnd { get; set; }
        public string sDaysR { get; set; }
        public string sDaysI { get; set; }
        public string sDaysB { get; set; }
    }

    public class Origin
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string extId { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string prognosisType { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string track { get; set; }
        public string rtTime { get; set; }
        public string rtDate { get; set; }
        public bool hasMainMast { get; set; }
        public string mainMastId { get; set; }
        public string mainMastExtId { get; set; }
    }

    public class Destination
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string extId { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string prognosisType { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string track { get; set; }
        public string rtTime { get; set; }
        public string rtDate { get; set; }
        public bool hasMainMast { get; set; }
        public string mainMastId { get; set; }
        public string mainMastExtId { get; set; }
    }

    public class JourneyDetailRef
    {
        public string @ref { get; set; }
    }

    public class Product
    {
        public string name { get; set; }
        public string num { get; set; }
        public string line { get; set; }
        public string catOut { get; set; }
        public string catIn { get; set; }
        public string catCode { get; set; }
        public string catOutS { get; set; }
        public string catOutL { get; set; }
        public string operatorCode { get; set; }
        public string @operator { get; set; }
        public string admin { get; set; }
    }

    public class Leg
    {
        public Origin Origin { get; set; }
        public Destination Destination { get; set; }
        public JourneyDetailRef JourneyDetailRef { get; set; }
        public string JourneyStatus { get; set; }
        public Product Product { get; set; }
        public string idx { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public bool reachable { get; set; }
        public string direction { get; set; }
        public string duration { get; set; }
        public int? dist { get; set; }
        public bool? hide { get; set; }
    }

    public class LegList
    {
        public List<Leg> Leg { get; set; }
    }

    public class FareItem
    {
        public string name { get; set; }
        public string desc { get; set; }
        public int price { get; set; }
        public string cur { get; set; }
    }

    public class FareSetItem
    {
        public List<FareItem> fareItem { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
    }

    public class TariffResult
    {
        public List<FareSetItem> fareSetItem { get; set; }
    }

    public class Trip
    {
        public List<ServiceDay> ServiceDays { get; set; }
        public LegList LegList { get; set; }
        public TariffResult TariffResult { get; set; }
        public int idx { get; set; }
        public string tripId { get; set; }
        public string ctxRecon { get; set; }
        public string duration { get; set; }
        public string checksum { get; set; }
    }

    public class RootObject
    {
        public List<Trip> Trip { get; set; }
        public string serverVersion { get; set; }
        public string dialectVersion { get; set; }
        public string requestId { get; set; }
        public string scrB { get; set; }
        public string scrF { get; set; }
    }
}


