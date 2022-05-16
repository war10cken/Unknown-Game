using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Won : MonoBehaviour
{
    [SerializeField] private Canvas _ui;
    [SerializeField] private Canvas _wonWindow;

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0;
        
        _ui.gameObject.SetActive(false);
        _wonWindow.gameObject.SetActive(true);
    }
}
