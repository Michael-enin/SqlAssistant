namespace SqlAssistant.Services.Metadata
{
    public class AIPromptBuilderService
    {
        public AIPromptBuilderService() { }
        public string BuildPrompt(ProcedureReuest procedureReuest)
        {
        return $@"
        GENERATAE A SQL Server Stored Procedure.
        Procedure Type:{procedureReuest.Type};
        Database:{procedureReuest.Database};
        Schema:{procedureReuest.Schema};
        Base Table:{procedureReuest.BaseTable};
        Required Columns:{string.Join(", ", procedureReuest.RequiredColumns)}
        AdditionalStandards:{procedureReuest.AdditionalStandards}
        Requirements:
        1. Use SQL Server syntax and best practices.
        2. Use alias for tables and columns to improve readability.
        3. Use SET NOCOUNT ON at the beginning of the procedure to improve performance.
        4 .U Use TrY CATCH block for error handling and return meaningful error messages.       
                ";
        }
    }
}
