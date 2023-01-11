using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// ガウス過程回帰のクラス
    /// </summary>
    public static partial class GaussianProcessRegression
    {
        /// <summary>
        /// 期待値[0]と分散[1]を計算する.
        /// </summary>
        /// <param name="trainingDesignMatrix">訓練データの計画行列</param>
        /// <param name="iKernel">カーネル</param>
        /// <param name="hyperParameters">カーネルのハイパーパラメータ</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値</param>
        /// <param name="gramMatrixInverse">グラム行列</param>
        /// <param name="testRowVector">テストデータの計画行列</param>
        /// <returns></returns>
        public static List<double> Predict(Matrix trainingDesignMatrix, IKernel iKernel, double[] hyperParameters, ColumnVector trainingMeasuredVariables, Matrix gramMatrixInverse, RowVector testRowVector)
        {
            List<double> result = new List<double>(2);

            iKernel.SetHyperParameters(hyperParameters); //カーネルにハイパーパラメータをセットする

            ColumnVector KInverseY = gramMatrixInverse * trainingMeasuredVariables; // グラム行列の逆行列

            RowVector kernelVector = iKernel.GramVectorTest(testRowVector, trainingDesignMatrix);
            result.Add(kernelVector * KInverseY); // 期待値

            result.Add(iKernel.KernelOneself(testRowVector) - (kernelVector * gramMatrixInverse).DotProduct(kernelVector)); // 分散

            return result;
        }

        /// <summary>
        /// 期待値[0]と分散[1]を計算する.
        /// </summary>
        /// <param name="trainingDesignMatrix">訓練データの計画行列</param>
        /// <param name="iKernel">カーネル</param>
        /// <param name="hyperParameters">カーネルのハイパーパラメータ</param>
        /// <param name="trainingMeasuredVariables">訓練データの測定値</param>
        /// <param name="gramMatrixInverse">グラム行列</param>
        /// <param name="testDesignMatrix">テストデータの計画行列行</param>
        /// <returns></returns>
        public static List<ColumnVector> Predict(Matrix trainingDesignMatrix, IKernel iKernel, double[] hyperParameters, ColumnVector trainingMeasuredVariables, Matrix gramMatrixInverse, Matrix testDesignMatrix)
        {
            List<ColumnVector> result = new List<ColumnVector>(2);

            iKernel.SetHyperParameters(hyperParameters); //カーネルにハイパーパラメータをセットする

            ColumnVector KInverseY = gramMatrixInverse * trainingMeasuredVariables;  // グラム行列の逆行列 K^(-1)

            // テストデータと教師データとのカーネル行列 K* = K(xtest, xtrain)
            Matrix gramMatrixTested = iKernel.GramMatrixTest(testDesignMatrix, trainingDesignMatrix);
            ColumnVector kernelOneself = iKernel.KernelOneself(testDesignMatrix);
            result.Add(gramMatrixTested * KInverseY); // 期待値 K* x K^(-1)

            // 分散 k* - (K* x K^(-1)) ⊙ K*
            double[,] kKK = (gramMatrixTested * gramMatrixInverse).HadamardProduct(gramMatrixTested)._array;
            double[] substractorSums = new double[kKK.GetLength(0)];
            for (int i = 0, lenI = substractorSums.Length; i < lenI; ++i)
            {
                for (int j = 0, lenJ = kKK.GetLength(1); j < lenJ; ++j)
                {
                    substractorSums[i] += kKK[i, j];
                }
            }
            result.Add(new ColumnVector(kernelOneself._array.Zip(substractorSums, (dl, dr) => dl - dr).ToArray(), false));

            return result;
        }
    }
}
