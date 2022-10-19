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
    public class CustomerViewViewModel
    {
        public int Id { get; set; }

        [DisplayName("Planta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int SiteId { get; set; }

        [DisplayName("Indicador")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int IndicatorId { get; set; }

        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int CustomerId { get; set; }

        [DisplayName("Ano")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório")]
        public int Year { get; set; }

        [DisplayName("Semana")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório")]
        public int WeekNumber { get; set; }

        [DisplayName("Resultado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal Result { get; set; }

        [DisplayName("Unidade de Negocio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int BusinessUnitId { get; set; }

    }
}