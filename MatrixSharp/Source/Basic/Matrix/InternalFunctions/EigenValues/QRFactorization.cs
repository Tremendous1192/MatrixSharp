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
        /// ギブンス回転行列を用いたQR分解を計算する
        /// </summary>
        /// <param name="Q">直交行列Q</param>
        /// <param name="R">上三角行列R</param>
        /// <param name="array">上三角行列(別名右三角行列)R</param>
        internal static void QRFactorization(out double[,] Q, out double[,] R, in double[,] array)
        {
            // 初期化
            Q = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < Q.GetLength(0); ++i) { Q[i, i] = 1; }
            R = (double[,])array.Clone();

            // (i, j)成分を 0 にするギブンス回転行列を作成する
            // https://www.wikiwand.com/ja/%E3%82%AE%E3%83%96%E3%83%B3%E3%82%B9%E5%9B%9E%E8%BB%A2
            // (n-1, n-2)成分から0になるように計算すると、無駄なく上三角行列を計算できる。
            // http://hooktail.org/computer/index.php?QR%CB%A1
            bool naNCheck_ij = false, naNCheck_jj = false;
            fixed (double* pRfix = &R[R.GetLength(0) - 1, 0], endPRRow = &R[0, 0])
            {
                int i = R.GetLength(0) - 1;
                for (double* pRRow = pRfix; pRRow != endPRRow; pRRow -= R.GetLength(1))
                {
                    int j = i - 1;
                    for (double* prij = pRRow + j, prjj = prij - R.GetLength(1); prij != pRRow - 1; --prij, prjj -= R.GetLength(1) + 1)
                    {
                        // 回転角の決定
                        double theta = 0;
                        naNCheck_ij = double.IsNaN(1.0 / (*prij)) || double.IsInfinity(1.0 / (*prij));
                        naNCheck_jj = double.IsNaN(1.0 / (*prjj)) || double.IsInfinity(1.0 / (*prjj));

                        if (naNCheck_ij)// 0にしたい成分が既に 0 だった場合
                        { --j; continue; }

                        if (naNCheck_jj) // 対角成分が 0 の場合
                        { theta = Math.PI / 2.0; }
                        else
                        { theta = Math.Atan(-(*prij) / (*prjj)); }

                        // 行列の更新
                        Matrix.GivensRotationRightSide(ref Q, i, j, -theta);
                        Matrix.GivensRotation(ref R, i, j, theta);

                        --j;
                    }

                    --i;
                }
            }
        }
    }
}
