using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static unsafe partial class Preprocessing
    {
        /// <summary>
        /// 前処理を行うクラス
        /// </summary>
        public static partial class PreProcessing
        {
            /// <summary>
            /// 線形回帰用に定数項を加える。
            /// xd -> φ(d+1) φ0 = 1, φ1 = x0, φ2 = x1, ..., φ(d+1)
            /// </summary>
            /// <param name="rowVector">行ベクトル</param>
            /// <returns></returns>
            public static RowVector AddConstant(RowVector rowVector)
            {
                return new RowVector(new double[] { 1 }.Concat(rowVector._array).ToArray(), false);
            }

            /// <summary>
            /// 線形回帰用に定数項を加える。
            /// xd -> φ(d+1) φ0 = 1, φ1 = x0, φ2 = x1, ..., φ(d+1)
            /// </summary>
            /// <param name="designMatrix">計画行列</param>
            /// <returns></returns>
            public static Matrix AddConstant(Matrix designMatrix)
            {
                double[,] concat = new double[designMatrix.Row, designMatrix.Column + 1];

                fixed (double* pdesignMatrix = designMatrix._array, pconcat = concat)
                {
                    int count = 0;
                    for (double* pde = pdesignMatrix, endpde = pdesignMatrix + designMatrix._array.Length, pcon = pconcat;
                        pde != endpde; ++pde, ++pcon, ++count)
                    {
                        // 定数項
                        if (count % designMatrix.Column == 0)
                        {
                            *pcon = 1;
                            ++pcon;
                        }

                        // 計画行列の値をコピーする
                        *pcon = *pde;
                    }
                }

                return new Matrix(concat, false);
            }
        }
    }
}
