using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using DOLStatsMango.Framework.Services.CareerBuilder;

namespace DOLStatsMangoTest
{
    /// <summary>
    /// Summary description for CareerBuilderAPITest
    /// </summary>
    [TestClass]
    public class CareerBuilderAPITest
    {
        #region Private Variables
        private const string _baseUrl = @"http://api.careerbuilder.com/v1/";
        private const string _apiKey = "WDH50QY78HN4GCKZSD6M";
        private RestClient _restClient;
        private RestRequest _restRequest;
        #endregion

        public CareerBuilderAPITest()
        {
            if (_restClient == null)
            {
                _restClient = new RestClient { BaseUrl = _baseUrl };
            }

            if (_restRequest == null)
            {
                _restRequest = new RestRequest();
            }
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetEducationCodesTest()
        {
            CareerBuilderAPIService svc = new CareerBuilderAPIService();
            svc.GetEducationCodes(
                (response) =>
                {
                    if (response != null)
                    {
                        var list = response;
                    }
                },
                (error) =>
                {
                });
        }
    }
}
