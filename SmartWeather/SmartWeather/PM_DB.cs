//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartWeather
{
    using System;
    using System.Collections.Generic;
    
    public partial class PM_DB
    {
        public int PM_PK { get; set; }
        public string ThingName { get; set; }
        public string Time { get; set; }
        public Nullable<double> Dust { get; set; }
        public Nullable<double> AQI { get; set; }
        public Nullable<double> Temp { get; set; }
        public Nullable<double> Hum { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lon { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
    }
}
