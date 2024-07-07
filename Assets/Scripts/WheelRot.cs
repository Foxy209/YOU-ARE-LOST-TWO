using UnityEngine;
public class WheelRot : MonoBehaviour
{
    private float tiltZ;
    private void FixedUpdate()
    {
        tiltZ = Input.GetAxis("Horizontals") * 15;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, tiltZ+180);
    }
}
