using UnityEngine;

public class ReturnControl : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPage;
    [SerializeField] private GameObject _continuesPage;

    private int _count = 0;

    private void Start()
    {
        _count = PlayerPrefs.GetInt("Return");
        if (_count > 0)
        {
            _continuesPage.SetActive(true);
            _loadingPage.SetActive(false);
        }
        else
        {
            _continuesPage.SetActive(false);
            _loadingPage.SetActive(true);
        }
        _count++;
        PlayerPrefs.SetInt("Return", _count);
    }
}
