using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    /// <summary>
    /// 判断是否是对称二叉树，从root看，左边和右边结构一致，节点值一致
    /// 
    /// 思路：
    /// 根节点的左右子树必须互为镜像;
    /// 左子树的左节点必须等于右子树的右节点;
    /// 左子树的右节点必须等于右子树的左节点
    /// 
    /// </summary>
    internal class SymmetricTreeNode
    {
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null) return true;
            return IsMirror(root.Left, root.Right);
        }

        private bool IsMirror(TreeNode left, TreeNode right)
        {
            // 两个节点都为null，对称
            if (left == null && right == null) return true;

            // 一个为null，一个不为null，不对称
            if (left == null || right == null) return false;

            // 值不相同，不对称
            if (left.Value != right.Value) return false;

            // 递归比较左子树的左节点和右子树的右节点，
            // 以及左子树的右节点和右子树的左节点
            return IsMirror(left.Left, right.Right) && IsMirror(left.Right, right.Left);
        }



        public bool IsSymmetricWithQueue(TreeNode root)
        {
            if (root == null) return true;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root.Left);
            queue.Enqueue(root.Right);

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

                // 将需要比较的节点对加入队列
                queue.Enqueue(node1.Left);
                queue.Enqueue(node2.Right);
                queue.Enqueue(node1.Right);
                queue.Enqueue(node2.Left);
            }

            return true;
        }
    }
}
