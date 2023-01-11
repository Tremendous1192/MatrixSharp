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
        /// 行列と列ベクトルの積を計算する
        /// </summary>
        /// <param name="matrixMultiplicand">掛けられる数(左側の行列)</param>
        /// <param name="columnVectorMultiplier">掛ける数(右側の行列)</param>
        /// <returns>行列と列ベクトルの積 Matrix</returns>
        /// <exception cref="ArgumentException">左側の行列の列数と右側の列ベクトルの次元が異なる</exception>
        public static ColumnVector operator *(in Matrix matrixMultiplicand, in ColumnVector columnVectorMultiplier)
        {
            if (matrixMultiplicand._column != columnVectorMultiplier._dim)
            {
                throw new ArgumentException("左の行列の列数と、右の列ベクトルの次元を揃えてください");
            }

            return new ColumnVector(ColumnVector.MultiplyMatrixColumnVectorI1K1(matrixMultiplicand._array, columnVectorMultiplier._array), false);
        }
    }
}
