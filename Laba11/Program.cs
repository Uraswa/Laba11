using System.Collections;
using l10;
using Laba10;

namespace Laba11
{
    class Program
    {
        private const int StandartAmount = 10;
        
        public static void Main()
        {
            Task1();
            Task2();
            Task3();
        }


        /// <summary>
        /// Задание 1
        /// </summary>
        public static void Task1()
        {
            Queue ochered = new Queue();
            for (int i = 0; i < StandartAmount; i++)
            {
                ochered.Enqueue(new VRGame("Игра" + (i + 1), 1, 15, new Game.IdNumber(i + 1), Device.Mobile, 15, true, true));
            }
            
            //запросы
            Request1Wrapper(ochered);
            Request2Wrapper(ochered);
            Request3Wrapper(ochered);

            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();
            
            Console.WriteLine("Вывод элементов");

            foreach (var item in ochered)
            {
                Game game = (Game) item;
                game.ShowVirtual();
                Console.WriteLine("------------------");
            }
            
            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();

            //клонирование очероеди
            Console.WriteLine("Произошло клонирование....");
            Queue newOchered = (Queue)ochered.Clone();
            
            //сортируем очередь
            SortQueue(newOchered);

            VideoGame searchElement = new VRGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15, true, true);

            foreach (var item in ochered)
            {
                if (item.Equals(searchElement) && item is Game)
                {
                    Game game = (Game) item;
                    game.ShowVirtual();
                    break;
                }
            }
        }

        /// <summary>
        /// Так как метода Sort у очереди изначально нет, приходится писать таким образом
        /// </summary>
        /// <param name="queue">Очередь для сортировки</param>
        static void SortQueue(Queue queue)
        {
            List<Game> listToSort = new List<Game>();
            foreach (var obj in queue)
            {
                listToSort.Add((Game)obj);
            }
            listToSort.Sort();

            queue.Clear();
            foreach (var game in listToSort)
            {
                queue.Enqueue(game);
            }
        }
        
        /// <summary>
        /// Запрос из задания 1. Возвращает игры, в которых мин кол-во игроков >= minGreaterOrEqual
        /// </summary>
        /// <param name="collection">коллекция для поиска</param>
        /// <param name="minGreaterOrEqual">Минимальное количество игроков</param>
        /// <returns></returns>
        static IEnumerable<Game> Request1(IEnumerable collection, uint minGreaterOrEqual)
        {
            foreach (var item in collection)
            {
                Game game = (Game) item;
                if (game.MinimumPlayers >= minGreaterOrEqual && minGreaterOrEqual <= game.MaximumPlayers)
                {
                    yield return game;
                }
            }
        }

        /// <summary>
        /// Пользовательская обертка над методом Request1
        /// </summary>
        /// <param name="collection">Коллекция для поиска</param>
        static void Request1Wrapper(IEnumerable collection)
        {
            uint minGreaterOrEqual = Helpers.Helpers.EnterUInt("Минимальное количество игроков", 1, uint.MaxValue);

            Console.WriteLine($"Игры, минмальное количество игроков >= {minGreaterOrEqual.ToString()}");
            foreach (Game game in Request1(collection, minGreaterOrEqual))
            {
                game.ShowVirtual();
                Console.WriteLine("-----------------------");
            }
        }

        /// <summary>
        /// Запрос 2 из задания. Возвращает игры, в которые можно поиграть на device
        /// </summary>
        /// <param name="collection">Коллекция для поиска</param>
        /// <param name="device">Искомое устройство</param>
        /// <returns></returns>
        static IEnumerable<Game> Request2(IEnumerable collection, Device device)
        {
            foreach (var item in collection)
            {
                Game game = (Game) item;
                if (game is VideoGame videoGame && videoGame.Device == device)
                {
                    yield return game;
                }
            }
        }

        /// <summary>
        /// Пользовательская обертка над Request2
        /// </summary>
        /// <param name="collection">Коллекция для поиска</param>
        static void Request2Wrapper(IEnumerable collection)
        {
            Console.WriteLine();
            uint deviceType = Helpers.Helpers.EnterUInt("тип устройства: 1 - телефон, 2 - пк, 3 - игровая консоль", 1, 3);
            Device device = Device.Mobile;

            if (deviceType == 1)
            {
                device = Device.Mobile;
            }
            else if (deviceType == 2)
            {
                device = Device.Pc;
            }
            else if (deviceType == 3)
            {
                device = Device.Console;
            }

            Console.WriteLine($"\nИгры, в которые можно играть на {device}");
            foreach (Game game in Request2(collection, device))
            {
                Console.WriteLine(game.Name);
            }
        }

        /// <summary>
        /// Запрос 3. Возвращает игры, в которые можно поиграть на VR очках
        /// </summary>
        /// <param name="collection">Коллекция для поиска</param>
        /// <returns></returns>
        static IEnumerable<Game> Request3(IEnumerable collection)
        {
            foreach (var item in collection)
            {
                Game game = (Game) item;
                if (game is VRGame vr && vr.AreVRGlassesRequired)
                {
                    yield return game;
                }
            }
        }

        /// <summary>
        /// Пользовательская обертка над Request3
        /// </summary>
        /// <param name="collection">Коллекция для поиска</param>
        static void Request3Wrapper(IEnumerable collection)
        {
            Console.WriteLine($"\nИгры, в которые можно играть в vr очках");
            foreach (Game game in Request3(collection))
            {
                Console.WriteLine(game.Name);
            }
        }

        /// <summary>
        /// Задание 2
        /// </summary>
        public static void Task2()
        {
            //заполнение листа
            List<VideoGame> list = new List<VideoGame>();
            for (int i = 0; i < StandartAmount; i++)
            {
                list.Add(new VRGame("Игра" + (i + 1), 1, 15, new Game.IdNumber(i + 1), Device.Mobile, 15, true, true));
            }
            
            //выполняем запросы уже к list
            Request1Wrapper(list);
            Request2Wrapper(list);
            Request3Wrapper(list);

            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();
            
            Console.WriteLine("Вывод элементов");

            foreach (var item in list)
            {
                Game game = (Game) item;
                game.ShowVirtual();
                Console.WriteLine("------------------");
            }
            
            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();

            Console.WriteLine("Произошло клонирование....");
            
            //клонирование через цикл foreach, так как метод Clone для List не определен
            List<VideoGame> newList = new List<VideoGame>();
            foreach (var videoGame in list)
            {
                newList.Add(videoGame);
            }
            
            //сортировка
            list.Sort();

            //поиска элемента
            VideoGame searchElement = new VRGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15, true, true);
 
            foreach (var item in list)
            {
                if (item.Equals(searchElement) && item is Game game)
                {
                    game.ShowVirtual();
                    break;
                }
            }
        }

        public static void Task3()
        {
            var testColletions = new TestCollections();
        }
        
        
    }
}
