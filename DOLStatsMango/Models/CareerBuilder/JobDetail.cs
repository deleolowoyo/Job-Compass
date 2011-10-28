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
using DOLStatsMango.Models.CareerBuilder.DTO;

namespace DOLStatsMango.Models.CareerBuilder
{
    public class JobDetail : ModelBase
    {
        #region Properties
        private string _did;
        public string DID
        {
            get
            {
                return _did;
            }
            set
            {
                if (value != _did)
                {
                    _did = value;
                    OnPropertyChanged("DID");
                }
            }
        }

        private string _displayJobID;
        public string DisplayJobID
        {
            get
            {
                return _displayJobID;
            }
            set
            {
                if (value != _displayJobID)
                {
                    _displayJobID = value;
                    OnPropertyChanged("DisplayJobID");
                }
            }
        }

        private string _jobTitle;
        public string JobTitle
        {
            get
            {
                return _jobTitle;
            }
            set
            {
                if (value != _jobTitle)
                {
                    _jobTitle = value;
                    OnPropertyChanged("JobTitle");
                }
            }
        }

        private string _jobDescription;
        public string JobDescription
        {
            get
            {
                return _jobDescription;
            }
            set
            {
                if (value != _jobDescription)
                {
                    _jobDescription = value;
                    OnPropertyChanged("JobDescription");
                }
            }
        }

        private string _jobRequirements;
        public string JobRequirements
        {
            get
            {
                return _jobRequirements;
            }
            set
            {
                if (value != _jobRequirements)
                {
                    _jobRequirements = value;
                    OnPropertyChanged("JobRequirements");
                }
            }
        }

