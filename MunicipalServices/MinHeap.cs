using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServices
{
    internal class MinHeap
    {
        private List<Tuple<int, ServiceIssue>> tuples = new List<Tuple<int, ServiceIssue>>();

        public MinHeap() 
        { }

        public int getSize()
        {
            return tuples.Count;
        }

        public bool isEmpty()
        {
            if(getSize()>0)
            {
                return false;
            }

            return true;
        }

        public void clearHeap()
        {
            tuples.Clear();
        }

        public void addToHeap(ServiceIssue issue, int priority)
        {
            Tuple<int, ServiceIssue> newValue = new Tuple<int, ServiceIssue>(priority, issue);

            tuples.Add(newValue);

            int counter = getSize() - 1;

            while(counter > 0)
            {
                int partition = (counter - 1) / 2;

                if (tuples[counter].Item1 >= tuples[partition].Item1)
                {
                    break;
                }

                Tuple<int, ServiceIssue> temp = tuples[counter];
                tuples[counter] = tuples[partition];
                tuples[partition] = temp;

                counter = partition;
            }
        }

        public ServiceIssue removeFromHeap()
        {
            if(isEmpty())
            {
                MessageBox.Show("Error occured in updating issues reported");
                return null;
            }

            ServiceIssue first = tuples[0].Item2;

            int lastIndex = getSize() - 1;

            tuples[0] = tuples[lastIndex];

            tuples.RemoveAt(lastIndex);

            lastIndex--;

            int partition = 0;

            while(true)
            {
                int leftStart = partition * 2 + 1;
                int rightStart = partition * 2 + 2;

                if(leftStart > lastIndex)
                {
                    break;
                }

                int minimum = 1;

                if((rightStart <= lastIndex) && (tuples[rightStart].Item1 < tuples[leftStart].Item1))
                {
                    minimum = rightStart;
                }

                if (tuples[partition].Item1 <= tuples[minimum].Item1)
                {
                    break;
                }

                Tuple<int, ServiceIssue> temp = tuples[partition];
                tuples[partition] = tuples[minimum];
                tuples[minimum] = temp;

                partition = minimum;
            }

            return first;
        }

        public ServiceIssue peek()
        {
            if(isEmpty())
            {
                return null;
            }

            return tuples[0].Item2;
        }

        public List<ServiceIssue> convertToList()
        {
            List<ServiceIssue> results = new List<ServiceIssue>();

            foreach(var elem in tuples)
            {
                results.Add(elem.Item2);
            }

            return results;
        }
    }
}
