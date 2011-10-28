using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DOLStatsMango.Models.CareerBuilder.DTO
{
    public class Category
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CategoryDTO
    {
        public List<Category> Categories { get; set; }
        public List<Error> Errors { get; set; }
    }
}
