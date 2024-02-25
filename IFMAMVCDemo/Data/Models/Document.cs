using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFMAMVCDemo.Data.Models;
public class Document
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FileName { get; set; }
    [ForeignKey("Member")]
    public int MemberId { get; set; }
    public virtual Member Member { get; set; }
}
