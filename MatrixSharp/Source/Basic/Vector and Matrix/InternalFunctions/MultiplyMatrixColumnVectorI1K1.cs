using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class ColumnVector
    {
        /// <summary>
        /// 行列と列ベクトルの積
        /// </summary>
        /// <param name="left">左側の積</param>
        /// <param name="right">右側のベクトル</param>
        /// <returns></returns>
        internal static double[] MultiplyMatrixColumnVectorI1K1(in double[,] left, in double[] right)
        {
            double[] calculated = new double[left.GetLength(0)];
            fixed (double* pcalculated = calculated, pleft = left, pright = right)
            {
                int count = 0;
                for (double* pcal = pcalculated, endP = pcalculated + calculated.Length; pcal != endP; ++pcal)
                {
                    fixed (double* plfix = &left[count, 0])
                    {
                        for (double* pl = plfix, pr = pright, endP2 = pright + right.Length; pr != endP2; ++pl, ++pr)
                        { *pcal += *pl * *pr; }
                    }
                    ++count;
                }
            }
            return calculated;
        }
    }
}
