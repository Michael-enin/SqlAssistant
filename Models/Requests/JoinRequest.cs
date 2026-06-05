public class JoinRequest
{
    public string JoinType { get; set; }
    public string LeftTable { get; set; }
    public string LeftColumn { get; set; }
    public string RightTable { get; set; }
    public string RightColumn { get; set; }
}