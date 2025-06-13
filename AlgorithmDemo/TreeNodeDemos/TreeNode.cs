using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmDemo.TreeNodeDemos
{
    internal class TreeNode
    {
        public int Value { get; set; }

        public TreeNode Right { get; set; }

        public TreeNode Left { get; set; }

        public TreeNode(int value = 0, TreeNode left = null, TreeNode right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }
}
