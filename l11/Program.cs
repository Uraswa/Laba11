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
                Game game = (Game)item;
                game.ShowVirtual();
                Console.WriteLine("------------------");
            }

            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();

            //клонирование очероеди
            Console.WriteLine("Произошло клонирование....");
            Queue newOchered = (Queue)ochered.Clone();

            //для полного копирования
            for(int i = 0; i < newOchered.Count; i++)
            {
                var deqItem = newOchered.Dequeue();
                var clonedItem = ((VRGame)deqItem).Clone();
                newOchered.Enqueue(clonedItem);
            }

            //сортируем очередь
            Queue sortedQueue = SortQueue(newOchered);

            VideoGame searchElement = new VRGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15, true, true);

            //проверка на наличие элемента
            bool containtment = ochered.Contains(searchElement);
            Console.WriteLine("Элемент: ");
            searchElement.ShowVirtual();
            if (containtment)
            {
                Console.WriteLine("Найден!");
            } else
            {
                Console.WriteLine("Не найден!");
            }
                        
            

            Console.WriteLine("Нажмите для продолжения....");
            Console.ReadKey();
        }

        /// <summary>
        /// Так как метода Sort у очереди изначально нет, приходится писать таким образом. Сортировка создает новую очередь,
        /// чтобы не потерять порядок исходных данных
        /// </summary>
        /// <param name="queue">Очередь для сортировки</param>
        static Queue SortQueue(Queue queue)
        {
            List<Game> listToSort = new List<Game>();
            foreach (var obj in queue)
            {
                listToSort.Add((Game)obj);
            }
            listToSort.Sort();

            Queue newQueue = new Queue(listToSort);
            return newQueue;
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
                Game game = (Game)item;
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

            Console.WriteLine($"Игры, минмальное количество игроков >= {minGreaterOrEqual.ToString()}:");
            bool found = false;
            foreach (Game game in Request1(collection, minGreaterOrEqual))
            {
                game.ShowVirtual();
                found = true;
                Console.WriteLine("-----------------------");
            }

            if (!found)
            {
                Console.WriteLine($"Игр с минимальным количеством игроков = {minGreaterOrEqual} не найдено.");
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
                Game game = (Game)item;
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

            bool found = false;
            Console.WriteLine($"\nИгры, в которые можно играть на {device}:");
            foreach (Game game in Request2(collection, device))
            {
                found = true;
                Console.WriteLine(game.Name);
            }

            if (!found)
            {
                Console.WriteLine($"Игр, в которые можно поиграть на {device} не найдено.");
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
                Game game = (Game)item;
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
            bool found = false;
            Console.WriteLine($"\nИгры, в которые можно играть в vr очках:");
            foreach (Game game in Request3(collection))
            {
                Console.WriteLine(game.Name);
                found = true;
            }

            if (!found)
            {
                Console.WriteLine($"Игр, в которые можно поиграть в VR очках не найдено.");
            }
        }

        /// <summary>
        /// Задание 2
        /// </summary>
        public static void Task2()
        {
            Console.Clear();
            Console.WriteLine("-----------ЗАДАНИЕ 2-----------");
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
                Game game = (Game)item;
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
                newList.Add((VideoGame)videoGame.Clone());
            }

            //сортировка клонированого объекта, чтобы не потерять порядок исходных данных
            newList.Sort();

            //поиска элемента
            VideoGame searchElement = new VRGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15, true, true);

            int searchResult = newList.BinarySearch(searchElement);
            if (searchResult >= 0)
            {
                newList[searchResult].ShowVirtual();
            } else
            {
                Console.WriteLine("Элемент не найден");
            }
        }

        public static void Task3()
        {
            var testColletions = new TestCollections();
        }


    }
}
