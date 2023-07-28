using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLifeDestroy : MonoBehaviour
{
    public float LifeTime = 5f;

    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
    }
}
