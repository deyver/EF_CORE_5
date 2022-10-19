using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class BarraSuperiorViewModel
    {

        [DisplayName("Id")]
        public Int32 _userId { get; set; }

        [DisplayName("Planta")]
        public Int32 _siteId { get; set; }

        [DisplayName("Idioma")]
        public string _Language { get; set; }

    }
}