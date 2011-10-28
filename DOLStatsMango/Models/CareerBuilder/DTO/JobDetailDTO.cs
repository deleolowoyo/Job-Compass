using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.Generic;

namespace DOLStatsMango.Models.CareerBuilder.DTO
{
    public class Money
    {
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string FormattedAmount { get; set; }
    }

    public class Pay
    {
        public Money Money { get; set; }
    }

    public class Job
    {
        public string DID { get; set; }
        public string DisplayJobID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirements { get; set; }
        public string CompanyDID { get; set; }
        public string Company { get; set; }
        public string CompanyDetailsURL { get; set; }
        public string CompanyImageURL { get; set; }
        public string ContactInfoName { get; set; }
        public string ContactInfoEmailURL { get; set; }
        public string ContactInfoPhone { get; set; }
        public string ContactInfoFax { get; set; }
        public string LocationStreet1 { get; set; }
        public string LocationStreet2 { get; set; }
        public string LocationCity { get; set; }
        public string LocationMetroCity { get; set; }
        public string LocationState { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
        public string LocationFormatted { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public string PayPer { get; set; }
        public string PayHighLowFormatted { get; set; }
        public Pay PayHigh { get; set; }
        public Pay PayLow { get; set; }
        public Pay PayCommission { get; set; }
        public Pay PayBonus { get; set; }
        public Pay PayOther { get; set; }
        public string EmploymentType { get; set; }
        public string Division { get; set; }
        public string Categories { get; set; }
        public string ApplyURL { get; set; }
        public string TravelRequired { get; set; }
        public string ExperienceRequired { get; set; }
        public string DegreeRequired { get; set; }
        public bool RelocationCovered { get; set; }
        public bool ManagesOthers { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class JobDetailDTO
    {
        public Job ResponseJob { get; set; }
        public List<Error> Errors { get; set; }
    }
}
