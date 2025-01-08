namespace AlgorithmDemo
{
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


    }
}
