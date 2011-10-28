using System;
using System.Collections.ObjectModel;
using System.Device.Location;
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
using DOLStatsMango.Framework.Services.Zillow;
using DOLStatsMango.Models.Zillow;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using DOLStatsMango.Framework.Services.CareerBuilder;
using DOLStatsMango.Models.CareerBuilder;
using Microsoft.Phone.Tasks;
using System.Xml.Linq;
using System.Linq;
using DOLStatsMango.Models.DOL;
using System.Collections.Generic;
using System.Threading;
using DOLStatsMango.Framework.Helpers;
using System.Text;

namespace DOLStatsMango.ViewModels
{
    public class JobViewModel : ViewModelBase
    {
        #region Private Variables

        #region Services
        ICareerBuilderAPIService cbSvc;
        IZillowAPIService zillowSvc;
        #endregion

        #region Commands
        private ICommand _sendEmailCommand;
        private ICommand _phoneCallCommand;
        private ICommand _applyForJobCommand;
        private ICommand _viewLargeMapCommand;
        private ICommand _viewListingCommand;
        private ICommand _goToJobDetailsCommand;
        #endregion

        #region Location
        #endregion

        #region Storage

        private readonly PersistentDataStorage persistentStore = new PersistentDataStorage();

        #endregion

        #endregion

        #region Ctor
        public JobViewModel()
        {
            RegisterEvents();
            RegisterAPIServices();
            WireUpCommands();
        }
        #endregion

        #region Properties

        #region Lists

        private ObservableCollection<OccupationalStats> _topJobEmploymentStats = new ObservableCollection<OccupationalStats>();
        public ObservableCollection<OccupationalStats> TopJobEmploymentStats
        {
            get
            {
                return _topJobEmploymentStats;
            }

            set
            {
                if (_topJobEmploymentStats == value)
                {
                    return;
                }

                _topJobEmploymentStats = value;
                NotifyPropertyChanged("TopJobEmploymentStats");
            }
        }

        private ObservableCollection<OccupationalStats> _topJobHourlyStats = new ObservableCollection<OccupationalStats>();
        public ObservableCollection<OccupationalStats> TopJobHourlyStats
        {
            get
            {
                return _topJobHourlyStats;
            }

            set
            {
                if (_topJobHourlyStats == value)
                {
                    return;
                }

                _topJobHourlyStats = value;
                NotifyPropertyChanged("TopJobHourlyStats");
            }
        }

        private ObservableCollection<OccupationalStats> _topJobYearlyStats = new ObservableCollection<OccupationalStats>();
        public ObservableCollection<OccupationalStats> TopJobYearlyStats
        {
            get
            {
                return _topJobYearlyStats;
            }

            set
            {
                if (_topJobYearlyStats == value)
                {
                    return;
                }

                _topJobYearlyStats = value;
                NotifyPropertyChanged("TopJobYearlyStats");
            }
        }

        private List<string> _demographicGroupHeaders = new List<string>();
        public List<string> DemographicGroupHeaders
        {
            get
            {
                return _demographicGroupHeaders;
            }

            set
            {
                if (_demographicGroupHeaders == value)
                {
                    return;
                }

                _demographicGroupHeaders = value;
                NotifyPropertyChanged("DemographicGroupHeaders");
            }
        }

        private ObservableCollection<DemographicPageInfoData> _demographicDetails = new ObservableCollection<DemographicPageInfoData>();
        public ObservableCollection<DemographicPageInfoData> DemographicDetails
        {
            get
            {
                return _demographicDetails;
            }

            set
            {
                if (_demographicDetails == value)
                {
                    return;
                }

                _demographicDetails = value;
                NotifyPropertyChanged("DemographicDetails");
            }
        }

        private ObservableCollection<JobDetail> _savedJobs = new ObservableCollection<JobDetail>();
        public ObservableCollection<JobDetail> SavedJobs
        {
            get
            {
                return _savedJobs;
            }

            set
            {
                if (_savedJobs == value)
                {
                    return;
                }

                _savedJobs = value;
                NotifyPropertyChanged("SavedJobs");
            }
        }

