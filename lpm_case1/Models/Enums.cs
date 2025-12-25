using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpm_case1.Models
{
    public enum CourseCategory
    {
        Programming,
        Design,
        Marketing,
        Languages,
        Other
    }

    public enum DifficultyLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }

    public enum CourseStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}
