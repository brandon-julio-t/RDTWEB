using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

namespace RDTWEB.Models
{
    public class Answer
    {
        public string StringAnswer { get; set; }
        public bool? BooleanAnswer { get; set; }
        public int? ChosenIndex { get; set; }
        public bool? IsCorrect { get; set; }
        public bool IsFinalized { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}