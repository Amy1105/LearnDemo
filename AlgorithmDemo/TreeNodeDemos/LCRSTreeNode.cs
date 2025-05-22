using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    /// <summary>
    /// 左孩子右兄弟表示法（Left-Child Right-Sibling Representation，简称LCRS）是一种将普通树（多叉树）转换为二叉树表示的方法，也称为"孩子兄弟表示法"
    /// 
    /// 基本概念
    /// 左指针（Left Child）：指向节点的第一个孩子（最左边的孩子）
    /// 右指针（Right Sibling）：指向节点的下一个兄弟（右侧的兄弟）
    /// 
    /// 任何多叉树都可以用二叉树的结构来表示
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LCRSTreeNode<T>
    {
        public T Data { get; set; }
        public LCRSTreeNode<T> LeftChild { get; set; }  // 第一个孩子
        public LCRSTreeNode<T> RightSibling { get; set; } // 下一个兄弟

        public LCRSTreeNode(T data)
        {
            Data = data;
        }

        // 添加孩子节点
        public void AddChild(LCRSTreeNode<T> child)
        {
            if (LeftChild == null)
            {
                LeftChild = child;
            }
            else
            {
                LCRSTreeNode<T> temp = LeftChild;
                while (temp.RightSibling != null)
                {
                    temp = temp.RightSibling;
                }
                temp.RightSibling = child;
            }
        }

        // 遍历孩子节点
        public IEnumerable<LCRSTreeNode<T>> GetChildren()
        {
            LCRSTreeNode<T> child = LeftChild;
            while (child != null)
            {
                yield return child;
                child = child.RightSibling;
            }
        }
    }
}
