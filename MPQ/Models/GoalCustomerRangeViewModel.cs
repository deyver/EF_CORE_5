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
    public class GoalCustomerRangeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Meta de Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int GoalCustomerId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Name { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.01, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal Value { get; set; }

        [Display(Name = "Cor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Color { get; set; }

        [DisplayName("Operador")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int OperatorId { get; set; }

    }
}
