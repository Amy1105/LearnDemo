using AlgorithmDemo.TreeNodeDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo
{
    internal class Helper
    {
        public static void PrintTree(TreeNode root)
        {
            // 实现树的打印逻辑（中序遍历）
            if (root == null)
            {
                Console.WriteLine("null");
                return;
            }
            Console.WriteLine(root.Value);
            if (root.Left != null || root.Right != null)
            {
                Console.WriteLine("Left child of " + root.Value + ":");
                PrintTree(root.Left);
                Console.WriteLine("Right child of " + root.Value + ":");
                PrintTree(root.Right);
            }
        }
    }
}
