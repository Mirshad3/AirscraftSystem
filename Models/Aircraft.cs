using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 
namespace AircraftSystem.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Make { get; set; }
        [StringLength(128)]
        public string Model { get; set; }
        public string Registration { get; set; }
        [StringLength(255)]
        public string Location { get; set; }
        [Display(Name = "Date And Time")]
        public DateTime DateAndTime { get; set; }
        [NotMapped]
        public IFormFile AircraftPicture { get; set; }
        
    }
}