using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

namespace RDTWEB.Models
{
    public class Answer
    {
        public string StringAnswer { get; set; } = null;
        public bool? BooleanAnswer { get; set; } = null;
        public int? ChosenIndex { get; set; } = null;
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        
        [NotMapped]
        public IBrowserFile BrowserFile { get; set; } = null;
    }
}
