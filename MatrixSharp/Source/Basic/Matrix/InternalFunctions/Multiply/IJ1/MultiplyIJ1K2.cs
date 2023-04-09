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
        /// 行列積
        /// </summary>
        /// <remarks>
        /// 左側の行列の列数が偶数の場合、for文を2ずつ進めることで時短できる
        /// </remarks>
        /// <param name="left">左側の積</param>
        /// <param name="rightT">右側の積</param>
        /// <returns></returns>
        internal static double[,] MultiplyIJ1K2(in double[,] left, in double[,] rightT)
        {
            double[,] calculated = new double[left.GetLength(0), rightT.GetLength(0)];

            fixed (double* pcalculated = calculated, pleft = left, prightT = rightT)
            {
                int count = 0;
                for (double* pcal = pcalculated, endP = pcalculated + calculated.Length; pcal != endP; ++pcal, ++count)
                {
                    double* plfix = &pleft[count / calculated.GetLength(1) * left.GetLength(1)];
                    double* prfix = &prightT[count % calculated.GetLength(1) * rightT.GetLength(1)];
                    for (double* pl = plfix, pr = prfix, endP2 = plfix + left.GetLength(1); pl != endP2;
                        pl += 2, pr += 2)
                    {
                        *pcal += *pl * *pr + *(pl + 1) * *(pr + 1);
                    }
                }
            }

            return calculated;
        }
    }
}
