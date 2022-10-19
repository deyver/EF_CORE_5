using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public interface IAplicacao
    {
        Guid GetDominio();
        Guid GetPlanta();
        Guid GetUsuario();
        Guid GetIdioma();
        bool CookieValido();
        string NomeUsuarioDisplay();
        string GetLogin();
        string GetHostName();
        Guid GetStatusAtivo();
        Guid GetStatusInativo();
        string GetAppToken();
    }
}
