using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaterLevelIndicator.Models.ViewModels
{
    public class AddBoxforWaterLevelViewModel
    {
        [Required]
        public string Label { get; set; }
    }
}