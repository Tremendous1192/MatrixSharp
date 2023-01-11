using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static partial class LogisticRegression
    {
        /// <summary>
        /// 2クラス分類を予測する。
        /// </summary>
        /// <param name="testDesignMatrix">テストデータの計画行列</param>
        /// <param name="coefficientW">係数ベクトル</param>
        /// <returns></returns>
        public static ColumnVector Predict(Matrix testDesignMatrix, ColumnVector coefficientW)
        {
            ColumnVector xw = testDesignMatrix * coefficientW;
            double[] result = new double[testDesignMatrix.Row];
            for (int i = 0; i < testDesignMatrix.Row; i++)
            {
                result[i] = 1.0 / (1 + Math.Exp(-xw[i]));
            }
            return new ColumnVector(result, false);
        }

        /// <summary>
        /// 2クラス分類を予測する。
        /// </summary>
        /// <param name="testRowVector">テストデータの行ベクトル</param>
        /// <param name="coefficientW">係数ベクトル</param>
        /// <returns></returns>
        public static double Predict(RowVector testRowVector, ColumnVector coefficientW)
        {
            return 1.0 / (1.0 + Math.Exp(-(testRowVector * coefficientW)));
        }
    }
}
