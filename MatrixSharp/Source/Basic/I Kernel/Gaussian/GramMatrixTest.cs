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
        /// カーネル関数を計算する
        /// </summary>
        /// <param name="designMatrixTest"></param>
        /// <param name="designMatrixTrain"></param>
        /// <returns></returns>
        public Matrix GramMatrixTest(Matrix designMatrixTest, Matrix designMatrixTrain)
        {
            double[,] kernelTest = new double[designMatrixTest.Row, designMatrixTrain.Row];
            fixed (double* pdesignMatrixTest = designMatrixTest._array, pdesignMatrixTrain = designMatrixTrain._array, pkernelTest = kernelTest)
            {
                double* pk = pkernelTest;
                for (double* pTest = pdesignMatrixTest, endpTest = pdesignMatrixTest + designMatrixTest._array.Length; pTest != endpTest; pTest += designMatrixTest.Column)
                {
                    for (double* pTrain = pdesignMatrixTrain, endpTrain = pdesignMatrixTrain + designMatrixTrain._array.Length; pTrain != endpTrain; pTrain += designMatrixTrain.Column, ++pk)
                    {
                        for (double* pt1 = pTest, endpt1 = pTest + designMatrixTest.Column, pt2 = pTrain; pt1 != endpt1; ++pt1, ++pt2)
                        {
                            *pk += (*pt1 - *pt2) * (*pt1 - *pt2);
                        }
                    }
                }
            }



            if (setHyperParameter)
            {
                fixed (double* pkernelTest = kernelTest)
                {
                    for (double* pk = pkernelTest, endpk = pkernelTest + kernelTest.Length; pk != endpk; ++pk)
                    {
                        *pk = Math.Exp(-*pk * hyperParameterArray[0] / 2.0);
                    }
                }
            }
            else
            {
                fixed (double* pkernelTest = kernelTest)
                {
                    for (double* pk = pkernelTest, endpk = pkernelTest + kernelTest.Length; pk != endpk; ++pk)
                    {
                        *pk = Math.Exp(-*pk / 2.0);
                    }
                }
            }

            return new Matrix(kernelTest, false);
        }

    }
}
