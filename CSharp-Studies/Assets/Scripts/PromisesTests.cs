
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class PromisesTests
{
    static public async void TestTasks()
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
}