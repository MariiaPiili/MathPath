using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AssignmentGenerator : MonoBehaviour
{
    public List<Assignment> Assignments;

    [SerializeField] private TextMeshProUGUI textForAssignment;
    [SerializeField] private TMP_InputField inputFieldForAnswer;
    [SerializeField] private GameObject correctIcon;
    [SerializeField] private GameObject wrongIcon;
    [SerializeField] private GameObject popUpCorrect;
    [SerializeField] private GameObject popUpWrong;
    [SerializeField] private UnityEvent eventLevelCompleted;
    [SerializeField] private Progress _progress;

    private Assignment currentAssignment;

    public AnimationCurve scaleCurve;
    public float animationDuration = 2f; 
    private float timer = 0f;
    private float winTimer = 0f;
    private float lostTimer = 0f;
    private bool generation = false;
    private float count;


    void Update()
    {
        if (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float scaleValue = scaleCurve.Evaluate(timer / animationDuration);
            wrongIcon.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
            correctIcon.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
        else
        {
            inputFieldForAnswer.gameObject.SetActive(true);
            if (generation)
            {

            Generate();
            }
        }

        if (winTimer < animationDuration)
        {
            popUpCorrect.SetActive(true);
            winTimer += Time.deltaTime;
        }
        else
        {
            popUpCorrect.SetActive(false);          
        }
        if (lostTimer < animationDuration)
        {
            popUpWrong.SetActive(true);
            lostTimer += Time.deltaTime;
        }
        else
        {
            popUpWrong.SetActive(false);
        }
    }

    private void Start()
    {
        Generate();
        winTimer = animationDuration;
        timer = animationDuration;
        lostTimer = animationDuration;
        count = 0F;
    }

    public void Check()
    {
        if (count <= 9)
        {

            if (inputFieldForAnswer.text == currentAssignment.Answer)
            {
                winTimer = 0;
                _progress.CorectedAnswersIncrease();

            }
            else
            {
                lostTimer = 0;
                _progress.InCorectedAnswersIncrease();
            }
            timer = 0f;
            inputFieldForAnswer.gameObject.SetActive(false);
            generation = true;
            count++;
        }
        else
        {
            count = 0f;
            eventLevelCompleted.Invoke();
            _progress.ProgressCoalcilate();
        }
    }

    public void Generate()
    {
        currentAssignment = Assignments[Random.Range(0, Assignments.Count)];    
        textForAssignment.text = currentAssignment.AssignmentItself;
        inputFieldForAnswer.text = "";
        generation = false;
        
    }
}
