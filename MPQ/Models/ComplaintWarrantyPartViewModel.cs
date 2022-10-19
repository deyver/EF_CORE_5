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
    public class ComplaintWarrantyPartViewModel
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

        [DisplayName("Data")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime ReceiptDate { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório")]
        public int PartQuantity { get; set; }

        [DisplayName("Problema")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Issue { get; set; }

        [DisplayName("Procede")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool? Legitimate { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Status { get; set; }

        [DisplayName("Unidade de Negocio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int BusinessUnitId { get; set; }

    }
}