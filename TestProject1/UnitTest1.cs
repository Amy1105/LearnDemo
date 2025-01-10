using doNetLearn;
using EnumsNET;
using Org.BouncyCastle.Asn1.X509;
using System.Collections;
using System.Diagnostics;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestFixture]
        public class ProductFilterTests
        {
            [Test]
            public void CategoryFilter_ShouldReturnOnlyMatchingProducts()
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Category = "Electronics", Price = 1000 },
                    new Product { Name = "Book", Category = "Books", Price = 20 }
                };
                var categoryFilter = new CategoryFilter("Electronics");
                var filteredProducts = categoryFilter.Filter(products);
                Assert.AreEqual(1, filteredProducts.Count());
                Assert.AreEqual("Laptop", filteredProducts.First().Name);
            }

            [Test]
            public void PriceFilter_ShouldReturnProductsWithinPriceRange()
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Category = "Electronics", Price = 1000 },
                    new Product { Name = "Book", Category = "Books", Price = 20 }
                };
                var priceFilter = new PriceFilter(50, 2000);
                var filteredProducts = priceFilter.Filter(products);
                Assert.AreEqual(1, filteredProducts.Count());
                Assert.AreEqual("Laptop", filteredProducts.First().Name);
            }


            [Test]
            public void check()
            {
                //3,2,4
                int[] nums = [1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1];
                int target = 11;
                Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
                int index = 0;
                foreach (int i in nums)
                {
                    int item = target - i;
                    if (!keyValuePairs.ContainsValue(item))
                    {
                        keyValuePairs.Add(index, i);
                    }
                    else
                    {
                        int first = index;
                        var itemKey = keyValuePairs.Where(x => Equals(x.Value, item)).Select(pair => pair.Key).FirstOrDefault();
                        int second = itemKey;
                    }
                    index++;
                }
            }

            [Test]
            public void check2()
            {
                int[] ints = { 1, 2, 3, 0, 0, 0 };
                int n = 3;
                int[] ints2 = { 2, 5, 6 };
                int m = 3;

                foreach (int i in Enumerable.Range(0, n - 1))
                {
                    ints[m + i] = ints2[i];
                }
                ArrayList arrayList = new ArrayList();
                arrayList.Add(ints);
                arrayList.Sort();
                ints = arrayList.Cast<int>().ToArray();
            }

            [Test]
            public void check3()
            {
                //int[] nums1,int m,int[] nums2,int n
                int[] nums1 = { 1, 2, 3 };
                int n = 3;
                int[] nums2 = { 2, 5, 6 };
                int m = 3;

                int k = m + n;
                int[] temp = new int[k];
                for (int index = 0, numsIndex = 0, nums2Index = 0; index < k; index++)
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

                for (int i = 0; i < k; i++)
                {
                    nums1[i] = temp[i];
                }
            }


            [Test]
            public void check4()
            {
                //int[] nums1,int m,int[] nums2,int n
                int[] nums1 = { 1, 2, 3, 0, 0, 0 };
                int n = 3;
                int[] nums2 = { 2, 5, 6 };
                int m = 3;

                int k = m + n - 1;

                for (int index = k, numsIndex = m - 1,
                    nums2Index = n - 1; index >= 0; index--)
                {
                    if (numsIndex < 0)
                    {
                        nums1[index] = nums2[nums2Index--];
                    }
                    else if (nums2Index < 0)
                    {
                        break;
                    }
                    else if (nums1[numsIndex] < nums2[nums2Index])
                    {
                        nums1[index] = nums2[nums2Index--];
                    }
                    else
                    {
                        nums1[index] = nums1[numsIndex--];
                    }
                }

                foreach (int i in nums1)
                {
                    Console.WriteLine(i);
                }
            }

            [Test]
            public void OneExtension()
            {
                int[] nums = [4, 5, 2, 7, 22, 8, 11, 15]; int target = 21;
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
                            var first = i;
                            var second = j;
                            var third = key.Key;
                            if(first == second || second == third || first == third)
                            {
                                continue;
                            }
                            else
                            {
                                Console.Write(first+"-"+second+"-"+third);
                            }
                        }
                    }
                }
            }
        }
    }
}