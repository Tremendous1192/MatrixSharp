using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static partial class GaussianProcessRegression
    {
        /// <summary>
        /// グラム行列の逆行列を計算する.
        /// </summary>
        /// <param name="trainingDesignMatrix">訓練データの計画行列</param>
        /// <param name="iKernel">カーネル</param>
        /// <param name="hyperParameters">カーネルのハイパーパラメータ</param>
        /// <param name="noiseLambda">グラム行列の逆行列計算のハイパーパラメータ</param>
        /// <returns></returns>
        public static Matrix Learn(Matrix trainingDesignMatrix, IKernel iKernel, double[] hyperParameters, double noiseLambda = 0)
        {
            iKernel.SetHyperParameters(hyperParameters); //カーネルにハイパーパラメータをセットする

            if (noiseLambda < 0) { throw new FormatException("ノイズ λ は非負の実数です"); }

            Matrix gramMatrix = iKernel.GramMatrixTrain(trainingDesignMatrix);
            for (int i = 0; i < gramMatrix.Row; i++)
            {
                gramMatrix[i, i] += noiseLambda;
            }

            return gramMatrix.Inverse();
        }
    }
}
