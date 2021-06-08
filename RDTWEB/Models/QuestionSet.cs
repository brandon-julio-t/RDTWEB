using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDTWEB.Models
{
    public class QuestionSet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartAt { get; set; } = null;
        public DateTime? EndAt { get; set; } = null;
        public virtual List<Question> Questions { get; set; } = new();

        public string Status 
        {
            get 
            {
                if (StartAt == null || EndAt == null)
                {
                    return "-";
                }

                var now = DateTime.Now;

                var isOver = now > EndAt;
                if (isOver)
                {
                    return "Is Over";
                }

                var isNotStarted = now < StartAt;
                if (isNotStarted)
                {
                    return "Not Started";
                }

                if (now >= StartAt && now <= EndAt)
                {
                    return "Ongoing";
                }

                return "T口T"; // should not reach here
            }
        }
    }
}
