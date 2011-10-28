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
using DOLStatsMango.Models.CareerBuilder.DTO;
using System.Runtime.Serialization;

namespace DOLStatsMango.Models.CareerBuilder
{
    [DataContractAttribute]
    public class JobEducationCode : ModelBase
    {
        #region Properties
        private string _code;
        [DataMember]
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (value != _code)
                {
                    _code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        private string _name;
        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        } 
        #endregion

        public JobEducationCode(Education educationCode)
        {
            Code = educationCode.Code;
            Name = educationCode.Name;
        }
    }
}
