using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// ロジスティック回帰のクラス
    /// </summary>
    public static partial class LogisticRegression
    {
        /// <summary>
        /// ロジスティック回帰の係数ベクトル w を計算する。
        /// </summary>
        /// <param name="trainingDsignMatrix">訓練データの計画行列。</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値</param>
        /// <param name="iterationCount">ニュートン-ニュートンラフソン法の繰り返し計算回数。デフォルトは100回。</param>
        /// <returns></returns>
        public static ColumnVector Learn(Matrix trainingDsignMatrix, ColumnVector trainingMeasuredVariables, int iterationCount = 100)
        {
            Matrix Phi_T = trainingDsignMatrix.Transpose(); // 計画行列の転置
            ColumnVector w = new ColumnVector(trainingDsignMatrix.Column); // 係数wの初期化
            for (int i = 0; i < w.Dimension; i++) { w[i] = 0.01; }

            // メモリ節約のためにループ前にインスタンス化する            
            ColumnVector Y; // 予測値            
            ColumnVector deltaEW; // 対数尤度のベクトル微分
            Matrix RPhi = new Matrix(trainingDsignMatrix.Row, trainingDsignMatrix.Column);
            Matrix H = new Matrix(trainingDsignMatrix.Row, trainingDsignMatrix.Column); // ヘッセ行列
            Matrix HInverse; // ヘッセ行列の逆行列

            for (int repeat = 0; repeat < iterationCount; repeat++)
            {
                Y = LogisticRegression.Predict(trainingDsignMatrix, w); // 予測値
                deltaEW = Phi_T * (Y - trainingMeasuredVariables);// 対数尤度のベクトル微分

                for (int j = 0; j < trainingDsignMatrix.Row; j++)// ヘッセ行列と、その逆行列
                {
                    for (int k = 0; k < trainingDsignMatrix.Column; k++)
                    {
                        RPhi[j, k] = Y[j] * trainingDsignMatrix[j, k];
                    }
                }
                H = Phi_T * RPhi;
                double hMin = 0;
                for (int j = 0; j < H.Row; j++) { hMin = Math.Min(hMin, Math.Abs(H[j, j])); }
                hMin /= 1000;
                for (int j = 0; j < H.Row; j++) { H[j, j] += hMin; }// 逆行列の計算安定化
                HInverse = H.Inverse();

                w = w - HInverse * deltaEW;// 係数ベクトルの更新
            }

            return w;
        }


    }
}
