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
        /// 主成分回帰
        /// </summary>
        /// <param name="trainingDsignMatrix">訓練データの計画行列</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値ベクトル</param>
        /// <param name="numberLatentVariables">潜在変数の数</param>
        /// <returns></returns>
        public static ColumnVector LearnPCR(Matrix trainingDsignMatrix, ColumnVector trainingMeasuredVariables, int numberLatentVariables)
        {
            if (numberLatentVariables <= 0 || trainingDsignMatrix.Column < numberLatentVariables)
            {
                throw new FormatException("潜在変数の次元が不適切で");
            }

            // ローディングベクトルから主成分射影行列を計算する
            List<ColumnVector> pList = PrincipalComponentAnalysis.PrincipalComponents(trainingDsignMatrix, numberLatentVariables);
            Matrix P = new Matrix(pList[0].Dimension, numberLatentVariables);
            for (int i = 0; i < pList[0].Dimension; i++)
            {
                for (int j = 0; j < P.Column; j++)
                {
                    P[i, j] = pList[j][i];
                }
            }
            Matrix T = trainingDsignMatrix * P; // 潜在変数(主成分得点ベクトルと読んだ方がしっくりくる)
            ColumnVector betaR = LinearRegression.Learn(T, trainingMeasuredVariables); // 潜在変数の回帰係数            
            return P * betaR; // 主成分回帰の回帰係数
        }
    }
}
