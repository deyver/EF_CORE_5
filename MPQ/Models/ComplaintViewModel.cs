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
    public class ComplaintViewModel
    {
        public int Id { get; set; }

        [DisplayName("Indicador")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int IndicatorId { get; set; }

        [DisplayName("Data")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime ComplaintDate { get; set; }

        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int CustomerId { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantity { get; set; }

        [DisplayName("Problema")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Issue { get; set; }

        [DisplayName("Ação de Contenção")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ContainmentAction { get; set; }

        [DisplayName("Data da Ação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime ActionDate { get; set; }

        [DisplayName("Responsável pela ação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ActionResponsible { get; set; }

        [DisplayName("Num GQRS")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string GqrsNumber { get; set; }

        [DisplayName("Sumário enviado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string SummarySent { get; set; }

        [DisplayName("Planta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int SiteId { get; set; }

        [DisplayName("Unidade de Negocio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int BusinessUnitId { get; set; }

    }
}
