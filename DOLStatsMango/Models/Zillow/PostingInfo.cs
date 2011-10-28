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
using DOLStatsMango.Models.Zillow.DTO;
using DOLStatsMango.Resources.en;

namespace DOLStatsMango.Models.Zillow
{
    public class PostingInfo : ModelBase
    {
        #region Properties

        private PostingInfoDetail _makeMeMove;
        public PostingInfoDetail MakeMeMove
        {
            get
            {
                return _makeMeMove;
            }
            set
            {
                if (value != _makeMeMove)
                {
                    _makeMeMove = value;
                    OnPropertyChanged("MakeMeMove");
                }
            }
        }

        private PostingInfoDetail _forSaleByOwner;
        public PostingInfoDetail ForSaleByOwner
        {
            get
            {
                return _forSaleByOwner;
            }
            set
            {
                if (value != _forSaleByOwner)
                {
                    _forSaleByOwner = value;
                    OnPropertyChanged("ForSaleByOwner");
                }
            }
        }

        private PostingInfoDetail _forSaleByAgent;
        public PostingInfoDetail ForSaleByAgent
        {
            get
            {
                return _forSaleByAgent;
            }
            set
            {
                if (value != _forSaleByAgent)
                {
                    _forSaleByAgent = value;
                    OnPropertyChanged("ForSaleByAgent");
                }
            }
        }

        private PostingInfoDetail _reportForSale;
        public PostingInfoDetail ReportForSale
        {
            get
            {
                return _reportForSale;
            }
            set
            {
                if (value != _reportForSale)
                {
                    _reportForSale = value;
                    OnPropertyChanged("ReportForSale");
                }
            }
        }

        private PostingInfoDetail _forRent;
        public PostingInfoDetail ForRent
        {
            get
            {
                return _forRent;
            }
            set
            {
                if (value != _forRent)
                {
                    _forRent = value;
                    OnPropertyChanged("ForRent");
                }
            }
        }

        private PostingInfoDetail _allListing;
        public PostingInfoDetail AllListing
        {
            get
            {
                return _allListing;
            }
            set
            {
                if (value != _allListing)
                {
                    _allListing = value;
                    OnPropertyChanged("AllListing");
                }
            }
        }

        #endregion

        public PostingInfo()
        {
            InitializeProperties();
        }

        public PostingInfo(PostingResponseData data)
        {
            InitializeProperties();
            SetPropertyInfoDetails(MakeMeMove, data.MakeMeMove);
            SetPropertyInfoDetails(ForSaleByOwner, data.ForSaleByOwner);
            SetPropertyInfoDetails(ForSaleByAgent, data.ForSaleByAgent);
            SetPropertyInfoDetails(ReportForSale, data.ReportForSale);
            SetPropertyInfoDetails(ForRent, data.ForRent);
            SetAllListingDetails(MakeMeMove, ForSaleByOwner, ForSaleByAgent, ReportForSale, ForRent);
        }

        private void SetPropertyInfoDetails(PostingInfoDetail posting, PropertyData propData)
        {
            if(propData != null)
            {
                posting.Count = propData.Count;
                if (propData.PropertyListings != null)
                {
                    foreach (var prop in propData.PropertyListings)
                    {
                        PostingListing listing = new PostingListing();
                        listing.LastRefreshedDate = prop.LastRefreshedDate;
                        listing.Price = prop.Price;
                        if (prop.Property != null)
                        {
                            listing.PropertyType = prop.Property.UseCode;
                            listing.Bedrooms = prop.Property.Bedrooms;
                            listing.Bathrooms = prop.Property.Bathrooms;
                            listing.LotSize = prop.Property.LotSizeSqFt;
                            listing.FinishedSize = prop.Property.FinishedSqFt;
                            if (prop.Property.Links != null)
                            {
                                listing.HomeDetailUrl = prop.Property.Links.HomeDetails;
                            }
                            if (prop.Property.Address != null)
                            {
                                listing.StreetAddress = prop.Property.Address.Street;
                                listing.City = prop.Property.Address.City;
                                listing.State = prop.Property.Address.State;
                                listing.ZipCode = prop.Property.Address.ZipCode;
                                listing.Latitude = prop.Property.Address.Latitude;
                                listing.Longitude = prop.Property.Address.Longitude;
                            }
                        }

                        posting.Listings.Add(listing);
                    }
                }
            }
        }

        /// <summary>
        /// Sets all listing details.
        /// </summary>
        /// <param name="makeMeMove">The make me move.</param>
        /// <param name="forSaleByOwner">For sale by owner.</param>
        /// <param name="forSaleByAgent">For sale by agent.</param>
        /// <param name="reportForSale">The report for sale.</param>
        /// <param name="forRent">For rent.</param>
        private void SetAllListingDetails(PostingInfoDetail makeMeMove, PostingInfoDetail forSaleByOwner, PostingInfoDetail forSaleByAgent, PostingInfoDetail reportForSale, PostingInfoDetail forRent)
        {
            int ranking = 1;
            int totalCount = makeMeMove.Count + forSaleByOwner.Count + forSaleByAgent.Count + reportForSale.Count + forRent.Count;

            AllListing.Count = totalCount;

            foreach (var listing1 in makeMeMove.Listings)
            {
                listing1.Ranking = ranking++;
                listing1.ListingCategory = AppResources.Zillow_MakeMeMove;
                AllListing.Listings.Add(listing1);
            }

            foreach (var listing2 in forSaleByOwner.Listings)
            {
                listing2.Ranking = ranking++;
                listing2.ListingCategory = "For Sale By Owner";
                AllListing.Listings.Add(listing2);
            }

            foreach (var listing3 in forSaleByAgent.Listings)
            {
                listing3.Ranking = ranking++;
                listing3.ListingCategory = "For Sale By Agent";
                AllListing.Listings.Add(listing3);
            }

            foreach (var listing4 in reportForSale.Listings)
            {
                listing4.Ranking = ranking++;
                listing4.ListingCategory = "Report For Sale";
                AllListing.Listings.Add(listing4);
            }

            foreach (var listing5 in forRent.Listings)
            {
                listing5.Ranking = ranking++;
                listing5.ListingCategory = "For Rent";
                AllListing.Listings.Add(listing5);
            }
        }
  
