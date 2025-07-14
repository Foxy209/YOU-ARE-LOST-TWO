using UnityEngine;
using System.Collections.Generic;
public class CheckpointSystem : MonoBehaviour
{
    public static CheckpointSystem Instance; // Синглтон для доступа из других скриптов
    public Vector3 LastCheckpointPosition { get; private set; }
    public Quaternion LastCheckpointRotation { get; private set; }

    [SerializeField] private ParticleSystem checkpointActivatedEffect;
    [SerializeField] private AudioClip checkpointSound;

    private Dictionary<GameObject, bool> checkpointStatus = new Dictionary<GameObject, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 position, Quaternion rotation, GameObject checkpointObject)
    {
        LastCheckpointPosition = position;
        LastCheckpointRotation = rotation;
    
        // Сохраняем состояние уровня
        SaveLevelState();
    
        // Эффекты и звуки 
        if (!checkpointStatus.ContainsKey(checkpointObject)) 
        {
            if (checkpointActivatedEffect != null)
                Instantiate(checkpointActivatedEffect, position, Quaternion.identity);
        
            AudioSource.PlayClipAtPoint(checkpointSound, position);
        
            checkpointStatus[checkpointObject] = true;
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        // Восстанавливаем состояние карты
        RestoreLevelState();
    
        // Возвращаем игрока
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;
    
        player.transform.position = LastCheckpointPosition;
        player.transform.rotation = LastCheckpointRotation;
    
        if (controller != null) controller.enabled = true;
    
        // Сброс здоровья
        //PlayerHealth health = player.GetComponent<PlayerHealth>();
        //if (health != null) health.ResetToCheckpoint();
    }
    
    
    [System.Serializable]
    public class ObjectState
    {
        public GameObject obj;
        public Vector3 position;
        public Quaternion rotation;
        public bool isActive;
    }

    public List<ObjectState> savedStates = new List<ObjectState>();

// Сохраняем состояние объектов при активации чекпоинта
    public void SaveLevelState()
    {
        savedStates.Clear();
    
        // Находим все объекты с тегом "Dynamic"
        GameObject[] dynamicObjects = GameObject.FindGameObjectsWithTag("Dynamic");
    
        foreach (GameObject obj in dynamicObjects)
        {
            ObjectState state = new ObjectState
            {
                obj = obj,
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                isActive = obj.activeSelf
            };
            savedStates.Add(state);
        }
    }

// Восстанавливаем состояние при респавне
    public void RestoreLevelState()
    {
        foreach (ObjectState state in savedStates)
        {
            if (state.obj != null)
            {
                state.obj.transform.position = state.position;
                state.obj.transform.rotation = state.rotation;
                state.obj.SetActive(state.isActive);
            }
        }
    }
}
