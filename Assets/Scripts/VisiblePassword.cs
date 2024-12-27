using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisiblePassword : MonoBehaviour
{
    [SerializeField] private Sprite _visiblePassword;
    [SerializeField] private Sprite _unvisiblePassword;

    [SerializeField] private Image _buttonImage;

    [SerializeField] private TMP_InputField _inputField;

    private bool _visible;

    private void Start()
    {
        _visible = true;
        SetVisible();
    }

    public void SetVisible()
    {
        _visible = !_visible;
        if (_visible)
        {
            _buttonImage.sprite = _unvisiblePassword;
            _inputField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            _buttonImage.sprite = _visiblePassword;
            _inputField.contentType = TMP_InputField.ContentType.Password;
        }

        _inputField.enabled= false;
        _inputField.enabled= true;
    }
}
