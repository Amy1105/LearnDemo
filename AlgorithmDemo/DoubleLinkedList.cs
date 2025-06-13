using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlgorithmDemo;

/// <summary>
/// 双向链表
/// </summary>
public class DoubleLinkedList
{

    /// <summary>
    /// 哨兵节点:1.虚拟的边界标记;2.永久的占位符;3.简化算法的守卫;
    /// 
    /// 哨兵节点的实际应用
    /// Linux内核链表 - 广泛使用哨兵节点设计
    /// Redis的链表实现 - 使用双向链表+哨兵
    /// Java的LinkedList - 虽然没有显式哨兵，但有类似的头尾指针概念
    /// 哨兵节点虽然会占用少量额外内存，但带来的代码简洁性和可靠性提升在大多数情况下是值得的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedListWithSentinel<T>
    {


        private Node sentinel;

        public DoublyLinkedListWithSentinel()
        {
            // 创建哨兵节点（数据为default(T)）
            sentinel = new Node(default(T));
            sentinel.Next = sentinel;  // 指向自己
            sentinel.Prev = sentinel;   // 指向自己
        }

        // 在链表头部插入
        public void InsertAtHead(T data)
        {
            Node newNode = new Node(data);
            newNode.Next = sentinel.Next;
            newNode.Prev = sentinel;
            sentinel.Next.Prev = newNode;
            sentinel.Next = newNode;
        }

        // 在链表尾部插入
        public void InsertAtTail(T data)
        {
            Node newNode = new Node(data);
            newNode.Next = sentinel;
            newNode.Prev = sentinel.Prev;
            sentinel.Prev.Next = newNode;
            sentinel.Prev = newNode;
        }

        // 删除指定节点
        public void Delete(Node node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }

        // 查找节点
        public Node Search(T data)
        {
            Node current = sentinel.Next;
            while (current != sentinel)
            {
                if (current.Data.Equals(data))
                    return current;
                current = current.Next;
            }
            return null;
        }
    }


    private class Node
    {
        public T Data;
        public Node Prev;
        public Node Next;

        public Node(T data)
        {
            Data = data;
        }
    }
}

