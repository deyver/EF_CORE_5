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
    public class GoalStfViewModel
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

        [DisplayName("Meta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int GoalId { get; set; }

        [DisplayName("STF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public int StfNumber { get; set; }

        [DisplayName("Valor Geral")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal GeneralTarget { get; set; }

        [DisplayName("Valor Janeiro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal JanTarget { get; set; }

        [DisplayName("Valor Fevereiro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal FebTarget { get; set; }

        [DisplayName("Valor Março")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal MarTarget { get; set; }

        [DisplayName("Valor Abril")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal AprTarget { get; set; }

        [DisplayName("Valor Maio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal MayTarget { get; set; }

        [DisplayName("Valor Junho")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal JunTarget { get; set; }

        [DisplayName("Valor Julho")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal JulTarget { get; set; }

        [DisplayName("Valor Agosto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal AugTarget { get; set; }

        [DisplayName("Valor Setembro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal SepTarget { get; set; }

        [DisplayName("Valor Outubro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal OctTarget { get; set; }

        [DisplayName("Valor Novembro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal NovTarget { get; set; }

        [DisplayName("Valor Dezembro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal DecTarget { get; set; }

        [DisplayName("Unidade de Negocio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int BusinessUnitId { get; set; }

    }
}
