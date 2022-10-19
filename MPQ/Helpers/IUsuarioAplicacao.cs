using MPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public interface IUsuarioAplicacao
    {
        /// <summary>
        /// Será feita tentativa de busca em memórica, caso não encontrando, como contingência buscar em api armazenando resultado.
        /// </summary>
        /// <returns></returns>
        UsuarioViewModel Recuperar();
        void Armazenar(UsuarioViewModel usu);
    }
}