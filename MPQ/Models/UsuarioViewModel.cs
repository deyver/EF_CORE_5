using Microsoft.AspNetCore.Mvc;
using MPQ.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Registro]
        //[Remote("LoginExists", "Usuario", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "Login já cadastrado.")]
        public string Login { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Name { get; set; }

        [DisplayName("Site Padrão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int DefaultSiteId { get; set; }

        [DisplayName("Idioma Padrão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public string DefaultLanguage { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public int Status { get; set; }

        [DisplayName("Outros Sites")]
        public List<int> UserSiteId { get; set; }

        [DisplayName("Grupos")]
        [Required(ErrorMessage = "O campo {0} é obrigatório selecionar um item")]
        public List<int> UserGroupId { get; set; }

    }
}
