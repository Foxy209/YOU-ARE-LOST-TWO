using UnityEngine;
using System.Collections;
using TMPro;
public class TxtAnim : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset fon;
    [SerializeField] private TMP_FontAsset font;
    private TMP_Text txta;
    void Start()
    {
        txta = GetComponent<TMP_Text>();
        StartCoroutine(nameof(Anim));
    }
    IEnumerator Anim()
    {
        while (true) 
        { 
            yield return new WaitForSeconds(1);
            txta.font = fon;
            yield return new WaitForSeconds(1);
            txta.font = font;
        }
    }
}
