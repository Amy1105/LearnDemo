namespace AlgorithmDemo
{
    public class StackAndQueue
    {
        /// <summary>
        /// 使用两个栈（Stack1和Stack2）可以实现一个队列
        /// 
        /// 基本思路：
        /// 
        /// 入队操作：直接将元素压入Stack1
        /// 
        /// 出队操作：如果Stack2为空，则将Stack1中的所有元素弹出并压入Stack2，然后从Stack2弹出元素；
        /// 如果Stack2不为空，则直接从Stack2弹出元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class QueueWithTwoStacks<T>
        {
            private Stack<T> stack1 = new Stack<T>(); // 用于入队
            private Stack<T> stack2 = new Stack<T>(); // 用于出队

            // 入队操作
            public void Enqueue(T item)
            {
                stack1.Push(item);
            }

            // 出队操作
            public T Dequeue()
            {
                if (stack2.Count == 0)
                {
                    if (stack1.Count == 0)
                    {
                        throw new InvalidOperationException("Queue is empty");
                    }

                    // 将stack1的所有元素转移到stack2
                    while (stack1.Count > 0)
                    {
                        stack2.Push(stack1.Pop());
                    }
                }

                return stack2.Pop();
            }

            // 查看队首元素
            public T Peek()
            {
                if (stack2.Count == 0)
                {
                    if (stack1.Count == 0)
                    {
                        throw new InvalidOperationException("Queue is empty");
                    }

                    while (stack1.Count > 0)
                    {
                        stack2.Push(stack1.Pop());
                    }
                }

                return stack2.Peek();
            }

            // 判断队列是否为空
            public bool IsEmpty()
            {
                return stack1.Count == 0 && stack2.Count == 0;
            }

            // 获取队列元素数量
            public int Count()
            {
                return stack1.Count + stack2.Count;
            }
        }


        /// <summary>
        /// 用两个队列实现一个栈，并分析相关栈操作的运行时间
        /// 
        /// 基本思路：
        /// 
        /// 入栈操作：将新元素加入非空队列（如果都为空，任选一个）
        /// 
        /// 出栈操作：将非空队列中的元素（除最后一个）全部转移到另一个队列，然后弹出最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class StackWithTwoQueues<T>
        {
            private Queue<T> queue1 = new Queue<T>();
            private Queue<T> queue2 = new Queue<T>();

            // 入栈操作
            public void Push(T item)
            {
                // 总是将新元素加入非空队列
                if (queue1.Count > 0)
                {
                    queue1.Enqueue(item);
                }
                else
                {
                    queue2.Enqueue(item);
                }
            }

            // 出栈操作
            public T Pop()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty");
                }

                Queue<T> nonEmptyQueue = queue1.Count > 0 ? queue1 : queue2;
                Queue<T> emptyQueue = queue1.Count == 0 ? queue1 : queue2;

                // 将非空队列中除最后一个元素外的所有元素转移到空队列
                while (nonEmptyQueue.Count > 1)
                {
                    emptyQueue.Enqueue(nonEmptyQueue.Dequeue());
                }

                // 返回并移除最后一个元素（即栈顶元素）
                return nonEmptyQueue.Dequeue();
            }

            // 查看栈顶元素
            public T Peek()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty");
                }

                Queue<T> nonEmptyQueue = queue1.Count > 0 ? queue1 : queue2;
                Queue<T> emptyQueue = queue1.Count == 0 ? queue1 : queue2;

                // 将非空队列中除最后一个元素外的所有元素转移到空队列
                while (nonEmptyQueue.Count > 1)
                {
                    emptyQueue.Enqueue(nonEmptyQueue.Dequeue());
                }

                // 获取最后一个元素（但不移除）
                T top = nonEmptyQueue.Peek();
                emptyQueue.Enqueue(nonEmptyQueue.Dequeue());

                return top;
            }

            // 判断栈是否为空
            public bool IsEmpty()
            {
                return queue1.Count == 0 && queue2.Count == 0;
            }

            // 获取栈中元素数量
            public int Count()
            {
                return queue1.Count + queue2.Count;
            }
        }

        /// <summary>
        /// 通过维护一个变量来记录栈顶元素，优化Peek操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class OptimizedStackWithTwoQueues<T>
        {
            private Queue<T> queue1 = new Queue<T>();
            private Queue<T> queue2 = new Queue<T>();
            private T top;

            // 入栈操作
            public void Push(T item)
            {
                if (queue1.Count > 0)
                {
                    queue1.Enqueue(item);
                }
                else
                {
                    queue2.Enqueue(item);
                }
                top = item;
            }

            // 出栈操作
            public T Pop()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty");
                }

                Queue<T> nonEmptyQueue = queue1.Count > 0 ? queue1 : queue2;
                Queue<T> emptyQueue = queue1.Count == 0 ? queue1 : queue2;

                // 将非空队列中除最后一个元素外的所有元素转移到空队列
                while (nonEmptyQueue.Count > 1)
                {
                    top = nonEmptyQueue.Dequeue();
                    emptyQueue.Enqueue(top);
                }

                return nonEmptyQueue.Dequeue();
            }

            // 查看栈顶元素（优化后）
            public T Peek()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty");
                }
                return top;
            }

            public bool IsEmpty()
            {
                return queue1.Count == 0 && queue2.Count == 0;
            }

            public int Count()
            {
                return queue1.Count + queue2.Count;
            }
        }

    }
}
