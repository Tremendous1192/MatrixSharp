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

            double[,] rightT = right.Transpose()._array;
            double[,] calculated = new double[left._row, right.Column];
            int lenI = left._array.GetLength(0), lenJ = right._array.GetLength(1), lenK = right._array.GetLength(0);
            fixed (double* pleft = left._array, pright = rightT, pcalculated = calculated)
            {
                for (double* pc = pcalculated, endpc = pcalculated + calculated.Length,
                    pl = pleft, pr = pright;
                    pc != endpc; ++pc, pl = pleft + (pc - pcalculated) / lenJ, pr = pright + (pc - pcalculated) % lenJ)
                {
                    for (double* plk = pl, endplk = pl + lenK, prk = pr; plk != endplk; ++plk, ++prk)
                    {
                        *pc += *plk * *prk;
                    }
                }
            }

            return new Matrix(calculated, false);
        }
    }
}
