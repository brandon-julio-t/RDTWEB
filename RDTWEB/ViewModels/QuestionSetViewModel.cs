using RDTWEB.Models;

namespace RDTWEB.ViewModels
{
    public class QuestionSetViewModel
    {
        public QuestionSet QuestionSet { get; set; }
        public bool IsEditing { get; set; } = false;
    }
}