        #endregion

        #region Selected

        private PostingListing _selectedListing;
        public PostingListing SelectedListing
        {
            get
            {
                return _selectedListing;
            }
            set
            {
                if (value != _selectedListing)
                {
                    _selectedListing = value;
                    NotifyPropertyChanged("SelectedListing");
                }
            }
        }

        #endregion

        #region General

        private JobDetail _jobInfo = new JobDetail();
        public JobDetail JobInfo
        {
            get
            {
                return _jobInfo;
            }
            set
            {
                if (value != _jobInfo)
                {
                    _jobInfo = value;
                    NotifyPropertyChanged("JobInfo");
                }
            }
        }

        private JobDetail _selectedSavedJob = new JobDetail();
        public JobDetail SelectedSavedJob
        {
            get
            {
                return _selectedSavedJob;
            }
            set
            {
                if (value != _selectedSavedJob)
                {
                    _selectedSavedJob = value;
                    NotifyPropertyChanged("SelectedSavedJob");
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

        private bool _hasFieldData;
        public bool HasFieldData
        {
            get
            {
                return _hasFieldData;
            }
            set
            {
                if (_hasFieldData == value)
                {
                    return;
                }

                _hasFieldData = value;
                NotifyPropertyChanged("HasFieldData");
            }
        }

        private DemographicMainInfo _jobDemographics = new DemographicMainInfo();
        public DemographicMainInfo JobDemographics
        {
            get
            {
                return _jobDemographics;
            }
            set
            {
                if (value != _jobDemographics)
                {
                    _jobDemographics = value;
                    NotifyPropertyChanged("JobDemographics");
                }
            }
        }

        private PostingInfo _listingInfo = new PostingInfo();
        public PostingInfo ListingInfo
        {
            get
            {
                return _listingInfo;
            }
            set
            {
                if (value != _listingInfo)
                {
                    _listingInfo = value;
                    NotifyPropertyChanged("ListingInfo");
                }
            }
        }

        private PostingInfoDetail _makeMeMoveInfo = new PostingInfoDetail();
        public PostingInfoDetail MakeMeMoveInfo
        {
            get
            {
                return _makeMeMoveInfo;
            }

            set
            {
                if (_makeMeMoveInfo == value)
                {
                    return;
                }

                _makeMeMoveInfo = value;
                NotifyPropertyChanged("MakeMeMoveInfo");
            }
        }

        private PostingInfoDetail _forSaleByOwnerInfo = new PostingInfoDetail();
        public PostingInfoDetail ForSaleByOwnerInfo
        {
            get
            {
                return _forSaleByOwnerInfo;
            }

            set
            {
                if (_forSaleByOwnerInfo == value)
                {
                    return;
                }

                _forSaleByOwnerInfo = value;
                NotifyPropertyChanged("ForSaleByOwnerInfo");
            }
        }

        private PostingInfoDetail _forSaleByAgentInfo = new PostingInfoDetail();
        public PostingInfoDetail ForSaleByAgentInfo
        {
            get
            {
                return _forSaleByAgentInfo;
            }

            set
            {
                if (_forSaleByAgentInfo == value)
                {
                    return;
                }

                _forSaleByAgentInfo = value;
                NotifyPropertyChanged("ForSaleByAgentInfo");
            }
        }

        private PostingInfoDetail _reportForSaleInfo = new PostingInfoDetail();
        public PostingInfoDetail ReportForSaleInfo
        {
            get
            {
                return _reportForSaleInfo;
            }

            set
            {
                if (_reportForSaleInfo == value)
                {
                    return;
                }

                _reportForSaleInfo = value;
                NotifyPropertyChanged("ReportForSaleInfo");
            }
        }

        private PostingInfoDetail _forRentInfo = new PostingInfoDetail();
        public PostingInfoDetail ForRentInfo
        {
            get
            {
                return _forRentInfo;
            }

            set
            {
                if (_forRentInfo == value)
                {
                    return;
                }

                _forRentInfo = value;
                NotifyPropertyChanged("ForRentInfo");
            }
        }

        private PostingInfoDetail _compiledListing = new PostingInfoDetail();
        public PostingInfoDetail CompiledListing
        {
            get
            {
                return _compiledListing;
            }

            set
            {
                if (_compiledListing == value)
                {
                    return;
                }

                _compiledListing = value;
                NotifyPropertyChanged("CompiledListing");
            }
        }

        private bool _isLoadingJobs;
        public bool IsLoadingJobs
        {
            get
            {
                return _isLoadingJobs;
            }

            set
            {
                if (_isLoadingJobs == value)
                {
                    return;
                }

                _isLoadingJobs = value;
                NotifyPropertyChanged("IsLoadingJobs");
            }
        }

        #endregion

        #endregion

        #region Commands

        #region Send Email Command

        public ICommand SendEmailCommand
        {
            get { return this._sendEmailCommand; }
        }

        private void DoSendEmail(object arg)
        {
            WebBrowserTask webBrowseTask = new WebBrowserTask
            {
                Uri = new Uri(JobInfo.ContactInfoEmailURL, UriKind.Absolute)
            };

            webBrowseTask.Show();
        }

        #endregion

        #region Apply For Job Command

        public ICommand ApplyForJobCommand
        {
            get { return this._applyForJobCommand; }
        }

        private void DoApplyForJob(object arg)
        {
            WebBrowserTask webBrowseTask = new WebBrowserTask
            {
                Uri = new Uri(JobInfo.ApplyURL, UriKind.Absolute)
            };

            webBrowseTask.Show();
        }

        #endregion

        #region Phone call Command

        public ICommand PhoneCallCommand
        {
            get { return this._phoneCallCommand; }
        }

        private void DoPhoneCall(object arg)
        {
            PhoneCallTask phoneCall = new PhoneCallTask
            {
                DisplayName = (String.IsNullOrWhiteSpace(JobInfo.ContactInfoName) ? JobInfo.Company : JobInfo.ContactInfoName),
                PhoneNumber = JobInfo.ContactInfoPhone
            };

            phoneCall.Show();
        }

        #endregion

        #region View Large Map Command

        public ICommand ViewLargeMapCommand
        {
            get { return this._viewLargeMapCommand; }
        }

        private void DoViewLargeMap(object arg)
        {
            BingMapsTask mapTask = new BingMapsTask
            {
                Center = new GeoCoordinate(JobInfo.LocationLatitude, JobInfo.LocationLongitude),
                ZoomLevel = 15
            };

            mapTask.Show();
        }

        #endregion

        #region View Large Map Command

        public ICommand ViewListingCommand
        {
            get { return this._viewListingCommand; }
        }

        private void DoViewListing(object arg)
        {
            if (SelectedListing != null && !String.IsNullOrWhiteSpace(SelectedListing.HomeDetailUrl))
            {
                WebBrowserTask browseTask = new WebBrowserTask
                {
                    Uri = new Uri(SelectedListing.HomeDetailUrl, UriKind.Absolute)
                };

                browseTask.Show();
            }
        }

        #endregion

        #region Go to Job Details Command

        public ICommand GoToJobDetailsCommand
        {
            get { return this._goToJobDetailsCommand; }
        }

        private void DoGoToJobDetails(object arg)
        {
            if (SelectedSavedJob != null)
            {
                JobInfo = SelectedSavedJob;
                SendNavigationRequestMessage(new Uri(String.Format("/JobDetailsPage/{0}/{1}", string.Empty, string.Empty), UriKind.Relative), "GoToJobDetailsRequest");
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

            if (zillowSvc == null)
            {
                zillowSvc = new ZillowAPIService();
            }
        }

        private void RegisterEvents()
        {
            Messenger.Default.Register<bool>(this, "GetNeighborhoodInfoRequest", OnGetNeighborhoodInfo);
            Messenger.Default.Register<bool>(this, "SaveJobRequest", OnSaveJob);
            Messenger.Default.Register<bool>(this, "DeleteJobRequest", OnDeleteJob);
            Messenger.Default.Register<bool>(this, "EmailJobRequest", OnEmailJob);
            Messenger.Default.Register<bool>(this, "OnDeleteSavedJobsRequest", OnDeleteSavedJobs);
        }

        private void WireUpCommands()
        {
            _sendEmailCommand = new DelegateCommand(this.DoSendEmail);
            _phoneCallCommand = new DelegateCommand(this.DoPhoneCall);
            _applyForJobCommand = new DelegateCommand(this.DoApplyForJob);
            _viewLargeMapCommand = new DelegateCommand(this.DoViewLargeMap);
            _viewListingCommand = new DelegateCommand(this.DoViewListing);
            _goToJobDetailsCommand = new DelegateCommand(this.DoGoToJobDetails);
        }

        private string TitleTrimmer(string title)
        {
            string retVal = title;

            if (title.Length > 20)
            {
                retVal = String.Format("{0}...", title.Substring(0, 20));
            }

            return retVal;
        }

        #region Job Details Helper

        internal void GetJobDetails(string jobId, string ranking)
        {
            if (!IsNetworkAvailable())
                return;

            IsBusy = true;
            cbSvc.GetJobDetails(jobId,
                (response) =>
                {
                    if (response != null)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            JobInfo = response;
                            JobInfo.Ranking = int.Parse(ranking);
                            Messenger.Default.Send<bool>(true, "LoadJobCompleteRequest");
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

        /// <summary>
        /// Emails the job.
        /// </summary>
        /// <param name="jobInfo">The job info.</param>
        private void EmailJob(JobDetail jobInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("I thought you would be interested in this job:");
            sb.AppendLine();
            sb.AppendLine("JOB INFORMATION");
            sb.AppendLine(String.Format("Job Title: {0}", jobInfo.JobTitle));
            sb.AppendLine(String.Format("Company: {0}", jobInfo.Company));
            sb.AppendLine(String.Format("Location: {0}", jobInfo.LocationFormatted));
            sb.AppendLine(String.Format("Employee Type: {0}", jobInfo.EmploymentType));
            sb.AppendLine(String.Format("Req'd Education: {0}", jobInfo.DegreeRequired));
            sb.AppendLine(String.Format("Req'd Experience: {0}", jobInfo.ExperienceRequired));
            sb.AppendLine(String.Format("Pay: {0}", jobInfo.PayHighLowFormatted));
            sb.AppendLine();
            sb.AppendLine(String.Format("Apply Now: {0}", jobInfo.ApplyURL));

            EmailComposeTask emailTask = new EmailComposeTask
            {
                Subject = String.Format("{0} - {1}", jobInfo.JobTitle, jobInfo.LocationFormatted),
                Body = sb.ToString()
            };

            emailTask.Show();
        }

        #endregion

        #region Stats Helper

        internal void GetRelatedOccupations(object o)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = true;
            });

            //Get the job categories/industries
            string[] categories = JobInfo.Categories.Split(',');

            List<OccupationalClass> chosenClasses = new List<OccupationalClass>();
            List<OccupationalStats> chosenStatClasses = new List<OccupationalStats>();

            //Get matching occupations
            XElement allOccupations = XElement.Load(@"Resources\Data\OccupationalClassifications.xml");

            foreach (var cat in categories)
            {
                var occupationItems = from occ in allOccupations.Descendants("OccupationalClassifications")
                                      where occ.Element("SOCTitle").Value.ToLower().Trim().Contains(cat.ToLower().Trim()) || occ.Element("SOCDefinition").Value.ToLower().Trim().Contains(cat.ToLower().Trim())
                                      select new OccupationalClass
                                      {
                                          Code = occ.Element("SOCCode").Value,
                                          Title = occ.Element("SOCTitle").Value,
                                          Description = occ.Element("SOCDefinition").Value
                                      };
                foreach (var occItem in occupationItems)
                {
                    chosenClasses.Add(occItem);
                }
            }

            if (chosenClasses.Count > 0)
            {
                //Get matching occupation stats
                XElement allStats = XElement.Load(@"Resources\Data\OES.xml");

                foreach (var occClass in chosenClasses)
                {
                    var occupationStatItems = from occStat in allStats.Descendants("OES")
                                              where occStat.Element("OCC_CODE").Value.Equals(occClass.Code)
                                              select new OccupationalStats
                                              {
                                                  Code = occStat.Element("OCC_CODE").Value,
                                                  //Title = TitleTrimmer(occStat.Element("OCC_TITLE").Value),
                                                  Title = occStat.Element("OCC_TITLE").Value,
                                                  TotalEmployment = Convert.ToInt32(occStat.Element("TOT_EMP").Value),
                                                  YearlyMeanWage = Convert.ToInt32(occStat.Element("A_MEAN").Value),
                                                  HourlyMeanWage = Convert.ToDouble(occStat.Element("H_MEAN").Value)
                                              };
                    foreach (var stat in occupationStatItems)
                    {
                        chosenStatClasses.Add(stat);
                    }
                }

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    TopJobEmploymentStats.Clear();
                    TopJobYearlyStats.Clear();
                    TopJobHourlyStats.Clear();

                    TopJobEmploymentStats = new ObservableCollection<OccupationalStats>(chosenStatClasses.OrderByDescending(pr => pr.TotalEmployment).Take(10));
                    TopJobYearlyStats = new ObservableCollection<OccupationalStats>(chosenStatClasses.OrderByDescending(pr => pr.YearlyMeanWage).Take(10));
                    TopJobHourlyStats = new ObservableCollection<OccupationalStats>(chosenStatClasses.OrderByDescending(pr => pr.HourlyMeanWage).Take(10));

                    HasFieldData = true;

                    IsBusy = false;
                });
            }
            else
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    HasFieldData = false;
                    IsBusy = false;
                });
            }
        }

