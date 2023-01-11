using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class ColumnVector
    {
        /// <summary>
        /// 列ベクトルと行ベクトルの積を計算する
        /// </summary>
        /// <param name="columnVectorMultiplicand">掛けられる数(左側の列ベクトル)</param>
        /// <param name="rowVectorMultiplier">掛ける数(右側の行ベクトル)</param>
        /// <returns>列ベクトルと行ベクトルの積 Matrix</returns>
        public static Matrix operator *(in ColumnVector columnVectorMultiplicand, in RowVector rowVectorMultiplier)
        {
            return new Matrix(Vector.ProductMakeMatrixLength1(columnVectorMultiplicand._array, rowVectorMultiplier._array), false);
        }
    }
}
