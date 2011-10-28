using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DOLStatsMango.Models.Zillow.DTO
{
    public class PropertyAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class PropertyLink
    {
        public string HomeDetails { get; set; }
    }

    public class PropertyDetail
    {
        public int Zpid { get; set; }
        public PropertyLink Links { get; set; }
        public PropertyAddress Address { get; set; }
        public string UseCode { get; set; }
        public string LotSizeSqFt { get; set; }
        public string FinishedSqFt { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }
    }

    public class Result
    {
        public DateTime LastRefreshedDate { get; set; }
        public PropertyDetail Property { get; set; }
        public int Price { get; set; }
    }

    public class PropertyData
    {
        public int Count { get; set; }
        public List<Result> PropertyListings { get; set; }
    }

    public class PostingResponseData
    {
        public int RegionId { get; set; }
        public PropertyData MakeMeMove { get; set; }
        public PropertyData ForSaleByOwner { get; set; }
        public PropertyData ForSaleByAgent { get; set; }
        public PropertyData ReportForSale { get; set; }
        public PropertyData ForRent { get; set; }
    }

    public class PostingResponseMessage
    {
        public string Text { get; set; }
        public int Code { get; set; }
    }

    public class PostingDTO
    {
        public PostingResponseData Response { get; set; }
        public PostingResponseMessage Message { get; set; }
    }
}
