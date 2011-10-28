using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOLStatsMango.Models.Yahoo;

namespace DOLStatsMango.Framework.Services.Yahoo
{
    public interface IYahooAPIService
    {
        void GetPhysicalLocation(double longitude, double latitude, Action<List<Address>> success, Action<string> error);
    }
}
