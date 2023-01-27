using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// 行列の和を計算する
        /// </summary>
        /// <param name="left">足される数(左側の行列)</param>
        /// <param name="right">足す数(右側の行列)</param>
        /// <returns>Matrix</returns>
        /// <exception cref="ArgumentException">2つの行列の行数・列数が異なる</exception>
        public static Matrix operator +(in Matrix left, in Matrix right)
        {
            if (left._row != right._row || left._column != right._column)
            {
                throw new ArgumentException("行列の形が異なります。計算できません");
            }

            if (left._array.Length % 5 == 0)
            {
                return new Matrix(Matrix.AddLength5(left._array, right._array), false);
            }
            else if (left._array.Length % 4 == 0)
            {
                return new Matrix(Matrix.AddLength4(left._array, right._array), false);
            }
            else if (left._array.Length % 3 == 0)
            {
                return new Matrix(Matrix.AddLength3(left._array, right._array), false);
            }
            else if (left._array.Length % 2 == 0)
            {
                return new Matrix(Matrix.AddLength2(left._array, right._array), false);
            }
            return new Matrix(Matrix.AddLength1(left._array, right._array), false);
        }
    }
}
