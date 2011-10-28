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
using DOLStatsMango.Framework;
using DOLStatsMango.Framework.Services.Yahoo;
using DOLStatsMango.Models.Yahoo;
using GalaSoft.MvvmLight.Threading;
using DOLStatsMango.Framework.Services.CareerBuilder;
using System.Collections.ObjectModel;
using DOLStatsMango.Models.CareerBuilder;
using System.Threading;
using System.Xml.Linq;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using System.Device.Location;
using System.Text.RegularExpressions;
using DOLStatsMango.Models;
using DOLStatsMango.Framework.Helpers;

namespace DOLStatsMango.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        #region Private Variables

        #region Services
        ICareerBuilderAPIService cbSvc;
        IYahooAPIService yahooSvc;
        #endregion

        #region Commands
        private ICommand _quickSearchCommand;
        private ICommand _advancedSearchCommand;
        private ICommand _previewSearchCommand;
        private ICommand _saveSearchCommand;
        private ICommand _resetSearchCriteriaCommand;
        private ICommand _goToJobDetailsCommand;
        private ICommand _loadMoreJobsCommand;
        private ICommand _runSavedSearchCommand;
        #endregion

        #region Location
        private GeoCoordinateWatcher watcher;
        #endregion

        #region Storage

        private readonly PersistentDataStorage persistentStore = new PersistentDataStorage();

        #endregion

        #endregion

        #region Ctor
        public SearchViewModel()
        {
            RegisterEvents();
            RegisterAPIServices();
            WireUpCommands();
        }
        #endregion

        #region Properties

        #region Lists

        private ObservableCollection<JobSearchResultInfo> _jobSearchResults = new ObservableCollection<JobSearchResultInfo>();
        public ObservableCollection<JobSearchResultInfo> JobSearchResults
        {
            get
            {
                return _jobSearchResults;
            }

            set
            {
                if (_jobSearchResults == value)
                {
                    return;
                }

                _jobSearchResults = value;
                NotifyPropertyChanged("JobSearchResults");
            }
        }

        private ObservableCollection<JobSearchResultInfo> _companyJobSearchResults = new ObservableCollection<JobSearchResultInfo>();
        public ObservableCollection<JobSearchResultInfo> CompanyJobSearchResults
        {
            get
            {
                return _companyJobSearchResults;
            }

            set
            {
                if (_companyJobSearchResults == value)
                {
                    return;
                }

                _companyJobSearchResults = value;
                NotifyPropertyChanged("CompanyJobSearchResults");
            }
        }

        private ObservableCollection<JobCategory> _jobCategories = new ObservableCollection<JobCategory>();
        public ObservableCollection<JobCategory> JobCategories
        {
            get
            {
                return _jobCategories;
            }

            set
            {
                if (_jobCategories == value)
                {
                    return;
                }

                _jobCategories = value;
                NotifyPropertyChanged("JobCategories");
            }
        }

        private ObservableCollection<JobEducationCode> _jobEducationCodes = new ObservableCollection<JobEducationCode>();
        public ObservableCollection<JobEducationCode> JobEducationCodes
        {
            get
            {
                return _jobEducationCodes;
            }

            set
            {
                if (_jobEducationCodes == value)
                {
                    return;
                }

                _jobEducationCodes = value;
                NotifyPropertyChanged("JobEducationCodes");
            }
        }

        private ObservableCollection<JobEmployeeType> _jobEmployeeTypes = new ObservableCollection<JobEmployeeType>();
        public ObservableCollection<JobEmployeeType> JobEmployeeTypes
        {
            get
            {
                return _jobEmployeeTypes;
            }

            set
            {
                if (_jobEmployeeTypes == value)
                {
                    return;
                }

                _jobEmployeeTypes = value;
                NotifyPropertyChanged("JobEmployeeTypes");
            }
        }

        private ObservableCollection<JobLastPosted> _jobLastPostedDays = new ObservableCollection<JobLastPosted>();
        public ObservableCollection<JobLastPosted> JobLastPostedDays
        {
            get
            {
                return _jobLastPostedDays;
            }

            set
            {
                if (_jobLastPostedDays == value)
                {
                    return;
                }

                _jobLastPostedDays = value;
                NotifyPropertyChanged("JobLastPostedDays");
            }
        }

        private ObservableCollection<JobRadius> _jobRadiuses = new ObservableCollection<JobRadius>();
        public ObservableCollection<JobRadius> JobRadiuses
        {
            get
            {
                return _jobRadiuses;
            }

            set
            {
                if (_jobRadiuses == value)
                {
                    return;
                }

                _jobRadiuses = value;
                NotifyPropertyChanged("JobRadiuses");
            }
        }

        private ObservableCollection<Country> _countries = new ObservableCollection<Country>();
        public ObservableCollection<Country> Countries
        {
            get
            {
                return _countries;
            }

            set
            {
                if (_countries == value)
                {
                    return;
                }

                _countries = value;
                NotifyPropertyChanged("Countries");
            }
        }

        private ObservableCollection<JobSalary> _salaries = new ObservableCollection<JobSalary>();
        public ObservableCollection<JobSalary> Salaries
        {
            get
            {
                return _salaries;
            }

            set
            {
                if (_salaries == value)
                {
                    return;
                }

                _salaries = value;
                NotifyPropertyChanged("Salaries");
            }
        }

        private ObservableCollection<MapPushPin> _mapSearchPins = new ObservableCollection<MapPushPin>();
        public ObservableCollection<MapPushPin> MapSearchPins
        {
            get
            {
                return _mapSearchPins;
            }

            set
            {
                if (_mapSearchPins == value)
                {
                    return;
                }

                _mapSearchPins = value;
                NotifyPropertyChanged("MapSearchPins");
            }
        }

        private ObservableCollection<MapPushPin> _companyMapSearchPins = new ObservableCollection<MapPushPin>();
        public ObservableCollection<MapPushPin> CompanyMapSearchPins
        {
            get
            {
                return _companyMapSearchPins;
            }

            set
            {
                if (_companyMapSearchPins == value)
                {
                    return;
                }

                _companyMapSearchPins = value;
                NotifyPropertyChanged("CompanyMapSearchPins");
            }
        }

        private ObservableCollection<JobSearchCriteria> _savedSearches = new ObservableCollection<JobSearchCriteria>();
        public ObservableCollection<JobSearchCriteria> SavedSearches
        {
            get
            {
                return _savedSearches;
            }

            set
            {
                if (_savedSearches == value)
                {
                    return;
                }

                _savedSearches = value;
                NotifyPropertyChanged("SavedSearches");
            }
        }

        #endregion

        #region Selected

        private JobCategory _selectedCategory;
        public JobCategory SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (_selectedCategory == value)
                {
                    return;
                }

                _selectedCategory = value;
                NotifyPropertyChanged("SelectedCategory");
            }
        }

        private JobEducationCode _selectedEducationCode;
        public JobEducationCode SelectedEducationCode
        {
            get
            {
                return _selectedEducationCode;
            }

            set
            {
                if (_selectedEducationCode == value)
                {
                    return;
                }

                _selectedEducationCode = value;
                NotifyPropertyChanged("SelectedEducationCode");
            }
        }

        private JobEmployeeType _selectedEmployeeType;
        public JobEmployeeType SelectedEmployeeType
        {
            get
            {
                return _selectedEmployeeType;
            }

            set
            {
                if (_selectedEmployeeType == value)
                {
                    return;
                }

                _selectedEmployeeType = value;
                NotifyPropertyChanged("SelectedEmployeeType");
            }
        }

        private JobSearchResultInfo _selectedJobResult;
        public JobSearchResultInfo SelectedJobResult
        {
            get
            {
                return _selectedJobResult;
            }

            set
            {
                if (_selectedJobResult == value)
                {
                    return;
                }

                _selectedJobResult = value;
                NotifyPropertyChanged("SelectedJobResult");
            }
        }

        #endregion

        #region General

        private Setting _currentSettings;
        public Setting CurrentSettings
        {
            get
            {
                return _currentSettings;
            }
            set
            {
                if (value != _currentSettings)
                {
                    _currentSettings = value;
                    NotifyPropertyChanged("CurrentSettings");
                }
            }
        }

        private JobSearchCriteria _criteria = new JobSearchCriteria();
        public JobSearchCriteria Criteria
        {
            get
            {
                return _criteria;
            }
            set
            {
                if (value != _criteria)
                {
                    _criteria = value;
                    NotifyPropertyChanged("Criteria");
                }
            }
        }

        private JobSearchMainInfo _searchMainInfo = new JobSearchMainInfo();
        public JobSearchMainInfo SearchMainInfo
        {
            get
            {
                return _searchMainInfo;
            }
            set
            {
                if (value != _searchMainInfo)
                {
                    _searchMainInfo = value;
                    NotifyPropertyChanged("SearchMainInfo");
                }
            }
        }

        private JobSearchMainInfo _companySearchMainInfo = new JobSearchMainInfo();
        public JobSearchMainInfo CompanySearchMainInfo
        {
            get
            {
                return _companySearchMainInfo;
            }
            set
            {
                if (value != _companySearchMainInfo)
                {
                    _companySearchMainInfo = value;
                    NotifyPropertyChanged("CompanySearchMainInfo");
                }
            }
        }

        private Address _physicalAddress;
        public Address PhysicalAddress
        {
            get
            {
                return _physicalAddress;
            }
            set
            {
                if (value != _physicalAddress)
                {
                    _physicalAddress = value;
                    NotifyPropertyChanged("PhysicalAddress");
                }
            }
        }

        private int _numOfPreviewResults = new int();
        public int NumOfPreviewResults
        {
            get
            {
                return _numOfPreviewResults;
            }
            set
            {
                if (value != _numOfPreviewResults)
                {
                    _numOfPreviewResults = value;
                    NotifyPropertyChanged("NumOfPreviewResults");
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                if (_isBusy == value)
                {
                    return;
                }

                _isBusy = value;
                NotifyPropertyChanged("IsBusy");
            }
        }

        private bool _isFindingLocation;
        public bool IsFindingLocation
        {
            get
            {
                return _isFindingLocation;
            }

            set
            {
                if (_isFindingLocation == value)
                {
                    return;
                }

                _isFindingLocation = value;
                NotifyPropertyChanged("IsFindingLocation");
            }
        }

        private bool _isFullSearch;
        public bool IsFullSearch
        {
            get
            {
                return _isFullSearch;
            }

            set
            {
                if (_isFullSearch == value)
                {
                    return;
                }

                _isFullSearch = value;
                NotifyPropertyChanged("IsFullSearch");
            }
        }

        private bool _isLoadMore;
        public bool IsLoadMore
        {
            get
            {
                return _isLoadMore;
            }

            set
            {
                if (_isLoadMore == value)
                {
                    return;
                }

                _isLoadMore = value;
                NotifyPropertyChanged("IsLoadMore");
            }
        }

        public int CurrentPage
        {
            get
            {
                if (SearchMainInfo == null || SearchMainInfo.TotalCount <= 0)
                {
                    return 1;
                }
                return (SearchMainInfo.LastItemIndex / 25);
            }
        }

        private bool _isFromSavedSearch;
        public bool IsFromSavedSearch
        {
            get
            {
                return _isFromSavedSearch;
            }

            set
            {
                if (_isFromSavedSearch == value)
                {
                    return;
                }

                _isFromSavedSearch = value;
                NotifyPropertyChanged("IsFromSavedSearch");
            }
        }

        private bool _isLoadingSearches;
        public bool IsLoadingSearches
        {
            get
            {
                return _isLoadingSearches;
            }

            set
            {
                if (_isLoadingSearches == value)
                {
                    return;
                }

                _isLoadingSearches = value;
                NotifyPropertyChanged("IsLoadingSearches");
            }
        }

        private JobSearchCriteria _selectedSavedSearch = new JobSearchCriteria();
        public JobSearchCriteria SelectedSavedSearch
        {
            get
            {
                return _selectedSavedSearch;
            }
            set
            {
                if (value != _selectedSavedSearch)
                {
                    _selectedSavedSearch = value;
                    NotifyPropertyChanged("SelectedSavedSearch");
                }
            }
        }

        #endregion

        #endregion

        #region Commands

        #region Preview Search Command

        public ICommand PreviewSearchCommand
        {
            get { return this._previewSearchCommand; }
        }

        private void DoPreviewSearch(object arg)
        {
            if (String.IsNullOrWhiteSpace(Criteria.Keywords))
            {
                MessageBox.Show("A search keyword is required to perform job searches.  Please specify at least one keyword and try again.");
                return;
            }
            IsLoadMore = false;
            IsFullSearch = true;
            PerformPreviewSearch(Criteria);
        }

        private bool CanDoPreviewSearch(object arg)
        {
            return (Criteria != null);
        }

        #endregion

        #region Quick Search Command

        public ICommand QuickSearchCommand
        {
            get { return this._quickSearchCommand; }
        }

        private void DoQuickSearch(object arg)
        {
            Criteria.Keywords = (string)arg;
            if (String.IsNullOrWhiteSpace(Criteria.Keywords))
            {
                MessageBox.Show("A search keyword is required to perform job searches.  Please specify at least one keyword and try again.");
                return;
            }
            IsLoadMore = false;
            IsFullSearch = false;
            PerformQuickSearch(Criteria);
        }

        private bool CanDoQuickSearch(object arg)
        {
            return (Criteria != null);
        }

        #endregion

        #region Advanced Search Command

        public ICommand AdvancedSearchCommand
        {
            get { return this._advancedSearchCommand; }
        }

        private void DoAdvancedSearch(object arg)
        {
            if (String.IsNullOrWhiteSpace(Criteria.Keywords))
            {
                MessageBox.Show("A search keyword is required to perform job searches.  Please specify at least one keyword and try again.");
                return;
            }

            //if (String.IsNullOrWhiteSpace(Criteria.FriendlyLocation) && (Criteria.Latitude == 0 || Criteria.Longitude == 0))
            //{
            //    MessageBox.Show("A location is required to perform advanced job searches.  Please find or enter your location then try again.");
            //    return;
            //}

            IsLoadMore = false;
            IsFullSearch = true;
            PerformAdvancedSearch(Criteria);
        }

        private bool CanDoAdvancedSearch(object arg)
        {
            return (Criteria != null);
        }

        #endregion

        #region Save Search Command

        public ICommand SaveSearchCommand
        {
            get { return this._saveSearchCommand; }
        }

        private void DoSaveSearch(object arg)
        {
            if (Criteria == null)
            {
                MessageBox.Show("Sorry for the inconvenience, but it looks like there is no search available to be saved.  Please try again.");
                return;
            }

            if (String.IsNullOrWhiteSpace(Criteria.Keywords))
            {
                MessageBox.Show("A search keyword is required to save job searches.  Please specify at least one keyword and try again.");
                return;
            }

            /*
            if (String.IsNullOrWhiteSpace(Criteria.FriendlyLocation) && (Criteria.Latitude == 0 || Criteria.Longitude == 0))
            {
                MessageBox.Show("A location is required to save job searches.  Please find or enter your location then try again.");
                return;
            }
             * */
            PerformSaveSearch(Criteria);
        }

        private bool CanDoSaveSearch(object arg)
        {
            return (Criteria != null);
        }

        #endregion

        #region Reset Search Criteria Command

        public ICommand ResetSearchCriteriaCommand
        {
            get { return this._resetSearchCriteriaCommand; }
        }

        private void DoResetSearchCriteria(object arg)
        {
            PerformResetSearchCriteria();
        }

        #endregion

        #region Go to Job Details Command

        public ICommand GoToJobDetailsCommand
        {
            get { return this._goToJobDetailsCommand; }
        }

        private void DoGoToJobDetails(object arg)
        {
            if (SelectedJobResult != null)
            {
                SendNavigationRequestMessage(new Uri(String.Format("/JobDetailsPage/{0}/{1}", SelectedJobResult.DID, SelectedJobResult.Ranking), UriKind.Relative), "GoToJobDetailsRequest");
            }
        }

        #endregion

        #region Load More Jobs Command

        public ICommand LoadMoreJobsCommand
        {
            get { return this._loadMoreJobsCommand; }
        }

        private void DoLoadMoreJobs(object arg)
        {
            Criteria.PageNumber = CurrentPage + 1;
            IsLoadMore = true;
            if (IsFullSearch)
            {
                PerformAdvancedSearch(Criteria);
            }
            else
            {
                PerformQuickSearch(Criteria);
            }
        }

        private bool CanDoLoadMoreJobs(object arg)
        {
            return (CurrentPage < SearchMainInfo.TotalPages);
        }

        #endregion

        #region Run Saved Search Command

        public ICommand RunSavedSearchCommand
        {
            get { return this._runSavedSearchCommand; }
        }

        private void DoRunSavedSearch(object arg)
        {
            if (SelectedSavedSearch != null)
            {
                App.SearchVM.Criteria = SelectedSavedSearch;
                SendNavigationRequestMessage(new Uri(String.Format("/SearchResultsPage/{0}/{1}", string.Empty, true.ToString()), UriKind.Relative), "PerformSavedSearchRequest");
            }
        }

        #endregion

        #endregion

        #region Helper Methods

        /// <summary>
        /// Registers the API services.
        /// </summary>
        private void RegisterAPIServices()
        {
            if (cbSvc == null)
            {
                cbSvc = new CareerBuilderAPIService();
            }

            if (yahooSvc == null)
            {
                yahooSvc = new YahooAPIService();
            }
        }

        private void RegisterEvents()
        {
            Messenger.Default.Register<bool>(this, "FindMyLocationRequest", OnFindMyLocation);
            Messenger.Default.Register<JobSearchEnum.SortingCriteria>(this, "SortSearchResultsRequest", OnSortSearchResults);
            Messenger.Default.Register<bool>(this, "OnDeleteSavedSearchesRequest", OnDeleteSavedSearches);
        }

        private void WireUpCommands()
        {
            _quickSearchCommand = new DelegateCommand(this.DoQuickSearch, this.CanDoQuickSearch);
            _advancedSearchCommand = new DelegateCommand(this.DoAdvancedSearch, this.CanDoAdvancedSearch);
            _previewSearchCommand = new DelegateCommand(this.DoPreviewSearch, this.CanDoPreviewSearch);
            _saveSearchCommand = new DelegateCommand(this.DoSaveSearch, this.CanDoSaveSearch);
            _resetSearchCriteriaCommand = new DelegateCommand(this.DoResetSearchCriteria);
            _goToJobDetailsCommand = new DelegateCommand(this.DoGoToJobDetails);
            _loadMoreJobsCommand = new DelegateCommand(this.DoLoadMoreJobs, this.CanDoLoadMoreJobs);
            _runSavedSearchCommand = new DelegateCommand(this.DoRunSavedSearch);
        }

        #region Search Helpers

        /// <summary>
        /// Performs the quick search.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        private void PerformQuickSearch(JobSearchCriteria criteria)
        {
            if (!IsNetworkAvailable())
                return;

            IsBusy = true;
            int ranking = 1;
            cbSvc.QuickSearch(criteria,
                (response) =>
                {
                    if (response != null)
                    {
                        SearchMainInfo = response;
                        if (SearchMainInfo.ResultInfo != null)
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                                if (!IsLoadMore)
                                {
                                    JobSearchResults.Clear();
                                }

                                if (IsLoadMore)
                                {
                                    ranking = JobSearchResults.Count + ranking;
                                }

                                foreach (var job in SearchMainInfo.ResultInfo)
                                {
                                    job.Ranking = ranking++;
                                    JobSearchResults.Add(job);
                                }

                                NotifyPropertyChanged("JobSearchResults");

                                if (JobSearchResults.Count > 0)
                                {
                                    LoadMapPins(JobSearchResults);
                                }

                                if (IsLoadMore)
                                {
                                    Messenger.Default.Send<bool>(true, "LoadMoreCompleteRequest");
                                }
                            });
                        }
                    }
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                    });
                },
                (error) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                        ShowErrorMessage();
                    });
                });
        }

        /// <summary>
        /// Performs the company quick search.
        /// </summary>
        /// <param name="companyName">The name of the company.</param>
        internal void PerformCompanyQuickSearch(string companyName)
        {
            if (!IsNetworkAvailable())
                return;

            IsLoadMore = false;
            IsFullSearch = false;
            IsBusy = true;
            int ranking = 1;
            cbSvc.CompanySearch(companyName,
                (response) =>
                {
                    if (response != null)
                    {
                        CompanySearchMainInfo = response;
                        if (CompanySearchMainInfo.ResultInfo != null)
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                                if (!IsLoadMore)
                                {
                                    CompanyJobSearchResults.Clear();
                                }

                                if (IsLoadMore)
                                {
                                    ranking = CompanyJobSearchResults.Count + ranking;
                                }

                                foreach (var job in CompanySearchMainInfo.ResultInfo)
                                {
                                    job.Ranking = ranking++;
                                    CompanyJobSearchResults.Add(job);
                                }

                                if (CompanyJobSearchResults.Count > 0)
                                {
                                    CompanyLoadMapPins(CompanyJobSearchResults);
                                }

                                if (IsLoadMore)
                                {
                                    Messenger.Default.Send<bool>(true, "CompanyLoadMoreCompleteRequest");
                                }
                            });
                        }
                    }
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                    });
                },
                (error) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                        ShowErrorMessage();
                    });
                });
        }

        /// <summary>
        /// Performs the preview search.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        private void PerformPreviewSearch(JobSearchCriteria criteria)
        {
            if (!IsNetworkAvailable())
                return;

            IsBusy = true;
            cbSvc.PreviewSearch(criteria,
                (response) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        NumOfPreviewResults = response;
                        Messenger.Default.Send<int>(NumOfPreviewResults, "PreviewSearchCompleteRequest");
                    });
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                    });
                },
                (error) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                        ShowErrorMessage();
                    });
                });
        }

        /// <summary>
        /// Performs advanced search.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        private void PerformAdvancedSearch(JobSearchCriteria criteria)
        {
            if (!IsNetworkAvailable())
                return;

            IsBusy = true;
            int ranking = 1;
            cbSvc.JobSearch(criteria,
                (response) =>
                {
                    if (response != null)
                    {
                        SearchMainInfo = response;
                        if (SearchMainInfo.ResultInfo != null)
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                {
                                    if (!IsLoadMore)
                                    {
                                        JobSearchResults.Clear();
                                    }

                                    if (IsLoadMore)
                                    {
                                        ranking = JobSearchResults.Count + ranking;
                                    }

                                    foreach (var job in SearchMainInfo.ResultInfo)
                                    {
                                        job.Ranking = ranking++;
                                        JobSearchResults.Add(job);
                                    }

                                    if (JobSearchResults.Count > 0)
                                    {
                                        LoadMapPins(JobSearchResults);
                                    }

                                    if (IsLoadMore)
                                    {
                                        Messenger.Default.Send<bool>(true, "LoadMoreCompleteRequest");
                                    }

                                    SendNavigationRequestMessage(new Uri(String.Format("/SearchResultsPage/{0}/{1}", string.Empty, false.ToString()), UriKind.Relative), "AdvancedSearchComplete");
                                });
                        }
                    }
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                    });
                },
                (error) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                        ShowErrorMessage();
                    });
                });
        }

        /// <summary>
        /// Loads the map pins.
        /// </summary>
        /// <param name="jobSearchResults">The job search results.</param>
        private void LoadMapPins(ObservableCollection<JobSearchResultInfo> jobSearchResults)
        {
            foreach (var job in jobSearchResults)
            {
                MapSearchPins.Add(new MapPushPin
                {
                    Location = new GeoCoordinate(job.LocationLatitude, job.LocationLongitude),
                    Icon = new Uri("/Resources/Images/MapPinColor.png", UriKind.Relative),
                    Ranking = job.Ranking,
                    Tag = String.Format("{0};{1};{2};{3}", job.JobTitle, job.DID, job.Location, job.Ranking)
                });
            }

            Messenger.Default.Send<bool>(true, "LoadMapPinsCompleteRequest");
        }

        /// <summary>
        /// Loads the map pins.
        /// </summary>
        /// <param name="jobSearchResults">The job search results.</param>
        private void CompanyLoadMapPins(ObservableCollection<JobSearchResultInfo> jobSearchResults)
        {
            foreach (var job in jobSearchResults)
            {
                CompanyMapSearchPins.Add(new MapPushPin
                {
                    Location = new GeoCoordinate(job.LocationLatitude, job.LocationLongitude),
                    Icon = new Uri("/Resources/Images/MapPinColor.png", UriKind.Relative),
                    Ranking = job.Ranking,
                    Tag = String.Format("{0};{1};{2};{3}", job.JobTitle, job.DID, job.Location, job.Ranking)
                });
            }

            Messenger.Default.Send<bool>(true, "CompanyLoadMapPinsCompleteRequest");
        }

        /// <summary>
        /// Performs the reset search criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        private void PerformResetSearchCriteria()
        {
            Criteria = new JobSearchCriteria();
            ClearDropDowns();
            Criteria.SpecificEducation = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadDropDowns));
            Messenger.Default.Send<bool>(true, "ResetSearchCompleteRequest");
        }
        #endregion

        #region DropDown Helpers
        /// <summary>
        /// Loads the default drop downs.
        /// </summary>
        internal void LoadDropDowns(object o)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = true;
            });
            if (JobEmployeeTypes.Count <= 0)
            {
                cbSvc.GetEmployeeTypes(
                    (response) =>
                    {
                        if (response != null)
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                                JobEmployeeTypes.Clear();

                                foreach (var empType in response)
                                {
                                    JobEmployeeTypes.Add(empType);
                                }
                                JobEmployeeTypes.Insert(0, new JobEmployeeType { Code = "-1", Name = "Any" });
                                if (!IsFromSavedSearch)
                                {
                                    Criteria.EmployeeType = JobEmployeeTypes[0];
                                    NotifyPropertyChanged("Criteria");
                                }
                            });
                        }
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            IsBusy = false;
                        });
                    },
                    (error) =>
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            IsBusy = false;
                            ShowErrorMessage();
                        });
                    });
            }

            if (JobCategories.Count <= 0)
            {
                cbSvc.GetCategories(
                    (response) =>
                    {
                        if (response != null)
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                                JobCategories.Clear();

                                foreach (var cat in response)
                                {
                                    JobCategories.Add(cat);
                                }
                                JobCategories.Insert(0, new JobCategory { Code = "-1", Name = "Any" });
                                if (!IsFromSavedSearch)
                                {
                                    Criteria.Category = JobCategories[0];
                                    NotifyPropertyChanged("Criteria");
                                }
                            });
                        }
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            IsBusy = false;
                        });
                    },
                    (error) =>
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            IsBusy = false;
                            ShowErrorMessage();
                        });
                    });
            }

            if (JobEducationCodes.Count <= 0)
            {
                cbSvc.GetEducationCodes(
                    (response) =>
                    {
                        if (response != null)
                        {
                            DispatcherHelper.CheckBeginInvokeOnUI(() =>
                            {
                                JobEducationCodes.Clear();

                                foreach (var ed in response)
                                {
                                    JobEducationCodes.Add(ed);
                                }
                                if (!IsFromSavedSearch)
                                {
                                    Criteria.SpecificEducation = true;
                                    NotifyPropertyChanged("Criteria");
                                }
                            });
                        }
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            IsBusy = false;
                        });
                    },
                    (error) =>
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            IsBusy = false;
                            ShowErrorMessage();
                        });
                    });
            }

            if (JobLastPostedDays.Count <= 0)
            {
                XElement postedDays = XElement.Load(@"Resources\Data\PostedTimePeriod.xml");

                var postedDayItems = from pd in postedDays.Descendants("PostedTimePeriod")
                                     select new JobLastPosted
                                     {
                                         Code = pd.Element("Code").Value,
                                         Name = pd.Element("Description").Value
                                     };

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    JobLastPostedDays.Clear();

                    foreach (var day in postedDayItems)
                    {
                        JobLastPostedDays.Add(day);
                        if (!IsFromSavedSearch)
                        {
                            //Temporary
                            if (day.Code == "30")
                            {
                                Criteria.LastPosted = day;
                                NotifyPropertyChanged("Criteria");
                            }
                        }
                    }
                });

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                });
            }

            if (JobRadiuses.Count <= 0)
            {
                XElement radiusValues = XElement.Load(@"Resources\Data\Radius.xml");

                var radiusValueItems = from rad in radiusValues.Descendants("Radius")
                                       select new JobRadius
                                       {
                                           Code = rad.Element("Code").Value,
                                           Name = rad.Element("Description").Value
                                       };

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    JobRadiuses.Clear();

                    foreach (var rd in radiusValueItems)
                    {
                        JobRadiuses.Add(rd);
                        if (!IsFromSavedSearch)
                        {
                            //Temporary
                            if (rd.Code == "30")
                            {
                                Criteria.Radius = rd;
                                NotifyPropertyChanged("Criteria");
                            }
                        }
                    }
                });

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                });
            }

            if (Countries.Count <= 0)
            {
                XElement countries = XElement.Load(@"Resources\Data\CountryCodes.xml");

                var countryItems = from rad in countries.Descendants("Country")
                                   select new Country
                                   {
                                       Code = rad.Element("Code").Value,
                                       Name = rad.Element("Name").Value
                                   };

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    Countries.Clear();

                    foreach (var co in countryItems)
                    {
                        Countries.Add(co);
                    }
                });

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                });
            }

            if (Salaries.Count <= 0)
            {
                XElement salaries = XElement.Load(@"Resources\Data\Salary.xml");

                var salaryItems = from rad in salaries.Descendants("Salary")
                                  select new JobSalary
                                  {
                                      Code = rad.Element("Code").Value,
                                      Name = rad.Element("Description").Value
                                  };

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    Salaries.Clear();

                    foreach (var sal in salaryItems)
                    {
                        Salaries.Add(sal);
                        if (!IsFromSavedSearch)
                        {
                            //Temporary
                            if (sal.Code == "130000")
                            {
                                Criteria.PayHigh = sal;
                                NotifyPropertyChanged("Criteria");
                            }
                            //Temporary
                            if (sal.Code == "0")
                            {
                                Criteria.PayLow = sal;
                                NotifyPropertyChanged("Criteria");
                            }
                        }
                    }
                });

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                });
            }

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = false;
            });
        }

        private void ClearDropDowns()
        {
            Salaries.Clear();
            Countries.Clear();
            JobRadiuses.Clear();
            JobLastPostedDays.Clear();
            JobEducationCodes.Clear();
            JobCategories.Clear();
            JobEmployeeTypes.Clear();
        }
        #endregion

        #region Location

        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyStatusChanged(e));
        }

        private void MyStatusChanged(GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see if the user has disabled the Location Service.
                    if (watcher.Permission == GeoPositionPermission.Denied)
                    {
                        // The user has disabled the Location Service on their device.
                        MessageBox.Show("Sorry for the inconvenience, but Job Compass needs your permission to access the GPS location service.  Please do so on your phone's location settings page.");
                    }
                    else
                    {
                        MessageBox.Show("Sorry for the inconvenience, but the GPS location service is currently non-functional.  Please be sure it is active.");
                    }
                    //Stop the Location Service to conserve battery power.
                    //IsRetrieved();
                    watcher.Stop();
                    break;

                case GeoPositionStatus.Initializing:
                    // The Location Service is initializing.
                    // Disable the Start Location button.
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    // Alert the user and enable the Stop Location button.
                    //GeoCoordinate cto = new GeoCoordinate(34.103179, -84.246876);
                    //RetrievePhysicalAddress(cto);
                    MessageBox.Show("Sorry for the inconvenience, but your location data is currently unavailable.");
                    //Stop the Location Service to conserve battery power.
                    //IsRetrieved();
                    watcher.Stop();
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    // Show the current position and enable the Stop Location button.
                    // Use the Position property of the GeoCoordinateWatcher object to get the current location.
                    GeoCoordinate co = watcher.Position.Location;
                    RetrievePhysicalAddress(co);
                    //Stop the Location Service to conserve battery power.
                    watcher.Stop();
                    break;
            }
        }

        private void RetrievePhysicalAddress(GeoCoordinate co)
        {
            if (!IsNetworkAvailable())
                return;

            Criteria.Latitude = co.Latitude;
            Criteria.Longitude = co.Longitude;

            yahooSvc.GetPhysicalLocation(co.Latitude, co.Longitude,
                (response) =>
                {
                    if (response != null)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            PhysicalAddress = response.FirstOrDefault();
                            Criteria.FriendlyLocation = String.Format("{0}, {1}", PhysicalAddress.City, PhysicalAddress.StateCode);
                            NotifyPropertyChanged("Criteria");
                        });
                    }
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                    });
                },
                (error) =>
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        IsBusy = false;
                        ShowErrorMessage();
                    });
                });
        }

        #endregion

        #region Saved Searches

        /// <summary>
        /// Performs the save search.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        private void PerformSaveSearch(JobSearchCriteria criteria)
        {
            //get watch list
            GetSavedSearches(null);

            if (SavedSearches.Count <= 0)
            {
                //insert immediately
                SavedSearches.Add(criteria);
                persistentStore.Backup(IsolatedStorageKeys.SAVED_SEARCHES, SavedSearches);
                MessageBox.Show("Your search has been saved.");
            }
            else
            {
                //make sure current alert doesn't already exists
                //if so, raise message, if not, save alert
                if (IsSearchInSavedSearches(criteria))
                {
                    MessageBox.Show("A similar job search already exists in your saved searches");
                    return;
                }

                SavedSearches.Add(criteria);
                persistentStore.Backup(IsolatedStorageKeys.SAVED_SEARCHES, SavedSearches);
                MessageBox.Show("Your search has been saved.");
            }
        }

        internal void GetSavedSearches(object o)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsLoadingSearches = true;
            });
            var searches = persistentStore.Restore<ObservableCollection<JobSearchCriteria>>(IsolatedStorageKeys.SAVED_SEARCHES);
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                SavedSearches = (searches == null ? new ObservableCollection<JobSearchCriteria>() : searches);
                IsLoadingSearches = false;
            });
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Messenger.Default.Send<bool>(true, "GetSavedSearchesComplete");
            });
        }

        private void RemoveSavedSearch(JobSearchCriteria savedSearch)
        {
            if (IsSearchInSavedSearches(savedSearch))
            {
                if (SavedSearches.Remove(savedSearch))
                {
                    persistentStore.Backup(IsolatedStorageKeys.SAVED_SEARCHES, SavedSearches);
                }
            }
            else
            {
                MessageBox.Show("This saved search no longer exists.  If this is incorrect, please try again or contact us.");
                return;
            }
            //MessageBox.Show("Your search(es) have been removed.");
        }

        private bool IsSearchInSavedSearches(JobSearchCriteria searchItem)
        {
            foreach (var search in SavedSearches)
            {
                //if so, raise message, if not, save alert
                if (searchItem.Keywords == search.Keywords
                    && searchItem.FriendlyLocation == search.FriendlyLocation)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Settings Helper

        internal void LoadSettings(object o)
        {
            if (CurrentSettings != null)
                return;

            CurrentSettings = new Setting();
            CurrentSettings = persistentStore.Restore<Setting>(IsolatedStorageKeys.SETTINGS_DATA);

            if (CurrentSettings == null)
                CurrentSettings = new Setting();
        }

        #endregion

        #endregion

        #region Event Handlers

        private void OnDeleteSavedSearches(bool payload)
        {
            if (payload)
            {
                IsLoadingSearches = true;

                ObservableCollection<JobSearchCriteria> tempSavedSearches = new ObservableCollection<JobSearchCriteria>();
                foreach (var j in SavedSearches)
                {
                    tempSavedSearches.Add(j);
                }

                foreach (var search in tempSavedSearches)
                {
                    if (search.IsChecked)
                    {
                        RemoveSavedSearch(search);
                    }
                }
                IsLoadingSearches = false;
                MessageBox.Show("Your search(es) have been removed.");
                Messenger.Default.Send<bool>(true, "DeleteSavedSearchesCompleteRequest");
            }
        }

        private void OnFindMyLocation(bool payload)
        {
            if (payload)
            {
                if (CurrentSettings != null && CurrentSettings.IsLocationAware)
                {
                    IsBusy = true;
                    // The watcher variable was previously declared as type GeoCoordinateWatcher.
                    if (watcher == null)
                    {
                        watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy
                        watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal
                        watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
                    }
                    DispatcherHelper.CheckBeginInvokeOnUI(() => watcher.Start());
                }
                else
                {
                    MessageBox.Show("Sorry for the inconvenience, but Job Compass needs your permission to access the GPS location service.  Please do so on the app's settings page.");
                }
            }
        }

        private void OnSortSearchResults(JobSearchEnum.SortingCriteria payload)
        {
            if (Criteria != null)
            {
                switch (payload)
                {
                    case JobSearchEnum.SortingCriteria.Date:
                        Criteria.OrderBy = "Date";
                        Criteria.OrderDirection = string.Empty;
                        break;
                    case JobSearchEnum.SortingCriteria.PayHigh:
                        Criteria.OrderBy = "Pay";
                        Criteria.OrderDirection = "DESC";
                        break;
                    case JobSearchEnum.SortingCriteria.PayLow:
                        Criteria.OrderBy = "Pay";
                        Criteria.OrderDirection = "ASC";
                        break;
                    case JobSearchEnum.SortingCriteria.Title:
                        Criteria.OrderBy = "Title";
                        Criteria.OrderDirection = "ASC";
                        break;
                    case JobSearchEnum.SortingCriteria.Company:
                        Criteria.OrderBy = "Company";
                        Criteria.OrderDirection = "ASC";
                        break;
                    case JobSearchEnum.SortingCriteria.Distance:
                        Criteria.OrderBy = "Distance";
                        Criteria.OrderDirection = string.Empty;
                        break;
                    case JobSearchEnum.SortingCriteria.Relevance:
                        Criteria.OrderBy = "Relevance";
                        Criteria.OrderDirection = string.Empty;
                        break;
                    default:
                        break;
                }

                //sort
                SearchMainInfo = new JobSearchMainInfo();
                IsLoadMore = false;
                Criteria.PageNumber = 1;
                if (IsFullSearch)
                {
                    PerformAdvancedSearch(Criteria);
                }
                else
                {
                    PerformQuickSearch(Criteria);
                }
            }
        }
        #endregion
    }
}
