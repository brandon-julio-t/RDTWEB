using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDTWEB.Data
{
    public class QuestionSet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartAt { get; set; } = null;
        public DateTime? EndAt { get; set; } = null;
        public virtual List<Question> Questions { get; set; } = new();
    }
}
