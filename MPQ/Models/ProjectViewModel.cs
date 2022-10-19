using MPQ.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Name { get; set; }

        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int CustomerId { get; set; }

        [DisplayName("Planta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int SiteId { get; set; }
    }
}
