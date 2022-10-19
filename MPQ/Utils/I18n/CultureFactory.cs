using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Utils.I18n
{
    public class CultureFactory : ICultureFactory
    {
        
        public string GetCulture(string idioma)
        {
            if(string.IsNullOrEmpty(idioma))
            {
                return Idiomas.cultureBrasil;
            }


            idioma = idioma.ToLower();

            if (idioma.StartsWith("espan"))
                return Idiomas.cultureEspanha;
            else if (idioma.StartsWith("ingl"))
                return "en-US";

            return Idiomas.cultureBrasil;
        }
    }
}
