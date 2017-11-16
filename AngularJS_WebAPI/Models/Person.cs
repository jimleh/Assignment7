using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJS_WebAPI.Models
{
    public class Person
    {
        [Key, Display(Name="ID")]
        public int ID { get; set; }
        [Required, Display(Name="Name")]
        public string Name { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }
    }
}