using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static unsafe partial class LinearRegression
    {
        /// <summary>
        /// Ridge回帰の係数ベクトルを係数を学習する.
        /// </summary>
        /// <param name="trainingDsignMatrix">訓練データの計画行列</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値</param>
        /// <param name="lambda">Ridge回帰のハイパーパラメータ</param>
        /// <returns></returns>
        public static ColumnVector LearnRidge(Matrix trainingDsignMatrix, ColumnVector trainingMeasuredVariables, double lambda)
        {
            if (lambda <= 0)
            {
                throw new FormatException("ハイパーパラメータλは正の数でなければなりません");
            }

            Matrix PhiT = trainingDsignMatrix.Transpose(); // 計画行列Φの転置
            Matrix PhiTPhiPlusLambdaI = PhiT * trainingDsignMatrix;
            for (int i = 0; i < PhiTPhiPlusLambdaI.Row; i++) { PhiTPhiPlusLambdaI._array[i, i] += lambda; }
            // 最小二乗法係数ベクトル w = (Φ^T * Φ + λI)^(-1) * Φ^T * t
            return PhiTPhiPlusLambdaI.Inverse() * (PhiT * trainingMeasuredVariables);
        }
    }
}
