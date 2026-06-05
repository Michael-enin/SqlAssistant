
using System.Reflection.Metadata.Ecma335;
using SqlAssistant.Types;

public class ProcedureReuest
{
    public string Schema { get; set; }
    public string Database { get; set; }
    public string BaseTable { get; set; }
    public ProcedureType Type { get; set; }
    public string Name { get; set; }
    public List<string> RequiredColumns { get; set; } = [];
    public List<string> Parameters { get; set; } = [];
    public List<JoinRequest> Joins { get; set; } = [];
    public List<FilterRequest> Filters { get; set; } = [];
    public List<string> GroupByColumns { get; set; } = [];
    public List<string> OrderByColumns { get; set; } = [];
    public string AdditionalStandards { get; set; }

}