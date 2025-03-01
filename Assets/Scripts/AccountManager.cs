using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AccountManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userNameReg;
    [SerializeField] private TMP_InputField _userEmailReg;
    [SerializeField] private TMP_InputField _userPasswordReg;

    [SerializeField] private TMP_InputField _userNameAuth;
    [SerializeField] private TMP_InputField _userPasswordAuth;

    public UnityEvent IsAuthorized;

    [SerializeField] private GameObject _signUpPage;
    [SerializeField] private GameObject _signInPage;
    [SerializeField] private GameObject _nameIsLength;
    [SerializeField] private GameObject _nameIsLetter;
    [SerializeField] private GameObject _emailContainsAt;
    [SerializeField] private GameObject _passwordContinsLetter;
    [SerializeField] private GameObject _passwordContainsSymbol;
    [SerializeField] private GameObject _passwordContainsDigit;
    [SerializeField] private Loading _loading;

    [SerializeField] private Button _registration;

    private string _userName;
    private string _userEmail;
    private string _userPassword;
    //private bool _userAuth;

    private bool _isName;
    private bool _isEmail;
    private bool _isPassword;
    private bool _isToggled;

    public string UserEmail => _userEmail;
    public string UserName => _userName;

    private void Start()
    {
        Load();
        if(_userName != "" && _userEmail !="" && _userPassword != "")
        {
            _loading.NextPage = _signInPage;
        }
        else
        {
            _loading.NextPage = _signUpPage;
        }
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
        if (_userName == _userNameAuth.text && _userPassword == _userPasswordAuth.text && _userNameAuth.text != "" && _userPasswordAuth.text != "")
        {            
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

    private void IsActiveButton()
    {
        _registration.interactable = _isName && _isEmail && _isPassword && _isToggled;
    }

    public void InputName(string inputUserName)
    {
        bool isLength = inputUserName.Length >= 4;
        bool isLetter = inputUserName.All(char.IsLetter) && inputUserName.Length > 0;
        _isName = isLength && isLetter;
        _nameIsLength.SetActive(isLength);
        _nameIsLetter.SetActive(isLetter);
        IsActiveButton();
    }

    public void InputEmail(string inputUserEmail)
    {
        bool isContainsAt = inputUserEmail.Contains("@");
        _isEmail = isContainsAt;
        _emailContainsAt.SetActive(isContainsAt);
        IsActiveButton();
    }

    public void InputPassword(string inputUserPassword) 
    {
        bool containsLetters = inputUserPassword.Any(char.IsLetter); 
        bool containsDigits = inputUserPassword.Any(char.IsDigit);   
        bool containsSymbols = inputUserPassword.Any(c => !char.IsLetterOrDigit(c));
        _isPassword = containsLetters && containsDigits && containsSymbols;
        _passwordContinsLetter.SetActive(containsLetters);
        _passwordContainsSymbol.SetActive(containsSymbols);
        _passwordContainsDigit.SetActive(containsDigits);
        IsActiveButton();
    }

    public void InputToggle(bool toggle)
    {        
        _isToggled = toggle;
        IsActiveButton() ;
    }
}
