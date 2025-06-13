using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    /// <summary>
    /// 检查两棵二叉树是否相同，要求两个树在结构上相同，并且节点具有相同的值，则认为它们是相同的
    /// 
    /// 这两种方法都能有效判断两棵二叉树是否相同，递归方法代码更简洁，而迭代方法避免了递归可能导致的栈溢出问题，适合深度较大的树
    /// </summary>
    internal class SameTreeNode
    {
        /// <summary>
        /// 使用递归
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            // 两个节点都为null，认为相同
            if (p == null && q == null) return true;
            // 一个为null，一个不为null，不相同
            if (p == null || q == null) return false;
            // 值不相同
            if (p.Value != q.Value) return false;
            // 递归检查左右子树
            return IsSameTree(p.Left, q.Left) && IsSameTree(p.Right, q.Right);
        }

        /// <summary>
        /// 使用队列
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsSameTreeWithQueue(TreeNode p, TreeNode q)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(p);
            queue.Enqueue(q);
            while (queue.Count > 0)
            {
                TreeNode node1 = queue.Dequeue();
                TreeNode node2 = queue.Dequeue();

                // 两个节点都为null，继续检查
                if (node1 == null && node2 == null) continue;

                // 一个为null，一个不为null
                if (node1 == null || node2 == null) return false;

                // 值不相同
                if (node1.Value != node2.Value) return false;

                // 将左右子节点加入队列
                queue.Enqueue(node1.Left);
                queue.Enqueue(node2.Left);
                queue.Enqueue(node1.Right);
                queue.Enqueue(node2.Right);
            }
            return true;
        }

        public void TestMethod()
        {
            // 示例1: 相同的树
            TreeNode p1 = new TreeNode(1, new TreeNode(2), new TreeNode(3));
            TreeNode q1 = new TreeNode(1, new TreeNode(2), new TreeNode(3));
            Console.WriteLine(IsSameTree(p1, q1)); // 输出: True

            // 示例2: 不同的树
            TreeNode p2 = new TreeNode(1, new TreeNode(2), null);
            TreeNode q2 = new TreeNode(1, null, new TreeNode(2));
            Console.WriteLine(IsSameTree(p2, q2)); // 输出: False

            // 示例3: 都为空树
            Console.WriteLine(IsSameTree(null, null)); // 输出: True
        }
    }
}