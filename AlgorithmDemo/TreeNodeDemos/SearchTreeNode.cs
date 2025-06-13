using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    /// <summary>
    /// 二叉搜索树：
    /// 二叉搜索树的特点是左子树所有节点值小于根节点，右子树所有节点值大于根节点，且高度平衡意味着两个子树的高度差不超过1
    /// 
    /// 二叉搜索树的中序遍历是升序序列，题目给定的数组是按照升序排序的有序数组，因此可以确保数组是二叉搜索树的中序遍历序列。
    /// </summary>
    internal class SearchTreeNode
    {
        public TreeNode SortedArrayToBST(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }
            return BuildBST(nums, 0, nums.Length - 1);
        }

        private TreeNode BuildBST(int[] nums, int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            // 选择中间位置作为根节点
            int mid = left + (right - left) / 2;
            TreeNode root = new TreeNode(nums[mid]);
            // 递归构建左右子树
            root.Left = BuildBST(nums, left, mid - 1);
            root.Right = BuildBST(nums, mid + 1, right);
            return root;
        }

        public TreeNode SortedArrayToBSTWithStack(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }
            Stack<(TreeNode, int, int)> stack = new Stack<(TreeNode, int, int)>();
            TreeNode dummy = new TreeNode();
            stack.Push((dummy, 0, nums.Length - 1));
            while (stack.Count > 0)
            {
                var (parent, left, right) = stack.Pop();

                if (left > right)
                {
                    continue;
                }

                int mid = left + (right - left) / 2;
                TreeNode node = new TreeNode(nums[mid]);

                // 确定是左孩子还是右孩子
                if (parent.Value == 0)
                { // dummy节点
                    dummy.Left = node;
                }
                else if (node.Value < parent.Value)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
                // 先压入右子树，后压入左子树（栈是LIFO）
                stack.Push((node, mid + 1, right));
                stack.Push((node, left, mid - 1));
            }
            return dummy.Left;
        }

        public void TestMethod()
        {
            // 示例1: 奇数个元素
            int[] nums1 = { -10, -3, 0, 5, 9 };
            TreeNode result1 = SortedArrayToBST(nums1);
            Helper.PrintTree(result1); // 可能的输出结构:
                                       //       0
                                       //      / \
                                       //    -3   9
                                       //    /   /
                                       // -10   5

            // 示例2: 偶数个元素
            int[] nums2 = { 1, 3 };
            TreeNode result2 = SortedArrayToBST(nums2);
            Helper.PrintTree(result2); // 可能的输出结构:
                                       //     3
                                       //    /
                                       //   1

            // 示例3: 空数组
            TreeNode result3 = SortedArrayToBST(new int[0]);
            Helper.PrintTree(result3); // 输出: null
        }
    }
}
