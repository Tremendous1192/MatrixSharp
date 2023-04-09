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
        /// 行列の積を計算する
        /// </summary>
        /// <param name="left">掛けられる数(左側の行列)</param>
        /// <param name="right">掛ける数(右側の行列)</param>
        /// <returns>行列積 Matrix</returns>
        /// <exception cref="ArgumentException">左側の行列の列数と右側の行列の行数が異なる</exception>
        public static Matrix operator *(in Matrix left, in Matrix right)
        {
            if (left._column != right._row)
            {
                throw new ArgumentException("左の行列の列数と、右の行列の行数を揃えてください");
            }


            if (left._column % 4 == 0)
            {
                return new Matrix(MultiplyIJ1K4(left._array, right.Transpose()._array));
            }
            else if (left._column % 3 == 0)
            {
                return new Matrix(MultiplyIJ1K3(left._array, right.Transpose()._array));
            }
            else if (left._column % 2 == 0)
            {
                return new Matrix(MultiplyIJ1K2(left._array, right.Transpose()._array));
            }

            return new Matrix(MultiplyIJ1K1(left._array, right.Transpose()._array), false);
        }
    }
}
