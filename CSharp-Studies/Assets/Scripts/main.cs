using UnityEngine;

public class main : MonoBehaviour
{
    GameObject objectOne;
    GameObject objectTwo;
    GameObject objectOneThree;

    void Start()
    {
        PromisesTests.AsyncMakeBreakfast();

        // PromisesTests.AsyncFunction_WithAwaitOnTaskCreation_RunsOnParallel();
        // Debug.Log("Já Chamei agora to esperando");
    }
}
