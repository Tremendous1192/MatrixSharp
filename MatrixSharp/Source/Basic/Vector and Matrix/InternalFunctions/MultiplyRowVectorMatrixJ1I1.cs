using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class RowVector
    {
        /// <summary>
        /// 行ベクトルと行列の積
        /// </summary>
        /// <param name="left">左側の行ベクトル</param>
        /// <param name="right">右側の行列</param>
        /// <returns></returns>
        internal static double[] MultiplyRowVectorMatrixJ1I1(in double[] left, in double[,] right)
        {
            double[,] rightT = Matrix.TransposeLength1(right);
            double[] calculated = new double[right.GetLength(1)];
            fixed (double* pcalculated = calculated, pleft = left, prightT = rightT)
            {
                int count = 0;
                for (double* pcal = pcalculated, endP = pcalculated + calculated.Length; pcal != endP; ++pcal)
                {
                    for (double* pl = pleft, endpl = pleft + left.Length,
                        prT = prightT + count * right.GetLength(0); pl != endpl; ++pl, ++prT)
                    {
                        *pcal += *pl * *prT;
                    }
                    ++count;
                }
            }
            return calculated;
        }
    }
}
