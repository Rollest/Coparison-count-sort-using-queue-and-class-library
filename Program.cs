using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Queueueue
{
    public class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Queue queue = new Queue();
            Queue queue_count = new Queue();
            
            
            for (int i = 100; i > 0; i--)
            {
                queue.Add(random.Next(10));
                //queue.Add(i);
            }
            //queue.Add(5);
            for (int i = 0; i < queue.Count(); i++)
            {
                queue_count.Add(0);
            }
            Console.WriteLine($"длина списка: {queue.Count()}");
            queue.Print();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            long count = 0;

            count += 1; // 2 + 
            for (int i = 0; i < queue.Count() - 1; i++) // 2*count**3 + 6*count**2 - 42*count + 34
            {
                int temp = queue.Peek(); // +2
                queue.Add(queue.Pop()); // +2

                count += 4;
                count += 1;
                for (int j = 0; j < queue.Count() - i - 1; j++) // 1 + (count-((count+1)/2)-1)*(4*count + 26)
                {
                    if(queue.Peek() < temp || queue.Peek() == temp) // 4*count + 26
                    {

                        queue_count.Set(i + 1, queue_count.Get(i) + 1); // 4*count + 17
                        count += 4 * queue.Count() + 17;

                    }
                    else
                    {

                        queue_count.Set(i + j + 1 + 1, queue_count.Get(i + j + 1) + 1); // 4*count + 21
                        count += 4 * queue.Count() + 21;
                    }
                    queue.Add(queue.Pop()); // + 2
                    count += 2;
                }
                count += 1;
                for (int j = 0; j < i + 1; j++) // 1 + (((count+1)/2)+1)*2
                {
                    queue.Add(queue.Pop()); // + 2
                    count += 2;
                }
            }

            queue.Add(queue.Pop());
            //queue.Print();
            count += 2;

            queue.Get_And_Set(queue_count);// 10 + count + count(10*count + 8)
            count += 10 + queue.Count() + queue.Count()*(10 * queue.Count() + 8);
            stopWatch.Stop();
            queue.Print();
            Console.WriteLine(count);
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }

    class Queue
    {
        private Queue<int> queue;

        public int Count() // +1
        {
            return queue.Count;
        }

        public int Peek() // +1
        {
            return (int)queue.Peek();
        }

        public int Pop() // +1
        {
            return queue.Dequeue();
        }

        public void Add(int value) // +1
        {
            queue.Enqueue(value);
        }

        public int Get(int pos) // 2 * count + 8
        {
            for (int i = 0; i < pos; i++) // pos*2 + 2
            {
                Add(Pop()); // +2
            }

            int res = queue.Peek(); // +2

            for (int i = pos; i < queue.Count; i++) // (count - pos)*2 + 2
            {
                Add(Pop()); // +2
            }

            return res;
        }

        public void Set(int pos, int value) // 2*(count - 1) + 9
        {
            for (int i = 0; i < pos - 1; i++) // 1 + (pos-1)*2 + 3
            {
                Add(Pop()); // +2
            }

            Pop(); // +1
            Add(value); // +1

            for (int i = pos; i < queue.Count; i++) // 1 + (count - pos)*2 + 2
            {
                Add(Pop()); // +2
            }
        }

        public void Get_And_Set(Queue array) // 10 + count + count(10*count + 8)
        {

            Queue queue_end = new Queue(); // +1
            for (int i = 0; i < queue.Count(); i++) // 1 + count + 2
            {
                queue_end.Add(0); // +1
            }
            for (int i = 0; i < queue.Count(); i++) // 1 + count * (4*count + 3) + 2
            {
                queue_end.Set(i + 1, Get(i)); //1 + 2*(count - 1) + 2 + 2 * count + 2 + 1
            }
            //for (int i = 0; i < queue.Count(); i++)
            //{
            //    Console.Write(queue_end.Get(i));
            //    Console.WriteLine();
            //}
            for (int i = 0; i < queue.Count(); i++) // 1 + count*(6*count + 5) + 2
            {
                Set(array.Get(i) + 1, queue_end.Get(i)); // 6 * count + 5
            }
        }

        public void Print()
        {
            foreach (var item in queue)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            Console.WriteLine("--------------");
        }

        public Queue()
        {
            queue = new Queue<int>();
        }

    }
}