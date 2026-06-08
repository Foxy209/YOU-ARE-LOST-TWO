using UnityEngine;
using TMPro;
public class ShotgunAmmo : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private int maxShells = 6;
    [SerializeField] private float reloadTimePerShell = 0.4f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    private int currentShells;
    private bool isReloading;
    private float reloadTimer;
    private int shellsToReload;

    public bool CanShoot => currentShells > 0 && !isReloading;

    void Start()
    {
        currentShells = maxShells;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentShells < maxShells && !isReloading)
        {
            StartReload();
        }

        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f)
            {
                currentShells++;
                shellsToReload--;
                UpdateUI();

                if (currentShells >= maxShells || shellsToReload <= 0)
                {
                    isReloading = false;
                    currentShells = maxShells;
                    UpdateUI();
                }
                else
                {
                    reloadTimer = reloadTimePerShell;
                }
            }
        }
    }

    public bool TryShoot()
    {
        if (!CanShoot) return false;
        currentShells--;
        UpdateUI();
        if (currentShells <= 0) StartReload();
        return true;
    }

    void StartReload()
    {
        if (currentShells >= maxShells) return;
        shellsToReload = maxShells - currentShells;
        isReloading = true;
        reloadTimer = reloadTimePerShell;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (ammoText == null) return;

        if (isReloading)
            ammoText.text = $"RELOAD {currentShells}/{maxShells}";
        else
            ammoText.text = $"{currentShells} / ∞";
    }
}
