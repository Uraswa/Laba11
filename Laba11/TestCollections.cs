﻿using System.Diagnostics;
using l10;
using Laba10;

namespace Laba11;

public class TestCollections
{
    private static long IterationsCount = 500;
    
    private struct Measure
    {
        public long findElementTimeQueue1;
        public long findElementTimeQueue2;
        public long findElementTimeInCollection3Value;
        
        public long findElementTimeInCollection3Key;
        public long findElementTimeInCollection4Value;
        public long findElementTimeInCollection4Key;
    }
    
    //collection 1
    public Queue<VideoGame> queue1 = new Queue<VideoGame>();
    //collection 2
    public Queue<string> queue2 = new Queue<string>();
    //collection 3
    public SortedDictionary<Game, VideoGame> GameToVideoGame = new SortedDictionary<Game, VideoGame>();
    //collection 4
    public SortedDictionary<string, VideoGame> StringToVideoGame = new SortedDictionary<string, VideoGame>();

    public TestCollections()
    {
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
            new VideoGame("Игра1", 1, 15, new Game.IdNumber(1), Device.Mobile, 15),
            new VideoGame("Игра501", 1, 15, new Game.IdNumber(501), Device.Mobile, 15),
            new VideoGame("Игра999", 1, 15, new Game.IdNumber(1000), Device.Mobile, 15),
            new VideoGame("Игра2000", 1, 15, new Game.IdNumber(2000), Device.Mobile, 15),
        };

        Measure[] results = new Measure[4];

        
        for (int i = 0; i < IterationsCount; i++)
        {
            for (int j = 0; j < itemsToSearch.Length; j++)
            {
                VideoGame item = itemsToSearch[j];

                Measure measure = results[j];
                
                measure.findElementTimeQueue1 += MeasureOperation(() => queue1.Contains(item));
                measure.findElementTimeQueue2 += MeasureOperation(() => queue2.Contains(item.ToString()));

                measure.findElementTimeInCollection3Value += MeasureOperation(() => GameToVideoGame.ContainsValue(item));
                measure.findElementTimeInCollection3Key += MeasureOperation(() => GameToVideoGame.ContainsKey(item));
        
                measure.findElementTimeInCollection4Value += MeasureOperation(() => StringToVideoGame.ContainsValue(item));
                measure.findElementTimeInCollection4Key += MeasureOperation(() => StringToVideoGame.ContainsKey(item.ToString()));


                results[j] = measure;
            }
        }

        for (int i = 0; i < results.Length; i++)
        {
            string elementKey = "";
            if (i == 0)
            {
                elementKey = "первого";
            } else if (i == 1)
            {
                elementKey = "серединного";
            } else if (i == 2)
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

    
    private long MeasureOperation(Action operation)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        operation();
        return stopwatch.ElapsedTicks;
    }
}