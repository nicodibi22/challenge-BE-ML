using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge_be_ml.Models
{
    public class Satellite
    {
        public string name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public float? distance { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string[] message { get; set; }
    }
}
