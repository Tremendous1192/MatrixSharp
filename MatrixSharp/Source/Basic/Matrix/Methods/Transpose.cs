using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// 行列の転置
        /// </summary>
        /// <returns>Matrix</returns>
        public Matrix Transpose() 
        {
            double[,] calculated = new double[this.Column, this._row];
            fixed (double* parray = this._array, pcalculated = calculated)
            {
                int count = 0;
                int lenI = this._array.GetLength(0), lenJ = this._array.GetLength(1);
                for (double* pa = parray, endpa = parray + this._array.Length; pa != endpa; ++pa, ++count)
                {
                    // calculated[count % columnInput, count / columnInput] = array[count / columnInput, count % columnInput]
                    *(pcalculated + count % lenJ * lenI + count / lenJ) = *pa;
                }
            }

            return new Matrix(calculated, false);
        }
    }
}
