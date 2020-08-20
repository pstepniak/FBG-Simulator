using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.FBGManagement
{
    public struct DecComplex
    {
        // Member variables.
        private decimal real;
        private decimal imaginary;


        // Read-only properties.
        public decimal Real { get { return real; } }
        public decimal Imaginary { get { return imaginary; } }


        // Constructors.
        public DecComplex(decimal real, decimal imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public DecComplex(double real, double imaginary)
        {
            this.real = (decimal)real;
            this.imaginary = (decimal)imaginary;
        }

        public static DecComplex ImaginaryOne = new DecComplex(0m, 1m);


        // Arithmetic operators.
        public static DecComplex operator -(DecComplex value)
        {
            return new DecComplex(-value.real, -value.imaginary);
        }

        public static DecComplex operator +(DecComplex left, DecComplex right)
        {
            return new DecComplex(left.real + right.real, left.imaginary + right.imaginary);
        }

        public static DecComplex operator -(DecComplex left, DecComplex right)
        {
            return new DecComplex(left.real - right.real, left.imaginary - right.imaginary);
        }

        public static DecComplex operator *(DecComplex left, DecComplex right)
        {
            return new DecComplex(left.real * right.real - left.imaginary * right.imaginary, left.real * right.imaginary + left.imaginary * right.real);
        }

        public static DecComplex operator *(DecComplex left, decimal right)
        {
            return new DecComplex(left.real * right, left.imaginary * right);
        }

        public static DecComplex operator *(DecComplex left, double right)
        {
            return new DecComplex(left.real * (decimal)right, left.imaginary * (decimal)right);
        }

        public static DecComplex operator /(DecComplex left, DecComplex right)
        {
            var denominator = right.real * right.real + right.imaginary * right.imaginary;
            var real = (left.real / denominator * right.real + left.imaginary / denominator * right.imaginary);
            var imaginary = (left.imaginary / denominator * right.real - left.real / denominator * right.imaginary);
            return new DecComplex(real, imaginary);
        }

        public static DecComplex operator /(decimal left, DecComplex right)
        {
            var denominator = right.real * right.real + right.imaginary * right.imaginary;
            var real = left * right.real / denominator;
            var imaginary = -left * right.imaginary / denominator;
            return new DecComplex(real, imaginary);
        }

        public static DecComplex operator /(double left, DecComplex right)
        {
            var denominator = right.real * right.real + right.imaginary * right.imaginary;
            var real = (decimal)left * right.real / denominator;
            var imaginary = -(decimal)left * right.imaginary / denominator;
            return new DecComplex(real, imaginary);
        }


        // Conversion operators.
        public static explicit operator System.Numerics.Complex(DecComplex value)
        {
            return new System.Numerics.Complex((double)value.Real, (double)value.Imaginary);
        }


        // Methods.
        public static decimal Abs(DecComplex value)
        {
            return Sqrt(value.real * value.real + value.imaginary * value.imaginary);
        }

        public static DecComplex Conj(DecComplex value)
        {
            return new DecComplex(value.Real, value.Imaginary * -1);
        }

        public static DecComplex Pow(DecComplex value, int exponent)
        {
            if (exponent == 0)
                return new DecComplex(1.0, 0.0);

            var result = value;
            for (var i = 1; i < exponent; i++)
            {
                result = result * value;
            }

            if (exponent < 0)
                return 1.0M / result;
            else
                return result;
        }

        public override string ToString()
        {
            return string.Format("({0}; {1})", this.real, this.imaginary);
        }


        // Sqrt-Method for the decimal class (by SLenik, http://stackoverflow.com/a/6755197/4469336).
        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        public static DecComplex Sinh(DecComplex z)
        {
            return new DecComplex(Math.Sinh((double)z.real) * Math.Cos((double)z.imaginary), Math.Cosh((double)z.real) * Math.Sin((double)z.imaginary));
        }

        public static DecComplex Cosh(DecComplex z)
        {
            return new DecComplex(Math.Cosh((double)z.real) * Math.Cos((double)z.imaginary), Math.Sinh((double)z.real) * Math.Sin((double)z.imaginary));
        }
    }
}
