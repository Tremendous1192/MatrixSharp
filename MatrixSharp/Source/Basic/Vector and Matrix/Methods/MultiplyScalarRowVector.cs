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
        /// ベクトルのスカラー倍を計算する
        /// </summary>
        /// <param name="scalar">スカラー</param>
        /// <param name="vector">ベクトル</param>
        /// <returns>行ベクトル RowVector</returns>
        public static RowVector operator *(double scalar, RowVector vector)
        {
            return new RowVector(vector._array.Select(x => x * scalar).ToArray(), false);
        }

        /// <summary>
        /// ベクトルのスカラー倍を計算する
        /// </summary>
        /// <param name="vector">ベクトル</param>
        /// <param name="scalar">スカラー</param>
        /// <returns>行ベクトル RowVector</returns>
        public static RowVector operator *(RowVector vector, double scalar)
        {
            return new RowVector(vector._array.Select(x => x * scalar).ToArray(), false);
        }
    }
}
