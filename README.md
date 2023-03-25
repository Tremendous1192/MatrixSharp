# MatrixSharp
It is simple matrix calculation package.
It provides matrix and vector and more like hand calculation.

# Constructor
```cs
using System;
// My package
using Tremendous1192.SelfEmployed.MatrixSharp;
namespace YourApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] array1 = new double[2, 3]{{1, 2, 3}, {4, 5, 6}};
            Matrix m = new Matrix(array1);

            double[] array2 = new double[2]{1, 2};
            ColumnVector c = new ColumnVector(array2);
            RowVector r = new RowVector(array2);
        }
    }
}
```

[Japanese read me is in Zenn](https://zenn.dev/tremendous1192/articles/824b2d32381173)

[GitHub](https://github.com/Tremendous1192/MatrixSharp)

[Nuget](https://www.nuget.org/packages/MatrixSharp/)

# Revision history
## ver 0.0.11
Cleaned comment out code.

Don't worry any user does NOT need to modify your code.

## ver 0.0.10
Modified the connection between public multiply method and internal function.

## ver 0.0.9
Modified the algorithm to solve homogeneous equation.

Don't worry any user does NOT need to modify your code.

## ver 0.0.8
Modify README

## ver 0.0.7
Append internal function of faster matrix subtract.

If your matrix element number (row x column) is multiples of 2, 3, or 4, 5, your calculation will be faster than usual.

## ver 0.0.6  
3/Feb./2023

Modify Clone Method of Matrix and Vector classes.

## ver.0.0.5
Append internal function faster matrix addition.

If your matrix element number (row x column) is multiples of 2, 3, or 4, 5, you are able to calculate matrix addition quickly.