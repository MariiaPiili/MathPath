using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TextClickHandler : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    public string ActionNameOne;
    public UnityEvent onLinkClickedOne; // UnityEvent, который будет вызван при клике по ссылке
    
    public string ActionNameTwo;
    public UnityEvent onLinkClickedTwo; // UnityEvent, который будет вызван при клике по ссылке
    
    public string ActionNameThree;
    public UnityEvent onLinkClickedThree; // UnityEvent, который будет вызван при клике по ссылке

    void Update()
    {
        // Проверяем, нажата ли мышь
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем позицию мыши
            Vector3 mousePosition = Input.mousePosition;

            // Проверяем, на какой элемент текста мы нажали
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, mousePosition, null);
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];
                string linkId = linkInfo.GetLinkID();

                // Проверяем ID ссылки и вызываем UnityEvent
                if (linkId == ActionNameOne)
                {                    
                    onLinkClickedOne?.Invoke();
                }
                if (linkId == ActionNameTwo)
                {
                    onLinkClickedTwo?.Invoke();
                }
                if (linkId == ActionNameThree)
                {
                    onLinkClickedThree?.Invoke();
                }
            }
        }
    }
}
