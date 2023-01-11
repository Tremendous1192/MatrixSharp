using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    internal static unsafe partial class Vector
    {
        /// <summary>
        /// ベクトルの和
        /// </summary>
        /// <param name="left">左側のベクトル</param>
        /// <param name="right">右側のベクトル</param>
        /// <returns>ベクトルの和 double[]</returns>
        internal static double[] AddLength1(in double[] left, in double[] right)
        {
            double[] calculated = new double[left.Length];
            fixed (double* pcalculated = calculated, pleft = left, pright = right)
            {
                for (double* pa = pcalculated, pl = pleft, pr = pright, endP = pcalculated + calculated.Length; pa != endP; ++pa, ++pl, ++pr)
                { *pa = *pl + *pr; }
            }
            return calculated;
        }
    }
}
