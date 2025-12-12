using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServices
{
    public class AVLNode
    {
        public int Key;
        public ServiceIssue Value;
        public AVLNode Left;
        public AVLNode Right;
        public int height; 

        public AVLNode(int key, ServiceIssue issue)
        {
            this.Key = key;
            Value = issue;
            height = 1;

            this.Left = null;
            this.Right = null;
        }
    }
}
