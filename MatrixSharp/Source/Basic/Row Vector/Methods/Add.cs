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
        /// ベクトルの和を計算する
        /// </summary>
        /// <param name="left">足される数(左側のベクトル)</param>
        /// <param name="right">足す数(右側のベクトル)</param>
        /// <returns>ColumnVector</returns>
        /// <exception cref="ArgumentException">2つのベクトルの次元が異なる</exception>
        public static RowVector operator +(in RowVector left, in RowVector right)
        {
            if (left._dim != right._dim)
            {
                throw new ArgumentException("ベクトルの次元が異なります。計算できません");
            }

            return new RowVector(Vector.AddLength1(left._array, right._array), false);
        }
    }
}
