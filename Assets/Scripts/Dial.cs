using System;
using System.Collections;
using DG.Tweening;
using EvolveGames;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
public class Dial : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HeadBob playerHeadBob;
    [SerializeField] private HandsHolder playerHandHolder;
    [SerializeField] private MovementEffects playerMoveFX;
    [SerializeField] private TMP_Text dialtxt;
    [SerializeField] private TMP_Text spkrtxt;
    [SerializeField] private AudioClip btnsnd;
    [SerializeField] private AudioSource aud;
    [SerializeField] private Image spkrim;
    [SerializeField] private Image disapr;
    public IEnumerator Dialog(string[] names, string[] texts, Sprite[] images, AudioClip[] sounds)
    {
        yield return disapr.gameObject.GetComponent<Image>().DOFade(0,2);
        dialtxt.text = null;
        spkrtxt.text = null;
        yield return new WaitForSeconds(0.5f);
        spkrim.sprite = images[0];
        spkrim.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        foreach (string i in texts)
        {
            spkrim.sprite = images[Array.IndexOf(texts, i)];
            spkrtxt.text = names[Array.IndexOf(texts, i)];
            dialtxt.text = null;
            foreach(char a in i)
            {
                aud.PlayOneShot(sounds[Array.IndexOf(texts, i)]);
                dialtxt.text += a; 
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            aud.PlayOneShot(btnsnd);
        }
        disapr.color = new Color(0,0,0,1);
        player.enabled = true;
        playerMoveFX.CanMovementFXF = true;
        playerHandHolder.enabled = true;
        playerHeadBob.EnabledF = true;
        gameObject.SetActive(false);
    }
}
