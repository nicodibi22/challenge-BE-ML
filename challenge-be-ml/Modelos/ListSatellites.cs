using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge_be_ml.Models
{
    public class ListSatellites
    {
        [System.ComponentModel.DataAnnotations.Required]
        public List<Satellite> satellites { get; set; }
    }
}
