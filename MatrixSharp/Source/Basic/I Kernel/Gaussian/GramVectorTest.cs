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
        /// <param name="rowVectorTest">テストデータの行ベクトル</param>
        /// <param name="designMatrixTrain">訓練データの計画行列</param>
        /// <returns>RowVector</returns>
        public RowVector GramVectorTest(RowVector rowVectorTest, Matrix designMatrixTrain)
        {
            double[] result = new double[designMatrixTrain._array.GetLength(0)];
            fixed (double* presult = result, prowVectorTest = rowVectorTest._array, pdesignMatrixTrain = designMatrixTrain._array)
            {
                for (double* pre = presult, endpre = presult + result.Length, pdesign = pdesignMatrixTrain;
                    pre != endpre; ++pre, pdesign += designMatrixTrain.Column)
                {
                    for (double* prow = prowVectorTest, endprow = prowVectorTest + rowVectorTest.Dimension, pde = pdesign;
                        prow != endprow; ++prow, ++pde)
                    {
                        *pre += (*prow - *pde) * (*prow - *pde);
                    }
                }
            }

            if (setHyperParameter)
            {
                result = result.Select(x => Math.Exp(-x / 2.0 * hyperParameterArray[0])).ToArray();
            }
            else
            {
                result = result.Select(x => Math.Exp(-x / 2.0)).ToArray();
            }

            return new RowVector(result, false);
        }
    }
}
