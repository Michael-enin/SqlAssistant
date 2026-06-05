using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SqlAssistant.Models.Responses;
using SqlAssistant.Services.Generators;
using SqlAssistant.Services.Metadata;
using SqlAssistant.Services.OpenAI;
using System.Text;

namespace SqlAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
         private readonly SPGeneratorService _spGenerator;
        public ProcedureController(
            SPGeneratorService sPGeneratorService)
        {
            _spGenerator = sPGeneratorService;
        }
        [HttpPost("generateSelect")]
        public IActionResult GenerateSelectSP(ProcedureReuest procedureReuest)
        {
            var generatedProcedure = _spGenerator.GenerateSelectSP(procedureReuest);
            return GeneratesqlFile(procedureReuest, generatedProcedure);
        }
        [HttpPost("generateInsert")]
        public IActionResult GenerateInsert(ProcedureReuest pr)
        {
            var generated = _spGenerator.GenerateInsertSP(pr);
            return GeneratesqlFile(pr, generated);
        }
        [HttpPost("generateUpdate")]
        public IActionResult GenerateUpdate(ProcedureReuest pr)
        {
            var generated = _spGenerator.GenerateUpdateSP(pr);
            return GeneratesqlFile(pr, generated);
        }
        [HttpPost("generateDelete")]
        public IActionResult GenerateDelete(ProcedureReuest pr)
        {
            var generated = _spGenerator.GenerateDeleteSP(pr);
            return GeneratesqlFile(pr, generated);
        }
        private FileContentResult GeneratesqlFile(ProcedureReuest pr, string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            return File(bytes, "application/sql", $"{pr.Schema}.{pr.Name}.sql");
        }
    }
}
