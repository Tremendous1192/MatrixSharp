using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static unsafe partial class SpectralClustering
    {
        /// <summary>
        /// クラスタリング
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <param name="numberOfClass">クラス数</param>
        /// <param name="iGraphLaplacian">ラプラシアン</param>
        /// <param name="hyperparameters">ハイパーパラメータ</param>
        /// <returns>int[]</returns>
        /// <exception cref="FormatException"></exception>
        public static int[] Labeling(Matrix designMatrix, int numberOfClass, IGraphLaplacian iGraphLaplacian, double[] hyperparameters)
        {
            if (numberOfClass < 2) { throw new FormatException("クラス分けは2クラス以上に分類するタスクです。もう一度クラス数を選択してください"); }

            // グラフラプラシアン
            iGraphLaplacian.SetHyperParameters(hyperparameters);
            Matrix laplacian = iGraphLaplacian.LaplacianMatrix(designMatrix);

            // ラプラシアンの固有ベクトル
            List<ColumnVector> eigenVectors = laplacian.EigenVectors(numberOfClass, false);
            double[,] eigenArray = new double[eigenVectors[0]._array.Length, eigenVectors.Count];
            for (int j=0;j<eigenVectors.Count;++j)
            {
                for (int i = 0; i < eigenVectors[j]._array.Length;++i)
                {
                    eigenArray[i, j] = eigenVectors[j]._array[i];
                }            
            }

            // 固有ベクトル集合のk-平均法
            return KMeansClustering.Labeling(new Matrix(eigenArray, false), numberOfClass);
        }

    }
}
