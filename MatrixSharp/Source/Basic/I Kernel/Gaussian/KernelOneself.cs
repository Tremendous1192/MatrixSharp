using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class GaussianKernel : IKernel
    {
        /// <summary>
        /// 2つのベクトル変数が等しい場合のカーネル関数を計算する。
        /// </summary>
        /// <param name="rowVector01">行ベクトル</param>
        /// <returns>double</returns>
        public double KernelOneself(RowVector rowVector01)
        {
            return 1;
        }

        /// <summary>
        /// 2つのベクトル変数が等しい場合のカーネル関数を計算する。
        /// </summary>
        /// <param name="designMatrixTest">テストデータの計画行列</param>
        /// <returns>ColumnVector</returns>
        public ColumnVector KernelOneself(Matrix designMatrixTest)
        {
            return new ColumnVector(Enumerable.Repeat<double>(1, designMatrixTest.Row).ToArray(), false);
        }
    }
}
