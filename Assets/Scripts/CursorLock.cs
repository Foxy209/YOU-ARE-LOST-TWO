using UnityEngine;

public class CursorLock : MonoBehaviour
{
	void Awake() => Cursor.lockState = CursorLockMode.Locked;
}