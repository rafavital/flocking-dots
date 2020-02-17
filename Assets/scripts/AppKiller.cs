using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppKiller : MonoBehaviour
{

    [SerializeField] private float timeToKill = 10f;
    private float timer;

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToKill) {
            Application.Quit ();
        }

    }
}
