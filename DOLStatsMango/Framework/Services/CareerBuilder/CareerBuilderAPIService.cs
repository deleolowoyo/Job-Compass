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
using DOLStatsMango.Models.CareerBuilder;
using System.Collections.Generic;
using RestSharp;
using DOLStatsMango.Resources.en;
using DOLStatsMango.Models.CareerBuilder.DTO;
using System.Xml.Linq;
using System.Linq;
using DOLStatsMango.Framework.Helpers;

namespace DOLStatsMango.Framework.Services.CareerBuilder
{
    public class CareerBuilderAPIService : ICareerBuilderAPIService
    {
        #region Private Variables
        private readonly RestClient _restClient;
        private readonly RestRequest _restRequest;
        #endregion

        #region Ctor
        public CareerBuilderAPIService()
        {
            if (_restClient == null)
            {
                _restClient = new RestClient { BaseUrl = AppResources.CareerBuilderBaseUrl };
            }

            if (_restRequest == null)
            {
                _restRequest = new RestRequest();
            }
        }
        #endregion

        #region Job Search
        public void QuickSearch(JobSearchCriteria criteria, Action<JobSearchMainInfo> success, Action<string> error)
        {
            JobSearchMainInfo jobSearchMainResults = new JobSearchMainInfo();

            string resource = string.Format("/jobSearch?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);
            //Add Search Parameters
            if (criteria != null)
            {
                resource = String.Format("{0}&Keywords={1}", resource, criteria.Keywords);
                resource = String.Format("{0}&PageNumber={1}", resource, (criteria.PageNumber <= 0 ? "1" : criteria.PageNumber.ToString()));
                resource = String.Format("{0}&PerPage={1}", resource, 25);
                resource = String.Format("{0}&OrderBy={1}", resource, (String.IsNullOrWhiteSpace(criteria.OrderBy) ? "Date" : criteria.OrderBy));
                if (!String.IsNullOrWhiteSpace(criteria.OrderDirection))
                {
                    resource = String.Format("{0}&OrderDirection={1}", resource, criteria.OrderDirection);
                }                
            }

            _restRequest.Resource = resource;

            _restClient.ExecuteAsync<JobSearchResultDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        //Set paging values
                        jobSearchMainResults.TotalPages = args.Data.TotalPages;
                        jobSearchMainResults.TotalCount = args.Data.TotalCount;
                        jobSearchMainResults.FirstItemIndex = args.Data.FirstItemIndex;
                        jobSearchMainResults.LastItemIndex = args.Data.LastItemIndex;

                        if (args.Data.Results != null)
                        {
                            foreach (var result in args.Data.Results)
                            {
                                jobSearchMainResults.ResultInfo.Add(new JobSearchResultInfo(result));
                            }
                            success(jobSearchMainResults);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Quick Search Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Quick Search Error - args.Data.Results is null");
                            }
                        }
                    }
                    else
                    {
                        error("Quick Search Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
        public void CompanySearch(string companyName, Action<JobSearchMainInfo> success, Action<string> error)
        {
            JobSearchMainInfo jobSearchMainResults = new JobSearchMainInfo();

            string resource = string.Format("/jobSearch?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);
            resource = String.Format("{0}&CompanyName={1}", resource, companyName);
            resource = String.Format("{0}&PerPage={1}", resource, 50);

            _restRequest.Resource = resource;

            _restClient.ExecuteAsync<JobSearchResultDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        //Set paging values
                        jobSearchMainResults.TotalPages = args.Data.TotalPages;
                        jobSearchMainResults.TotalCount = args.Data.TotalCount;
                        jobSearchMainResults.FirstItemIndex = args.Data.FirstItemIndex;
                        jobSearchMainResults.LastItemIndex = args.Data.LastItemIndex;

                        if (args.Data.Results != null)
                        {
                            foreach (var result in args.Data.Results)
                            {
                                jobSearchMainResults.ResultInfo.Add(new JobSearchResultInfo(result));
                            }
                            success(jobSearchMainResults);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Quick Search Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Quick Search Error - args.Data.Results is null");
                            }
                        }
                    }
                    else
                    {
                        error("Quick Search Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }

        public void JobSearch(JobSearchCriteria criteria, Action<JobSearchMainInfo> success, Action<string> error)
        {
            JobSearchMainInfo jobSearchMainResults = new JobSearchMainInfo();

            string resource = string.Format("/jobSearch?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);

            //Add Search Parameters
            if (criteria != null)
            {
                resource = String.Format("{0}&Keywords={1}", resource, criteria.Keywords);
                resource = String.Format("{0}&CompanyName={1}", resource, criteria.CompanyName);
                resource = String.Format("{0}&PageNumber={1}", resource, (criteria.PageNumber <= 0 ? "1" : criteria.PageNumber.ToString()));
                resource = String.Format("{0}&PerPage={1}", resource, 25);
                resource = String.Format("{0}&OrderBy={1}", resource, (String.IsNullOrWhiteSpace(criteria.OrderBy) ? "Date" : criteria.OrderBy));

                if (criteria.Latitude != 0 && criteria.Longitude != 0)
                {
                    resource = String.Format("{0}&Location={1}", resource, String.Format("{0}::{1}", criteria.Latitude, criteria.Longitude));
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(criteria.FriendlyLocation))
                    {
                        resource = String.Format("{0}&Location={1}", resource, criteria.FriendlyLocation);
                    }
                }

                if (criteria.Radius != null && criteria.Radius.Code != null)
                {
                    resource = String.Format("{0}&Radius={1}", resource, criteria.Radius.Code);
                }
                if (criteria.Category != null && criteria.Category.Code != null)
                {
                    if (!criteria.Category.Code.Equals("-1"))
                    {
                        resource = String.Format("{0}&Category={1}", resource, criteria.Category.Code);
                    }
                }
                if (criteria.EducationCode != null && criteria.EducationCode.Code != null)
                {
                    if (!criteria.EducationCode.Code.Equals("-1"))
                    {
                        resource = String.Format("{0}&EducationCode={1}", resource, criteria.EducationCode.Code);
                    }
                }
                if (criteria.SpecificEducation.HasValue)
                {
                    bool val = !criteria.SpecificEducation.Value;
                    resource = String.Format("{0}&SpecificEducation={1}", resource, val.ToString());
                }
                if (criteria.LastPosted != null && criteria.LastPosted.Code != null)
                {
                    resource = String.Format("{0}&PostedWithin={1}", resource, criteria.LastPosted.Code);
                }
                if (criteria.EmployeeType != null && criteria.EmployeeType.Code != null)
                {
                    if (!criteria.EmployeeType.Code.Equals("-1"))
                    {
                        resource = String.Format("{0}&EmpType={1}", resource, criteria.EmployeeType.Code);
                    }
                }
                if (criteria.PayLow != null && criteria.PayLow.Code != null)
                {
                    resource = String.Format("{0}&PayLow={1}", resource, criteria.PayLow.Code);
                }
                if (criteria.PayHigh != null && criteria.PayHigh.Code != null)
                {
                    resource = String.Format("{0}&PayHigh={1}", resource, criteria.PayHigh.Code);
                }
            }

            _restRequest.Resource = resource;

            _restClient.ExecuteAsync<JobSearchResultDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        //Set paging values
                        jobSearchMainResults.TotalPages = args.Data.TotalPages;
                        jobSearchMainResults.TotalCount = args.Data.TotalCount;
                        jobSearchMainResults.FirstItemIndex = args.Data.FirstItemIndex;
                        jobSearchMainResults.LastItemIndex = args.Data.LastItemIndex;

                        if (args.Data.Results != null)
                        {
                            foreach (var result in args.Data.Results)
                            {
                                jobSearchMainResults.ResultInfo.Add(new JobSearchResultInfo(result));
                            }
                            success(jobSearchMainResults);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Job Search Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Job Search Error - args.Data.Results is null");
                            }
                        }
                    }
                    else
                    {
                        error("Job Search Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }

        public void PreviewSearch(JobSearchCriteria criteria, Action<int> success, Action<string> error)
        {
            string resource = string.Format("/jobSearch?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);

            //Add Search Parameters
            if (criteria != null)
            {
                resource = String.Format("{0}&Keywords={1}", resource, criteria.Keywords);
                //_restRequest.AddUrlSegment("CompanyName", criteria.CompanyName);
                //resource = String.Format("{0}&PageNumber={1}", resource, (criteria.PageNumber <= 0 ? "1" : criteria.PageNumber.ToString()));
                resource = String.Format("{0}&OrderBy={1}", resource, (String.IsNullOrWhiteSpace(criteria.OrderBy) ? "Date" : criteria.OrderBy));

                if (criteria.Latitude != 0 && criteria.Longitude != 0)
                {
                    resource = String.Format("{0}&Location={1}", resource, String.Format("{0}::{1}", criteria.Latitude, criteria.Longitude));
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(criteria.FriendlyLocation))
                    {
                        resource = String.Format("{0}&Location={1}", resource, criteria.FriendlyLocation);
                    }
                }

                if (criteria.Radius != null)
                {
                    resource = String.Format("{0}&Radius={1}", resource, criteria.Radius.Code);
                }
                if (criteria.Category != null)
                {
                    if (!criteria.Category.Code.Equals("-1"))
                    {
                        resource = String.Format("{0}&Category={1}", resource, criteria.Category.Code);
                    }
                }
                if (criteria.EducationCode != null)
                {
                    if (!criteria.EducationCode.Code.Equals("-1"))
                    {
                        resource = String.Format("{0}&EducationCode={1}", resource, criteria.EducationCode.Code);
                    }
                }
                if (criteria.SpecificEducation.HasValue)
                {
                    bool val = !criteria.SpecificEducation.Value;
                    resource = String.Format("{0}&SpecificEducation={1}", resource, val.ToString());
                }
                if (criteria.LastPosted != null)
                {
                    resource = String.Format("{0}&PostedWithin={1}", resource, criteria.LastPosted.Code);
                }
                if (criteria.EmployeeType != null)
                {
                    if (!criteria.EmployeeType.Code.Equals("-1"))
                    {
                        resource = String.Format("{0}&EmpType={1}", resource, criteria.EmployeeType.Code);
                    }
                }
                if (criteria.PayLow != null)
                {
                    resource = String.Format("{0}&PayLow={1}", resource, criteria.PayLow.Code);
                }
                if (criteria.PayHigh != null)
                {
                    resource = String.Format("{0}&PayHigh={1}", resource, criteria.PayHigh.Code);
                }
            }

            _restRequest.Resource = resource;

            _restClient.ExecuteAsync<JobSearchResultDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.Results != null)
                        {
                            success(args.Data.TotalCount);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Job Search Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Job Search Error - args.Data.Results is null");
                            }
                        }
                    }
                    else
                    {
                        error("Job Search Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
        #endregion

        #region Job Details
        public void GetJobDetails(string jobDID, Action<JobDetail> success, Action<string> error)
        {
            JobDetail job = new JobDetail();

            _restRequest.Resource = string.Format("/job?DeveloperKey={0}&DID={1}", AppResources.CareerBuilderDeveloperKey, jobDID);

            //Add Search Parameters
            if (!string.IsNullOrWhiteSpace(jobDID))
            {
                //_restRequest.AddUrlSegment("DID", jobDID);
            }
            _restClient.ExecuteAsync<JobDetailDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    /*
                    job = ParseJobDetails(args.Content);
                    if (job != null)
                    {
                        success(job);
                    }
                    else
                    {
                        error("Job Details Error - job is null");
                    }
                     * */

                    if (args.Data != null)
                    {
                        if (args.Data.ResponseJob != null)
                        {
                            job = new JobDetail(args.Data.ResponseJob);
                            success(job);
                        }
                        else if (!String.IsNullOrWhiteSpace(args.Content))
                        {
                            job = ParseJobDetails(args.Content);
                            success(job);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Job Details Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Job Details Error - args.Data.Results is null");
                            }
                        }
                    }
                    else
                    {
                        error("Job Details Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
        #endregion

        #region Job Search Helpers
        public void GetEducationCodes(Action<List<JobEducationCode>> success, Action<string> error)
        {
            List<JobEducationCode> jobEducationCodes = new List<JobEducationCode>();

            _restRequest.Resource = string.Format("/educationcodes?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);
            _restClient.ExecuteAsync<EducationCodeDto>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.EducationCodes != null)
                        {
                            foreach (var edCode in args.Data.EducationCodes)
                            {
                                jobEducationCodes.Add(new JobEducationCode(edCode));
                            }
                            success(jobEducationCodes);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Get Education Code Error - {0}", args.Data.Errors[0].Value));
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

        public void GetEmployeeTypes(Action<List<JobEmployeeType>> success, Action<string> error)
        {
            List<JobEmployeeType> jobEmployeeType = new List<JobEmployeeType>();

            _restRequest.Resource = string.Format("/employeeTypes?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);
            _restClient.ExecuteAsync<EmployeeTypeDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.EmployeeTypes != null)
                        {
                            foreach (var empType in args.Data.EmployeeTypes)
                            {
                                jobEmployeeType.Add(new JobEmployeeType(empType));
                            }
                            success(jobEmployeeType);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Get Employee Type Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Get Employee Type Error - args.Data.EmployeeTypes is null");
                            }
                        }
                    }
                    else
                    {
                        error("Get Employee Type Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }

        public void GetCategories(Action<List<JobCategory>> success, Action<string> error)
        {
            List<JobCategory> jobCategories = new List<JobCategory>();

            _restRequest.Resource = string.Format("/categories?DeveloperKey={0}", AppResources.CareerBuilderDeveloperKey);
            _restClient.ExecuteAsync<CategoryDTO>(_restRequest, (args) =>
            {
                if (args.StatusCode == HttpStatusCode.OK)
                {
                    if (args.Data != null)
                    {
                        if (args.Data.Categories != null)
                        {
                            foreach (var cat in args.Data.Categories)
                            {
                                jobCategories.Add(new JobCategory(cat));
                            }
                            success(jobCategories);
                        }
                        else
                        {
                            if (args.Data.Errors != null && args.Data.Errors.Count > 0)
                            {
                                error(String.Format("Get Categories Error - {0}", args.Data.Errors[0].Value));
                            }
                            else
                            {
                                error("Get Categories Error - args.Data.Categories is null");
                            }
                        }
                    }
                    else
                    {
                        error("Get Categories Error - args.Data is null");
                    }
                }
                else
                {
                    error(args.ErrorMessage);
                }
            });
        }
        #endregion

        #region Helper Methods

        private JobDetail ParseJobDetails(string responseContent)
        {
            XElement jobDetails = XElement.Parse(responseContent);
            JobDetail retVal = new JobDetail();

            var jobDetailItems = from jd in jobDetails.Descendants("Job")
                                 select new JobDetail
                                 {
                                     DID = jd.Element("DID").Value,
                                     DisplayJobID = jd.Element("DisplayJobID").Value,
                                     JobTitle = jd.Element("JobTitle").Value,
                                     JobDescription = Utilities.StripHTML(HttpUtility.HtmlDecode(jd.Element("JobDescription").Value)).Trim(),
                                     JobRequirements = (String.IsNullOrWhiteSpace(HttpUtility.HtmlDecode(jd.Element("JobRequirements").Value)) ? "N/A" : Utilities.StripHTML(HttpUtility.HtmlDecode(jd.Element("JobRequirements").Value)).Trim()),
                                     CompanyDID = jd.Element("CompanyDID").Value,
                                     Company = jd.Element("Company").Value,
                                     CompanyDetailsURL = jd.Element("CompanyDetailsURL").Value,
                                     CompanyImageURL = jd.Element("CompanyImageURL").Value,
                                     ContactInfoName = jd.Element("ContactInfoName").Value,
                                     ContactInfoEmailURL = jd.Element("ContactInfoEmailURL").Value,
                                     ContactInfoPhone = jd.Element("ContactInfoPhone").Value,
                                     LocationStreet1 = jd.Element("LocationStreet1").Value,
                                     LocationStreet2 = jd.Element("LocationStreet2").Value,
                                     LocationCity = jd.Element("LocationCity").Value,
                                     LocationMetroCity = jd.Element("LocationMetroCity").Value,
                                     LocationState = jd.Element("LocationState").Value,
                                     LocationPostalCode = jd.Element("LocationPostalCode").Value,
                                     LocationCountry = jd.Element("LocationCountry").Value,
                                     LocationFormatted = jd.Element("LocationFormatted").Value,
                                     LocationLatitude = Convert.ToDouble(jd.Element("LocationLatitude").Value),
                                     LocationLongitude = Convert.ToDouble(jd.Element("LocationLongitude").Value),
                                     PayPer = jd.Element("PayPer").Value,
                                     PayHighLowFormatted = jd.Element("PayHighLowFormatted").Value,
                                     EmploymentType = jd.Element("EmploymentType").Value,
                                     Division = jd.Element("Division").Value,
                                     Categories = jd.Element("Categories").Value,
                                     ApplyURL = jd.Element("ApplyURL").Value,
                                     TravelRequired = jd.Element("TravelRequired").Value,
                                     ExperienceRequired = jd.Element("ExperienceRequired").Value,
                                     DegreeRequired = jd.Element("DegreeRequired").Value,
                                     RelocationCovered = Convert.ToBoolean(jd.Element("RelocationCovered").Value),
                                     ManagesOthers = Convert.ToBoolean(jd.Element("ManagesOthers").Value)
                                 };

            retVal = jobDetailItems.FirstOrDefault();

            return retVal;
        }

        #endregion
    }
}
