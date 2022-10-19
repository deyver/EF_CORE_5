using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Utils.I18n
{
    public interface ICultureFactory
    {
        string GetCulture(string idioma);
    }
}