        #endregion

        #region Neighborhood

        private void GetNeighborhoodInfo(object o)
        {
            PerformGetNeighborhoodInfo();
            PerformGetListings();
        }

        /// <summary>
        /// Performs the get neighborhood info.
        /// </summary>
        private void PerformGetNeighborhoodInfo()
        {
            if (!IsNetworkAvailable())
                return;

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = true;
            });
            zillowSvc.GetDemographics(JobInfo.LocationPostalCode, JobInfo.LocationCity, JobInfo.LocationState,
                (response) =>
                {
                    if (response != null)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            JobDemographics = response;

                            //Set Jumplist group headers                    
                            foreach (var demo in JobDemographics.DemographicPage)
                            {
                                DemographicGroupHeaders.Add(demo.Name);

                                //Set details
                                if (demo.DemographicPageData != null)
                                {
                                    foreach (var data in demo.DemographicPageData)
                                    {
                                        DemographicDetails.Add(data);
                                    }
                                }
                            }
                            Messenger.Default.Send<bool>(true, "LoadedDemographicComplete");
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

        /// <summary>
        /// Performs the get listings.
        /// </summary>
        private void PerformGetListings()
        {
            if (!IsNetworkAvailable())
                return;

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = true;
            });
            zillowSvc.GetPostings(JobInfo.LocationPostalCode, JobInfo.LocationCity, JobInfo.LocationState,
                (response) =>
                {
                    if (response != null)
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            ListingInfo = response;

                            CompiledListing = response.AllListing;

                            /*
                            //Set Listing groups
                            if(ListingInfo.MakeMeMove != null)
                            {
                                MakeMeMoveInfo = ListingInfo.MakeMeMove;
                            }

                            if (ListingInfo.ForSaleByOwner != null)
                            {
                                ForSaleByOwnerInfo = ListingInfo.ForSaleByOwner;
                            }

                            if (ListingInfo.ForSaleByAgent != null)
                            {
                                ForSaleByAgentInfo = ListingInfo.ForSaleByAgent;
                            }

                            if (ListingInfo.ReportForSale != null)
                            {
                                ReportForSaleInfo = ListingInfo.ReportForSale;
                            }

                            if (ListingInfo.ForRent != null)
                            {
                                ForRentInfo = ListingInfo.ForRent;
                            }
                             * */
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

        #region Saved Jobs

        /// <summary>
        /// Performs the save job.
        /// </summary>
        /// <param name="job">The job.</param>
        private void PerformSaveJob(JobDetail job)
        {
            //get watch list
            //GetSavedJobs(null);

            if (SavedJobs.Count <= 0)
            {
                //insert immediately
                SavedJobs.Add(job);
                persistentStore.Backup(IsolatedStorageKeys.JOBS_DATA, SavedJobs);
                MessageBox.Show("This job has been added to your favorites.");
            }
            else
            {
                //make sure current job doesn't already exists
                //if so, raise message, if not, save job
                if (IsJobInSavedJobs(job))
                {
                    MessageBox.Show("This job already exists as a favorite.");
                    return;
                }

                SavedJobs.Add(job);
                persistentStore.Backup(IsolatedStorageKeys.JOBS_DATA, SavedJobs);
                MessageBox.Show("This job has been added to your favorites.");
            }
            Messenger.Default.Send<bool>(true, "SaveDeleteJobCompleteRequest");
        }

        internal void GetSavedJobs(object o)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsLoadingJobs = true;
            });
            var jobs = persistentStore.Restore<ObservableCollection<JobDetail>>(IsolatedStorageKeys.JOBS_DATA);
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                SavedJobs = (jobs == null ? new ObservableCollection<JobDetail>() : jobs);
                IsLoadingJobs = false;
            });
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Messenger.Default.Send<bool>(true, "GetSavedJobsComplete");
            });
        }

        internal bool IsJobInSavedJobs(JobDetail jobItem)
        {
            foreach (var job in SavedJobs)
            {
                //if so, raise message, if not, save job
                if (jobItem.DID == job.DID)
                {
                    return true;
                }
            }

            return false;
        }

        private void RemoveJob(JobDetail jobItem)
        {
            if (IsJobInSavedJobs(jobItem))
            {
                if (SavedJobs.Remove(jobItem))
                {
                    persistentStore.Backup(IsolatedStorageKeys.JOBS_DATA, SavedJobs);
                    MessageBox.Show("This job has been removed from your favorites.");
                }
            }
            else
            {
                MessageBox.Show("This job no longer exists as a favorite.  If this is incorrect, please try again or contact us.");
                return;
            }
            Messenger.Default.Send<bool>(true, "SaveDeleteJobCompleteRequest");
        }

        private void RemoveSavedJob(JobDetail jobItem)
        {
            if (IsJobInSavedJobs(jobItem))
            {
                if (SavedJobs.Remove(jobItem))
                {
                    persistentStore.Backup(IsolatedStorageKeys.JOBS_DATA, SavedJobs);
                }
            }
            else
            {
                MessageBox.Show("This job no longer exists as a favorite.  If this is incorrect, please try again or contact us.");
                return;
            }
            //MessageBox.Show("Your job(s) have been removed from your favorites.");
        }

        #endregion

        #endregion

        #region Event Handlers

        private void OnGetNeighborhoodInfo(bool payload)
        {
            if (payload)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(GetNeighborhoodInfo));
            }
        }

        private void OnSaveJob(bool payload)
        {
            if (payload)
            {
                PerformSaveJob(JobInfo);
            }
        }

        private void OnDeleteJob(bool payload)
        {
            if (payload)
            {
                RemoveJob(JobInfo);
            }
        }

        private void OnDeleteSavedJobs(bool payload)
        {
            if (payload)
            {
                IsLoadingJobs = true;

                ObservableCollection<JobDetail> tempSavedJobs = new ObservableCollection<JobDetail>();
                foreach (var j in SavedJobs)
                {
                    tempSavedJobs.Add(j);
                }

                foreach (var job in tempSavedJobs)
                {
                    if (job.IsChecked)
                    {
                        RemoveSavedJob(job);
                    }
                }
                IsLoadingJobs = false;
                MessageBox.Show("Your job(s) have been removed from your favorites.");
                Messenger.Default.Send<bool>(true, "DeleteSavedJobsCompleteRequest");
            }
        }

        private void OnEmailJob(bool payload)
        {
            if (payload)
            {
                EmailJob(JobInfo);
            }
        }

        #endregion
    }
}
