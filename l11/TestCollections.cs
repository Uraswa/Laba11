using System.Diagnostics;
using l10;
using Laba10;

namespace Laba11;

public class TestCollections
{
    //количество итераций на каждый тип элемента: (начало, середина, конец, несущ.)
    private static long IterationsCount = 500;

    private Stopwatch _stopwatch;

    /// <summary>
    /// Измерение времени выполнения для типа элемента: (начало, середина, конец, несущ.)
    /// </summary>
    private struct Measure
    {
        public long findElementTimeQueue1; // время поиска в коллектион 1
        public long findElementTimeQueue2; // время поиска в коллектион 2
        public long findElementTimeInCollection3Value; // время поиска в коллектион 3 по значению

        public long findElementTimeInCollection3Key; // время поиска в коллектион 3 по ключу
        public long findElementTimeInCollection4Value; // время поиска в коллекции 4 по значению
        public long findElementTimeInCollection4Key; // время поиска в коллекции 4 по ключу
    }

    //collection 1
    public Queue<VideoGame> queue1 = new Queue<VideoGame>();
    //collection 2
    public Queue<string> queue2 = new Queue<string>();
    //collection 3
    public SortedDictionary<Game, VideoGame> GameToVideoGame = new SortedDictionary<Game, VideoGame>();
    //collection 4
    public SortedDictionary<string, VideoGame> StringToVideoGame = new SortedDictionary<string, VideoGame>();

    /// <summary>
    /// Тестирование коллекций по заданию выполняется в конструкторе
    /// </summary>
    public TestCollections()
    {
        _stopwatch = new Stopwatch();

        //инициализируем элементы так, чтобы можно было понять какой элемент с каким индексом стоит
        for (int i = 0; i < 1000; i++)
        {
            VideoGame videoGame = new VideoGame(
                "Игра" + (i + 1).ToString(), 1, 15,
                new Game.IdNumber(i + 1), Device.Console, 15
            );
            Game game = videoGame.Base;

            queue1.Enqueue(videoGame);
            queue2.Enqueue(videoGame.ToString());
            GameToVideoGame.Add(game, videoGame);
            StringToVideoGame.Add(videoGame.ToString(), videoGame);
        }


        //розыск
        VideoGame[] itemsToSearch = new VideoGame[]
        {
            (VideoGame)StringToVideoGame["Игра1"].Clone(),
            (VideoGame)StringToVideoGame["Игра501"].Clone(),
            (VideoGame)StringToVideoGame["Игра999"].Clone(),
            new VideoGame("Игра2000", 1, 15, new Game.IdNumber(2000), Device.Mobile, 15),
        };

        Measure[] results = new Measure[4];

        //процесс измерения
        for (int i = 0; i < IterationsCount; i++)
        {
            for (int j = 0; j < itemsToSearch.Length; j++)
            {
                VideoGame item = itemsToSearch[j];

                Measure measure = results[j];

                _stopwatch.Restart();
                bool contains = queue1.Contains(item);
                //Console.WriteLine(contains);
                measure.findElementTimeQueue1 += _stopwatch.ElapsedTicks;

                string videoGameToString = item.ToString();
                _stopwatch.Restart();
                contains = queue2.Contains(videoGameToString);
                //Console.WriteLine(contains);
                measure.findElementTimeQueue2 += _stopwatch.ElapsedTicks;

                _stopwatch.Restart();
                contains = GameToVideoGame.ContainsValue(item);
                //Console.WriteLine(contains);
                measure.findElementTimeInCollection3Value += _stopwatch.ElapsedTicks;

                _stopwatch.Restart();
                contains = GameToVideoGame.ContainsKey(item.Base);
                //Console.WriteLine(contains);
                measure.findElementTimeInCollection3Key += _stopwatch.ElapsedTicks;

                _stopwatch.Restart();
                contains = StringToVideoGame.ContainsValue(item);
                //Console.WriteLine(contains);
                measure.findElementTimeInCollection4Value += _stopwatch.ElapsedTicks;

                videoGameToString = item.ToString();
                _stopwatch.Restart();
                contains = StringToVideoGame.ContainsKey(videoGameToString);
                //Console.WriteLine(contains);
                measure.findElementTimeInCollection4Key += _stopwatch.ElapsedTicks;

                results[j] = measure;
            }
        }

        for (int i = 0; i < results.Length; i++)
        {
            string elementKey = "";
            if (i == 0)
            {
                elementKey = "первого";
            }
            else if (i == 1)
            {
                elementKey = "серединного";
            }
            else if (i == 2)
            {
                elementKey = "последнего";
            }
            else
            {
                elementKey = "несуществующего";
            }

            Measure measure = results[i];

            Console.WriteLine("Результаты по поиску " + elementKey + " элемента в разных коллекциях (ср время в тиках)");
            Console.WriteLine($"Среднее время поиска {elementKey} элемента в Queue<VideoGame> = {measure.findElementTimeQueue1 / IterationsCount}");
            Console.WriteLine($"Среднее время поиска {elementKey} элемента в Queue<string> = {measure.findElementTimeQueue2 / IterationsCount}");

            Console.WriteLine($"Среднее время поиска {elementKey} элемента в SortedDictionary<Game, VideoGame> по значению = {measure.findElementTimeInCollection3Value / IterationsCount}");
            Console.WriteLine($"Среднее время поиска {elementKey} элемента в SortedDictionary<Game, VideoGame> по ключу = {measure.findElementTimeInCollection3Key / IterationsCount}");

            Console.WriteLine($"Среднее время поиска {elementKey} элемента в SortedDictionary<string, VideoGame> по значению = {measure.findElementTimeInCollection4Value / IterationsCount}");
            Console.WriteLine($"Среднее время поиска {elementKey} элемента в SortedDictionary<string, VideoGame> по ключу = {measure.findElementTimeInCollection4Key / IterationsCount}");

            Console.WriteLine("------------------------------");
        }


    }
}