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
    public class DataValue
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class NationalValue
    {
        public DataValue Value { get; set; }
    }
    public class NeighborhoodValue
    {
        public DataValue Value { get; set; }
    }

    public class AttributeValue
    {
        public NeighborhoodValue Zip { get; set; }
        public NationalValue Nation { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public AttributeValue Values { get; set; }
    }

    public class PageTableData
    {
        public List<Attribute> Attributes { get; set; }
    }

    public class PageTable
    {
        public string Name { get; set; }
        public PageTableData Data { get; set; }
    }

    public class Page
    {
        public string Name { get; set; }
        public List<PageTable> Tables { get; set; }
    }

    public class Chart
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class LinksData
    {
        public string Main { get; set; }
        public string Affordability { get; set; }
        public string HomesAndRealEstate { get; set; }
        public string People { get; set; }
        public string ForSale { get; set; }
        public string ForSaleByOwner { get; set; }
        public string Foreclosures { get; set; }
        public string RecentlySold { get; set; }
    }

    public class RegionData
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ResponseData
    {
        public RegionData Region { get; set; }
        public LinksData Links { get; set; }
        public List<Chart> Charts { get; set; }
        public List<Page> Pages { get; set; }
    }

    public class ResponseMessage
    {
        public string Text { get; set; }
        public int Code { get; set; }
    }

    public class DemographicDTO
    {
        public ResponseData Response { get; set; }
        public ResponseMessage Message { get; set; }
    }
}
