using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOLStatsMango.Models.CareerBuilder;

namespace DOLStatsMango.Framework.Services.CareerBuilder
{
    public interface ICareerBuilderAPIService
    {
        #region Job Search
        void PreviewSearch(JobSearchCriteria criteria, Action<int> success, Action<string> error);
        void QuickSearch(JobSearchCriteria criteria, Action<JobSearchMainInfo> success, Action<string> error);
        void CompanySearch(string companyName, Action<JobSearchMainInfo> success, Action<string> error);
        void JobSearch(JobSearchCriteria criteria, Action<JobSearchMainInfo> success, Action<string> error);
        #endregion

        #region Job Details
        void GetJobDetails(string jobDID, Action<JobDetail> success, Action<string> error);
        #endregion

        #region Job Search Helpers
        void GetEducationCodes(Action<List<JobEducationCode>> success, Action<string> error);
        void GetEmployeeTypes(Action<List<JobEmployeeType>> success, Action<string> error);
        void GetCategories(Action<List<JobCategory>> success, Action<string> error);
        #endregion
    }
}
