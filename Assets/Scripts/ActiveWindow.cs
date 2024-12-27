using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWindow : MonoBehaviour
{
    [SerializeField] private int _numberWindow;

    [SerializeField] private SaveActiveWindow _saveActiveWindow;

    private void OnEnable()
    {
        _saveActiveWindow.SaveWindow(_numberWindow);
    }
}
