using System.Reflection.PortableExecutable;
public class RelationshipMetadata
{
    public string BaseTable { get; set; }
    public string BaseColumn { get; set; }
    public string ReferenceTable { get; set; }
    public string ReferenceColumn { get; set; }
}