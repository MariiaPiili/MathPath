using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private int _totalAnswers;
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _progressResilt;
    [SerializeField] private TextMeshProUGUI _progressHeading;
    [SerializeField] private TextMeshProUGUI _progressDescription;

    [Header("Awesome!")]
    [SerializeField] private string _headingAwesome;
    [SerializeField] private string _descriptionAwesome;

    [Header("Good effort!")]
    [SerializeField] private string _headingGood;
    [SerializeField] private string _descriptionGood;

    [Header("Keep going")]
    [SerializeField] private string _headingKeepGoing;
    [SerializeField] private string _descriptionKeepGoing;

    private int _correctedAnswers;
    private int _incorrectedAnswers;       

    public void CorectedAnswersIncrease()
    {
        _correctedAnswers++;
    }

    public void InCorectedAnswersIncrease()
    {
        _incorrectedAnswers++;
    }

    public void ResetPogress()
    {
        _correctedAnswers = 0;
        _incorrectedAnswers = 0;
    }

    public void ProgressCoalcilate()
    {
        _progressResilt.text = $"{_correctedAnswers}/{_totalAnswers}";
        _progressBar.fillAmount = _correctedAnswers*1.0f / _totalAnswers;
        if (_correctedAnswers > 7)
        {
            _progressHeading.text = _headingAwesome;
            _progressDescription.text = _descriptionAwesome;
        }
        else if(_correctedAnswers > 4 && _correctedAnswers <= 7)
        {
            _progressHeading.text = _headingGood;
            _progressDescription.text = _descriptionGood;
        }
        else
        {
            _progressHeading.text = _headingKeepGoing;
            _progressDescription.text = _headingKeepGoing ;
        }
    }
}
