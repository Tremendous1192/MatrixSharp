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
        /// 行列のアダマール積を計算する
        /// </summary>
        /// <param name="right">掛ける数(右側の行列)</param>
        /// <returns>Matrix</returns>
        /// <exception cref="ArgumentException">2つの行列の行数・列数が異なる</exception>
        public Matrix HadamardProduct(in Matrix right)
        {
            if (this._row != right._row || this._column != right._column)
            {
                throw new ArgumentException("行列の形が異なります。計算できません");
            }

            double[,] calculated = new double[this._row, this.Column];
            fixed (double* pleft = this._array, pright = right._array, pcalculated = calculated)
            {
                for (double* pl = pleft, endpl = pleft + this._array.Length, pr = pright, pc = pcalculated; pl != endpl; ++pl, ++pr, ++pc)
                { *pc = *pl * *pr; }
            }

            return new Matrix(calculated, false);
        }
    }
}
