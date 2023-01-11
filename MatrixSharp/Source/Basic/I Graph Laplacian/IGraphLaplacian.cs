using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// グラフラプラシアン
    /// </summary>
    public partial interface IGraphLaplacian
    {
        // ハイパーパラメータ関係
        /// <summary>
        /// カーネル関数のハイパーパラメータをセットする
        /// </summary>
        /// <param name="hyperParameter">ハイパーパラメータ (1次元)</param>
        void SetHyperParameters(double[] hyperParameter);
        /// <summary>
        /// ハイパーパラメータの要素数を返す
        /// </summary>
        /// <returns>int</returns>
        int HyperParameterDimension();

        /// <summary>
        /// 隣接行列を返す
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns>Matrix</returns>
        Matrix AdjacencyMatrix(Matrix designMatrix);

        /// <summary>
        /// 次数行列を返す
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns>Matrix</returns>
        Matrix DegreeMatrix(Matrix designMatrix);

        /// <summary>
        /// ラプラシアン行列
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns>Matrix</returns>
        Matrix LaplacianMatrix(Matrix designMatrix);

    }
}
