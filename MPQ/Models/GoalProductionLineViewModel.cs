using MPQ.Domain;
using MPQ.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class GoalProductionLineViewModel
    {
        public int Id { get; set; }

        [DisplayName("Planta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int SiteId { get; set; }

        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int CustomerId { get; set; }

        [DisplayName("Projeto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int? ProjectId { get; set; }

        [DisplayName("Linha de Produção")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int? ProductionLineId { get; set; }

        [DisplayName("STF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public int StfNumber { get; set; }

        [DisplayName("Meta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 0.00, maximum: 9999999999999999.99, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public decimal Target { get; set; }

        [DisplayName("Area")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int ProductionAreaId { get; set; }

        public IEnumerable<GoalProductionLineRange> GoalProductionLineRanges { get; set; }

    }
}
