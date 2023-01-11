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
        /// 行ベクトルを取り出す
        /// </summary>
        /// <param name="row">行</param>
        /// <returns>行ベクトル RowVector</returns>
        /// <exception cref="ArgumentException">行の指定が正しくない</exception>
        public RowVector PickUpRowVector(int row)
        {
            if (row < 0 || _array.GetLength(0) <= row)
            {
                throw new ArgumentException("行の指定が正しくありません。計算できません");
            }

            double[] pickedUp = new double[_column];
            for (int j = 0, lenJ = _array.GetLength(1); j < lenJ; j++)
            {
                pickedUp[j] = _array[row, j];
            }

            return new RowVector(pickedUp, false);
        }
    }
}
