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
        /// 行列のスカラー倍
        /// </summary>
        /// <param name="scalar">スカラー</param>
        /// <param name="matrix">行列</param>
        /// <returns>行列 Matrix</returns>
        public static Matrix operator *(double scalar, Matrix matrix)
        {
            double[,] calculated = (double[,])matrix._array.Clone();
            fixed (double* pcalculated = calculated)
            {
                for (double* pc = pcalculated, endpc = pcalculated + calculated.Length; pc != endpc; ++pc)
                { *pc *= scalar; }
            }

            return new Matrix(calculated, false);
        }

        /// <summary>
        /// 行列のスカラー倍
        /// </summary>
        /// <param name="matrix">行列</param>
        /// <param name="scalar">スカラー</param>
        /// <returns>行列 Matrix</returns>
        public static Matrix operator *(Matrix matrix, double scalar)
        {
            return scalar * matrix;
        }
    }
}
