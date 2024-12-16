using Microsoft.AspNetCore.Mvc;
using TaskApp.ApiModels;
using TaskApp.Services;
using TaskAppDTO;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController:Controller
    {
        private readonly ExcelService _excelService;

        public ExcelController()
        {
            _excelService = new ExcelService();
        }

        [HttpGet("read-excel")]
        public IActionResult ReadExcel()
        {
            //string filePath = "C:\\MyFolder\\Test\\TaskTAAest.xlsx";
            var queryParams = Request.Query;
            if (queryParams.TryGetValue("src", out var srcQueryParam))
            {
                if (string.IsNullOrWhiteSpace(srcQueryParam))
                {
                    throw new CustomException(message: "File path cannot be empty.", statusCode:400);
                }
                Console.WriteLine(queryParams);
                try
                {
                    var excelData = _excelService.ReadExcelData(srcQueryParam);
                    return Ok(new { message = "Data read successfully", data = excelData });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                throw new CustomException(message: "Query param does not exist.", statusCode: 400);
            }
        }
    }
}
