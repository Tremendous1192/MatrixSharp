using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class GaussianKernel : IKernel
    {
        /// <summary>
        /// 訓練データのグラム行列を計算する
        /// </summary>
        /// <param name="designMatrixTrain">訓練データの計画行列</param>
        /// <returns>Matrix</returns>
        public Matrix GramMatrixTrain(Matrix designMatrixTrain)
        {
            double[,] kernelTrain = new double[designMatrixTrain.Row, designMatrixTrain.Row];
            fixed (double* pdesignMatrixTrain = designMatrixTrain._array, pkernelTrain = kernelTrain)
            {
                int count = 0;
                for (double* pkernel = pkernelTrain + designMatrixTrain.Row, endpkernel = pkernelTrain + kernelTrain.Length, pdesign1 = pdesignMatrixTrain + designMatrixTrain.Column;
                    pkernel != endpkernel; pkernel += designMatrixTrain.Row, pdesign1 += designMatrixTrain.Column)
                {
                    for (double* pdesign2 = pdesignMatrixTrain, pk = pkernel;
                        pdesign2 != pdesign1; pdesign2 += designMatrixTrain.Column, ++pk)
                    {
                        for (double* pd1 = pdesign1, endpd1 = pdesign1 + designMatrixTrain.Column, pd2 = pdesign2;
                            pd1 != endpd1; ++pd1, ++pd2)
                        {
                            *pk += (*pd1 - *pd2) * (*pd1 - *pd2);
                        }
                    }
                }
            }


            if (setHyperParameter)
            {
                for (int i = 0, lenI = kernelTrain.GetLength(0); i < lenI; ++i)
                {
                    kernelTrain[i, i] = 1;
                    for (int j = 0; j < i; j++)
                    {
                        kernelTrain[i, j] = Math.Exp(-kernelTrain[i, j] * hyperParameterArray[0] / 2.0);
                        kernelTrain[j, i] = kernelTrain[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0, lenI = kernelTrain.GetLength(0); i < lenI; ++i)
                {
                    kernelTrain[i, i] = 1;
                    for (int j = 0; j < i; j++)
                    {
                        kernelTrain[i, j] = Math.Exp(-kernelTrain[i, j] / 2.0);
                        kernelTrain[j, i] = kernelTrain[i, j];
                    }
                }
            }

            return new Matrix(kernelTrain, false);
        }

    }
}
