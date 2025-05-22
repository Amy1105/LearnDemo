using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    /// <summary>
    /// 判断平衡二叉树
    /// 
    /// 平衡二叉树是指任意节点的左右子树高度差不超过1的二叉树
    /// 
    /// </summary>
    internal class BalanceTreeNode
    {
        public bool IsBalanced(TreeNode root)
        {
            if (root == null) return true;

            // 检查当前节点是否平衡
            int LeftHeight = GetHeight(root.Left);
            int RightHeight = GetHeight(root.Right);
            if (Math.Abs(LeftHeight - RightHeight) > 1) return false;

            // 递归检查左右子树
            return IsBalanced(root.Left) && IsBalanced(root.Right);
        }

        private int GetHeight(TreeNode node)
        {
            if (node == null) return 0;
            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        public bool IsBalanced2(TreeNode root)
        {
            return CheckHeight(root) != -1;
        }

        private int CheckHeight(TreeNode node)
        {
            if (node == null) return 0;

            int LeftHeight = CheckHeight(node.Left);
            if (LeftHeight == -1) return -1;

            int RightHeight = CheckHeight(node.Right);
            if (RightHeight == -1) return -1;

            if (Math.Abs(LeftHeight - RightHeight) > 1) return -1;

            return Math.Max(LeftHeight, RightHeight) + 1;
        }
    }
}