        private string _company;
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                if (value != _company)
                {
                    _company = value;
                    OnPropertyChanged("Company");
                }
            }
        }

        private string _companyDID;
        public string CompanyDID
        {
            get
            {
                return _companyDID;
            }
            set
            {
                if (value != _companyDID)
                {
                    _companyDID = value;
                    OnPropertyChanged("CompanyDID");
                }
            }
        }

        private string _companyDetailsURL;
        public string CompanyDetailsURL
        {
            get
            {
                return _companyDetailsURL;
            }
            set
            {
                if (value != _companyDetailsURL)
                {
                    _companyDetailsURL = value;
                    OnPropertyChanged("CompanyDetailsURL");
                }
            }
        }

        private string _companyImageURL;
        public string CompanyImageURL
        {
            get
            {
                return _companyImageURL;
            }
            set
            {
                if (value != _companyImageURL)
                {
                    _companyImageURL = value;
                    OnPropertyChanged("CompanyImageURL");
                }
            }
        }

        private string _companyInfoEmailURL;
        public string CompanyInfoEmailURL
        {
            get
            {
                return _companyInfoEmailURL;
            }
            set
            {
                if (value != _companyInfoEmailURL)
                {
                    _companyInfoEmailURL = value;
                    OnPropertyChanged("CompanyInfoEmailURL");
                }
            }
        }

        private string _contactInfoName;
        public string ContactInfoName
        {
            get
            {
                return _contactInfoName;
            }
            set
            {
                if (value != _contactInfoName)
                {
                    _contactInfoName = value;
                    OnPropertyChanged("ContactInfoName");
                }
            }
        }

        private string _contactInfoEmailURL;
        public string ContactInfoEmailURL
        {
            get
            {
                return _contactInfoEmailURL;
            }
            set
            {
                if (value != _contactInfoEmailURL)
                {
                    _contactInfoEmailURL = value;
                    OnPropertyChanged("ContactInfoEmailURL");
                }
            }
        }

        private string _contactInfoPhone;
        public string ContactInfoPhone
        {
            get
            {
                return _contactInfoPhone;
            }
            set
            {
                if (value != _contactInfoPhone)
                {
                    _contactInfoPhone = value;
                    OnPropertyChanged("ContactInfoPhone");
                }
            }
        }

        private string _locationStreet1;
        public string LocationStreet1
        {
            get
            {
                return _locationStreet1;
            }
            set
            {
                if (value != _locationStreet1)
                {
                    _locationStreet1 = value;
                    OnPropertyChanged("LocationStreet1");
                }
            }
        }

        private string _locationStreet2;
        public string LocationStreet2
        {
            get
            {
                return _locationStreet2;
            }
            set
            {
                if (value != _locationStreet2)
                {
                    _locationStreet2 = value;
                    OnPropertyChanged("LocationStreet2");
                }
            }
        }

        private string _locationCity;
        public string LocationCity
        {
            get
            {
                return _locationCity;
            }
            set
            {
                if (value != _locationCity)
                {
                    _locationCity = value;
                    OnPropertyChanged("LocationCity");
                }
            }
        }

        private string _locationMetroCity;
        public string LocationMetroCity
        {
            get
            {
                return _locationMetroCity;
            }
            set
            {
                if (value != _locationMetroCity)
                {
                    _locationMetroCity = value;
                    OnPropertyChanged("LocationMetroCity");
                }
            }
        }

        private string _locationState;
        public string LocationState
        {
            get
            {
                return _locationState;
            }
            set
            {
                if (value != _locationState)
                {
                    _locationState = value;
                    OnPropertyChanged("LocationState");
                }
            }
        }

        private string _locationPostalCode;
        public string LocationPostalCode
        {
            get
            {
                return _locationPostalCode;
            }
            set
            {
                if (value != _locationPostalCode)
                {
                    _locationPostalCode = value;
                    OnPropertyChanged("LocationPostalCode");
                }
            }
        }

        private string _locationCountry;
        public string LocationCountry
        {
            get
            {
                return _locationCountry;
            }
            set
            {
                if (value != _locationCountry)
                {
                    _locationCountry = value;
                    OnPropertyChanged("LocationCountry");
                }
            }
        }

        private string _locationFormatted;
        public string LocationFormatted
        {
            get
            {
                return _locationFormatted;
            }
            set
            {
                if (value != _locationFormatted)
                {
                    _locationFormatted = value;
                    OnPropertyChanged("LocationFormatted");
                }
            }
        }

        private double _locationLatitude;
        public double LocationLatitude
        {
            get
            {
                return _locationLatitude;
            }
            set
            {
                if (value != _locationLatitude)
                {
                    _locationLatitude = value;
                    OnPropertyChanged("LocationLatitude");
                }
            }
        }

        private double _locationLongitude;
        public double LocationLongitude
        {
            get
            {
                return _locationLongitude;
            }
            set
            {
                if (value != _locationLongitude)
                {
                    _locationLongitude = value;
                    OnPropertyChanged("LocationLongitude");
                }
            }
        }

        private string _payPer;
        public string PayPer
        {
            get
            {
                return _payPer;
            }
            set
            {
                if (value != _payPer)
                {
                    _payPer = value;
                    OnPropertyChanged("PayPer");
                }
            }
        }

        private string _payHighLowFormatted;
        public string PayHighLowFormatted
        {
            get
            {
                return _payHighLowFormatted;
            }
            set
            {
                if (value != _payHighLowFormatted)
                {
                    _payHighLowFormatted = value;
                    OnPropertyChanged("PayHighLowFormatted");
                }
            }
        }

        private string _employmentType;
        public string EmploymentType
        {
            get
            {
                return _employmentType;
            }
            set
            {
                if (value != _employmentType)
                {
                    _employmentType = value;
                    OnPropertyChanged("EmploymentType");
                }
            }
        }

        private string _division;
        public string Division
        {
            get
            {
                return _division;
            }
            set
            {
                if (value != _division)
                {
                    _division = value;
                    OnPropertyChanged("Division");
                }
            }
        }

        private string _categories;
        public string Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                if (value != _categories)
                {
                    _categories = value;
                    OnPropertyChanged("Categories");
                }
            }
        }

        private string _applyURL;
        public string ApplyURL
        {
            get
            {
                return _applyURL;
            }
            set
            {
                if (value != _applyURL)
                {
                    _applyURL = value;
                    OnPropertyChanged("ApplyURL");
                }
            }
        }

        private string _travelRequired;
        public string TravelRequired
        {
            get
            {
                return _travelRequired;
            }
            set
            {
                if (value != _travelRequired)
                {
                    _travelRequired = value;
                    OnPropertyChanged("TravelRequired");
                }
            }
        }

        private string _experienceRequired;
        public string ExperienceRequired
        {
            get
            {
                return _experienceRequired;
            }
            set
            {
                if (value != _experienceRequired)
                {
                    _experienceRequired = value;
                    OnPropertyChanged("ExperienceRequired");
                }
            }
        }

        private string _degreeRequired;
        public string DegreeRequired
        {
            get
            {
                return _degreeRequired;
            }
            set
            {
                if (value != _degreeRequired)
                {
                    _degreeRequired = value;
                    OnPropertyChanged("DegreeRequired");
                }
            }
        }

        private bool _relocationCovered;
        public bool RelocationCovered
        {
            get
            {
                return _relocationCovered;
            }
            set
            {
                if (value != _relocationCovered)
                {
                    _relocationCovered = value;
                    OnPropertyChanged("RelocationCovered");
                }
            }
        }

        private bool _managesOthers;
        public bool ManagesOthers
        {
            get
            {
                return _managesOthers;
            }
            set
            {
                if (value != _managesOthers)
                {
                    _managesOthers = value;
                    OnPropertyChanged("ManagesOthers");
                }
            }
        }

        private int _ranking;
        public int Ranking
        {
            get
            {
                return _ranking;
            }
            set
            {
                if (value != _ranking)
                {
                    _ranking = value;
                    OnPropertyChanged("Ranking");
                }
            }
        }

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
        #endregion

        public JobDetail() { }

        public JobDetail(Job jobDetail)
        {
            DID = jobDetail.DID;
            DisplayJobID = jobDetail.DisplayJobID;
            JobTitle = jobDetail.JobTitle;
            JobDescription = jobDetail.JobDescription;
            JobRequirements = jobDetail.JobRequirements;
            CompanyDID = jobDetail.CompanyDID;
            Company = jobDetail.Company;
            CompanyDetailsURL = jobDetail.CompanyDetailsURL;
            CompanyImageURL = jobDetail.CompanyImageURL;
            ContactInfoName = jobDetail.ContactInfoName;
            ContactInfoEmailURL = jobDetail.ContactInfoEmailURL;
            ContactInfoPhone = jobDetail.ContactInfoPhone;
            LocationStreet1 = jobDetail.LocationStreet1;
            LocationStreet2 = jobDetail.LocationStreet2;
            LocationCity = jobDetail.LocationCity;
            LocationMetroCity = jobDetail.LocationMetroCity;
            LocationState = jobDetail.LocationState;
            LocationPostalCode = jobDetail.LocationPostalCode;
            LocationCountry = jobDetail.LocationCountry;
            LocationFormatted = jobDetail.LocationFormatted;
            LocationLatitude = jobDetail.LocationLatitude;
            LocationLongitude = jobDetail.LocationLongitude;
            PayPer = jobDetail.PayPer;
            PayHighLowFormatted = jobDetail.PayHighLowFormatted;
            EmploymentType = jobDetail.EmploymentType;
            Division = jobDetail.Division;
            Categories = jobDetail.Categories;
            ApplyURL = jobDetail.ApplyURL;
            TravelRequired = jobDetail.TravelRequired;
            ExperienceRequired = jobDetail.ExperienceRequired;
            DegreeRequired = jobDetail.DegreeRequired;
            RelocationCovered = jobDetail.RelocationCovered;
            ManagesOthers = jobDetail.ManagesOthers;
        }
    }
}
