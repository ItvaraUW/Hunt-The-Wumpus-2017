using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusEngineTester
{
    /// <summary>
    /// Replacement for standard random number generator - produces known sequence of numbers
    /// Note - overrides int random generation only; calls to other methods
    /// will give normal behavior for Random object
    /// </summary>
    class NotRandom : Random
    {
        private int[] values;
        private int index;

        /// <summary>
        /// Creates a new instance of the NotRandom class that will generate the given
        /// ints in the given order (mapping them to specified ranges where necessary).
        /// </summary>
        /// <param name="args">integer</param>
        public NotRandom(params int[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Must provide at least one value to use");
            this.values = args;
            this.index = -1; // will be incremented before first use
        }

        public override int Next()
        {
            this.index = (this.index + 1) % this.values.Length;
            return this.values[this.index];
        }

        public override int Next(int maxValue)
        {
            return this.Next(0, maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            return minValue + this.Next() % (maxValue - minValue);
        }
    }
}