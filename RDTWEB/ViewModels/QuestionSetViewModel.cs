using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDTWEB.Data;

namespace RDTWEB.ViewModels
{
    public class QuestionSetViewModel
    {
        public QuestionSet QuestionSet { get; set; }
        public bool IsEditing { get; set; } = false;
    }
}
