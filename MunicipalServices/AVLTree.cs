using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MunicipalServices
{
    internal class AVLTree
    {
        private AVLNode root;
        
        public AVLTree()
        {
            root = null;
        }

        public int getHeight(AVLNode node)
        {
            if(node == null)
            {
                return 0;
            }

            return node.height;
        }

        public int getFactor(AVLNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int factor = getHeight(node.Left) - getHeight(node.Right);

            return factor;
        }

        public void shiftHeight(AVLNode node)
        {
            int maxHeight = Math.Max(getHeight(node.Left),getHeight(node.Right));

            node.height = 1 + maxHeight;
        }

        public AVLNode rotateRight(AVLNode parent)
        {
            AVLNode child = parent.Left;
            AVLNode grandchild = child.Right;

            child.Right = parent;
            parent.Left = grandchild;

            shiftHeight(parent);
            shiftHeight(child);

            return child;
        }

        public AVLNode rotateLeft(AVLNode parent)
        {
            AVLNode child = parent.Right;
            AVLNode grandchild = child.Left;

            child.Left = parent;
            parent.Right = grandchild;

            shiftHeight(parent);
            shiftHeight(child);

            return child;
        }

        public AVLNode searchNode(int IssueID)
        {
            AVLNode foundNode = null;
            AVLNode currentNode = root;

            if (root == null)
            {
                MessageBox.Show("Sorry, the Report you're looking for does not exist");
                return null;
            }

            while (currentNode != null)
            {
                if (currentNode.Key == IssueID)
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
            Stack<AVLNode> subtree = new Stack<AVLNode>();

            if (root == null)
            {
                return null;
            }

            AVLNode currentNode = root;

            while (currentNode != null || subtree.Count > 0)
            {
                while (currentNode != null)
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

        public void InsertNode(int Key, ServiceIssue Value)
        {
            if (Value == null)
            {
                MessageBox.Show("Your issue could not be reported");
                return;
            }

            if (root == null)
            {
                root = new AVLNode(Key, Value);
                return;
            }
            else
            {
                List<AVLNode> traversedNodes = new List<AVLNode>();

                AVLNode previousNode = null;
                AVLNode currentNode = root;

                while (currentNode != null)
                {
                    traversedNodes.Add(currentNode);

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
                    else if (Key == currentNode.Key)
                    {
                        currentNode.Value = Value;
                        return;
                    }

                }

                AVLNode newNode = new AVLNode(Key, Value);

                if (Key > previousNode.Key)
                {
                    previousNode.Right = newNode;
                }
                else if (Key < previousNode.Key)
                {
                    previousNode.Left = newNode;
                }

                traversedNodes.Add(newNode);

                AVLNode child = newNode;

                for(int i = traversedNodes.Count - 2; i >= 0; i--)
                {
                    AVLNode onNode = traversedNodes[i];

                    if (child.Key < onNode.Key)
                    {
                        onNode.Left = child;
                    }
                    else if (child.Key > onNode.Key)
                    {
                        onNode.Right = child;
                    }

                    AVLNode balancedNode = balanceTree(onNode, Key, Value);

                    child = balancedNode;

                    if (i > 0)
                    {
                        AVLNode parentNode = traversedNodes[i - 1];

                        if (balancedNode.Key < parentNode.Key)
                        {
                            parentNode.Left = balancedNode;
                        }
                        else
                        {
                            parentNode.Right = balancedNode;
                        }
                    }
                    else
                    {
                        root = balancedNode;
                    }
                }
            }
        }

        public AVLNode balanceTree(AVLNode node, int Key, ServiceIssue Value)
        {
            shiftHeight(node);

            int balance = getFactor(node);

            if(balance > 1 && Key<node.Key)
            {
                return rotateRight(node);
            }

            if (balance < -11 && Key > node.Key)
            {
                return rotateLeft(node);
            }

            if (balance > 1 && Key > node.Key)
            {
                node.Left = rotateLeft(node.Left);
                return rotateRight(node);
            }

            if (balance < -1 && Key < node.Key)
            {
                node.Right = rotateRight(node.Right);
                return rotateLeft(node);
            }

            return node;
        }
    }
}
