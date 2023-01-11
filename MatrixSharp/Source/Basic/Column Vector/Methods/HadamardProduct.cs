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
        /// ベクトルのアダマール積を計算する
        /// </summary>
        /// <param name="right">掛ける数(右側の行列)</param>
        /// <returns>ベクトルのアダマール積 ColumnVector</returns>
        /// <exception cref="ArgumentException">2つのベクトルの次元が異なる</exception>
        public ColumnVector HadamardProduct(in ColumnVector right)
        {
            if (this._dim != right._dim)
            {
                throw new ArgumentException("ベクトルの次元が異なります。計算できません");
            }

            return new ColumnVector(Vector.HadamardProductLength1(this._array, right._array), false);
        }
    }
}
