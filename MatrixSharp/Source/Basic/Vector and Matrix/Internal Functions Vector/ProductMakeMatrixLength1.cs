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
        /// 列ベクトルと行ベクトルの積
        /// </summary>
        /// <param name="left">左側のベクトル</param>
        /// <param name="right">右側のベクトル</param>
        /// <returns>列ベクトルと行ベクトルの積 double[,]</returns>
        internal static unsafe double[,] ProductMakeMatrixLength1(in double[] left, in double[] right)
        {
            double[,] calculated = new double[left.Length, right.Length];
            fixed (double* pcalculated = calculated, pleft = left, pright = right)
            {
                int count = 0;
                for (double* pc = pcalculated, endpc = pcalculated + calculated.Length; pc != endpc; ++pc)
                {
                    *pc = *(pleft + count / right.Length) * *(pright + count % right.Length);
                    ++count;
                }
            }
            return calculated;
        }
    }
}
