using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class codeHide : MonoBehaviour
{
    [Header("Подсказка")]
    [SerializeField] private int digit;              
    [SerializeField] private TextMeshProUGUI clueText;

    [Header("Подсветка")]
    [SerializeField] private GameObject highlight;

    void Start()
    {
        if (clueText != null)
            clueText.text = digit.ToString();
    }

    
    public void ShowClue()
    {
        if (highlight != null)
            highlight.SetActive(true);
    }

    public void HideClue()
    {
        if (highlight != null)
            highlight.SetActive(false);
    }
}
