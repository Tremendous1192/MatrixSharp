using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace MatrixSharp.Source.DesignMatrix
{
    public static unsafe partial class DesignMatrix
    {
        /// <summary>
        /// 相関係数行列を返す。
        /// </summary>
        /// <param name="designMatrix"></param>
        /// <returns></returns>
        public static Matrix CorelationMatrix(Matrix designMatrix)
        {
            Matrix corelationMatrix = DesignMatrix.VarianceCovarianceMatrix(designMatrix); // 共分散行列

            double[] std = new double[corelationMatrix.Row]; // 標準偏差
            for (int j = 0; j < corelationMatrix.Row; j++)
            {
                std[j] = Math.Sqrt(corelationMatrix[j, j]);
            }

            double temp = 0; // 配列呼び出し回数を減らすための変数
            for (int j = 0; j < corelationMatrix.Row; j++)
            {
                for (int k = j; k < corelationMatrix.Column; k++)
                {
                    temp = corelationMatrix[j, k] / (std[j] * std[k]);
                    corelationMatrix[j, k] = temp;
                    corelationMatrix[k, j] = temp;
                }
            }

            return corelationMatrix;
        }
    }
}
