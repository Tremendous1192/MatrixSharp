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
        /// ベクトルの差を計算する
        /// </summary>
        /// <param name="left">引かれる数(左側のベクトル)</param>
        /// <param name="right">引く数(右側のベクトル)</param>
        /// <returns>ベクトルの差 ColumnVector</returns>
        /// <exception cref="ArgumentException">2つのベクトルの次元が異なる</exception>
        public static ColumnVector operator -(in ColumnVector left, in ColumnVector right)
        {
            if (left._dim != right._dim)
            {
                throw new ArgumentException("ベクトルの次元が異なります。計算できません");
            }

            return new ColumnVector(Vector.SubtractLength1(left._array, right._array), false);
        }
    }
}
