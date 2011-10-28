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

namespace DOLStatsMango.Models.CareerBuilder.DTO
{
    public class Education
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class EducationCodeDto
    {
        public List<Education> EducationCodes { get; set; }
        public List<Error> Errors { get; set; }
    }
}
