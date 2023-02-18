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
        /// 行列の差の内部関数
        /// </summary>
        /// <remarks>
        /// 行列の要素数が4の倍数の場合、for文を4ずつ進めることで時短できる
        /// </remarks>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        internal static double[,] SubtractLength4(in double[,] left, in double[,] right)
        {
            double[,] result = new double[left.GetLength(0), left.GetLength(1)];
            fixed (double* pleft = left, pright = right, presult = result)
            {
                for (double* pl = pleft, endpl = pleft + left.Length,
                    pr = pright, pre = presult;
                    pl != endpl;
                    pl += 4, pr += 4, pre += 4)
                {
                    *pre = *pl - *pr;
                    *(pre + 1) = *(pl + 1) - *(pr + 1);
                    *(pre + 2) = *(pl + 2) - *(pr + 2);
                    *(pre + 3) = *(pl + 3) - *(pr + 3);
                }
            }
            return result;
        }
    }
}
