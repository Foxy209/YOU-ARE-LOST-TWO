using UnityEngine;
using UnityEngine.UI;

public class gps_navigator : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float maxTrackingDistance = 100f;

    [Header("Marker Settings")]
    [SerializeField] private RectTransform mapRect;
    [SerializeField] private RectTransform marker;
    [SerializeField] private float borderOffset = 10f;
    [SerializeField] private float edgeThreshold = 0.9f;

    [Header("Color Settings")]
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private float closeDistance = 10f;
    [SerializeField] private float farDistance = 50f;

    private Image markerImage;
    private bool targetBehind;

    void Start()
    {
        markerImage = marker.GetComponent<Image>();
        marker.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0;
        float distance = Mathf.Clamp(toTarget.magnitude, 0, maxTrackingDistance);

        Vector3 forward = transform.forward;
        forward.y = 0;
        float dotProduct = Vector3.Dot(forward.normalized, toTarget.normalized);
        targetBehind = dotProduct < -edgeThreshold;

        Vector3 localDirection = transform.InverseTransformDirection(toTarget);
        Vector2 direction = new Vector2(localDirection.x, localDirection.z).normalized;

        UpdateMarkerPosition(direction, distance);
    }

    void UpdateMarkerPosition(Vector2 direction, float distance)
    {
        float width = mapRect.rect.width / 2 - borderOffset;
        float height = mapRect.rect.height / 2 - borderOffset;
        
        if (targetBehind)
        {
            direction.x = (direction.x > 0) ? 1 : -1;
            direction.y = 0;
        }

        Vector2 markerPos = new Vector2(
            Mathf.Clamp(direction.x * width, -width, width),
            Mathf.Clamp(direction.y * height, -height, height)
        );

        marker.anchoredPosition = markerPos;
        
        float distanceLerp = Mathf.InverseLerp(closeDistance, farDistance, distance);
        markerImage.color = colorGradient.Evaluate(distanceLerp);
    }
}
