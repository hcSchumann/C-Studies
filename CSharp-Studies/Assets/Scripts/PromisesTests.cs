
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

class PromiseDummy
{
    // public TaskAwaiter GetAwaiter()
    // {
    //     return new TaskAwaiter();
    // }
}

class Coffee : PromiseDummy
{ }
class Egg : PromiseDummy
{ }
class Bacon : PromiseDummy
{ }
class Toast : PromiseDummy
{ }
class Juice : PromiseDummy
{ }

public class PromisesTests
{
    private const int waitTime = 10000;
    static public async void AsyncFunction_WithAwaitOnTaskCreation_RunsOnParallel()
    {
        var promise = new TaskCompletionSource<int>();

        await Task.Factory.StartNew(() =>
        {
            for (int i = 0; i < 1000; i++)
            {
                Debug.Log(i);
            }
            promise.TrySetResult(7);
        });


        var myResult = promise.Task.Result;

        Debug.Log("Final result should be 7: " + myResult);
    }

    static private Coffee PourCoffee()
    {
        for (int i = 0; i < waitTime; i++)
        {
            Debug.Log(" Doing: PourCoffee");
        }
        return new Coffee();
    }

    static private Task<Egg> FryEggs(int amount)
    {
        Task<Egg> eggs = Task.Run(() =>
       {
           for (int i = 0; i < waitTime * amount; i++)
           {
               Debug.Log(" Doing: Eggs");
           }
           return new Egg();
       });

        return eggs;
    }

    static private Task<Bacon> FryBacon(int amount)
    {
        var bacon = Task.Run(() =>
        {
            for (int i = 0; i < waitTime * amount; i++)
            {
                Debug.Log(" Doing: Bacon");
            }

            return new Bacon();
        });

        return bacon;
    }

    static private Task<Toast> ToastBread(int amount)
    {
        var toast = Task.Run(() =>
        {
            for (int i = 0; i < waitTime * amount; i++)
            {
                Debug.Log(" Doing: Toast");
            }
            return new Toast();
        });

        return toast;
    }

    static private Juice PourOJ()
    {
        for (int i = 0; i < waitTime; i++)
        {
            Debug.Log(" Doing: PourOJ");
        }
        return new Juice();
    }

    static public async Task AsyncMakeBreakfast()
    {
        Coffee cup = PourCoffee();
        Debug.Log("coffee is ready");

        Task<Egg> eggTask = FryEggs(2);
        Task<Bacon> baconTask = FryBacon(3);
        Task<Toast> toastTask = ToastBread(2);

        // This implementation doesnt care about the order of the tasks. Runs fully paralel
        var allTasks = new List<Task> { eggTask, baconTask, toastTask };
        while (allTasks.Count > 0)
        {
            Task finished = await Task.WhenAny(allTasks);
            if (finished == eggTask)
            {
                Debug.Log("eggs are ready");
                allTasks.Remove(eggTask);
                Egg eggs = await eggTask;
            }
            else if (finished == baconTask)
            {
                Debug.Log("bacon is ready");
                allTasks.Remove(baconTask);

                Bacon bacon = await baconTask;
            }
            else if (finished == toastTask)
            {
                Debug.Log("toast is ready");
                allTasks.Remove(toastTask);

                Toast toast = await toastTask;
            }
        }


        // // This implementation awaits the completion of each tasks in the order they appear.
        // Egg eggs = await eggTask;
        // Debug.Log("eggs are ready");

        // Bacon bacon = await baconTask;
        // Debug.Log("bacon is ready");

        // Toast toast = await toastTask;
        // Debug.Log("toast is ready");


        Juice oj = PourOJ();
        Debug.Log("oj is ready");

        Debug.Log("Breakfast is ready!");
    }
}