        private void InitializeProperties()
        {
            MakeMeMove = new PostingInfoDetail();
            ForSaleByOwner = new PostingInfoDetail();
            ForSaleByAgent = new PostingInfoDetail();
            ReportForSale = new PostingInfoDetail();
            ForRent = new PostingInfoDetail();
            AllListing = new PostingInfoDetail();
        }
    }
    public class PostingInfoDetail : ModelBase
    {
        #region Properties

        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value != _count)
                {
                    _count = value;
                    OnPropertyChanged("Count");
                }
            }
        }

        private List<PostingListing> _listings;
        public List<PostingListing> Listings
        {
            get
            {
                return _listings;
            }
            set
            {
                if (value != _listings)
                {
                    _listings = value;
                    OnPropertyChanged("Listings");
                }
            }
        }

        #endregion

        public PostingInfoDetail()
        {
            Listings = new List<PostingListing>();
        }
    }

    public class PostingListing : ModelBase
    {
        #region Properties

        private DateTime? _lastRefreshedDate;
        public DateTime? LastRefreshedDate
        {
            get
            {
                return _lastRefreshedDate;
            }
            set
            {
                if (value != _lastRefreshedDate)
                {
                    _lastRefreshedDate = value;
                    OnPropertyChanged("LastRefreshedDate");
                }
            }
        }

        private string _homeDetailUrl;
        public string HomeDetailUrl
        {
            get
            {
                return _homeDetailUrl;
            }
            set
            {
                if (value != _homeDetailUrl)
                {
                    _homeDetailUrl = value;
                    OnPropertyChanged("HomeDetailUrl");
                }
            }
        }

        private string _streetAddress;
        public string StreetAddress
        {
            get
            {
                return _streetAddress;
            }
            set
            {
                if (value != _streetAddress)
                {
                    _streetAddress = value;
                    OnPropertyChanged("StreetAddress");
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged("State");
                }
            }
        }

        private string _zipCode;
        public string ZipCode
        {
            get
            {
                return _zipCode;
            }
            set
            {
                if (value != _zipCode)
                {
                    _zipCode = value;
                    OnPropertyChanged("ZipCode");
                }
            }
        }

        private double _latitude;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (value != _latitude)
                {
                    _latitude = value;
                    OnPropertyChanged("Latitude");
                }
            }
        }

        private double _longitude;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (value != _longitude)
                {
                    _longitude = value;
                    OnPropertyChanged("Longitude");
                }
            }
        }

        private string _propertyType;
        public string PropertyType
        {
            get
            {
                return _propertyType;
            }
            set
            {
                if (value != _propertyType)
                {
                    _propertyType = value;
                    OnPropertyChanged("PropertyType");
                }
            }
        }

        private string _listingCategory;
        public string ListingCategory
        {
            get
            {
                return _listingCategory;
            }
            set
            {
                if (value != _listingCategory)
                {
                    _listingCategory = value;
                    OnPropertyChanged("ListingCategory");
                }
            }
        }

        private string _lotSize;
        public string LotSize
        {
            get
            {
                return _lotSize;
            }
            set
            {
                if (value != _lotSize)
                {
                    _lotSize = (String.IsNullOrWhiteSpace(value) ? "--" : String.Format("{0:#,#}", value));
                    OnPropertyChanged("LotSize");
                }
            }
        }

        private string _finishedSize;
        public string FinishedSize
        {
            get
            {
                return _finishedSize;
            }
            set
            {
                if (value != _finishedSize)
                {
                    _finishedSize = (String.IsNullOrWhiteSpace(value) ? "--" : String.Format("{0:#,#}", value));
                    OnPropertyChanged("FinishedSize");
                }
            }
        }

        private string _bathrooms;
        public string Bathrooms
        {
            get
            {
                return _bathrooms;
            }
            set
            {
                if (value != _bathrooms)
                {
                    _bathrooms = (String.IsNullOrWhiteSpace(value) ? "--" : value);
                    OnPropertyChanged("Bathrooms");
                }
            }
        }

        private string _bedrooms;
        public string Bedrooms
        {
            get
            {
                return _bedrooms;
            }
            set
            {
                if (value != _bedrooms)
                {
                    _bedrooms = (String.IsNullOrWhiteSpace(value) ? "--" : value);
                    OnPropertyChanged("Bedrooms");
                }
            }
        }

        private int _price;
        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged("Price");
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

        public string CityLine
        {
            get
            {
                return String.Format("{0}, {1} {2}", City, State, ZipCode);
            }
        }

        public string PriceLine
        {
            get
            {
                return String.Format("{0} {1} Bed/ {2} Bath", String.Format("{0:#,#}", Price), Bedrooms, Bathrooms);
            }
        }

        public string SqFtLine
        {
            get
            {
                return String.Format("{0} Sq Ft. {1} Sq Ft Lot. {2}", FinishedSize, LotSize, PropertyType);
            }
        }

        #endregion
    }

    public enum ListingCategory
    {
        MakeMeMove,
        ForSaleByOwner,
        ForSaleByAgent,
        ReportForSale,
        ForRent
    }
}
