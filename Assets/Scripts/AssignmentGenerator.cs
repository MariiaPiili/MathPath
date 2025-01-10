
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class AssignmentGenerator : MonoBehaviour
{
    public List<Assignment> Assignments;

    [SerializeField] private TextMeshProUGUI _textForAssignment;
    [SerializeField] private TMP_InputField _inputFieldForAnswer;
    [SerializeField] private GameObject _correctIcon;
    [SerializeField] private GameObject _wrongIcon;
    [SerializeField] private GameObject _popUpCorrect;
    [SerializeField] private GameObject _popUpWrong;
    [SerializeField] private UnityEvent _eventLevelCompleted;
    [SerializeField] private Progress _progress;

    private Assignment _currentAssignment;

    public AnimationCurve ScaleCurve;
    public float AnimationDuration;

    private float _timer;
    private float _winTimer;
    private float _lostTimer;
    private bool _generation;
    private float _count;
    private bool _firstTry;

    private void Update()
    {
        if (_timer < AnimationDuration)
        {
            _timer += Time.deltaTime;
            float scaleValue = ScaleCurve.Evaluate(_timer / AnimationDuration);
            _wrongIcon.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            _correctIcon.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
        else
        {
            _inputFieldForAnswer.gameObject.SetActive(true);
            if (_generation)
            {
                Generate();
            }
        }

        if (_winTimer < AnimationDuration)
        {
            _popUpCorrect.SetActive(true);
            _winTimer += Time.deltaTime;
        }
        else
        {
            _popUpCorrect.SetActive(false);
        }
        if (_lostTimer < AnimationDuration)
        {
            _popUpWrong.SetActive(true);
            _lostTimer += Time.deltaTime;
        }
        else
        {
            _popUpWrong.SetActive(false);
        }
    }

    private void Awake()
    {
        GetJsonData(1);
    }

    private void Start()
    {        
        _winTimer = AnimationDuration;
        _timer = AnimationDuration;
        _lostTimer = AnimationDuration;
        _count = _progress.correctedAnswers + _progress.incorrectedAnswers;
        _firstTry = false;
    }

    public void Check()
    {
        if (_count < 9)
        {
            if (_inputFieldForAnswer.text == _currentAssignment.Answer)
            {
                _winTimer = 0;
                _progress.CorectedAnswersIncrease();

            }
            else
            {
                _lostTimer = 0;
                _progress.InCorectedAnswersIncrease();
            }

            _timer = 0f;
            _inputFieldForAnswer.gameObject.SetActive(false);
            _generation = true;
            _count++;
        }
        else
        {
            _count = 0f;
            _eventLevelCompleted.Invoke();//Переход к сранице с результатом
            PlayerPrefs.SetInt("level", 2);
            _progress.ProgressCoalcilate();
            _inputFieldForAnswer.text = "";
            //_progress.ResetPogress();
            _firstTry = true;
        }
    }

    public void Generate()
    {

        if (_firstTry)
        {
            Debug.Log("load");
            Load();
            _firstTry = false;
        }
        else
        {
            Debug.Log("Save");
            int index = UnityEngine.Random.Range(0, Assignments.Count);
            _currentAssignment = Assignments[index];
            Assignments.RemoveAt(index);
            _textForAssignment.text = _currentAssignment.AssignmentItself;
            _inputFieldForAnswer.text = "";
            _generation = false;
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("saveAssignment", _currentAssignment.AssignmentItself);
        PlayerPrefs.SetString("saveAnswer", _currentAssignment.Answer);
    }

    private void Load()
    {
        Debug.Log("_currentAssignment" + _currentAssignment==null);
        Debug.Log("_textForAssignment" + _textForAssignment == null);
        Debug.Log("_inputFieldForAnswer" + _inputFieldForAnswer == null);
        _currentAssignment.AssignmentItself = PlayerPrefs.GetString("saveAssignment");
        _currentAssignment.Answer = PlayerPrefs.GetString("saveAnswer");
        _textForAssignment.text = _currentAssignment.AssignmentItself;
        _inputFieldForAnswer.text = "";
    }
    //public Wrapper<Assignment> wrapper;
    private void SetJsonData()
    {
        string json = JsonUtility.ToJson(new Wrapper<Assignment>(Assignments), true);

        // Укажите путь для сохранения файла (например, в папку Application.persistentDataPath)
        string path = Path.Combine(Application.streamingAssetsPath, "assignments.json");

        // Сохранение в файл
        File.WriteAllText(path, json);

        Debug.Log($"JSON saved to: {path}");
    }

    public void GetJsonData(int level)
    {
        StartCoroutine(LoadJson(level));
    }

    private IEnumerator LoadJson(int level)
    {
        string path = Path.Combine(Application.streamingAssetsPath, $"assignments{level}.json");

        Debug.Log($"Attempting to load JSON from path: {path}");


        Debug.Log("Open file" + File.Exists(path));
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("JSON file loaded successfully!");

            string json = request.downloadHandler.text;
            Debug.Log("Text:" + json);
            Wrapper<Assignment> wrapper;

            try
            {
                 wrapper = JsonConvert.DeserializeObject<Wrapper<Assignment>>(json);
                Debug.Log("JSON parsed successfully!");
                Assignments = wrapper.Items;
                Debug.Log("Check Assignments");
                foreach (var item in Assignments)
                {
                    Debug.Log(item.Answer + " " + item.AssignmentItself);
                }
                Generate();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to parse JSON: {ex.Message}");
            }           
            
        }
        else
        {
            Debug.LogError($"Failed to load JSON. Error: {request.error}");
        }
    }
}
