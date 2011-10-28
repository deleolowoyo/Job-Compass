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
using DOLStatsMango.Models.Zillow;
using DOLStatsMango.Models.Zillow.DTO;
using RestSharp;
using DOLStatsMango.Resources.en;

namespace DOLStatsMango.Framework.Services.Zillow
{
    public class ZillowAPIService : IZillowAPIService
    {
        #region Private Variables
        private readonly RestClient _restClient;
        private readonly RestRequest _restRequest;
        #endregion

        #region Ctor
        public ZillowAPIService()
        {
            if (_restClient == null)
            {
                _restClient = new RestClient { BaseUrl = AppResources.ZillowBaseUrl };
            }

            if (_restRequest == null)
            {
                _restRequest = new RestRequest();
            }
        }
        #endregion

        #region Demographics
        /// <summary>
        /// Gets the demographics.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="success">The success.</param>
        /// <param name="error">The error.</param>
        public void GetDemographics(string postalCode, string city, string state, Action<DemographicMainInfo> success, Action<string> error)
        {
            //http://www.zillow.com/webservice/GetDemographics.htm?zws-id=X1-ZWz1d6xwzun5e3_4dboj&zip=30004
            DemographicMainInfo demoInfo = new DemographicMainInfo();

            string resource = string.Format("/GetDemographics.htm?zws-id={0}", AppResources.ZillowApiKey);

            if(!String.IsNullOrWhiteSpace(postalCode))
            {
                resource = String.Format("{0}&zip={1}", resource, postalCode);
            }
            else if (!String.IsNullOrWhiteSpace(city) && !String.IsNullOrWhiteSpace(state))
            {
                resource = String.Format("{0}&state={1}&city={2}", resource, state, city);
            }
            else
            {
                success(demoInfo);
                return;
            }

            _restRequest.Resource = resource;

            _restClient.ExecuteAsync<DemographicDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.Message.Code == 0)
                        {
                            if (args.Data.Response != null)
                            {
                                demoInfo = new DemographicMainInfo(args.Data.Response);
                            }
                            success(demoInfo);
                        }
                        else
                        {
                            if (args.Data.Message != null)
                            {
                                error(String.Format("Get Demographics Error - {0}", args.Data.Message.Text));
                            }
                            else
                            {
                                error("Get Demographics Error ");
                            }
                        }
                    }
                    else
                    {
                        error("Get Demographics Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
        #endregion


        #region Postings
        /// <summary>
        /// Gets the postings.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <param name="success">The success.</param>
        /// <param name="error">The error</param>
        public void GetPostings(string postalCode, string city, string state, Action<PostingInfo> success, Action<string> error)
        {
            //http://www.zillow.com/webservice/GetRegionPostings.htm?zws-id=X1-ZWz1d6xwzun5e3_4dboj&zipcode=30004&rental=true
            PostingInfo posting = new PostingInfo();

            string resource = string.Format("/GetRegionPostings.htm?zws-id={0}", AppResources.ZillowApiKey);

            if (!String.IsNullOrWhiteSpace(postalCode))
            {
                resource = String.Format("{0}&zipcode={1}&rental=true", resource, postalCode);
            }
            else if (!String.IsNullOrWhiteSpace(city) && !String.IsNullOrWhiteSpace(state))
            {
                resource = String.Format("{0}&citystatezip={1}&rental=true", resource, HttpUtility.UrlEncode(city+"+"+state));
            }
            else
            {
                success(posting);
                return;
            }

            _restRequest.Resource = resource;

            _restClient.ExecuteAsync<PostingDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.Message.Code == 0)
                        {
                            if (args.Data.Response != null)
                            {
                                posting = new PostingInfo(args.Data.Response);
                            }
                            success(posting);
                        }
                        else
                        {
                            if (args.Data.Message != null)
                            {
                                error(String.Format("Get Postings Error - {0}", args.Data.Message.Text));
                            }
                            else
                            {
                                error("Get Postings Error ");
                            }
                        }
                    }
                    else
                    {
                        error("Get Postings Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
        #endregion
    }
}
