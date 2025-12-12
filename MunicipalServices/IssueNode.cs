using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServices
{
    public class IssueNode
    {
        public int Key;
        public ServiceIssue Value;
        public IssueNode Left;
        public IssueNode Right;

        public IssueNode(int key, ServiceIssue node) 
        {
            this.Key = key;
            Value = node;
            Left = null;
            Right = null;
        }  
    }
}
