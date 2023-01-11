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
        /// ベクトルの内積
        /// </summary>
        /// <param name="left">左側のベクトル</param>
        /// <param name="right">右側のベクトル</param>
        /// <returns>double</returns>
        internal static double DotProductLength1(in double[] left, in double[] right)
        {
            double calculated = 0;
            fixed (double* pleft = left, pright = right)
            {
                for (double* pl = pleft, pr = pright, endP = pleft + left.Length; pl != endP;
                    ++pl, ++pr)
                { calculated += *pl * *pr; }
            }
            return calculated;
        }
    }
}
