using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 
namespace AircraftSystem.Models
{
    public class AircraftVM
    {
        public Aircraft Aircraft { get; set; }
        public FormFile AircraftPicture { get; set; }
        
    }
}