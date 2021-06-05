using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDTWEB.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public bool BooleanAnswer { get; set; }
        public List<string> Choices { get; set; }
        public int CorrectChoiceIndex { get; set; } = -1;
        public int QuestionSetId { get; set; }
        public QuestionSet QuestionSet { get; set; }
    }
}
