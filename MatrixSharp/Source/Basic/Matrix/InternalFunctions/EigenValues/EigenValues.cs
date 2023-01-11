using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        internal static double[] EigenValues(double[,] array)
        {
            //以下、3行以上の固有値計算
            // 行列の初期化
            double[,] An = (double[,])array.Clone();
            double lambda = 0;

            // QR法のループ
            double[,] Q, R;
            for (int loop = 0; loop < 50; loop++)
            {
                // レイリー商シフト
                lambda = An[An.GetLength(0) - 1, An.GetLength(1) - 1];

                for (int i = 0; i < An.GetLength(0); i++) { An[i, i] -= lambda; }
                Matrix.QRFactorization(out Q, out R, An);
                An = Matrix.MultiplyIJ1K1(R, Q);
                for (int i = 0; i < An.GetLength(0); i++) { An[i, i] += lambda; }
            }

            // 固有値を戻り値のリストにコピーする
            List<double> eigenValuesList = new List<double>(An.GetLength(0));
            for (int i = 0; i < An.GetLength(0); i++) { eigenValuesList.Add(An[i, i]); }

            // 重複を取り除く
            double threshold = Math.Pow(10, -13); // 閾値(doubleの有効数字は15～17桁のため)
            double elementTemp = 0;
            for (int i = 0; i < eigenValuesList.Count - 1; i++)
            {
                elementTemp = eigenValuesList[i];
                for (int j = eigenValuesList.Count - 1; j > i; j--)
                {
                    if (Math.Abs(elementTemp - eigenValuesList[j]) < threshold)
                    {
                        eigenValuesList.RemoveAt(j);
                    }
                }
            }

            return eigenValuesList.ToArray();
        }
    }
}
