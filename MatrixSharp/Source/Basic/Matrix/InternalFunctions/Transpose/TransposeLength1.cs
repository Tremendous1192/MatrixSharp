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
        /// 2次元配列の転置
        /// </summary>
        /// <param name="array">配列</param>
        /// <returns></returns>
        internal static unsafe double[,] TransposeLength1(double[,] array)
        {
            double[,] calculated = new double[array.GetLength(1), array.GetLength(0)];

            int lenI = array.GetLength(0), lenJ = array.GetLength(1);
            fixed (double* pcalculated = calculated, parray = array)
            {
                int count = 0;
                for (double* pin = parray, endP = parray + array.Length; pin != endP; ++pin)
                {
                    // calculated[count % columnInput, count / columnInput] = array[count / columnInput, count % columnInput]
                    *(pcalculated + count % lenJ * lenI + count / lenJ) = *pin;
                    ++count;
                }
            }

            return calculated;
        }
    }
}
