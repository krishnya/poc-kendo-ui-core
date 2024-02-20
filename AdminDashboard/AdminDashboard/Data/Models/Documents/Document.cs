using AdminDashboard.Data.Models.Members;
using System.ComponentModel.DataAnnotations.Schema;

public class Document
{
    public int Id { get; set; }
    public string FileName { get; set; }
    [ForeignKey("Member")]
    public int MemberId { get; set; }
    public virtual Member Member { get; set; }
}
