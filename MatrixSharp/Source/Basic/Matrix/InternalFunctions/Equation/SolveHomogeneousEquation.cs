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
        /// 同次連立1次方程式を解く
        /// </summary>
        /// <param name="left">左側の行列</param>
        /// <returns></returns>
        internal static double[] SolveHomogeneousEquation(in double[,] left)
        {
            // 初期化
            double[,] reduced = new double[left.GetLength(0), left.GetLength(1)]; // 左辺の行列
            double[] result = new double[left.GetLength(0)]; // 方程式の解
            double temp = 0; // 入れ替え用の変数
            double coefficinet = 1; // [i, i]成分を1にしたり、他の列の値を上手く消すための係数

            fixed (double* pleft = left, preduced = reduced, presult = result)
            {
                // データを移す
                int count = 0;
                // 行列
                for (double* pl = pleft, endpl = pleft + left.Length, pre = preduced; pl != endpl; ++pl, ++pre)
                {
                    *pre = *pl;
                    ++count;
                }
                // データを移す ここまで

                // 行列の簡約化
                count = 0;
                for (double* preRow = preduced, endpreRow = preduced + reduced.Length; preRow != endpreRow; preRow += reduced.GetLength(1))
                {
                    // reduced[i, i] が0の場合、下の行と入れ替える
                    if (double.IsNaN(1.0 / (*(preRow + count))) || double.IsInfinity(1.0 / (*(preRow + count))))
                    {
                        for (double* preRow2 = preRow + reduced.GetLength(1); preRow2 != endpreRow; preRow2 += reduced.GetLength(1))
                        {
                            if (!double.IsNaN(1.0 / (*(preRow2 + count))) && !double.IsInfinity(1.0 / (*(preRow2 + count))))
                            {
                                for (double* pr = preRow, pr2 = preRow2, endpr = preRow + reduced.GetLength(1); pr != endpr; ++pr, ++pr2)
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

                    // [i, i]要素を1にする
                    coefficinet = *(preRow + count);
                    for (double* pr = preRow, endpr = preRow + reduced.GetLength(1); pr != endpr; ++pr)
                    {
                        *pr /= coefficinet;
                    }

                    // [i2, i] 要素を0にする
                    for (double* preRow2 = preRow + reduced.GetLength(1); preRow2 != endpreRow; preRow2 += reduced.GetLength(1))
                    {
                        if (!double.IsNaN(1.0 / (*(preRow2 + count))) && !double.IsInfinity(1.0 / (*(preRow2 + count))))
                        {
                            coefficinet = *(preRow2 + count);
                            for (double* pr = preRow, pr2 = preRow2, endpr = preRow + reduced.GetLength(1); pr != endpr; ++pr, ++pr2)
                            {
                                *pr2 -= *pr * coefficinet;
                            }
                        }
                    }

                    ++count;
                }
                // 行列の簡約化 ここまで


                // [i, i]要素が0の成分の繰り返し計算を省略する
                bool[] zero = new bool[reduced.GetLength(0)];
                for (int i = reduced.GetLength(0) - 1; i >= 0; --i)
                {
                    if (double.IsNaN(1.0 / reduced[i, i]) || double.IsInfinity(1.0 / reduced[i, i]))
                    {
                        zero[i] = true;
                    }
                }

                // ヤコビ法
                // 初期化
                for (double* pr = presult, endpr = presult + result.Length; pr != endpr; ++pr)
                { *pr = 1; }
                // ループ計算
                for (int loop = 0; loop < 20; ++loop)
                {
                    count = 0;
                    for (double* preRow = preduced, endpreRow = preduced + reduced.Length; preRow != endpreRow; preRow += reduced.GetLength(1))
                    {
                        if (!zero[count])
                        {
                            *(presult + count) = 0;

                            for (double* pre = preRow + count + 1, endpre = preRow + reduced.GetLength(1), pres = presult + count + 1; pre != endpre; ++pre, ++pres)
                            { *(presult + count) -= *pre * *pres; }
                        }

                        ++count;
                    }

                    // 発散しないように絶対値の最大値で割る
                    double absMax = Math.Abs(*presult);
                    for (double* pr = presult, endpr = presult + result.Length; pr != endpr; ++pr)
                    { absMax = absMax > Math.Abs(*pr) ? absMax : Math.Abs(*pr); }
                    for (double* pr = presult, endpr = presult + result.Length; pr != endpr; ++pr)
                    { *pr /= absMax; }
                }
                // ヤコビ法 ここまで
            }


            return result;
        }
    }
}
