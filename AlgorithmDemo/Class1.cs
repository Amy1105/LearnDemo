using System.Collections;

namespace AlgorithmDemo
{
    /// <summary>
    /// 70-爬楼梯；以及斐波那契数列
    /// </summary>
    public class Class1
    {
        private Dictionary<int, int> storeMap = new Dictionary<int, int>();
        public int climbStairs(int n)
        {
            if(n<=0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;
            if (storeMap.ContainsKey(n))
            {
                return storeMap[n];
            }
            else
            {
                int result = climbStairs(n - 1) + climbStairs(n - 2);
                storeMap.Add(n, result);
                return result;
            }
        }

        /// <summary>
        /// fn=前两个之和
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int climbStairs2(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;
            int result = 0;
            int pre = 2;
            int prePre = 1;
            for (int i = 3; i <= n; i++)
            {
                result = pre + prePre;
                prePre = pre;
                pre = result;
            }
            return result;
        }

        /// <summary>
        /// 两数之和  nums = [2,7,11,15], target = 9
        /// 0,4,3,0
        /// </summary>
        public int[]? One(int[] nums, int target)
        {
            //暴力穷举，遍历两遍  O(n^2)


            //考虑：

            //利用hashmap，避免第二次扫描 O(n)                   
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            int index = 0;
            foreach (int i in nums)
            {
                int item = target - i;
                if (item >= 0)
                {
                    if (!keyValuePairs.ContainsKey(item))
                    {
                        keyValuePairs.Add(i, index);
                    }
                    else
                    {
                        return new int[] { index, keyValuePairs[item] };
                    }
                    index++;
                }
            }
            return null;
        }

        /// <summary>
        /// 三数之和  nums = [2,7,11,15], target = 9
        /// 0,4,3,0
        /// </summary>
        public int[]? OneExtension(int[] nums, int target)
        {
            //利用hashmap，避免第二次扫描 O(n)                   
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length - 1; i++)
            {
                keyValuePairs.Add(i, nums[i]);
            }

            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length - 1; j++)
                {
                    var temp = target - nums[i] - nums[j];
                    if (keyValuePairs.ContainsValue(temp))
                    {
                        var key = keyValuePairs.Where(x => x.Value == temp).First();
                        return new int[] { i, j, key.Key };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 88 合并两个数组，升序排列
        /// </summary>
        public void eightyeight()
        {
            //int[] nums1,int m,int[] nums2,int n
            int[] nums1 = { 1, 2, 3};
            int n = 3;
            int[] nums2 = { 2, 5, 6 };
            int m = 3;
            int k = m + n;
            int[] temp=new int[k];
            for(int index = 0, numsIndex = 0, nums2Index = 0; index < k; index++)
            {
                if (numsIndex >= m)
                {
                    temp[index] = nums2[nums2Index++];
                }
                else if (nums2Index >= n)
                {
                    temp[index] = nums1[numsIndex++];
                }
                else if (nums1[numsIndex] < nums2[nums2Index])
                {
                    temp[index] = nums1[numsIndex++];
                }
                else
                {
                    temp[index] = nums2[nums2Index++];
                }
            }
            for(int i = 0; i < k; i++)
            {
                nums1[i] = temp[i];
            }
        }
    }
}
