using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TextClickHandler : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Текстовый компонент
    public Camera uiCamera; // Камера для Screen Space - Camera

    public string ActionNameOne;
    public UnityEvent onLinkClickedOne; // UnityEvent для первой ссылки

    public string ActionNameTwo;
    public UnityEvent onLinkClickedTwo; // UnityEvent для второй ссылки

    public string ActionNameThree;
    public UnityEvent onLinkClickedThree; // UnityEvent для третьей ссылки

    void Update()
    {
        // Проверяем, нажата ли кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем позицию мыши в экранных координатах
            Vector3 mousePosition = Input.mousePosition;

            // Преобразуем координаты мыши в локальные координаты RectTransform текста
            RectTransform rectTransform = textComponent.rectTransform;
            Vector2 localMousePosition;

            // Преобразование с учетом UI камеры
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, mousePosition, uiCamera, out localMousePosition))
            {
                // Проверяем, нажата ли ссылка
                int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, mousePosition, uiCamera);

                if (linkIndex != -1)
                {
                    TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];
                    string linkId = linkInfo.GetLinkID();
                    Debug.Log($"Clicked on link ID: {linkId}");

                    // Проверяем ID ссылки и вызываем соответствующий UnityEvent
                    if (linkId == ActionNameOne)
                    {
                        onLinkClickedOne?.Invoke();
                    }
                    else if (linkId == ActionNameTwo)
                    {
                        onLinkClickedTwo?.Invoke();
                    }
                    else if (linkId == ActionNameThree)
                    {
                        onLinkClickedThree?.Invoke();
                    }
                }
            }
        }
    }
}
