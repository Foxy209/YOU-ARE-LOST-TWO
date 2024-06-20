using UnityEngine;

public class CursorLock : MonoBehaviour
{
	void Start() => Cursor.lockState = CursorLockMode.Locked;
	//void Update()
	//{
		//if (Input.GetKeyDown(KeyCode.Escape))
			//Cursor.lockState = CursorLockMode.None;
		//else if (Input.GetMouseButtonDown(1))
            //Cursor.lockState = CursorLockMode.Locked;
    //}
}