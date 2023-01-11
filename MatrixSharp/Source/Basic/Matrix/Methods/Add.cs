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

            double[,] calculated = new double[left._row, left.Column];
            fixed (double* pleft = left._array, pright = right._array, pcalculated = calculated)
            {
                for (double* pl = pleft, endpl = pleft + left._array.Length, pr = pright, pc = pcalculated; pl != endpl; ++pl, ++pr, ++pc)
                { *pc = *pl + *pr; }
            }

            return new Matrix(calculated, false);
        }
    }
}
