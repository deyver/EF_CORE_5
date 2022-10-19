using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Helpers
{
    public interface IExcelHelper
    {
        byte[] ExportarXlsx(string xml);
    }
}
