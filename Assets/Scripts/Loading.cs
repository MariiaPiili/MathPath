using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPage;
    [SerializeField] private float _loadingTime;

    public GameObject NextPage;
    private void Start()
    {
        StartCoroutine(LoadingMethod());
    }

    private IEnumerator LoadingMethod()
    {
        yield return new WaitForSeconds(_loadingTime);
        _loadingPage.SetActive(false);
        NextPage.SetActive(true);
        yield return null;
    }
}
