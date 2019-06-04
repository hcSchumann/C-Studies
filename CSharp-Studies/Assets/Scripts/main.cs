using UnityEngine;

public class main : MonoBehaviour
{
    GameObject objectOne;
    GameObject objectTwo;
    GameObject objectOneThree;

    void Start()
    {
        PromisesTests.AsyncMakeBreakfast();
        Debug.Log("Já Chamei AsyncMakeBreakfast agora to esperando");

        // PromisesTests.AsyncFunction_WithAwaitOnTaskCreation_RunsOnParallel();
    }
}
