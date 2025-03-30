using System.Diagnostics;

namespace _13_DZ_Parallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Применение разных способов распараллеливания задач и оценка оптимального способа реализации");

            int[] countArray = { 100000, 1000000, 10000000 };
            var watch = new Stopwatch();           

            foreach (int i in countArray)
            {
                var arr = Enumerable.Range(0, i).ToArray();

                Console.WriteLine($"\r\nРазмер: {i.ToString("n")}");

                watch.Restart();
                SequenceSum(arr);
                Console.WriteLine($"Последовательное выполнение: {watch.ElapsedMilliseconds} мс");
                watch.Stop();

                watch.Restart();
                PalallelLingSum(arr);
                Console.WriteLine($"Параллельное через Thread: {watch.ElapsedMilliseconds} мс");
                watch.Stop();

                watch.Restart();
                PalallelLingSum(arr);
                Console.WriteLine($"Параллельное с помощью LINQ: {watch.ElapsedMilliseconds} мс");
                watch.Stop();
            }

            Console.WriteLine($"Все посчитано");
            Console.ReadKey();
        }

        static void SequenceSum(int[] array)
        {
            float sum = 0;
            array.Sum(x => sum += x);
        }
        static void PalallelThreadSum(int[] array)
        {
            long sum = 0;           
            Parallel.ForEach(array, x => Interlocked.Add(ref sum, x));
        }

        static void PalallelLingSum(int[] array)
        {
            float sum = 0;
            array.AsParallel().Sum(x => sum += x);
        }
    }
}