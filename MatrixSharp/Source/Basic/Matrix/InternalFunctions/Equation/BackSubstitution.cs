using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {

        internal static double[] BackSubstitution(ref double[,] matrix, ref double[] columneVector)
        {            
            double[] result = new double[matrix.GetLength(1)];

            // 連立方程式の式の数より変数が多い場合に、計算用のダミー列を加える
            if (matrix.GetLength(0) < matrix.GetLength(1))
            {
                double[,] copied2D = new double[matrix.GetLength(1), matrix.GetLength(1)];
                double[] copied1D = new double[matrix.GetLength(1)];

                for (int i = 0; i < matrix.GetLength(0); ++i)
                {
                    for (int j = 0; j < matrix.GetLength(1); ++j)
                    {
                        copied2D[i, j] = matrix[i, j];
                    }
                    copied1D[i] = columneVector[i];
                }

                matrix = copied2D;
                columneVector = copied1D;
            }


            // 後退代入のための行の移動
            for (int i = 0; i < matrix.GetLength(0) - 1; ++i)
            {
                if (double.IsNaN(1.0 / matrix[i, i]))
                {
                    for (int i2 = matrix.GetLength(0) - 1; i2 >= i; --i2)
                    {
                        for (int j = i; j < matrix.GetLength(1); ++j)
                        {
                            matrix[i2, j] = matrix[i, j];
                        }
                        columneVector[i2] = columneVector[i];
                    }
                }
            }


            // [i, i]要素を1にする正規化
            for (int i = 0; i < matrix.GetLength(0) - 1; ++i)
            {
                if (double.IsNaN(1.0 / matrix[i, i]) || matrix[i, i] == 1)
                { }
                else
                {
                    double coefficient = matrix[i, i];
                    for (int j = i; j < matrix.GetLength(1); ++j)
                    {
                        matrix[i, j] /= coefficient;
                    }
                    columneVector[i] /= coefficient;
                }
            }


            // 任意の実数が解となる変数を確認する
            for (int j = 0; j < matrix.GetLength(1); ++j)
            {
                double[] tempArray = new double[j + 1];
                for (int i = 0; i <= j; ++i)
                {
                    tempArray[i] = matrix[i, j];
                }

                if (!tempArray.Any(x => !double.IsNaN(1.0 / x)))
                {
                    result[j] = 1;
                }
            }


            // 後退代入を行う
            for (int i = matrix.GetLength(0) - 1; i >= 0; --i)
            {
                if (!double.IsNaN(1.0 / matrix[i, i]))
                {
                    result[i] = columneVector[i];

                    for (int i2 = i - 1; i2 >= 0; --i2)
                    {
                        columneVector[i2] -= matrix[i2, i] * result[i];
                    }
                }
            }

            return result;
        }

    }
}
