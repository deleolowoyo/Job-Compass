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

namespace DOLStatsMango.Models.DOL
{
    public class OccupationalStats
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public int TotalEmployment { get; set; }
        public int YearlyMeanWage { get; set; }
        public int YearlyPercentile10 { get; set; }
        public int YearlyPercentile25 { get; set; }
        public int YearlyPercentileMedian { get; set; }
        public int YearlyPercentile75 { get; set; }
        public int YearlyPercentile90 { get; set; }
        public double HourlyMeanWage { get; set; }
        public double HourlyPercentile10 { get; set; }
        public double HourlyPercentile25 { get; set; }
        public double HourlyPercentileMedian { get; set; }
        public double HourlyPercentile75 { get; set; }
        public double HourlyPercentile90 { get; set; }
    }
}
