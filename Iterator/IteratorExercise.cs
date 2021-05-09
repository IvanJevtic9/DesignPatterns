using System;
using System.Collections.Generic;
using System.Text;

namespace Iterator.Exercise
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                IEnumerable<T> TraversePreOrder(Node<T> current)
                {
                    yield return current.Value;
                    if (current.Left != null)
                    {
                        foreach (var left in TraversePreOrder(current.Left))
                        {
                            yield return left;
                        }
                    }
                    if (current.Right != null)
                    {
                        foreach (var right in TraversePreOrder(current.Right))
                        {
                            yield return right;
                        }
                    }
                }

                foreach (var nodeValue in TraversePreOrder(this))
                {
                    yield return nodeValue;
                }
            }
        }
    }
    public class IteratorExercise
    {
        public static void MainFunc(string[] args)
        {
            Node<int> node1 = new Node<int>(25);
            Node<int> node2 = new Node<int>(15);
            Node<int> node3 = new Node<int>(50);
            Node<int> node4 = new Node<int>(10);
            Node<int> node5 = new Node<int>(22);
            Node<int> node6 = new Node<int>(35);
            Node<int> node7 = new Node<int>(70);
            Node<int> node8 = new Node<int>(4);
            Node<int> node9 = new Node<int>(12);
            Node<int> node10 = new Node<int>(18);
            Node<int> node11 = new Node<int>(24);
            Node<int> node12 = new Node<int>(31);
            Node<int> node13 = new Node<int>(44);
            Node<int> node14 = new Node<int>(66);
            Node<int> node15 = new Node<int>(90);

            node1.Left = node2;
            node1.Right = node3;

            node2.Left = node4;
            node2.Right = node5;
            node2.Parent = node1;

            node3.Left = node6;
            node3.Right = node7;
            node3.Parent = node1;

            node4.Left = node8;
            node4.Right = node9;
            node4.Parent = node2;

            node5.Left = node10;
            node5.Right = node11;
            node5.Parent = node2;

            node6.Left = node12;
            node6.Right = node13;
            node6.Parent = node3;

            node7.Left = node14;
            node7.Right = node15;
            node7.Parent = node3;

            node8.Parent = node4;
            node9.Parent = node4;
            node10.Parent = node5;
            node11.Parent = node5;
            node12.Parent = node6;
            node13.Parent = node6;
            node14.Parent = node7;
            node15.Parent = node7;

            foreach(var v in node1.PreOrder)
            {
                Console.Write($"{v} ");
            }
        }
    }
}
