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
        /// カーネル関数を計算する。
        /// </summary>
        /// <param name="rowVector01">行ベクトル</param>
        /// <param name="rowVector02">行ベクトル</param>
        /// <returns></returns>
        public double Kernel(RowVector rowVector01, RowVector rowVector02)
        {
            double normSquare = rowVector01._array.Zip(rowVector02._array, (dl, dr) => (dl - dr) * (dl - dr)).Sum();
            return setHyperParameter ? Math.Exp(-normSquare / 2.0 * hyperParameterArray[0]) : Math.Exp(-normSquare / 2.0);
        }
    }
}
