using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using DOLStatsMango.Framework;
using DOLStatsMango.Framework.Helpers;
using DOLStatsMango.Models;
using DOLStatsMango.Models.CareerBuilder;
using DOLStatsMango.Models.DOL;
using GalaSoft.MvvmLight.Messaging;
using DOLStatsMango.Framework.Services.CareerBuilder;
using GalaSoft.MvvmLight.Threading;
using System.Device.Location;
using DOLStatsMango.Framework.Services.Yahoo;
using DOLStatsMango.Models.Yahoo;
using System.Linq;

namespace DOLStatsMango.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Variables

        #region Services
        ICareerBuilderAPIService cbSvc;
        #endregion

        #region Commands
        #endregion

        #region Storage

        private PersistentDataStorage persistentStore = new PersistentDataStorage();

        #endregion

        #endregion

        #region Ctor
        public MainViewModel()
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

        private ObservableCollection<OccupationalStats> _allJobs = new ObservableCollection<OccupationalStats>();
        public ObservableCollection<OccupationalStats> AllJobs
        {
            get
            {
                return _allJobs;
            }

            set
            {
                if (_allJobs == value)
                {
                    return;
                }

                _allJobs = value;
                NotifyPropertyChanged("AllJobs");
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

        private OccupationalStats _selectedJob;
        public OccupationalStats SelectedJob
        {
            get
            {
                return _selectedJob;
            }
            set
            {
                if (value != _selectedJob)
                {
                    _selectedJob = value;
                    NotifyPropertyChanged("SelectedJob");
                    Messenger.Default.Send<bool>(true, "SelectedJobComplete");
                }
            }
        }

        #endregion

        #region General

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

        private bool _isLoaded;
        public bool IsLoaded
        {
            get
            {
                return _isLoaded;
            }

            set
            {
                if (_isLoaded == value)
                {
                    return;
                }

                _isLoaded = value;
                NotifyPropertyChanged("IsLoaded");
            }
        }

        private string _errorString;
        public string ErrorString
        {
            get
            {
                return _errorString;
            }
            set
            {
                _errorString = value;
                NotifyPropertyChanged("ErrorString");
            }
        }

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

        #endregion

        #endregion

        #region Commands

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
        }

        private void RegisterEvents()
        {
            Messenger.Default.Register<bool>(this, "SaveSettingsRequest", OnSaveAppSettings);
            //Messenger.Default.Register<bool>(this, "SignInRequest", OnSignIn);
        }

        public void SetErrorString(string error)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                ErrorString = error;
            });
        }

        private void WireUpCommands()
        {
        }

        #region Stats Helper

        private void GetJobTrends()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = true;
            });

            //Get matching occupations
            XElement allStats = XElement.Load(@"Resources\Data\OES.xml");

            var occupations = from occStat in allStats.Descendants("OES")
                              select new OccupationalStats
                              {
                                  Code = occStat.Element("OCC_CODE").Value,
                                  Title = occStat.Element("OCC_TITLE").Value,
                                  TotalEmployment = Convert.ToInt32(occStat.Element("TOT_EMP").Value),
                                  YearlyMeanWage = Convert.ToInt32(occStat.Element("A_MEAN").Value),
                                  HourlyMeanWage = Convert.ToDouble(occStat.Element("H_MEAN").Value)
                              };

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                TopJobEmploymentStats.Clear();
                TopJobYearlyStats.Clear();
                TopJobHourlyStats.Clear();

                TopJobEmploymentStats = new ObservableCollection<OccupationalStats>(occupations.OrderByDescending(pr => pr.TotalEmployment).Take(10));
                TopJobYearlyStats = new ObservableCollection<OccupationalStats>(occupations.OrderByDescending(pr => pr.YearlyMeanWage).Take(10));
                TopJobHourlyStats = new ObservableCollection<OccupationalStats>(occupations.OrderByDescending(pr => pr.HourlyMeanWage).Take(10));

                IsLoaded = true;
                IsBusy = false;
            });
        }

        internal void GetAllJobs(object o)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                IsBusy = true;
            });
            //Get matching occupations
            XElement allStats = XElement.Load(@"Resources\Data\OES.xml");

            var jobs = from occStat in allStats.Descendants("OES")
                              select new OccupationalStats
                              {
                                  Code = occStat.Element("OCC_CODE").Value,
                                  Title = occStat.Element("OCC_TITLE").Value,
                                  TotalEmployment = Convert.ToInt32(occStat.Element("TOT_EMP").Value),
                                  YearlyMeanWage = Convert.ToInt32(occStat.Element("A_MEAN").Value),
                                  YearlyPercentile10 = Convert.ToInt32(occStat.Element("A_PCT10").Value) / 1000,
                                  YearlyPercentile25 = Convert.ToInt32(occStat.Element("A_PCT25").Value) / 1000,
                                  YearlyPercentileMedian = Convert.ToInt32(occStat.Element("A_MEDIAN").Value)/1000,
                                  YearlyPercentile75 = Convert.ToInt32(occStat.Element("A_PCT75").Value) /1000,
                                  YearlyPercentile90 = Convert.ToInt32(occStat.Element("A_PCT90").Value) /1000,
                                  HourlyMeanWage = Convert.ToDouble(occStat.Element("H_MEAN").Value),
                                  HourlyPercentile10 = Convert.ToDouble(occStat.Element("H_PCT10").Value),
                                  HourlyPercentile25 = Convert.ToDouble(occStat.Element("H_PCT25").Value),
                                  HourlyPercentileMedian = Convert.ToDouble(occStat.Element("H_MEDIAN").Value),
                                  HourlyPercentile75 = Convert.ToDouble(occStat.Element("H_PCT75").Value),
                                  HourlyPercentile90 = Convert.ToDouble(occStat.Element("H_PCT90").Value),
                              };

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                AllJobs.Clear();

                AllJobs = new ObservableCollection<OccupationalStats>(jobs);
                IsBusy = false;
            });
        }

        #endregion

        #region Settings Helper

        private void LoadSettings()
        {
            if (CurrentSettings != null)
                return;

            CurrentSettings = new Setting();
            CurrentSettings = persistentStore.Restore<Setting>(IsolatedStorageKeys.SETTINGS_DATA);

            if (CurrentSettings == null)
                CurrentSettings = new Setting();
        }

        internal void SaveLocationSettingsFromDisclaimer(bool value)
        {
            CurrentSettings = new Setting();
            CurrentSettings.IsLocationAware = value;
            persistentStore.Backup(IsolatedStorageKeys.SETTINGS_DATA, CurrentSettings);
        }

        #endregion

        internal void LoadJobTrendsandSettings(object o)
        {
            GetJobTrends();
            LoadSettings();
        }

        #endregion

        #region Event Handlers

        private void OnSaveAppSettings(bool payload)
        {
            if (payload)
            {
                IsBusy = true;
                try
                {
                    //Save app settings
                    persistentStore.Backup(IsolatedStorageKeys.SETTINGS_DATA, CurrentSettings);
                    IsBusy = false;

                    //Return to previous page
                    Messenger.Default.Send<bool>(true, "SaveAppSettingsComplete");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion
    }
}
