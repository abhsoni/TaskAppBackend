using ExcelDataReader;
using TaskApp.ApiModels;

namespace TaskApp.Services
{
    public class ExcelService
    {
        public List<Dictionary<string, object>> ReadExcelData(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            List<Dictionary<string, object>> excelData = new List<Dictionary<string, object>>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                {
                    List<string> headers = new List<string>();
                    if (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            headers.Add((string)reader.GetValue(i));
                        }
                    }
                    while (reader.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[headers[i]] = reader.GetValue(i);
                        }
                        excelData.Add(row);
                    }
                }
            }

            return excelData;
        }
    }
}
