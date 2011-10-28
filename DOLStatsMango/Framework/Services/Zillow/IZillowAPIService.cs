using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOLStatsMango.Models.Zillow;

namespace DOLStatsMango.Framework.Services.Zillow
{
    public interface IZillowAPIService
    {
        #region Demographics
        void GetDemographics(string postalCode, string city, string state, Action<DemographicMainInfo> success, Action<string> error);
        #endregion

        #region Postings
        void GetPostings(string postalCode, string city, string state, Action<PostingInfo> success, Action<string> error);
        #endregion
    }
}
