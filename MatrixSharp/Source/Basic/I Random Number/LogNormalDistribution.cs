﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 対数正規分布の乱数を生成するクラス.
    /// </summary>
    public class LogNormalDistribution : IRandomNumber
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
        double average, standardDeviation;

        bool even; // 計算回数

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogNormalDistribution()
        {
            // パラメータの設定
            seeds = new uint[2] { (uint)DateTime.Now.Millisecond, (uint)DateTime.Now.Minute };
            parameters = new double[2] { 0, 1 };

            // 内部パラメータの設定
            double[] udParameters = new double[2] { 0, 1 };
            ud1 = new UniformDistribution(seeds.Take(1).ToArray(), udParameters);
            ud2 = new UniformDistribution(seeds.Skip(1).ToArray(), udParameters);

            average = parameters[0];
            standardDeviation = parameters[1];

            even = true;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="setSeeds">乱数の種</param>
        /// <param name="setParameters">乱数のパラメータ</param>
        public LogNormalDistribution(uint[] setSeeds, double[] setParameters)
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

            average = parameters[0];
            standardDeviation = parameters[1];

            even = true;
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

            even = true;
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
            average = parameters[0];
            standardDeviation = parameters[1];
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
            double v1 = 2 * ud1.NextDouble() - 1;
            double v2 = 2 * ud2.NextDouble() - 1;
            double v = v1 * v1 + v2 * v2;

            if (v <= 0 || 1 <= v)
            { goto retry_point; }

            double w = Math.Sqrt(-2 * Math.Log(v) / v);

            double y1 = v1 * w;
            double y2 = v2 * w;

            even = !even;
            return even ? Math.Exp(y1 * standardDeviation + average) : Math.Exp(y2 * standardDeviation + average);
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
