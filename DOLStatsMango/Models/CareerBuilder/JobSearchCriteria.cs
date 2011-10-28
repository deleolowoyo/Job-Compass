using System;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DOLStatsMango.Models.CareerBuilder
{
    public class JobSearchCriteria : ModelBase
    {
        public string Keywords { get; set; }
        public string CompanyName { get; set; }
        public string FriendlyLocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public JobRadius Radius { get; set; }
        public string CountryCode { get; set; }
        public JobCategory Category { get; set; }
        public JobEducationCode EducationCode { get; set; }
        public bool? SpecificEducation { get; set; }
        public string SOCCode { get; set; }
        public JobLastPosted LastPosted { get; set; }
        public JobEmployeeType EmployeeType { get; set; }
        public JobSalary PayLow { get; set; }
        public JobSalary PayHigh { get; set; }
        public bool ExcludeNational { get; set; }
        public int PageNumber { get; set; }
        public int PerPage { get; set; }
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }

        public string FullSearchCriteria
        {
            get
            {
                string retVal = string.Empty;

                if (!String.IsNullOrWhiteSpace(CompanyName))
                {
                    retVal = String.Format("{0}{1} ~ ", retVal, CompanyName);
                }

                if (!String.IsNullOrWhiteSpace(FriendlyLocation))
                {
                    retVal = String.Format("{0}{1} ~ ", retVal, FriendlyLocation);
                }

                if (Radius != null && Radius.Code != null)
                {
                    retVal = String.Format("{0}{1} ~ ", retVal, Radius.Name);
                }

                if (Category != null && Category.Code != null)
                {
                    if (!Category.Code.Equals("-1"))
                    {
                        retVal = String.Format("{0}{1} ~ ", retVal, Category.Name);
                    }
                }
                if (EducationCode != null && EducationCode.Code != null)
                {
                    if (!EducationCode.Code.Equals("-1"))
                    {
                        retVal = String.Format("{0}{1} ~ ", retVal, EducationCode.Name);

                        if (SpecificEducation.HasValue)
                        {
                            bool val = !SpecificEducation.Value;
                            retVal = String.Format("{0}Lower Degrees: {1} ~ ", retVal, val.ToString());
                        }
                    }
                }

                if (LastPosted != null && LastPosted.Code != null)
                {
                    retVal = String.Format("{0}{1} ~ ", retVal, LastPosted.Name);
                }

                if (EmployeeType != null && EmployeeType.Code != null)
                {
                    if (!EmployeeType.Code.Equals("-1"))
                    {
                        retVal = String.Format("{0}{1} ~ ", retVal, EmployeeType.Name);
                    }
                }

                if ((PayLow != null && PayHigh != null) &&
                    (PayLow.Code != null && PayHigh.Code != null))
                {
                    retVal = String.Format("{0}{1} to {2} ~ ", retVal, PayLow.Name, PayHigh.Name);
                }

                return retVal;
            }
        }
    }
}
