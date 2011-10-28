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
using DOLStatsMango.Models.Yahoo;
using DOLStatsMango.Models.Yahoo.DTO;
using RestSharp;
using DOLStatsMango.Resources.en;

namespace DOLStatsMango.Framework.Services.Yahoo
{
    public class YahooAPIService : IYahooAPIService
    {
        #region Private Variables
        private RestClient _restClient;
        private RestRequest _restRequest;
        #endregion

        #region Ctor
        public YahooAPIService()
        {
            if (_restClient == null)
            {
                _restClient = new RestClient { BaseUrl = AppResources.YahooPlaceFinderBaseUrl };
            }

            if (_restRequest == null)
            {
                _restRequest = new RestRequest();
            }
        }
        #endregion

        public void GetPhysicalLocation(double latitude, double longitude, Action<List<Address>> success, Action<string> error)
        {
            List<Address> physicalAddress = new List<Address>();

            //http://where.yahooapis.com/geocode?location=37.787082+-122.400929&gflags=R&appid=yourappid

            _restRequest.Resource = string.Format("/geocode?location={0}&gflags=R&appid={1}",String.Format("{0} {1}", latitude, longitude), AppResources.YahooAppId);
            _restClient.ExecuteAsync<AddressDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.ResultSet != null)
                        {
                            foreach (var addy in args.Data.ResultSet)
                            {
                                physicalAddress.Add(new Address(addy));
                            }
                            success(physicalAddress);
                        }
                        else
                        {
                            if (args.Data.Error != null)
                            {
                                error(String.Format("Get Education Code Error - {0}", args.Data.Error.Value));
                            }
                            else
                            {
                                error("Get Education Code Error - args.Data.EducationCodes is null");
                            }
                        }
                    }
                    else
                    {
                        error("Get Education Code Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
    }
}
