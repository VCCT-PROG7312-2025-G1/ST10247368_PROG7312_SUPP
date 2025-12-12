using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServices
{
    public class BST
    {
        public IssueNode root;

        public BST()
        {
            root = null;
            
        }

        public void InsertNode(int Key, ServiceIssue Value)
        {
            if(Value == null)
            {
                MessageBox.Show("Your issue could not be reported");
                return;
            }

            if(root == null)
            {
                root = new IssueNode(Key, Value);
            }
            else
            {
                IssueNode previousNode = null;
                IssueNode currentNode = root;
                
                while(currentNode != null)
                {
                    if (Key > currentNode.Key)
                    {
                        previousNode = currentNode;
                        currentNode = currentNode.Right;
                    }
                    else if (Key < currentNode.Key)
                    {
                        previousNode = currentNode;
                        currentNode = currentNode.Left;
                    }
                    else if(Key == currentNode.Key)
                    {
                        currentNode.Value = Value;                            
                        break;
                    }                    
                    
                }

                IssueNode newNode = new IssueNode(Key, Value);

                if (Key > previousNode.Key)
                {
                    previousNode.Right = newNode;
                }
                else if (Key < previousNode.Key)
                {
                    previousNode.Left = newNode;
                }
            }    
        }

        public IssueNode searchNode(int IssueID)
        {
            IssueNode foundNode = null;
            IssueNode currentNode = root;

            if(root == null)
            {
                MessageBox.Show("Sorry, the Report you're looking for does not exist");
                return null;
            }

            while(currentNode!= null)
            {
                if(currentNode.Key == IssueID)
                {
                    foundNode = currentNode;
                    break;
                }
                else if (IssueID > currentNode.Key)
                {
                    currentNode = currentNode.Right;             
                    
                }
                else if (IssueID < currentNode.Key)
                {
                    currentNode = currentNode.Left;
                }
            }

            return foundNode;
        }

        public List<ServiceIssue> InOrderList()
        {
            List<ServiceIssue> results = new List<ServiceIssue>();
            Stack<IssueNode> subtree = new Stack<IssueNode>();

            if(root == null)
            {
                return null;
            }

            IssueNode currentNode = root;

            while(currentNode != null || subtree.Count > 0)
            {
                while(currentNode != null)
                {
                    subtree.Push(currentNode);
                    currentNode = currentNode.Left;
                }

                currentNode = subtree.Pop();

                results.Add(currentNode.Value);

                currentNode = currentNode.Right;
            }

            return results;
        }

    }
}
