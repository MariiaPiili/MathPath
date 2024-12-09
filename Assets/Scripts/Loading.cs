using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPage;
    [SerializeField] private GameObject _nextPage;
    [SerializeField] private float _loadingTime;

    private void Start()
    {
        StartCoroutine(LoadingMethod());
    }

    private IEnumerator LoadingMethod()
    {
        yield return new WaitForSeconds(_loadingTime);
        _loadingPage.SetActive(false);
        _nextPage.SetActive(true);
        yield return null;
    }
}
