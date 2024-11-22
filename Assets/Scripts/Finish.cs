using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Action OnFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnFinished?.Invoke();
        }
    }
}
