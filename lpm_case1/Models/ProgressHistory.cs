using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpm_case1.Models
{
    public class ProgressHistory
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public double ProgressValue { get; set; }
        public long CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
