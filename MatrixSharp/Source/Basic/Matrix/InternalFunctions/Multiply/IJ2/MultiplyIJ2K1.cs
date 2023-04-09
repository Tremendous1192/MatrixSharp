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
        /// 左側の行列の行数と右側の行列の列数の積が偶数の場合、for文を2ずつ進めることで時短できる
        /// </remarks>
        /// <param name="left">左側の積</param>
        /// <param name="rightT">右側の積</param>
        /// <returns></returns>
        internal static double[,] MultiplyIJ2K1(in double[,] left, in double[,] rightT)
        {
            double[,] calculated = new double[left.GetLength(0), rightT.GetLength(0)];

            fixed (double* pcalculated = calculated, pleft = left, prightT = rightT)
            {
                int count = 0;
                for (double* pcal = pcalculated, endP = pcalculated + calculated.Length; pcal != endP;
                    pcal += 2, count += 2)
                {
                    double* plfix0 = &pleft[count / calculated.GetLength(1) * left.GetLength(1)];
                    double* prfix0 = &prightT[count % calculated.GetLength(1) * rightT.GetLength(1)];

                    double* plfix1 = &pleft[(count + 1) / calculated.GetLength(1) * left.GetLength(1)];
                    double* prfix1 = &prightT[(count + 1) % calculated.GetLength(1) * rightT.GetLength(1)];
                    for (double* pl0 = plfix0, pr0 = prfix0, endP2 = plfix0 + left.GetLength(1),
                        pl1 = plfix1, pr1 = prfix1;
                        pl0 != endP2;
                        ++pl0, ++pr0, ++pl1, ++pr1)
                    {
                        *pcal += *pl0 * *pr0;
                        *(pcal + 1) += *pl1 * *pr1;
                    }
                }
            }

            return calculated;
        }
    }
}
