using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.Init();
    }
    private void Update()
    {
        GameManager.Instance.Update(Time.deltaTime);
    }

    private void OnDisable()
    {
        GameManager.Instance.Relesae();
    }
}
