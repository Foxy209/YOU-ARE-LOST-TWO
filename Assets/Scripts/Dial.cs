using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Dial : MonoBehaviour
{
    [SerializeField] private TMP_Text dialtxt;
    [SerializeField] private TMP_Text spkrtxt;
    [SerializeField] private AudioClip btnsnd;
    [SerializeField] private AudioSource aud;
    [SerializeField] private Image spkrim;
    [SerializeField] private Image disapr;
    [SerializeField] private Sprite[] spkrimgs;
    public Sprite[] spkrimgsF {set{spkrimgs = value;}}
    [SerializeField] private string[] spkrnams;
    public string[] spkrnamsF {set{spkrnams = value;}}
    [SerializeField] private string[] spkrtxts;
    public string[] spkrtxtsF {set{spkrtxts = value;}}
    [SerializeField] private AudioClip[] txtsnd;
    public AudioClip[] txtsndF {set{txtsnd = value;}}
    public void Strdial()
    {
        disapr.gameObject.GetComponent<Disapr>().Strfade();
        StartCoroutine(nameof(Dialog));
    }
    IEnumerator Dialog()
    {
        dialtxt.text = null;
        spkrtxt.text = null;
        yield return new WaitForSeconds(0.5f);
        spkrim.sprite = spkrimgs[0];
        spkrim.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        foreach (string i in spkrtxts)
        {
            spkrim.sprite = spkrimgs[Array.IndexOf(spkrtxts, i)];
            spkrtxt.text = spkrnams[Array.IndexOf(spkrtxts, i)];
            dialtxt.text = null;
            foreach(char a in i)
            {
                aud.PlayOneShot(txtsnd[Array.IndexOf(spkrtxts, i)]);
                dialtxt.text += a; 
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            aud.PlayOneShot(btnsnd);
        }
        disapr.color = new Color(0,0,0,1);
        gameObject.SetActive(false);
    }
}
