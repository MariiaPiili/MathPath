using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveActiveWindow : MonoBehaviour
{
    [SerializeField] private List<GameObject> _windowsList;

    [SerializeField] private Progress _progress;
    public void LoadWindow()
    {
        int numberWindow = PlayerPrefs.GetInt("window");
        for (int i = 0; i < _windowsList.Count; i++)
        {
            _windowsList[i].SetActive(false);
        }
        _windowsList[numberWindow].SetActive(true);
        if(numberWindow == 4)
        {
        _progress.ProgressCoalcilate(false);
        }
    }

    public void SaveWindow(int numberWindow)
    {
        PlayerPrefs.SetInt("window", numberWindow);

    }

    
}
