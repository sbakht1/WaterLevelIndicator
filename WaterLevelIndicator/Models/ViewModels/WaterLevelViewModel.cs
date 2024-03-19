using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WaterLevelIndicator.Models.ViewModels
{
    public class WaterLevelViewModel
    {
        [Key]
        public int ID { get; set; }
        public int? BoxID { get; set; }
        [Range(0, 5, ErrorMessage = "Height must be between 0 and 5.")]
        public decimal? Measurement { get; set; }
   
        public Nullable<System.DateTime> MeasurementDateTime { get; set; } = DateTime.Now;
        public string TriggerStatus { get; set; }
        public string Label { get; set; }

        public SelectList LabelsSelectList { get; set; }
    }
}