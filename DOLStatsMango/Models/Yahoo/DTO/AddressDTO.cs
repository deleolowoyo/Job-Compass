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
using System.Collections.Generic;

namespace DOLStatsMango.Models.Yahoo.DTO
{
    public class Result
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string Postal { get; set; }
        public string Uzip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }

    public class ErrorMessage
    {
        public string Value { get; set; }
    }

    public class AddressDTO
    {
        public List<Result> ResultSet { get; set; }
        public ErrorMessage Error { get; set; }
    }
}
