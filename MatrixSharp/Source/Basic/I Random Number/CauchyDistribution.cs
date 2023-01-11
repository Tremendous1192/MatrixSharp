using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// コーシー分布の乱数を生成するクラス.
    /// </summary>
    public class CauchyDistribution : IRandomNumber
    {
        uint[] seeds;
        double[] parameters;

        /// <summary>
        /// 一様分布のインスタンス
        /// </summary>
        UniformDistribution ud1, ud2;

        /// <summary>
        /// 乱数計算のパラメータ。配列呼び出し回数を減らすために用いる
        /// </summary>
        double location, scale;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CauchyDistribution()
        {
            // パラメータの設定
            seeds = new uint[2] { (uint)DateTime.Now.Millisecond, (uint)DateTime.Now.Minute };
            parameters = new double[2] { 0, 1 };

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);
            ud2 = new UniformDistribution(seeds.Skip(1).ToArray(), udParameters);

            location = parameters[0];
            scale = parameters[1];
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setSeeds">乱数の種</param>
        /// <param name="setParameters">乱数のパラメータ</param>
        public CauchyDistribution(uint[] setSeeds, double[] setParameters)
        {
            // パラメータの設定
            if (setSeeds != null)
            {
                if (setSeeds.Length < 2)
                {
                    seeds = new uint[2] { (uint)DateTime.Now.Millisecond, (uint)DateTime.Now.Minute };
                }
                else
                {
                    seeds = setSeeds.Take(2).ToArray();
                }
            }
            else
            {
                seeds = new uint[2] { (uint)DateTime.Now.Millisecond, (uint)DateTime.Now.Minute };
            }

            if (setParameters != null)
            {
                if (setParameters.Length < 2)
                {
                    parameters = new double[2] { 0, 1 };
                }
                else
                {
                    parameters = setParameters.Take(2).ToArray();
                    parameters[1] = Math.Abs(setParameters[1]);
                }
            }
            else
            {
                parameters = new double[2] { 0, 1 };
            }

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);
            ud2 = new UniformDistribution(seeds.Skip(1).ToArray(), udParameters);

            location = parameters[0];
            scale = parameters[1];
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
                if (setSeeds.Length < 2)
                {
                    seeds = new uint[2] { (uint)DateTime.Now.Millisecond, (uint)DateTime.Now.Minute };
                }
                else
                {
                    seeds = setSeeds.Take(2).ToArray();
                }
            }
            else
            {
                seeds = new uint[2] { (uint)DateTime.Now.Millisecond, (uint)DateTime.Now.Minute };
            }

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);
            ud2 = new UniformDistribution(seeds.Skip(1).ToArray(), udParameters);
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
                    parameters = setParameters.Take(2).ToArray();
                    parameters[1] = Math.Abs(setParameters[1]);
                }
            }
            else
            {
                parameters = new double[2] { 0, 1 };
            }

            // 内部パラメータの設定
            location = parameters[0];
            scale = parameters[1];
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
        retry_point:

            double u1 = ud1.NextDouble();
            double u2 = ud2.NextDouble();
            if (u2 == 0.5) { goto retry_point; }

            double v1 = 2 * u1 - 1;
            double v2 = 2 * u2 - 1;
            double w = v1 * v1 + v2 * v2;

            if (1 <= w) { goto retry_point; }

            double y = v1 / v2;

            return location + scale * y;
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
