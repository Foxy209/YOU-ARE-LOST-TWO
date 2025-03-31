using System.Collections;
using UnityEngine;
public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private bool DoYouNeedToChangeMaterial;
    [SerializeField] private Material MaterialOn;
    [SerializeField] private Material MaterialOff;
    private Light li;
    [SerializeField] private MeshRenderer mr;
    void Start()
    {
        li = GetComponent<Light>();
        //mr = GetComponent<MeshRenderer>();
        StartCoroutine(nameof(Anim));
    }
    IEnumerator Anim()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
            li.enabled = false;
            if (DoYouNeedToChangeMaterial)
                mr.material = MaterialOff;
            yield return new WaitForSeconds(Random.Range(0.01f,0.3f));
            li.enabled = true;
            if (DoYouNeedToChangeMaterial)
                mr.material = MaterialOn;
            yield return new WaitForSeconds(Random.Range(1, 2));
        }
        
    }
}
