using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 乱数を生成するインターフェース。
    /// 基本となる一様乱数はメルセンヌ・ツイスタに従って生成する。
    /// </summary>
    public partial interface IRandomNumber
    {
        /// <summary>
        /// 乱数の種を設定する
        /// </summary>
        /// <param name="setSeeds"></param>
        void SetSeeds(uint[] setSeeds);

        /// <summary>
        /// 乱数の種を取得する
        /// </summary>
        /// <returns></returns>
        uint[] GetSeeds();

        /// <summary>
        /// パラメータを設定する
        /// </summary>
        /// <param name="setParameters"></param>
        void SetParameters(double[] setParameters);

        /// <summary>
        /// パラメータを取得する
        /// </summary>
        /// <returns></returns>
        double[] GetParameters();

        /// <summary>
        /// 乱数を計算する
        /// </summary>
        /// <returns></returns>
        double NextDouble();

        /// <summary>
        /// 乱数配列を計算する
        /// </summary>
        /// <param name="length">乱数列の長さ</param>
        /// <returns></returns>
        double[] NextDoubles(int length);

        /// <summary>
        /// 乱数の二次元配列を計算する
        /// </summary>
        /// <param name="length0"></param>
        /// <param name="length1"></param>
        /// <returns></returns>
        double[,] NextDoubles(int length0, int length1);
    }
}
