using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DOLStatsMango.Models.Yahoo.DTO;
using System.Runtime.Serialization;

namespace DOLStatsMango.Models.Yahoo
{
    [DataContractAttribute]
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }

        public Address(Result addy)
        {
            Line1 = addy.Line1;
            Line2 = addy.Line2;
            City = addy.City;
            StateCode = addy.StateCode;
            PostalCode = addy.Uzip;
        }
    }
}
