using TMPro;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cursor;

    private float _allTime;

    private void Update()
    {
        _allTime += Time.deltaTime;
        if (_allTime > 1)
        {
            if (_cursor.text == "_")
            {
                _cursor.text = "";
            }
            else
            {
                _cursor.text = "_";
            }
            _allTime = 0;
        }
    }
}
