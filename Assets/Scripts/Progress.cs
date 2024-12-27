using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Progress : MonoBehaviour
{
    [SerializeField] private int _totalAnswers;
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _progressResilt;
    [SerializeField] private TextMeshProUGUI _progressHeading;
    [SerializeField] private TextMeshProUGUI _progressDescription;
    [SerializeField] private TextMeshProUGUI _welcomeToNextLevelHeading;
    [SerializeField] private TextMeshProUGUI _welcomeToNextLevelText;


    [Header("Awesome!")]
    [SerializeField] private string _headingAwesome;
    [SerializeField] private string _descriptionAwesome;

    [Header("Good effort!")]
    [SerializeField] private string _headingGood;
    [SerializeField] private string _descriptionGood;

    [Header("Keep going")]
    [SerializeField] private string _headingKeepGoing;
    [SerializeField] private string _descriptionKeepGoing;

    [SerializeField] private EmailSender _emailSender;
    [SerializeField] private AccountManager _accountManager;

    [SerializeField] private Color _colorGreat;
    [SerializeField] private Color _colorGood;
    [SerializeField] private Color _colorBad;

    private int _correctedAnswers;
    private int _incorrectedAnswers;

    public int incorrectedAnswers => _incorrectedAnswers;
    public int correctedAnswers => _correctedAnswers;

    private void Start()
    {
        Load();
    }

    public void CorectedAnswersIncrease()
    {
        _correctedAnswers++;
        Save();
    }

    public void InCorectedAnswersIncrease()
    {
        _incorrectedAnswers++;
        Save();
    }

    public void ResetPogress()
    {
        _correctedAnswers = 0;
        _incorrectedAnswers = 0;
        Save();
    }

    public void ProgressCoalcilate(bool calculate = true)
    {
        _progressResilt.text = $"{_correctedAnswers}/{_totalAnswers}";
        _progressBar.fillAmount = _correctedAnswers * 1.0f / _totalAnswers;
        if (_correctedAnswers > 7)
        {
            _progressHeading.text = _headingAwesome;
            _progressDescription.text = _descriptionAwesome.Replace("\\n", "\n"); ;
            _progressBar.color = _colorGreat;
            _progressHeading.color = _colorGreat;
        }
        else if (_correctedAnswers > 4 && _correctedAnswers <= 7)
        {
            _progressHeading.text = _headingGood;
            _progressDescription.text = _descriptionGood.Replace("\\n", "\n"); ;
            _progressBar.color = _colorGood;
            _progressHeading.color = _colorGood;
        }
        else
        {
            _progressHeading.text = _headingKeepGoing;
            _progressDescription.text = _descriptionKeepGoing.Replace("\\n", "\n");
            _progressBar.color = _colorBad;
            _progressHeading.color = _colorBad;
        }
        if (calculate)
        {
            _emailSender.SendEmail(_accountManager.UserEmail, "MathPath pgogress", $"{_accountManager.UserName} completed the level with {_correctedAnswers} corrected answers from {_totalAnswers}");
        }
        if (PlayerPrefs.GetInt("level") == 2)
        {
            _welcomeToNextLevelHeading.text = "Welcome to Level 2!";
            _welcomeToNextLevelText.text = "Get ready for even more exciting\\n math puzzles! New adventures\\n and challenges await. Let’s see\\n what you can do!";
            _welcomeToNextLevelText.text = _welcomeToNextLevelText.text.Replace("\\n", "\n");
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("win", _correctedAnswers);
        PlayerPrefs.SetInt("lost", _incorrectedAnswers);
    }

    private void Load()
    {
        _correctedAnswers = PlayerPrefs.GetInt("win");
        _incorrectedAnswers = PlayerPrefs.GetInt("lost");
    }
}
