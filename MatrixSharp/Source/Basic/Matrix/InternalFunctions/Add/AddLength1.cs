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
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        internal static double[,] AddLength1(in double[,] left, in double[,] right)
        {
            double[,] result = new double[left.GetLength(0), left.GetLength(1)];
            fixed (double* pleft = left, pright = right, presult = result)
            {
                for (double* pl = pleft, endpl = pleft + left.Length,
                    pr = pright, pre = presult;
                    pl != endpl;
                    ++pl, ++pr, ++pre)
                {
                    *pre = *pl + *pr;
                }
            }
            return result;
        }
    }
}
