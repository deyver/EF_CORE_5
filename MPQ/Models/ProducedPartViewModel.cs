using MPQ.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class ProducedPartViewModel
    {

        public int Id { get; set; }

        [DisplayName("Planta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O valor informado excede o tamanho máximo para o campo {0}.")]
        public int SiteId { get; set; }

        [DisplayName("Area")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O valor informado excede o tamanho máximo para o campo {0}.")]
        public int ProductionAreaId { get; set; }

        [DisplayName("Ano")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2040, ErrorMessage = "O valor informado excede o tamanho máximo para o campo {0}.")]
        public int Year { get; set; }

        [DisplayName("Semana")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 53, ErrorMessage = "O valor informado excede o tamanho máximo para o campo {0}.")]
        public int WeekNumber { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(minimum: 1, maximum: 2147483647, ErrorMessage = "O valor informado excede o tamanho máximo para o campo {0}.")]
        public int Quantity { get; set; }

        public virtual Site Site { get; set; }
        public virtual ProductionArea ProductionArea { get; set; }
    }
}
