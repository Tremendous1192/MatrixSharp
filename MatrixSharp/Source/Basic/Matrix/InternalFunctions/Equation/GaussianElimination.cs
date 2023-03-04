using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// ガウスの消去法で行列を簡約化する
        /// </summary>
        /// <param name="matrix">左辺の行列</param>
        /// <param name="columneVector">右辺の列ベクトル</param>
        /// <param name="calculatedMatrix">簡約化した後の左辺の行列</param>
        /// <param name="calculatedVector">簡約化した後の右辺の列ベクトル</param>
        internal static void GaussianElimination(in double[,] matrix, in double[] columneVector,
            out double[,] calculatedMatrix,out double[] calculatedVector)
        {
            // 初期化
            calculatedMatrix = (double[,])matrix.Clone(); // 左辺の行列
            calculatedVector = (double[])columneVector.Clone(); // 方程式の解
            double temp = 0; // 入れ替え用の変数
            double coefficinet = 1; // [i, i]成分を1にしたり、他の列の値を上手く消すための係数

            fixed (double* preduced = calculatedMatrix, presult = calculatedVector)
            {
                // 行列の簡約化
                int count = 0;
                for (double* preRow = preduced, endpreRow = preduced + calculatedMatrix.Length; preRow != endpreRow; preRow += calculatedMatrix.GetLength(1))
                {
                    // reduced[i, i] が0の場合、下の行と入れ替える
                    if (double.IsNaN(1.0 / (*(preRow + count))) || double.IsInfinity(1.0 / (*(preRow + count))))
                    {
                        for (double* preRow2 = preRow + calculatedMatrix.GetLength(1); preRow2 != endpreRow; preRow2 += calculatedMatrix.GetLength(1))
                        {
                            if (!double.IsNaN(1.0 / (*(preRow2 + count))) && !double.IsInfinity(1.0 / (*(preRow2 + count))))
                            {
                                for (double* pr = preRow, pr2 = preRow2, endpr = preRow + calculatedMatrix.GetLength(1); pr != endpr; ++pr, ++pr2)
                                {
                                    temp = *pr;
                                    *pr = *pr2;
                                    *pr2 = temp;
                                }
                                break;
                            }
                        }
                    }
                    if (double.IsNaN(1.0 / (*(preRow + count))) || double.IsInfinity(1.0 / (*(preRow + count))))
                    { ++count; continue; }

                    // [i, i]要素が1になるように1行目を[i, i]要素で除算する
                    coefficinet = *(preRow + count);
                    for (double* pr = preRow, endpr = preRow + calculatedMatrix.GetLength(1); pr != endpr; ++pr)
                    {
                        *pr /= coefficinet;
                    }

                    // [i2, i] 要素を0にする
                    for (double* preRow2 = preRow + calculatedMatrix.GetLength(1); preRow2 != endpreRow; preRow2 += calculatedMatrix.GetLength(1))
                    {
                        if (!double.IsNaN(1.0 / (*(preRow2 + count))) && !double.IsInfinity(1.0 / (*(preRow2 + count))))
                        {
                            coefficinet = *(preRow2 + count);
                            for (double* pr = preRow, pr2 = preRow2, endpr = preRow + calculatedMatrix.GetLength(1); pr != endpr; ++pr, ++pr2)
                            {
                                *pr2 -= *pr * coefficinet;
                            }
                        }
                    }

                    ++count;
                }
                // 行列の簡約化 ここまで
            }

        }
    }
}
