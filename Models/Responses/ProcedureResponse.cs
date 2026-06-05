namespace SqlAssistant.Models.Responses
{
    public class ProcedureResponse
    {
        public string GenerateSql { get; set; }
        public bool IsValid { get; set; }
        public List <string> ValidationErrors { get; set; }
    }
}
