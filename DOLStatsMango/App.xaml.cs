using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DOLStatsMango.Framework;
using DOLStatsMango.Framework.Helpers;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Shell;
using GalaSoft.MvvmLight.Threading;
using DOLStatsMango.ViewModels;
using Telerik.Windows.Controls;

namespace DOLStatsMango
{
    public partial class App : Application
    {
        #region View Models

        #region Main View Model
        private static MainViewModel _mainVM = null;
        /// <summary>
        /// A static ViewModel used by the Main views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static MainViewModel MainVM
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_mainVM == null)
                    _mainVM = new MainViewModel();

                return _mainVM;
            }
            set
            {
                if (_mainVM == value)
                {
                    return;
                }
                _mainVM = value;
            }
        }
        #endregion

        #region Search View Model
        private static SearchViewModel _searchVM = null;
        /// <summary>
        /// A static ViewModel used by the Search views to bind against.
        /// </summary>
        /// <returns>The SearchViewModel object.</returns>
        public static SearchViewModel SearchVM
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_searchVM == null)
                    _searchVM = new SearchViewModel();

                return _searchVM;
            }
            set
            {
                if (_searchVM == value)
                {
                    return;
                }
                _searchVM = value;
            }
        }
        #endregion

        #region Job View Model
        private static JobViewModel _jobVM = null;
        /// <summary>
        /// A static ViewModel used by the Job views to bind against.
        /// </summary>
        /// <returns>The JobViewModel object.</returns>
        public static JobViewModel JobVM
        {
            get
            {
                // Delay creation of the view model until necessary
                if (_jobVM == null)
                    _jobVM = new JobViewModel();

                return _jobVM;
            }
            set
            {
                if (_jobVM == value)
                {
                    return;
                }
                _jobVM = value;
            }
        }
        #endregion

        #endregion

        #region Transient Data Storage
        private static TransientDataStorage _transientStorageService = null;

        private static TransientDataStorage TransientStorageService
        {
            get
            {
                if (_transientStorageService == null)
                    _transientStorageService = new TransientDataStorage();

                return _transientStorageService;
            }
        }
        #endregion

        #region Persistent Data Storage
        private static PersistentDataStorage _persistentStorageService = null;

        private static PersistentDataStorage PersistentStorageService
        {
            get
            {
                if (_persistentStorageService == null)
                    _persistentStorageService = new PersistentDataStorage();

                return _persistentStorageService;
            }
        }
        #endregion

        #region IsTrial
        private static LicenseInformation _licenseInfo = new LicenseInformation();

        private static bool _isTrial = true;
        public static bool IsTrial
        {
            get
            {
                return _isTrial;
            }
        }

        #endregion

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        //public PhoneApplicationFrame RootFrame { get; private set; }
        public RadPhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            //Readies the DispatcherHelper for usage throughout application
            DispatcherHelper.Initialize();

            this.RootFrame.UriMapper = Resources["UriMapper"] as UriMapper;
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //CheckLicense();
            RestorePersistentData();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //CheckLicense();
            //Restore Transient data
            var store = new TransientDataStorage();
            App.MainVM = store.Restore<MainViewModel>(IsolatedStorageKeys.MAIN_VIEW_MODEL);
            App.SearchVM = store.Restore<SearchViewModel>(IsolatedStorageKeys.SEARCH_VIEW_MODEL);
            App.JobVM = store.Restore<JobViewModel>(IsolatedStorageKeys.JOB_VIEW_MODEL);
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            //Backup Transient data
            var store = new TransientDataStorage();
            store.Backup(IsolatedStorageKeys.MAIN_VIEW_MODEL, App.MainVM);
            store.Backup(IsolatedStorageKeys.SEARCH_VIEW_MODEL, App.SearchVM);
            store.Backup(IsolatedStorageKeys.JOB_VIEW_MODEL, App.JobVM);
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            BackupPersistentData();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
            e.Handled = true;
            string errDetails = GetNavErrorDetails(e);
            App.MainVM.SetErrorString(errDetails);
            RootFrame.Navigate(new Uri("/ErrorPage", UriKind.Relative));
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
            e.Handled = true;
            string errDetails = GetUnhandledErrorDetails(e);
            App.MainVM.SetErrorString(errDetails);
            RootFrame.Navigate(new Uri("/ErrorPage", UriKind.Relative));
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            //RootFrame = new PhoneApplicationFrame();
            RootFrame = new RadPhoneApplicationFrame();
            RootFrame.Background = new SolidColorBrush(Color.FromArgb(255, 155, 192, 192));

            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Using the telerik phone application frame control
            RadTransition transition = new RadTransition();
            transition.BackwardInAnimation = new RadMoveAnimation() { MoveDirection = MoveDirection.RightIn };
            transition.BackwardOutAnimation = new RadMoveAnimation() { MoveDirection = MoveDirection.RightOut };
            transition.ForwardInAnimation = new RadMoveAnimation() { MoveDirection = MoveDirection.LeftIn };
            transition.ForwardOutAnimation = new RadMoveAnimation() { MoveDirection = MoveDirection.LeftOut };

            RootFrame.Transition = transition;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Check the current license information for this application
        /// </summary>
        private void CheckLicense()
        {
            // When debugging, we want to simulate a trial mode experience. The following conditional allows us to set the _isTrial 
            // property to simulate trial mode being on or off. 
#if DEBUG
            string message = "This sample demonstrates the implementation of a trial mode in an application." +
                               "Press 'OK' to simulate trial mode. Press 'Cancel' to run the application in normal mode.";
            if (MessageBox.Show(message, "Debug Trial",
                 MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _isTrial = true;
            }
            else
            {
                _isTrial = false;
            }
#else
            _isTrial = _licenseInfo.IsTrial();
#endif
        }

        private void BackupPersistentData()
        {
            //var store = new PersistentDataStorage();
            //save persistent data
            //store.Backup("UserLoginInfo", App.UserLoginInfo);
        }
        private void RestorePersistentData()
        {
            //var store = new PersistentDataStorage();
            //restore persistent data
            //App.UserLoginInfo = store.Restore<UserLogin>("UserLoginInfo");
        }

        private string GetNavErrorDetails(NavigationFailedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Exception Details");
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine();
            sb.AppendLine(String.Format("Exception Message: \n{0}", e.Exception.Message));
            sb.AppendLine(String.Format("Main Stack Trace: \n{0}", e.Exception.StackTrace));

            if (e.Exception.InnerException != null)
            {
                sb.AppendLine();
                sb.AppendLine(String.Format("Inner Exception Message: \n{0}", e.Exception.InnerException.Message));
                sb.AppendLine(String.Format("Inner Stack Trace: \n{0}", e.Exception.InnerException.StackTrace));
                if (e.Exception.InnerException.InnerException != null)
                {
                    sb.AppendLine();
                    sb.AppendLine(String.Format("Super Inner Exception Message: \n{0}", e.Exception.InnerException.InnerException.Message));
                    sb.AppendLine(String.Format("Super Inner Stack Trace: \n{0}", e.Exception.InnerException.InnerException.StackTrace));
                }
            }

            return sb.ToString();
        }

        private string GetUnhandledErrorDetails(ApplicationUnhandledExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Exception Details");
            sb.AppendLine("-------------------------------------------------");
            sb.AppendLine();
            sb.AppendLine(String.Format("Exception Message: \n{0}", e.ExceptionObject.Message));
            sb.AppendLine(String.Format("Main Stack Trace: \n{0}", e.ExceptionObject.StackTrace));

            if (e.ExceptionObject.InnerException != null)
            {
                sb.AppendLine();
                sb.AppendLine(String.Format("Inner Exception Message: \n{0}", e.ExceptionObject.InnerException.Message));
                sb.AppendLine(String.Format("Inner Stack Trace: \n{0}", e.ExceptionObject.InnerException.StackTrace));
                if (e.ExceptionObject.InnerException.InnerException != null)
                {
                    sb.AppendLine();
                    sb.AppendLine(String.Format("Super Inner Exception Message: \n{0}", e.ExceptionObject.InnerException.InnerException.Message));
                    sb.AppendLine(String.Format("Super Inner Stack Trace: \n{0}", e.ExceptionObject.InnerException.InnerException.StackTrace));
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
