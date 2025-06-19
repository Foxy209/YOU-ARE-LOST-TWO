using UnityEngine;
using System.Linq;
public class THESTUPITESTSHITTHATIHADEVERWROTE : MonoBehaviour
{
    [SerializeField] private Transform[] SHIT;
    [SerializeField] private Transform STUPIDSHIT;
    private void Awake()
    {
        print(STUPIDSHIT.eulerAngles);
        SHIT = FindObjectsByType<Transform>(FindObjectsSortMode.None).Where(x => x.localEulerAngles.x == STUPIDSHIT.eulerAngles.x).ToArray();
    }
}
