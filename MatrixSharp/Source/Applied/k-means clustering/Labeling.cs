using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// k-平均法のクラス
    /// </summary>
    public static unsafe partial class KMeansClustering
    {
        /// <summary>
        /// k-means法によるクラスタリング
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <param name="numberOfClass">クラス数</param>
        /// <returns>int[]</returns>
        public static int[] Labeling(Matrix designMatrix, int numberOfClass)
        {
            if (numberOfClass < 2) { throw new FormatException("クラス分けは2クラス以上に分類するタスクです。もう一度クラス数を選択してください"); }

            int[] labels = new int[designMatrix.Row]; // 戻り値
            double[,] centroids = new double[numberOfClass, designMatrix.Column]; // Centroid
            double[,] distances = new double[designMatrix.Row, centroids.GetLength(0)]; // 各データ点とCentroidとの距離

            unsafe
            {
                fixed (double* pdesignMatrix = designMatrix._array, pcentroids = centroids, pdistances = distances)
                {
                    // Centroidの初期化にはk-means++を用いる
                    {
                        // k-means++の初期化用乱数
                        UniformDistribution ud = new UniformDistribution();
                        ud.SetParameters(new double[2] { -0.4, designMatrix.Row - 0.6 });

                        // 1個目
                        int firstNo = (int)Math.Round(ud.NextDouble());
                        for (double* pdes = pdesignMatrix + firstNo * designMatrix.Column, endp = pdesignMatrix + (firstNo + 1) * designMatrix.Column, pcen = pcentroids;
                            pdes != endp; ++pdes, ++pcen)
                        { *pcen = *pdes; }

                        // 2個目からは各Centroidからの距離に比例する確率分布で次のCentroidを決定する
                        int nextNumber = 0;
                        double[] probability = new double[designMatrix.Row];
                        fixed (double* pprobability = probability)
                        {
                            // 2個目以降
                            for (double* pcentroidStep = pcentroids + designMatrix.Column, endpcen = pcentroids + designMatrix._array.Length; pcentroidStep != endpcen; pcentroidStep += designMatrix.Column)
                            {
                                // 各データ点と決定済みのCentroidとの距離の最小値を計算する
                                double sumation = 0;
                                for (double* pdes = pdesignMatrix, endpdes = pdesignMatrix + designMatrix._array.Length, pdis = pdistances, ppro = pprobability;
                                    pdes != endpdes; pdes += designMatrix.Column, pdis += distances.GetLength(1), ++ppro)
                                {
                                    double distanceMin = double.MaxValue;
                                    // 決定済みCentroids
                                    for (double* pcen2 = pcentroids, endpcen2 = pcentroids + designMatrix.Column, pdis2 = pdis;
                                        pcen2 != endpcen2; pcen2 += designMatrix.Column, ++pdis2)
                                    {
                                        *pdis2 = 0;

                                        // データ点の各次元
                                        for (double* pd = pdes, endpd = pdes + designMatrix.Column, pc2 = pcen2;
                                            pd != pdes; ++pd, ++pc2)
                                        { *pdis2 += (*pd - *pc2) * (*pd - *pc2); }

                                        distanceMin = Math.Min(distanceMin, Math.Sqrt(*pdis2));
                                    }

                                    *ppro = distanceMin;
                                    sumation += *ppro;
                                }

                                // 確率に変換する
                                for (double* ppro = pprobability, endppro = pprobability + probability.Length; ppro != endppro; ++ppro)
                                { *ppro /= sumation; }

                                // 確率分布に基づいて新しいCentroidを決定する
                                bool set = false;
                                do
                                {
                                    nextNumber = (int)Math.Round(ud.NextDouble());
                                    if (probability[nextNumber] >= ud.NextDouble())
                                    {
                                        for (double* pc = pcentroidStep, endpc = pcentroidStep + centroids.GetLength(1), pd = pdesignMatrix + nextNumber * designMatrix.Column;
                                            pc != endpc; ++pc, ++pd)
                                        { *pc = *pd; }
                                        set = true;
                                    }
                                }
                                while (!set);
                            }
                        }
                    }// k-means++ここまで


                    // ループ計算
                    for (int loop = 0; loop < 20; loop++)
                    {
                        // 各データ点とCentroidとの距離を計算する
                        int count = 0;
                        // 各データ点
                        for (double* pdes = pdesignMatrix, endpdes = pdesignMatrix + designMatrix._array.Length, pdis = pdistances;
                            pdes != endpdes; pdes += designMatrix.Column, pdis += distances.GetLength(1), ++count)
                        {
                            // Centroid
                            for (double* pcen = pcentroids, endpcen = pcentroids + centroids.Length, pdi = pdis;
                                pcen != endpcen; pcen += centroids.GetLength(1), ++pdi)
                            {
                                *pdis = 0;

                                // 距離
                                for (double* pd = pdes, endpde = pdes + designMatrix.Column, pc = pcen;
                                    pd != endpde; ++pd, ++pc)
                                {
                                    *pdis += (*pd - *pc) * (*pd - *pc);
                                }
                            }

                            // ラベリング
                            int jMin = 0;
                            double distanceMin = *pdis;
                            for (int j = 1; j < distances.GetLength(1); ++j)
                            {
                                if (distanceMin > distances[count, j])
                                {
                                    distanceMin = distances[count, j];
                                    jMin = j;
                                }
                            }

                            labels[count] = jMin;
                        }

                        // 各Centroidのデータ点数を計数する
                        int[] countLebels = new int[centroids.GetLength(0)];
                        for (int i = 0; i < labels.Length; ++i)
                        { ++countLebels[labels[i]]; }

                        // Centroidの更新
                        centroids.Initialize();
                        for (int i = 0; i < designMatrix._array.GetLength(0); ++i)
                        {
                            for (int j = 0; j < designMatrix._array.GetLength(1); ++j)
                            {
                                centroids[labels[i], j] += designMatrix._array[i, j];
                            }
                        }
                        for (int i = 0; i < centroids.GetLength(0); ++i)
                        {
                            for (int j = 0; j < centroids.GetLength(1); ++j)
                            {
                                centroids[i, j] /= countLebels[i];
                            }
                        }
                    }
                    // ループ計算 ここまで


                }
            }

            return labels;
        }
    }
}
