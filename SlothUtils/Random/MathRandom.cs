using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothUtils
{
    class MathRandom
    {
    }
    /// <summary>
    /// 正态分布生成类
    /// </summary>
    public class N
    {
        Random rand = new Random();

        //标准正态分布
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Normal()
        {
            double s = 0, u = 0, v = 0;
            while (s > 1 || s == 0)
            {
                u = rand.NextDouble() * 2 - 1;
                v = rand.NextDouble() * 2 - 1;

                s = u * u + v * v;
            }

            var z = Math.Sqrt(-2 * Math.Log(s) / s) * u;
            return (z);
        }


        /// <summary>
        ///  符合要求的正态分布随机数
        /// </summary>
        /// <param name="miu"></param>
        /// <param name="sigma"></param>
        /// <returns></returns>
        public double RandomNormal(double miu = 0, double sigma= 10)
        {
            
            var z = Normal() * sigma + miu;
            return (z);
        }

        /// <summary>
        /// 获取示例
        /// </summary>
        /// <returns></returns>
        public string GetDemo()
        {
            string st = @"
            N n = new N();

            for (int i = 0; i < 9000; i++)
            {
                var z = n.RandomNormal(0, 10);
                Console.WriteLine(z);
            }

            ";
            return st;
        }
    }
}
