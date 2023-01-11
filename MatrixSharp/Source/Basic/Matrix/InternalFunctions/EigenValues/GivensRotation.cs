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
        /// Givens回転行列を左からかける
        /// </summary>
        /// <param name="array">配列</param>
        /// <param name="iRow">行1</param>
        /// <param name="jColumn">行2</param>
        /// <param name="theta">回転角</param>
        internal static void GivensRotation(ref double[,] array, int iRow, int jColumn, double theta)
        {
            // iRow行目と、jColumn行目のみ計算する
            double cos = Math.Cos(theta), sin = Math.Sin(theta);
            double a = 0, c = 0;
            fixed (double* parrayj = &array[jColumn, 0], parrayi = &array[iRow, 0])
            {
                for (double* pj = parrayj, pi = parrayi, endpj = parrayj + array.GetLength(1); pj != endpj; ++pj, ++pi)
                {
                    a = *pj;
                    c = *pi;

                    *pj = a * cos - c * sin;
                    *pi = a * sin + c * cos;
                }
            }
        }
    }
}
