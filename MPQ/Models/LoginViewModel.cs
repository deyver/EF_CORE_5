using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }

        [Display(Name = "Dominio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Dominio { get; set; }


        public virtual int Id { get; set; }
        //public virtual string Login { get; set; }
        public virtual string Name { get; set; }
        public virtual int DefaultSiteId { get; set; }
        public virtual string DefaultLanguage { get; set; }
        public virtual Int32 Status { get; set; }



    }
}
