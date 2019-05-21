
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


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
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(" Doing: PourCoffee");
        }
        return new Coffee();
    }

    static async private Task<Egg> FryEggs(int amount)
    {
        var eggs = await new Task<Egg>(() =>
        {
            for (int i = 0; i < 100 * amount; i++)
            {
                Debug.Log(" Doing: Eggs");
            }
            return new Egg();
        });

        return eggs;
    }

    static async private Task<Bacon> FryBacon(int amount)
    {
        var bacon = await new Task<Bacon>(() =>
        {
            for (int i = 0; i < 100 * amount; i++)
            {
                Debug.Log(" Doing: Bacon");
            }

            return new Bacon();
        });

        return bacon;
    }

    static async private Task<Toast> ToastBread(int amount)
    {
        var toast = await new Task<Toast>(() =>
        {
            for (int i = 0; i < 100 * amount; i++)
            {
                Debug.Log(" Doing: Toast");
            }
            Task.CompletedTask();
            return new Toast();
        });

        return toast;
    }

    static private Juice PourOJ()
    {
        for (int i = 0; i < 100; i++)
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

        Debug.Log("Tasks Created, but not working.... https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/async/#feedback");

        Egg eggs = await eggTask;
        Debug.Log("eggs are ready");

        Bacon bacon = await baconTask;
        Debug.Log("bacon is ready");

        Toast toast = await toastTask;
        Debug.Log("toast is ready");

        Juice oj = PourOJ();
        Debug.Log("oj is ready");

        Debug.Log("Breakfast is ready!");
    }
}