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
        /// 行ベクトルと行列の積を計算する
        /// </summary>
        /// <param name="rowVectorMultiplicand">掛けられる数(左側のベクトル)</param>
        /// <param name="matrixMultiplier">掛ける数(右側の行列)</param>
        /// <returns>行列と列ベクトルの積 Matrix</returns>
        /// <exception cref="ArgumentException">左側の行列の列数と右側の列ベクトルの次元が異なる</exception>
        public static RowVector operator *(in RowVector rowVectorMultiplicand, in Matrix matrixMultiplier)
        {
            if (rowVectorMultiplicand._dim != matrixMultiplier._row)
            {
                throw new ArgumentException("左の行ベクトル行列の次元と、右の行列の行を揃えてください");
            }

            return new RowVector(RowVector.MultiplyRowVectorMatrixJ1I1(rowVectorMultiplicand._array, matrixMultiplier._array));
        }
    }
}
