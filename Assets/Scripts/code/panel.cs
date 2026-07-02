using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class panel : MonoBehaviour
{
    [Header("Код")]
    [SerializeField] private string correctCode = "739"; 
    
    [Header("Дисплей")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private int maxDigits = 3;
    
    [Header("Что будет при правильном коде")]
    [SerializeField] private UnityEvent OnCodeCorrect;
    
    [Header("Звуки")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource wrongSound;
    
    private string currentInput = "";
    private bool isOpened;

    void Start()
    {
        UpdateDisplay();
    }

    public void PressDigit(int digit)
    {
        if (isOpened) return;
        
        if (currentInput.Length >= maxDigits) return;
        
        currentInput += digit.ToString();
        UpdateDisplay();
        
        if (buttonSound != null) buttonSound.Play();
        
        if (currentInput.Length >= maxDigits)
        {
            CheckCode();
        }
    }

    public void PressClear()
    {
        if (isOpened) return;
        
        currentInput = "";
        UpdateDisplay();
        
        if (buttonSound != null) buttonSound.Play();
    }

    void CheckCode()
    {
        if (currentInput == correctCode)
        {
            isOpened = true;
            displayText.text = "ОТКРЫТО";
            displayText.color = Color.green;
            
            if (correctSound != null) correctSound.Play();
            OnCodeCorrect?.Invoke();
        }
        else
        {
            if (wrongSound != null) wrongSound.Play();
            
            StartCoroutine(ShowWrongAndClear());
        }
    }

    System.Collections.IEnumerator ShowWrongAndClear()
    {
        displayText.text = "ОШИБКА";
        displayText.color = Color.red;
        
        yield return new WaitForSeconds(0.8f);
        
        currentInput = "";
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = currentInput.PadRight(maxDigits, '_');
            displayText.color = Color.white;
        }
    }
}
