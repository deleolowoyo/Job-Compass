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
using DOLStatsMango.Models.Zillow.DTO;

namespace DOLStatsMango.Models.Zillow
{
    public class DemographicMainInfo : ModelBase
    {
        #region Properties

        private int _regionId;
        public int RegionId
        {
            get
            {
                return _regionId;
            }
            set
            {
                if (value != _regionId)
                {
                    _regionId = value;
                    OnPropertyChanged("RegionId");
                }
            }
        }

        private string _regionState;
        public string RegionState
        {
            get
            {
                return _regionState;
            }
            set
            {
                if (value != _regionState)
                {
                    _regionState = value;
                    OnPropertyChanged("RegionState");
                }
            }
        }

        private string _regionCity;
        public string RegionCity
        {
            get
            {
                return _regionCity;
            }
            set
            {
                if (value != _regionCity)
                {
                    _regionCity = value;
                    OnPropertyChanged("RegionCity");
                }
            }
        }

        private string _regionZip;
        public string RegionZip
        {
            get
            {
                return _regionZip;
            }
            set
            {
                if (value != _regionZip)
                {
                    _regionZip = value;
                    OnPropertyChanged("RegionZip");
                }
            }
        }

        private double _regionLatitude;
        public double RegionLatitude
        {
            get
            {
                return _regionLatitude;
            }
            set
            {
                if (value != _regionLatitude)
                {
                    _regionLatitude = value;
                    OnPropertyChanged("RegionLatitude");
                }
            }
        }

        private double _regionLongitude;
        public double RegionLongitude
        {
            get
            {
                return _regionLongitude;
            }
            set
            {
                if (value != _regionLongitude)
                {
                    _regionLongitude = value;
                    OnPropertyChanged("RegionLongitude");
                }
            }
        }

        private string _forSaleUrl;
        public string ForSaleUrl
        {
            get
            {
                return _forSaleUrl;
            }
            set
            {
                if (value != _forSaleUrl)
                {
                    _forSaleUrl = value;
                    OnPropertyChanged("ForSaleUrl");
                }
            }
        }

        private List<DemographicPageInfo> _demographicPage;
        public List<DemographicPageInfo> DemographicPage
        {
            get
            {
                return _demographicPage;
            }
            set
            {
                if (value != _demographicPage)
                {
                    _demographicPage = value;
                    OnPropertyChanged("DemographicPage");
                }
            }
        }

        #endregion

        public DemographicMainInfo()
        {
            DemographicPage = new List<DemographicPageInfo>();
        }

        public DemographicMainInfo(ResponseData data)
        {
            DemographicPage = new List<DemographicPageInfo>();
            RegionId = data.Region.Id;
            RegionCity = data.Region.City;
            RegionState = data.Region.State;
            RegionZip = data.Region.Zip;
            RegionLatitude = data.Region.Latitude;
            RegionLongitude = data.Region.Longitude;
            ForSaleUrl = data.Links.ForSale;

            if (data.Pages != null)
            {
                foreach (var pg in data.Pages)
                {
                    DemographicPageInfo dPageInfo = new DemographicPageInfo();
                    dPageInfo.Name = pg.Name;

                    if (pg.Tables != null)
                    {
                        foreach (var table in pg.Tables)
                        {
                            if (table.Data != null && table.Data.Attributes != null)
                            {
                                foreach (var attr in table.Data.Attributes)
                                {
                                    DemographicPageInfoData dPageInfoData = new DemographicPageInfoData();
                                    dPageInfoData.Name = attr.Name;
                                    if (attr.Values != null)
                                    {
                                        if (attr.Values.Nation != null && attr.Values.Nation.Value != null)
                                        {
                                            if (!String.IsNullOrWhiteSpace(attr.Values.Nation.Value.Type))
                                            {
                                                dPageInfoData.DataType = attr.Values.Nation.Value.Type;
                                            }
                                            dPageInfoData.NationalValue = attr.Values.Nation.Value.Value;
                                        }
                                        if (attr.Values.Zip != null && attr.Values.Zip.Value != null)
                                        {
                                            if (!String.IsNullOrWhiteSpace(attr.Values.Nation.Value.Type))
                                            {
                                                dPageInfoData.DataType = attr.Values.Nation.Value.Type;
                                            }
                                            dPageInfoData.NeighborhoodValue = attr.Values.Zip.Value.Value;
                                        }
                                    }

                                    dPageInfo.DemographicPageData.Add(dPageInfoData);
                                }
                            }
                        }
                    }

                    DemographicPage.Add(dPageInfo);
                }
            }
        }
    }

    public class DemographicPageInfo : ModelBase
    {
        #region Properties

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = HttpUtility.HtmlDecode(value);
                    OnPropertyChanged("Name");
                }
            }
        }

        private List<DemographicPageInfoData> _demographicPageData;
        public List<DemographicPageInfoData> DemographicPageData
        {
            get
            {
                return _demographicPageData;
            }
            set
            {
                if (value != _demographicPageData)
                {
                    _demographicPageData = value;
                    OnPropertyChanged("DemographicPageData");
                }
            }
        }

        #endregion

        public DemographicPageInfo()
        {
            _demographicPageData = new List<DemographicPageInfoData>();
        }
    }

    public class DemographicPageInfoData : ModelBase
    {
        #region Properties

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = HttpUtility.HtmlDecode(value);
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _dataType;
        public string DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                if (value != _dataType)
                {
                    _dataType = value;
                    OnPropertyChanged("DataType");
                }
            }
        }

        private string _neighborhoodValue;
        public string NeighborhoodValue
        {
            get
            {
                return _neighborhoodValue;
            }
            set
            {
                if (value != _neighborhoodValue)
                {
                    _neighborhoodValue = value;
                    OnPropertyChanged("NeighborhoodValue");
                }
            }
        }

        private string _nationalValue;
        public string NationalValue
        {
            get
            {
                return _nationalValue;
            }
            set
            {
                if (value != _nationalValue)
                {
                    _nationalValue = value;
                    OnPropertyChanged("NationalValue");
                }
            }
        }

        public string FormattedNeighborhoodValue
        {
            get
            {
                string retVal = string.Empty;
                if(String.IsNullOrWhiteSpace(NeighborhoodValue))
                {
                    return retVal;
                }

                if (DataType != null && DataType.ToLower() == "usd")
                {
                    retVal = String.Format("${0:#,#}", Convert.ToInt32(NeighborhoodValue));
                }
                else if (DataType != null && DataType.ToLower() == "percent")
                {
                    retVal = String.Format("{0:P}", Convert.ToDouble(NeighborhoodValue));
                }
                return retVal;
            }
        }

        public string FormattedNationalValue
        {
            get
            {
                string retVal = string.Empty;
                if (String.IsNullOrWhiteSpace(NationalValue))
                {
                    return retVal;
                }

                if (DataType != null && DataType.ToLower() == "usd")
                {
                    retVal = String.Format("${0:#,#}", Convert.ToInt32(NationalValue));
                }
                else if (DataType != null && DataType.ToLower() == "percent")
                {
                    retVal = String.Format("{0:P}", Convert.ToDouble(NationalValue));
                }
                return retVal;
            }
        }

        #endregion
    }
}
