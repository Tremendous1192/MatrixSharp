using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// 逆行列を計算する
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">正方行列ではない</exception>
        public Matrix Inverse()
        {
            if (this._row != this._column)
            {
                throw new ArgumentException("正方行列ではありません。計算できません");
            }

            // 簡単に計算できる場合
            if (this._row == 1)
            {
                double[,] calculated = new double[1, 1] { { 1.0 / _array[0, 0] } };
                return new Matrix(calculated, false);
            }
            if (this._row == 2)
            {
                double determinant = _array[0, 0] * _array[1, 1] - _array[0, 1] * _array[1, 0];
                double[,] calculated = new double[2, 2]
                {
                    { _array[1, 1] /determinant,-_array[0, 1]/determinant},
                    { - _array[1, 0]/determinant,_array[0, 0]/determinant}
                };
                return new Matrix(calculated, false);
            }


            // 3行3列以上の大きな行列
            double[,] sweeped = new double[this._array.GetLength(0), this._array.GetLength(1) * 2];
            double[,] inverse = new double[this._array.GetLength(0), this._array.GetLength(1)];
            double swap = 0;
            fixed (double* parray = this._array, psweeped = sweeped, pinverse = inverse)
            {
                // 初期化
                int count = 0;
                // sweeped[count / array.GetLength(1), count % array.GetLength(1)] = array[count / array.GetLength(1), count % array.GetLength(1)]
                for (double* pin = parray, endpin = parray + this._array.Length; pin != endpin; ++pin,++count)
                { *(psweeped + count / this._array.GetLength(1) * sweeped.GetLength(1) + count % this._array.GetLength(1)) = *pin; }
                for (int i = 0, lenI = sweeped.GetLength(0); i < lenI; ++i)
                { sweeped[i, i + this._array.GetLength(1)] = 1.0; }
                // 初期化 ここまで

                // 掃き出し法
                for (int i = 0, lenI = sweeped.GetLength(0); i < lenI; ++i)
                {
                    // sweeped[i, i] == 0 の場合、下の行と入れ替える
                    if (double.IsNaN(1.0 / sweeped[i, i]) || double.IsInfinity(1.0 / sweeped[i, i]))
                    {
                        for (int i2 = i + 1, lenI2 = sweeped.GetLength(0); i2 < lenI2; ++i2)
                        {
                            if (!double.IsNaN(1.0 / sweeped[i2, i2]) && !double.IsInfinity(1.0 / sweeped[i2, i2]))
                            {
                                // sweepの定義上、列数は偶数で確定しているので、ポインタの遷移は +2 である
                                fixed (double* psfix = &sweeped[i, 0], psfix2 = &sweeped[i2, 0])
                                {
                                    for (double* ps = psfix, ps2 = psfix2, endps = psfix + sweeped.GetLength(1); ps != endps;
                                        ps += 2, ps2 += 2)
                                    {
                                        swap = *ps;
                                        *ps = *ps2;
                                        *ps2 = swap;

                                        swap = *(ps + 1);
                                        *(ps + 1) = *(ps2 + 1);
                                        *(ps2 + 1) = swap;
                                    }
                                }
                                break;
                            }
                        }
                        // 行の入れ替えができなかった場合、次の行に移る
                        if (double.IsNaN(1.0 / sweeped[i, i]) || double.IsInfinity(1.0 / sweeped[i, i]))
                        { continue; }
                    }

                    // sweeped[i, i] を 1 にする
                    double devider = sweeped[i, i];
                    fixed (double* psfix = &sweeped[i, i])
                    {
                        for (double* ps = psfix, endps = psfix + sweeped.GetLength(1) - i; ps != endps; ++ps)
                        {
                            *ps /= devider;
                        }
                    }

                    // i 列目の成分を 0 にする
                    for (int i2 = 0; i2 < i; ++i2)
                    {
                        double coefficient = sweeped[i2, i];
                        fixed (double* psfix = &sweeped[i, 0], psfix2 = &sweeped[i2, 0])
                        {
                            for (double* ps = psfix, ps2 = psfix2, endps = psfix + sweeped.GetLength(1); ps != endps;
                                ps += 2, ps2 += 2)
                            {
                                *ps2 -= coefficient * *ps;
                                *(ps2 + 1) -= coefficient * *(ps + 1);
                            }
                        }
                    }
                    for (int i2 = i + 1, lenI2 = sweeped.GetLength(0); i2 < lenI2; ++i2)
                    {
                        double coefficient = sweeped[i2, i];
                        fixed (double* psfix = &sweeped[i, 0], psfix2 = &sweeped[i2, 0])
                        {
                            for (double* ps = psfix, ps2 = psfix2, endps = psfix + sweeped.GetLength(1); ps != endps;
                                ps += 2, ps2 += 2)
                            {
                                *ps2 -= coefficient * *ps;
                                *(ps2 + 1) -= coefficient * *(ps + 1);
                            }
                        }
                    }
                }
                // 掃き出し法 ここまで

                // 戻り値にデータを移す
                count = 0;
                // inverse[count / array.GetLength(1), count % array.GetLength(1)] = sweeped[count / array.GetLength(1), count % array.GetLength(1) + array.GetLength(1)]
                for (double* pout = pinverse, endpout = pinverse + inverse.Length; pout != endpout; ++pout,++count)
                { *pout = *(psweeped + count / this._array.GetLength(1) * sweeped.GetLength(1) + count % this._array.GetLength(1) + this._array.GetLength(1)); }
                // 戻り値にデータを移す ここまで
            }

            return new Matrix(inverse, false);
        }
    }
}
