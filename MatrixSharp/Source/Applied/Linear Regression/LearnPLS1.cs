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
        /// 部分的最小二乗法
        /// </summary>
        /// <param name="trainingDsignMatrix">訓練データの計画行列</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値ベクトル</param>
        /// <param name="numberLatentVariables">潜在変数の数</param>
        /// <returns></returns>
        public static ColumnVector LearnPLS1(Matrix trainingDsignMatrix, ColumnVector trainingMeasuredVariables, int numberLatentVariables)
        {
            if (numberLatentVariables <= 0 || trainingDsignMatrix.Column < numberLatentVariables)
            {
                throw new FormatException("潜在変数の次元が不適切で");
            }

            // パラメータ            
            Matrix W = new Matrix(trainingDsignMatrix.Column, numberLatentVariables); // 重み
            Matrix P = new Matrix(trainingDsignMatrix.Column, numberLatentVariables); // ローディングベクトルを連ねた射影行列
            ColumnVector D = new ColumnVector(numberLatentVariables); // 潜在空間の回帰係数
            Matrix T = new Matrix(trainingDsignMatrix.Row, numberLatentVariables);　// 潜在変数(主成分得点の計画行列)

            // for文内の変数            
            ColumnVector w = new ColumnVector(1); // 重み
            //double wNormInverse = 1; // 重みを単位行列に整える
            ColumnVector t = new ColumnVector(1); // 潜在変数(主成分得点の列)
            double tSelfInnnerProductInverse = 1; // 潜在変数を単位行列に整える            
            ColumnVector p = new ColumnVector(1); // ローディングベクトル       
            double d = 1.0; // 潜在空間の回帰係数

            // デフレーションの変数
            Matrix X = trainingDsignMatrix.Clone(); // デフレーションされた計画行列
            Matrix XT = new Matrix(1, 1); // デフレーションされた計画行列の転置
            ColumnVector y = trainingMeasuredVariables.Clone(); // デフレーションされた測定値

            // NIPALS
            for (int i = 0; i < numberLatentVariables; i++)
            {
                XT = X.Transpose();

                w = (XT * y).UnitVector();
                //w = XT * y;
                //wNormInverse = 1.0 / w.Norm_L2();
                //w = w * wNormInverse;

                t = X * w;
                tSelfInnnerProductInverse = 1.0 / t.DotProduct(t);

                p = (XT * t) * tSelfInnnerProductInverse;

                //d = (t.Transpose() * y) * tSelfInnnerProductInverse;
                d = t.DotProduct(y) * tSelfInnnerProductInverse;

                //X = X - t * p.Transpose();
                //y = y - t * d;
                X -= t * p.Transpose();
                y -= t * d;


                for (int j = 0; j < W.Row; j++)
                {
                    W[j, i] = w[j];
                    P[j, i] = p[j];
                }
                for (int j = 0; j < T.Row; j++)
                {
                    T[j, i] = t[j];
                }
                D[i] = d;
            }

            // β
            return W * ((P.Transpose() * W).Inverse() * D);
        }
    }
}
