using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    /// <summary>
    /// 研究二叉树
    /// </summary>
    internal class TreeNodeDemo
    {
        //二叉树的遍历方式：前序遍历，中序遍历，后序遍历，层次遍历（也叫广度优先遍历）


        /// <summary>
        /// 递归实现：前序遍历，访问顺序：根节点 → 左子树 → 右子树
        /// </summary>
        /// <param name="root"></param>
        public void PreOrder(TreeNode root)
        {
            if (root == null) return;

            Console.Write(root.Value + " "); // 访问根节点
            PreOrder(root.Left);             // 遍历左子树
            PreOrder(root.Right);            // 遍历右子树
        }

        /// <summary>
        /// 使用栈，实现二叉树的前序遍历
        /// 访问顺序：根节点 → 左子树 → 右子树
        /// </summary>
        /// <param name="root"></param>
        public void PreOrderIterative(TreeNode root)
        {
            if (root == null) return;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();  //栈，先进后出
                Console.Write(node.Value + " ");

                // 右孩子先入栈，左孩子后入栈
                if (node.Right != null) stack.Push(node.Right);
                if (node.Left != null) stack.Push(node.Left);
            }
        }

        /// <summary>
        /// 递归实现：中序遍历，访问顺序：左子树 → 根节点 → 右子树
        /// </summary>
        /// <param name="root"></param>
        public void InOrder(TreeNode root)
        {
            if (root == null) return;

            InOrder(root.Left);            // 遍历左子树
            Console.Write(root.Value + " "); // 访问根节点
            InOrder(root.Right);           // 遍历右子树
        }

        /// <summary>
        /// 栈，实现 中序遍历，访问顺序：左子树 → 根节点 → 右子树
        /// </summary>
        /// <param name="root"></param>
        public void InOrderIterative(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                Console.Write(current.Value + " ");
                current = current.Right;
            }
        }


        /// <summary>
        /// 递归实现：访问顺序：左子树 → 右子树 → 根节点
        /// </summary>
        /// <param name="root"></param>
        public void PostOrder(TreeNode root)
        {
            if (root == null) return;

            PostOrder(root.Left);          // 遍历左子树
            PostOrder(root.Right);         // 遍历右子树
            Console.Write(root.Value + " "); // 访问根节点
        }

        /// <summary>
        /// 栈实现：访问顺序：左子树 → 右子树 → 根节点
        /// </summary>
        /// <param name="root"></param>
        public void PostOrderIterative(TreeNode root)
        {
            if (root == null) return;

            Stack<TreeNode> stack1 = new Stack<TreeNode>();
            Stack<TreeNode> stack2 = new Stack<TreeNode>();
            stack1.Push(root);

            while (stack1.Count > 0)
            {
                TreeNode node = stack1.Pop();
                stack2.Push(node);

                if (node.Left != null) stack1.Push(node.Left);
                if (node.Right != null) stack1.Push(node.Right);
            }

            while (stack2.Count > 0)
            {
                Console.Write(stack2.Pop().Value + " ");
            }
        }


        public void LevelOrder(TreeNode root)
        {
            if (root == null) return;

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();//先进先出
                Console.Write(node.Value + " ");

                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
        }

    }
}
