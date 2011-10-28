using System;
using System.Runtime.Serialization;
using DOLStatsMango.Models.CareerBuilder.DTO;
using System.Collections.Generic;

namespace DOLStatsMango.Models.CareerBuilder
{
    [DataContractAttribute]
    public class JobSearchResultInfo : ModelBase
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

        private string _descriptionTeaser;
        public string DescriptionTeaser
        {
            get
            {
                return _descriptionTeaser;
            }
            set
            {
                if (value != _descriptionTeaser)
                {
                    _descriptionTeaser = value;
                    OnPropertyChanged("DescriptionTeaser");
                }
            }
        }

        private DateTime? _postedDate;
        public DateTime? PostedDate
        {
            get
            {
                return _postedDate;
            }
            set
            {
                if (value != _postedDate)
                {
                    _postedDate = value;
                    OnPropertyChanged("PostedDate");
                }
            }
        }

        private string _pay;
        public string Pay
        {
            get
            {
                return _pay;
            }
            set
            {
                if (value != _pay)
                {
                    _pay = value;
                    OnPropertyChanged("Pay");
                }
            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
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

        private string _distance;
        public string Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                if (value != _distance)
                {
                    _distance = value;
                    OnPropertyChanged("Distance");
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

        #endregion

        public JobSearchResultInfo()
        { }

        public JobSearchResultInfo(JobSearchResult result)
        {
            DID = result.DID;
            JobTitle = result.JobTitle;
            DescriptionTeaser = result.DescriptionTeaser;
            PostedDate = result.PostedDate;
            Pay = result.Pay;
            Location = result.Location;
            LocationLatitude = result.LocationLatitude;
            LocationLongitude = result.LocationLongitude;
            Distance = result.Distance;
            Company = result.Company;
        }
    }

    public class JobSearchMainInfo : ModelBase
    {
        #region Properties

        private int _totalPages;
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
            set
            {
                if (value != _totalPages)
                {
                    _totalPages = value;
                    OnPropertyChanged("TotalPages");
                }
            }
        }

        private int _totalCount;
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                if (value != _totalCount)
                {
                    _totalCount = value;
                    OnPropertyChanged("TotalCount");
                }
            }
        }

        private int _firstItemIndex;
        public int FirstItemIndex
        {
            get
            {
                return _firstItemIndex;
            }
            set
            {
                if (value != _firstItemIndex)
                {
                    _firstItemIndex = value;
                    OnPropertyChanged("FirstItemIndex");
                }
            }
        }

        private int _lastItemIndex;
        public int LastItemIndex
        {
            get
            {
                return _lastItemIndex;
            }
            set
            {
                if (value != _lastItemIndex)
                {
                    _lastItemIndex = value;
                    OnPropertyChanged("LastItemIndex");
                }
            }
        }

        private List<JobSearchResultInfo> _resultInfo;
        public List<JobSearchResultInfo> ResultInfo
        {
            get
            {
                return _resultInfo;
            }
            set
            {
                if (value != _resultInfo)
                {
                    _resultInfo = value;
                    OnPropertyChanged("ResultInfo");
                }
            }
        }

        #endregion

        public JobSearchMainInfo()
        {
            ResultInfo = new List<JobSearchResultInfo>();
        }
    }
}
