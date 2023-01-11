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
        /// 固有ベクトル
        /// </summary>
        /// <param name="numberOfEigenValues">計算する固有ベクトルの数</param>
        /// <param name="orderByAbs">固有値の絶対値の昇順に取得する</param>
        /// <returns>固有ベクトルの集合 List of ColumnVector </returns>
        /// <exception cref="ArgumentException"></exception>
        public List<ColumnVector> EigenVectors(int numberOfEigenValues, bool orderByAbs)
        {
            if (this._row != this._column)
            {
                throw new ArgumentException("正方行列ではありません。計算できません");
            }
            if (numberOfEigenValues < 1)
            {
                throw new ArgumentException("取得する固有値の数は自然数を入力してください");
            }

            double[] eigenValues = this.EigenValues(numberOfEigenValues, orderByAbs);

            double[,] tempArray = (double[,])_array.Clone();

            List<ColumnVector> result = new List<ColumnVector>();
            foreach (double e in eigenValues)
            {
                for (int i = 0; i < tempArray.GetLength(0); ++i)
                {
                    tempArray[i, i] = _array[i, i] - e;
                }

                result.Add(new ColumnVector(Matrix.SolveHomogeneousEquation(tempArray)));
            }

            return result;
        }
    }
}
