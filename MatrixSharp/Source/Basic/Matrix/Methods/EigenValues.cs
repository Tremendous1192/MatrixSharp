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
        /// 固有値を計算する
        /// </summary>
        /// <param name="numberOfEigenValues">取得する固有値の数。行列の行数より大きい場合、全ての固有値を取得する</param>
        /// <param name="orderByAbs">固有値を絶対値の降順に並び替える</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double[] EigenValues(int numberOfEigenValues, bool orderByAbs)
        {
            if (this._row != this._column)
            {
                throw new ArgumentException("正方行列ではありません。計算できません");
            }
            if (numberOfEigenValues < 1)
            {
                throw new ArgumentException("取得する固有値の数は自然数を入力してください");
            }

            double[] eigenValues;

            // 小さい行列の固有値の計算
            if (_array.Length < 5)
            {
                eigenValues = Matrix.EigenValuesSmallSize(_array);
            }
            else
            {
                // 一般的なサイズの行列の固有値の計算
                eigenValues = Matrix.EigenValues(_array);
            }

            // 並べ替え
            if (orderByAbs)
            { eigenValues = Matrix.OrderByAbs(eigenValues); }
            else
            { eigenValues = Matrix.OrderByAbsDescending(eigenValues); }

            // 後ろ側のデータを取り除く
            if (numberOfEigenValues < eigenValues.Length)
            {
                return eigenValues.Select(x => x).Take(numberOfEigenValues).ToArray();
            }

            return eigenValues;
        }
    }
}
