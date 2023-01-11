using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 線形回帰のクラス
    /// </summary>
    public static unsafe partial class LinearRegression
    {
        /// <summary>
        /// 線形回帰の係数ベクトルを学習する
        /// </summary>
        /// <param name="trainingDesignMatrix">訓練データの計画行列</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値</param>
        /// <returns></returns>
        public static ColumnVector Learn(Matrix trainingDesignMatrix, ColumnVector trainingMeasuredVariables)
        {
            Matrix phiT = trainingDesignMatrix.Transpose(); // 計画行列Φの転置
            // 最小二乗法係数ベクトル w = (Φ^T * Φ)^(-1) * Φ^T * t
            return (phiT * trainingDesignMatrix).Inverse() * (phiT * trainingMeasuredVariables);
        }
    }
}
