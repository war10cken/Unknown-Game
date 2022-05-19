using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Player.Player _player;
    
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (_player.TryGetComponent(out AudioSource audioSource))
            {
                audioSource.enabled = false;
            }
        
            Time.timeScale = 0;
        }
    }

    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }
}
