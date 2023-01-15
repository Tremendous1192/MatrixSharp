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
        /// 分散・共分散行列を計算する。
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns></returns>
        public static Matrix VarianceCovarianceMatrix(Matrix designMatrix)
        {
            //Cov(x,y) = E[xy] - E[x]E[y] を計算する。
            double[] average = new double[designMatrix.Column];
            for (int i = 0; i < designMatrix.Row; i++)
            {
                for (int j = 0; j < designMatrix.Column; j++)
                {
                    average[j] += designMatrix[i, j];
                }
            }
            int row = designMatrix.Row;
            for (int j = 0; j < designMatrix.Column; j++)
            {
                average[j] /= row;
            }

            double[,] cov = new double[designMatrix.Column, designMatrix.Column];
            for (int i = 0; i < designMatrix.Row; i++)
            {
                for (int j = 0; j < designMatrix.Column; j++)
                {
                    for (int k = j; k < designMatrix.Column; k++)
                    {
                        cov[j, k] += designMatrix._array[i, j] * designMatrix._array[i, k];
                    }
                }
            }
            double temp = 0; // 配列呼び出し回数を減らすための変数
            for (int j = 0; j < designMatrix.Column; j++)
            {
                for (int k = j; k < designMatrix.Column; k++)
                {
                    temp = cov[j, k] / designMatrix.Row - average[j] * average[k];
                    cov[j, k] = temp;
                    cov[k, j] = temp;
                }
            }

            return new Matrix(cov);
        }
    }
}
