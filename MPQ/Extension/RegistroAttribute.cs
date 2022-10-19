using MPQ.Models;
using MPQ.Data.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

namespace MPQ.Extension
{
    public class RegistroAttribute : ValidationAttribute
    {
        private IUserRepository _userRepository;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) 
                return ValidationResult.Success;

            try
            {
                var _model = (UsuarioViewModel)validationContext.ObjectInstance;
                _userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));
                var _user = _userRepository.GetAllNoTrackingAsync().Result.Where(f => f.Login.Trim().ToLower() == value.ToString().Trim().ToLower() && f.Id != _model.Id).FirstOrDefault();

                if (!string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    if (_user?.Login != null)
                    {
                        return new ValidationResult("Usuário de rede já cadastrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult("Usuário de rede inválido");
            }
            finally
            {

            }

            return ValidationResult.Success;
        }

    }
}
