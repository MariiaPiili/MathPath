using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userNameReg;
    [SerializeField] private TMP_InputField _userEmailReg;
    [SerializeField] private TMP_InputField _userPasswordReg;

    [SerializeField] private TMP_InputField _userNameAuth;
    [SerializeField] private TMP_InputField _userPasswordAuth;

    public UnityEvent IsAuthorized;

    private string _userName;
    private string _userEmail;
    private string _userPassword;
    private bool _userAuth;

    private void Start()
    {
        Load();
    }

    public void Registration()
    {
        _userName = _userNameReg.text;
        _userEmail = _userEmailReg.text;
        _userPassword = _userPasswordReg.text;
        Save();
    }

    public void Authorization()
    {
        if (_userName == _userNameAuth.text && _userPassword == _userPasswordAuth.text)
        {
            _userAuth = true;
            IsAuthorized.Invoke();
        }
        else
        {
            Debug.Log("Authorization failed");
        }        
    }

    private void Save()
    {
        PlayerPrefs.SetString("name", _userName);
        PlayerPrefs.SetString("email", _userEmail);
        PlayerPrefs.SetString ("password", _userPassword);
    }

    private void Load()
    {
        _userName = PlayerPrefs.GetString("name");
        _userEmail = PlayerPrefs.GetString("email");
        _userPassword = PlayerPrefs.GetString("password");
    }
}
