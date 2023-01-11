using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// メルセンヌツイスターに基づく一様分布の乱数を生成するクラス.
    /// 乱数の周期は2^19937-1個であり,0.1ミリ秒毎に1度乱数を生成するとして, 1年間乱数を生成し続けるとしても2^39個の乱数列しか使用しないので、非常に余裕がある.
    /// </summary>
    public class UniformDistribution : IRandomNumber
    {
        uint[] seeds;
        double[] parameters;

        /// <summary>
        /// 乱数の種。配列呼び出し回数を減らすために用いる
        /// </summary>
        uint seed, X, Y, Z, W, T;

        /// <summary>
        /// 乱数計算のパラメータ。配列呼び出し回数を減らすために用いる
        /// </summary>
        double Min, Max, Range;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UniformDistribution()
        {
            // パラメータの設定
            seeds = new uint[1] { (uint)DateTime.Now.Millisecond };
            parameters = new double[2] { 0, 1 };

            // 内部パラメータの設定
            seed = seeds[0];
            X = 123456789;
            Y = (UInt32)(seed >> 32) & 0xFFFFFFFF;
            Z = (UInt32)(seed & 0xFFFFFFFF);
            W = X ^ Z;

            Min = parameters[0];
            Max = parameters[1];
            Range = Max - Min;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setSeeds">乱数の種(1次元)</param>
        internal UniformDistribution(uint[] setSeeds)
        {
            // パラメータの設定
            seeds = setSeeds.Take(1).ToArray() ?? new uint[1] { (uint)DateTime.Now.Millisecond };
            parameters = new double[2] { 0, 1 };

            // 内部パラメータの設定
            seed = seeds[0];
            X = 123456789;
            Y = (UInt32)(seed >> 32) & 0xFFFFFFFF;
            Z = (UInt32)(seed & 0xFFFFFFFF);
            W = X ^ Z;

            Min = parameters[0];
            Max = parameters[1];
            Range = Max - Min;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setSeeds">乱数の種(1次元)</param>
        /// <param name="setParameters">パラメータ. [0]最小値, [1]最大値 </param>
        public UniformDistribution(uint[] setSeeds, double[] setParameters)
        {
            // パラメータの設定
            seeds = setSeeds.Take(1).ToArray() ?? new uint[1] { (uint)DateTime.Now.Millisecond };
            if (setParameters != null)
            {
                if (setParameters.Length < 2)
                {
                    parameters = new double[2] { 0, 1 };
                }
                else
                {
                    double min = Math.Min(setParameters[0], setParameters[1]);
                    double max = Math.Max(setParameters[0], setParameters[1]);
                    parameters = new double[2] { min, max };
                }
            }
            else
            {
                parameters = new double[2] { 0, 1 };
            }

            // 内部パラメータの設定
            seed = seeds[0];
            X = 123456789;
            Y = (UInt32)(seed >> 32) & 0xFFFFFFFF;
            Z = (UInt32)(seed & 0xFFFFFFFF);
            W = X ^ Z;

            Min = parameters[0];
            Max = parameters[1];
            Range = Max - Min;
        }


        /// <summary>
        /// 乱数の種を設定する
        /// </summary>
        /// <param name="setSeeds"></param>
        public void SetSeeds(uint[] setSeeds)
        {
            seeds = setSeeds.Take(1).ToArray() ?? new uint[1] { (uint)DateTime.Now.Millisecond };
            seed = seeds[0];
        }

        /// <summary>
        /// 乱数の種を取得する
        /// </summary>
        /// <returns></returns>
        public uint[] GetSeeds()
        {
            return (uint[])seeds.Clone();
        }

        /// <summary>
        /// パラメータを設定する
        /// </summary>
        /// <param name="setParameters"></param>
        public void SetParameters(double[] setParameters)
        {
            if (setParameters != null)
            {
                if (setParameters.Length < 2)
                {
                    parameters = new double[2] { 0, 1 };
                }
                else
                {
                    double min = Math.Min(parameters[0], setParameters[1]);
                    double max = Math.Max(parameters[0], setParameters[1]);
                    parameters = new double[2] { min, max };
                }
            }
            else
            {
                parameters = new double[2] { 0, 1 };
            }

            Min = parameters[0];
            Max = parameters[1];
            Range = Max - Min;
        }

        /// <summary>
        /// パラメータを取得する
        /// </summary>
        /// <returns></returns>
        public double[] GetParameters()
        {
            return (double[])parameters.Clone();
        }

        /// <summary>
        /// 乱数を計算する
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            T = (X ^ (X << 11));
            X = Y;
            Y = Z;
            Z = W;
            W = (W = (W ^ (W >> 19)) ^ (T ^ (T >> 8)));

            return 1.0 * W / uint.MaxValue * Range + Min;
        }

        /// <summary>
        /// 乱数配列を計算する
        /// </summary>
        /// <param name="length">乱数列の長さ</param>
        /// <returns></returns>
        public double[] NextDoubles(int length)
        {
            double[] result = new double[Math.Max(1, length)];

            unsafe
            {
                fixed (double* presult = result)
                {
                    for (double* pre = presult, endpre = presult + result.Length; pre != endpre; ++pre)
                    {
                        *pre = this.NextDouble();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 乱数の二次元配列を計算する
        /// </summary>
        /// <param name="length0"></param>
        /// <param name="length1"></param>
        /// <returns></returns>
        public double[,] NextDoubles(int length0, int length1)
        {
            double[,] result = new double[Math.Max(1, length0), Math.Max(1, length1)];

            unsafe
            {
                fixed (double* presult = result)
                {
                    for (double* pre = presult, endpre = presult + result.Length; pre != endpre; ++pre)
                    {
                        *pre = this.NextDouble();
                    }
                }
            }

            return result;
        }
    }
}
