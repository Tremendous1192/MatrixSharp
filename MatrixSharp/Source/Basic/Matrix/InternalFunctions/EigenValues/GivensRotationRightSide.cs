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
        /// Givens回転行列を右からかける
        /// </summary>
        /// <param name="array">配列</param>
        /// <param name="iRow">列1</param>
        /// <param name="jColumn">列2</param>
        /// <param name="theta">回転角</param>
        internal static void GivensRotationRightSide(ref double[,] array, int iRow, int jColumn, double theta)
        {
            // iRow列目と、jColumn列目のみ計算する
            double cos = Math.Cos(theta), sin = Math.Sin(theta);
            double a = 0, b = 0;
            fixed (double* parrayj = &array[0, jColumn], parrayi = &array[0, iRow], endpj = &array[array.GetLength(0) - 1, jColumn])
            {
                for (double* pj = parrayj, pi = parrayi; pj <= endpj; pj += array.GetLength(1), pi += array.GetLength(1))
                {
                    a = *pj;
                    b = *pi;

                    *pj = a * cos + b * sin;
                    *pi = -a * sin + b * cos;
                }
            }
        }
    }
}
