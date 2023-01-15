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
        /// 予測値を計算する。
        /// </summary>
        /// <param name="testDesignMatrix">テストデータの計画行列</param>
        /// <param name="coefficientW">係数ベクトル</param>
        /// <returns></returns>
        public static ColumnVector Predict(Matrix testDesignMatrix, ColumnVector coefficientW)
        {
            return testDesignMatrix * coefficientW;
        }

        /// <summary>
        /// 予測値を計算する。
        /// </summary>
        /// <param name="testRowVector">テストデータの計画行列</param>
        /// <param name="coefficientW">係数ベクトル</param>
        /// <returns></returns>
        public static double Predict(RowVector testRowVector, ColumnVector coefficientW)
        {
            return testRowVector * coefficientW;
        }
    }
}
