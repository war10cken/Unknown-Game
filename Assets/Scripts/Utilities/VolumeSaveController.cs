using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveController : MonoBehaviour
{
    [SerializeField] private Slider _volume;
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private Canvas _pauseWindow;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        Load();
    }

    public void Change(float value)
    {
        _amount.text = $"{Mathf.RoundToInt(value * 100)}";
        AudioListener.volume = value;
    }

    public void Save()
    {
        float value = _volume.value;
        PlayerPrefs.SetFloat("VolumeValue", value);
        Load();
        
        gameObject.SetActive(false);
        _pauseWindow.gameObject.SetActive(true);
    }

    private void Load()
    {
        float value = PlayerPrefs.GetFloat("VolumeValue");
        _volume.value = value;
        AudioListener.volume = value;
    }
}
