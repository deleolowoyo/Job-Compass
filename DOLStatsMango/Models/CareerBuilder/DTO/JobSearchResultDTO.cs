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

namespace DOLStatsMango.Models.CareerBuilder.DTO
{
    public class JobSearchResult
    {
        public string DID { get; set; }
        public string JobTitle { get; set; }
        public string DescriptionTeaser { get; set; }
        public DateTime? PostedDate { get; set; }
        public string Pay { get; set; }
        public string Location { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
        public string Distance { get; set; }
        public string Company { get; set; }
    }

    public class JobSearchResultDTO
    {
        public List<JobSearchResult> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int FirstItemIndex { get; set; }
        public int LastItemIndex { get; set; }
        public List<Error> Errors { get; set; }
    }
}
