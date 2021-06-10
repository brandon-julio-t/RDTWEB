using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RDTWEB.Models
{
    public class QuestionSet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public virtual List<Question> Questions { get; set; } = new();

        private string _status = "T口T"; // should not be this crying emoji
        
        [NotMapped]
        public string Status
        {
            get
            {
                if (_status != "T口T") return _status;
                
                if (StartAt == null || EndAt == null) return _status = "-";

                var now = DateTime.Now;

                var isOver = now > EndAt;
                if (isOver) return _status = "Is Over";

                var isNotStarted = now < StartAt;
                if (isNotStarted) return _status = "Not Started";

                if (now >= StartAt && now <= EndAt) return _status = "Ongoing";

                return _status = "T口T"; // should not reach here
            }
            set => _status = value;
        }

        public void UpdateStatusByUserId(string userId)
        {
            var isFinalized = Questions.All(question =>
            {
                var answer = question.Answers.SingleOrDefault(a => a.UserId == userId);
                return answer?.UserId == userId && answer?.IsFinalized == true;
            });

            Status = isFinalized ? "Finalized" : Status;
        }
    }
}