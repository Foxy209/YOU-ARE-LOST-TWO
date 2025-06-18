using UnityEngine;
using System.Linq;
public class THESTUPITESTSHITTHATIHADEVERWROTE : MonoBehaviour
{
    [SerializeField] private Transform[] SHIT;
    [SerializeField] private Transform STUPIDSHIT;
    private void Awake()
    {
        print(STUPIDSHIT.eulerAngles);
        SHIT = ((Transform[])FindObjectsOfType(typeof(Transform))).Where(x => x.localEulerAngles.x == STUPIDSHIT.eulerAngles.x).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        print(STUPIDSHIT.eulerAngles);
    }
}
