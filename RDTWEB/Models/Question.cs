using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RDTWEB.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Type { get; set; } = "Multiple Choice";
        public bool BooleanAnswer { get; set; }
        public List<string> Choices { get; set; } = new();
        public int CorrectChoiceIndex { get; set; } = -1;
        public int QuestionSetId { get; set; }
        public virtual QuestionSet QuestionSet { get; set; }
        public virtual List<Answer> Answers { get; set; }

        [NotMapped]
        public Answer Answer { get; set; }

        public Question()
        {
            for (var i = 0; i < 4; i++)
            {
                Choices.Add("");
            }
        }
    }
}
