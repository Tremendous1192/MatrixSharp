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
        /// 列ベクトルを取り出す
        /// </summary>
        /// <param name="column">列</param>
        /// <returns>列ベクトル ColumnVector</returns>
        /// <exception cref="ArgumentException">列の指定が正しくない</exception>
        public ColumnVector PickUpColumnVector(int column)
        {
            if (column < 0 || _array.GetLength(1) <= column)
            {
                throw new ArgumentException("列の指定が正しくありません。計算できません");
            }

            double[] pickedUp = new double[_row];
            for (int i = 0, lenI = _array.GetLength(0); i < lenI; i++)
            {
                pickedUp[i] = _array[i, column];
            }

            return new ColumnVector(pickedUp, false);
        }
    }
}
