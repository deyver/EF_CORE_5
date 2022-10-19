using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace MPQ.Helpers
{
    public class ExcelHelper : IExcelHelper
    {
        public byte[] ExportarXlsx(string xml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var xmlWorsheets = xmlDoc.DocumentElement.GetElementsByTagName("Worksheet");
            if (xmlWorsheets == null || xmlWorsheets.Count == 0)
                throw new Exception("Sem conteúdo para exportar");

            var xmlTables = ((XmlElement)xmlWorsheets.Item(0)).GetElementsByTagName("Table");
            var xmlRows = ((XmlElement)xmlTables.Item(0)).GetElementsByTagName("Row");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Plan1");
                var currentRow = 1;

                foreach (XmlElement row in xmlRows)
                {
                    var xmlCells = row.GetElementsByTagName("Data");
                    var cellIndex = 1;

                    foreach (XmlElement cell in xmlCells)
                    {
                        var dataType = cell.GetAttribute("ss:Type");                        
                        worksheet.Cell(currentRow, cellIndex).Value = cell.InnerText;
                        
                        if(currentRow > 1)
                            worksheet.Cell(currentRow, cellIndex).DataType = DataTypeAdapter(dataType);
                        else
                            worksheet.Cell(currentRow, cellIndex).Style.Fill.SetBackgroundColor(XLColor.LightGray);

                        cellIndex++;
                    }

                    currentRow++;
                }

                worksheet.Columns().AdjustToContents();  // Adjust column width
                worksheet.Rows().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        private XLDataType DataTypeAdapter(string sourceDataType)
        {
            switch(sourceDataType)
            {
                case "String":
                    return XLDataType.Text;

                case "DateTime":
                    return XLDataType.DateTime;

                default:
                    return XLDataType.Text;
            }
        }
    }
}
