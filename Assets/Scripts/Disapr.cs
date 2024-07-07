using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Disapr : MonoBehaviour
{
    [SerializeField] private Image im;
    [SerializeField] private float i = 1;
    public void Strfade()
    {
        StartCoroutine(nameof(fade));
    }
    public IEnumerator fade()
    {
        while (im.color != new Color(0,0,0,0))
        {
            yield return new WaitForSeconds(0.1f);
            i -= 0.1f;
            im.color = new Color(0,0,0,i);
            if(i<= 0.2f)
            {
                i=0;
                im.color = new Color(0,0,0,0);
            }
        }
        i=1;
    }
}
