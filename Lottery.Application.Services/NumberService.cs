using Lottery.Application.Configuration.Options;
using Lottery.Domain.Abstract.Application;

using Microsoft.Extensions.Options;

namespace Lottery.Application.Services
{
    public class NumberService : INumberService
    {
        private readonly GeneratorOptions _options;
        private readonly Random _random = new Random();

        public NumberService(IOptions<GeneratorOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            if ( !_options.AllowDuplicates )
            {
                int range = _options.Max - _options.Min + 1;
                if ( _options.Count > range )
                {
                    throw new ArgumentException(
                        $"Count ({_options.Count}) cannot be greater than the range of numbers ({range}) when duplicates are not allowed.");
                }
            }
        }

        public int[] Generate( )
        {
            return _options.AllowDuplicates ? GenerateWithDuplicates( ) : GenerateUnique( );
        }

        private int[] GenerateWithDuplicates( )
        {
            var numbers = new int[_options.Count];
            for ( int i = 0 ; i < _options.Count ; i++ )
            {
                numbers[i] = _random.Next(_options.Min, _options.Max + 1);
            }
            return numbers;
        }

        private int[] GenerateUnique( )
        {
            int range = _options.Max - _options.Min + 1;
            /*
             * Adaptive optimization of numbers generation:
             *
             * If range between max and min is significantly larger than expected count, using a HashSet 
             * with collision checks is faster, as collisions remain rare.
             *
             * However, as Count approaches the size of the range, the probability of collisions increases dramatically
             * In such cases, a Fisher-Yates shuffle becomes more efficient due to its linear time complexity.
             *
             * The 20% threshold is based on empirical data and mathematical analysis of collisions probability from the Birthday paradox.
             * https://en.wikipedia.org/wiki/Birthday_problem
             *
             * Algorithms explained in more detail alongside their implementations
             */

            /* 
             * Possible improvement: Multithreading
             * 
             * If we could expect the Count to exceed very high values (e.g. over 100,000), it would also make sense to use multi-threading.
             * Random.Next is very fast CPU-bound operation. If we wanted to implement multi-threading, we could use the Random.Shared,
             * which is thread-safe form of Random.
             * For given specifications of this project, multithreading wouldn't make sence, thus not implemented.
             */
            return _options.Count <= range * 0.20 ? GenerateUniqueWithHashSet( ) : GenerateUniqueWithFisherYates( );
        }

        /*
         * Fisher-Yates Shuffle:
         * This approach maintains linear complexity O(n), where n is the range size.
         * When Count is close to the range size, creating and shuffling the entire collection 
         * is more efficient than repeatedly generating and checking for duplicates.
         * However for very small count and huge range, this approach may bring unnecessary memory usage and compute time.
         */
        private int[] GenerateUniqueWithFisherYates( )
        {
            var numbers = Enumerable.Range(_options.Min, _options.Max - _options.Min + 1).ToList();

            for ( int i = numbers.Count - 1 ; i > 0 ; i-- )
            {
                int j = _random.Next(0, i + 1);
                // Swap syntax (introduced from C# 7.0), used instead of creating temp var
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]); 
            }

            return numbers.Take(_options.Count).ToArray( );
        }

        /*
         * HashSet with Collision checks:
         * Optimal when count is significantly smaller than range
         * This approach may create collisions, but succeeding this prerequisite, collisions will be very rare (close to none).
         * However, the closer the count is to expected count, the more dramatic is probability of collisions, making this approach
         * nearly exponencially growing in compute time (checking collissions and regenerating on collisions)
         */
        private int[] GenerateUniqueWithHashSet( )
        {
            var numbers = new HashSet<int>();

            while ( numbers.Count < _options.Count )
            {
                int number = _random.Next(_options.Min, _options.Max + 1);
                numbers.Add(number); //Hashset ignores duplicate values on it's own
            }

            return numbers.ToArray( );
        }
    }
}
