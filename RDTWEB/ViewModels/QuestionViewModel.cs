using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDTWEB.Models;

namespace RDTWEB.ViewModels
{
    public class QuestionViewModel
    {
        public Question Question { get; set; }
        public bool IsEditing { get; set; }
    }
}
