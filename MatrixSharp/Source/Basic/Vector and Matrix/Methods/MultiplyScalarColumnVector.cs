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
        /// ベクトルのスカラー倍を計算する
        /// </summary>
        /// <param name="scalar">スカラー</param>
        /// <param name="vector">ベクトル</param>
        /// <returns>列ベクトル ColumnVector</returns>
        public static ColumnVector operator *(double scalar, ColumnVector vector)
        {
            return new ColumnVector(vector._array.Select(x => x * scalar).ToArray(), false);
        }

        /// <summary>
        /// ベクトルのスカラー倍を計算する
        /// </summary>
        /// <param name="vector">ベクトル</param>
        /// <param name="scalar">スカラー</param>
        /// <returns>列ベクトル ColumnVector</returns>
        public static ColumnVector operator *(ColumnVector vector, double scalar)
        {
            return new ColumnVector(vector._array.Select(x => x * scalar).ToArray(), false);
        }
    }
}
