using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// メルセンヌツイスターに基づく指数分布の乱数を生成するクラス.
    /// </summary>
    public class ExponentialDistribution : IRandomNumber
    {
        uint[] seeds;
        double[] parameters;

        /// <summary>
        /// 一様分布のインスタンス
        /// </summary>
        UniformDistribution ud1;

        /// <summary>
        /// 乱数計算のパラメータ。配列呼び出し回数を減らすために用いる
        /// </summary>
        double theta;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExponentialDistribution()
        {
            // パラメータの設定
            seeds = new uint[1] { (uint)DateTime.Now.Millisecond };
            parameters = new double[1] { 1 };

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);

            theta = parameters[0];
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setSeeds">乱数の種</param>
        /// <param name="setParameters">乱数のパラメータ</param>
        public ExponentialDistribution(uint[] setSeeds, double[] setParameters)
        {
            // パラメータの設定
            if (setSeeds != null)
            {
                if (setSeeds.Length < 1)
                {
                    seeds = new uint[1] { (uint)DateTime.Now.Millisecond };
                }
                else
                {
                    seeds = setSeeds.Take(1).ToArray();
                }
            }
            else
            {
                seeds = new uint[1] { (uint)DateTime.Now.Millisecond };
            }

            if (setParameters != null)
            {
                if (setParameters.Length < 1)
                {
                    parameters = new double[1] { 1 };
                }
                else
                {
                    parameters = setParameters.Take(1).ToArray();
                    parameters[0] = Math.Abs(parameters[0]);
                }
            }
            else
            {
                parameters = new double[1] { 1 };
            }

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);

            theta = parameters[0];
        }


        /// <summary>
        /// 乱数の種を設定する
        /// </summary>
        /// <param name="setSeeds"></param>
        public void SetSeeds(uint[] setSeeds)
        {
            // パラメータの設定
            if (setSeeds != null)
            {
                if (setSeeds.Length < 1)
                {
                    seeds = new uint[1] { (uint)DateTime.Now.Millisecond };
                }
                else
                {
                    seeds = setSeeds.Take(1).ToArray();
                }
            }
            else
            {
                seeds = new uint[1] { (uint)DateTime.Now.Millisecond };
            }

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);
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
                if (setParameters.Length < 1)
                {
                    parameters = new double[1] { 1 };
                }
                else
                {
                    parameters = setParameters.Take(1).ToArray();
                    parameters[0] = Math.Abs(parameters[0]);
                }
            }
            else
            {
                parameters = new double[1] { 1 };
            }

            // 内部パラメータの設定
            theta = parameters[0];
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
            return -Math.Log(Math.Max(0.000000000000001, 1 - ud1.NextDouble())) * theta;
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
