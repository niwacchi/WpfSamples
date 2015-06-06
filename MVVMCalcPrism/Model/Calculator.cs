using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalcPrism.Model
{
    public class Calculator
    {
        private static readonly Dictionary<CalculateType, Func<double, double, double>> calcMap = new Dictionary<CalculateType, Func<double, double, double>>
        {
            {
                CalculateType.None,
                (x,y) =>
                {
                    throw new InvalidOperationException();
                }
            },
            {
                CalculateType.Add, (x,y) => x + y
            },
            {
                CalculateType.Sub, (x,y) => x - y
            },
            {
                CalculateType.Mul, (x,y) => x * y
            },
            {
                CalculateType.Div, (x,y) => x / y
            }
        };

        public double Execute(double x, double y, CalculateType op)
        {
            return calcMap[op](x, y);
        }
    }
}
