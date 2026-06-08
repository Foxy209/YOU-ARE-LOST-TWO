using UnityEngine;
using System.Collections;

public class WeaponAnimator : MonoBehaviour
{
    [Header("Позиция оружия")]
    [SerializeField] private Transform weaponModel; // сама модель дробовика

    [Header("Отдача при выстреле")]
    [SerializeField] private float recoilBack = 0.08f;   // отдача назад
    [SerializeField] private float recoilUp = 0.03f;     // отдача вверх
    [SerializeField] private float recoilSpeed = 8f;     // скорость отдачи
    [SerializeField] private float returnSpeed = 5f;     // скорость возврата

    [Header("Тряска камеры")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shakeAmount = 1.5f;
    [SerializeField] private float shakeDuration = 0.1f;

    private Vector3 weaponStartPos;
    private Vector3 weaponTargetPos;
    private Vector3 weaponCurrentVelocity;
    private Vector3 cameraStartPos;

    void Start()
    {
        if (weaponModel != null)
            weaponStartPos = weaponModel.localPosition;
        if (playerCamera != null)
            cameraStartPos = playerCamera.transform.localPosition;
    }

    void Update()
    {
        // Плавный возврат оружия
        if (weaponModel != null)
        {
            weaponModel.localPosition = Vector3.SmoothDamp(
                weaponModel.localPosition,
                weaponStartPos,
                ref weaponCurrentVelocity,
                1f / returnSpeed
            );
        }
    }

    public void PlayShotEffect()
    {
        // Отдача оружия
        if (weaponModel != null)
        {
            weaponModel.localPosition = weaponStartPos + new Vector3(0, recoilUp, -recoilBack);
        }

        // Тряска камеры
        if (playerCamera != null)
        {
            StartCoroutine(CameraShake());
        }
    }

    IEnumerator CameraShake()
    {
        float elapsed = 0f;
        Vector3 originalPos = playerCamera.transform.localPosition;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            playerCamera.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.localPosition = originalPos;
    }
}

