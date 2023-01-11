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
        /// <param name="left">左側の積</param>
        /// <param name="rightT">右側の積</param>
        /// <returns></returns>
        internal static double[,] MultiplyIJ1K1(in double[,] left, in double[,] rightT)
        {
            double[,] calculated = new double[left.GetLength(0), rightT.GetLength(0)];

            fixed (double* pcalculated = calculated, pleft = left, prightT = rightT)
            {
                int count = 0;
                for (double* pcal = pcalculated, endP = pcalculated + calculated.Length; pcal != endP; ++pcal)
                {
                    fixed (double* plfix = &left[count / calculated.GetLength(1), 0],
                        prfix = &rightT[count % calculated.GetLength(1), 0])
                    {
                        for (double* pl = plfix, pr = prfix, endP2 = plfix + left.GetLength(1); pl != endP2; ++pl, ++pr)
                        {
                            *pcal += *pl * *pr;
                        }
                    }
                    ++count;
                }
            }

            return calculated;
        }
    }
}
