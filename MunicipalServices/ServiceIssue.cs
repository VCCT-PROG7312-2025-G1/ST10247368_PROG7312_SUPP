using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServices
{
    public class ServiceIssue
    {
        public int RequestID { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public List<string> MediaFiles { get; set; } = new List<string>();
        public int Priority { get; set; }
        public string Status { get; set; }
    }
}
