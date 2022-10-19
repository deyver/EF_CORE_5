using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nível")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Level { get; set; }

        [Display(Name = "Sequência")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo {0} precisa deve estar entre {2} e {1}.")]
        public int Sequence { get; set; }

        [Display(Name = "Pai")]
        public int? ParentId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Url { get; set; }

        [Display(Name = "Ícone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string IconUrl { get; set; }

    }
}