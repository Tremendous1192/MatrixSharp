using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// 行列の和の内部関数
        /// </summary>
        /// <remarks>
        /// 行列の要素数が3の倍数の場合、for文を3ずつ進めることで時短できる
        /// </remarks>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        internal static double[,] AddLength3(in double[,] left, in double[,] right)
        {
            double[,] result = new double[left.GetLength(0), left.GetLength(1)];
            fixed (double* pleft = left, pright = right, presult = result)
            {
                for (double* pl = pleft, endpl = pleft + left.Length,
                    pr = pright, pre = presult;
                    pl != endpl;
                    pl += 3, pr += 3, pre += 3)
                {
                    *pre = *pl + *pr;
                    *(pre + 1) = *(pl + 1) + *(pr + 1);
                    *(pre + 2) = *(pl + 2) + *(pr + 2);
                }
            }
            return result;
        }
    }
}
