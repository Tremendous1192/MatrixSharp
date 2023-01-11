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
        /// 行ベクトルと列ベクトルの積を計算する
        /// </summary>
        /// <param name="rowVectorMultiplicand">掛けられる数(左側の行列)</param>
        /// <param name="columnVectorMultiplier">掛ける数(右側の行列)</param>
        /// <returns>行ベクトルと列ベクトルの積 double</returns>
        /// <exception cref="ArgumentException">2つのベクトルの次元が異なる</exception>
        public static double operator *(in RowVector rowVectorMultiplicand, in ColumnVector columnVectorMultiplier)
        {
            if (rowVectorMultiplicand._dim != columnVectorMultiplier._dim)
            {
                throw new ArgumentException("2つのベクトルの次元を揃えてください");
            }

            return Vector.DotProductLength1(rowVectorMultiplicand._array, columnVectorMultiplier._array);
        }
    }
}
