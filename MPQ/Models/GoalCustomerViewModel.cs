using MPQ.Domain;
using MPQ.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class GoalCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Name { get; set; }

        [DisplayName("Planta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int SiteId { get; set; }

        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int CustomerId { get; set; }

        [Display(Name = "Unidade de Medida")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string UnitOfMeasurement { get; set; }

        [DisplayName("STF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public int StfNumber { get; set; }

        public IEnumerable<GoalCustomerRange> GoalCustomerRanges { get; set; }

    }
}
