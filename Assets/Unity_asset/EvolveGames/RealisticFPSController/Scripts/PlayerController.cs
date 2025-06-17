//by EvolveGames
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("PlayerController")]
        [SerializeField] public Transform Camera;
        [SerializeField] public ItemChange Items;
        [SerializeField, Range(0, 10)] public float walkingSpeed = 3.0f;
        [Range(0f, 5)] public float CroughSpeed = 1.0f;
        [SerializeField, Range(0, 20)] public float RuningSpeed = 4.0f;
        [SerializeField, Range(0, 20)] float jumpSpeed = 6.0f;
        [SerializeField, Range(0.5f, 10)] float lookSpeed = 2.0f;
        [SerializeField, Range(10, 120)] float lookXLimit = 80.0f;
        [Space(20)]
        [Header("Advance")]
        [SerializeField] float RunningFOV = 65.0f;
        [SerializeField] float SpeedToFOV = 4.0f;
        [SerializeField] float CroughHeight = 1.0f;
        [SerializeField] float gravity = 20.0f;
        [SerializeField] float timeToRunning = 2.0f;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool CanRunning = true;

        [Space(20)]
        [Header("Climbing")]
        [SerializeField] bool CanClimbing = true;
        [SerializeField, Range(1, 25)] float Speed = 2f;
        bool isClimbing = false;

        [Space(20)]
        [Header("HandsHide")]
        [SerializeField] bool CanHideDistanceWall = true;
        [SerializeField, Range(0.1f, 5)] float HideDistance = 1.5f;
        [SerializeField] int LayerMaskInt = 1;

        [Space(20)]
        [Header("Input")]
        [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        bool isCrough = false;
        float InstallCroughHeight;
        float rotationX = 0;
        [HideInInspector] public bool isRunning = false;
        Vector3 InstallCameraMovement;
        float InstallFOV;
        Camera cam;
        [HideInInspector] public bool Moving;
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float Lookvertical;
        [HideInInspector] public float Lookhorizontal;
        float RunningValue;
        float installGravity;
        bool WallDistance;
        [HideInInspector] public float WalkingValue;
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            if (Items == null && GetComponent<ItemChange>()) Items = GetComponent<ItemChange>();
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallCroughHeight = characterController.height;
            InstallCameraMovement = Camera.localPosition;
            InstallFOV = cam.fieldOfView;
            RunningValue = RuningSpeed;
            installGravity = gravity;
            WalkingValue = walkingSpeed;
        }

        void Update()
{
    RaycastHit CroughCheck;
    RaycastHit ObjectCheck;

    // Гравитация (если не на лестнице)
    if (!characterController.isGrounded && !isClimbing)
    {
        moveDirection.y -= gravity * Time.deltaTime;
    }

    // Получаем ввод
    float inputVertical = Input.GetAxis("Vertical");
    float inputHorizontal = Input.GetAxis("Horizontal");
    bool wantsToMove = (Mathf.Abs(inputVertical) > 0.1f || Mathf.Abs(inputHorizontal) > 0.1f);
    
    // Определяем состояние бега
    isRunning = !isCrough ? CanRunning ? Input.GetKey(KeyCode.LeftShift) && wantsToMove : false : false;

    // Рассчитываем целевую скорость
    float targetSpeed = 0f;
    if (canMove)
    {
        targetSpeed = isRunning ? RuningSpeed : isCrough ? CroughSpeed : walkingSpeed;
        
        // Если скорость должна быть 0 - мгновенная остановка
        if (targetSpeed <= 0.01f || !wantsToMove)
        {
            WalkingValue = 0f;
            RunningValue = 0f;
            moveDirection.x = 0f;
            moveDirection.z = 0f;
        }
        else
        {
            // Плавное изменение скорости
            if (isRunning)
                RunningValue = Mathf.Lerp(RunningValue, targetSpeed, timeToRunning * Time.deltaTime);
            else
                WalkingValue = Mathf.Lerp(WalkingValue, targetSpeed, 4f * Time.deltaTime);
        }
    }
    else
    {
        // Полная остановка, если движение запрещено
        WalkingValue = 0f;
        RunningValue = 0f;
        moveDirection.x = 0f;
        moveDirection.z = 0f;
    }

    // Текущая активная скорость
    float currentSpeed = isRunning ? RunningValue : WalkingValue;

    // Расчет направления движения ОТНОСИТЕЛЬНО КАМЕРЫ
    Vector3 cameraForward = Camera.transform.forward;
    Vector3 cameraRight = Camera.transform.right;
    cameraForward.y = 0f; // Игнорируем наклон камеры вверх/вниз
    cameraRight.y = 0f;
    cameraForward.Normalize();
    cameraRight.Normalize();

    // Комбинируем направление с учетом ввода
    Vector3 desiredMoveDirection = (cameraForward * inputVertical) + (cameraRight * inputHorizontal);
    desiredMoveDirection.Normalize();

    // Применяем движение
    if (canMove && wantsToMove)
    {
        moveDirection.x = desiredMoveDirection.x * currentSpeed;
        moveDirection.z = desiredMoveDirection.z * currentSpeed;
    }

    // Прыжок
    if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isClimbing)
    {
        moveDirection.y = jumpSpeed;
    }

    // Применяем движение
    characterController.Move(moveDirection * Time.deltaTime);
    Moving = (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f);

    // Вращение игрока по горизонтали (отдельно от камеры)
    if (Cursor.lockState == CursorLockMode.Locked && canMove)
    {
        Lookvertical = -Input.GetAxis("Mouse Y");
        Lookhorizontal = Input.GetAxis("Mouse X");

        rotationX += Lookvertical * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

        // Изменение FOV при беге
        if (isRunning && Moving) 
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
        else 
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
    }

    // Приседание (остается без изменений)
    if (Input.GetKey(CroughKey))
    {
        isCrough = true;
        float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
        characterController.height = Height;
        WalkingValue = Mathf.Lerp(WalkingValue, CroughSpeed, 6 * Time.deltaTime);
    }
    else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, 
                            transform.TransformDirection(Vector3.up), 
                            out CroughCheck, 0.8f, 1))
    {
        if (characterController.height != InstallCroughHeight)
        {
            isCrough = false;
            float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
            characterController.height = Height;
            WalkingValue = Mathf.Lerp(WalkingValue, walkingSpeed, 4 * Time.deltaTime);
        }
    }

    // Скрытие рук при близости к стене (без изменений)
    if (WallDistance != Physics.Raycast(GetComponentInChildren<Camera>().transform.position, 
                                      transform.TransformDirection(Vector3.forward), 
                                      out ObjectCheck, HideDistance, LayerMaskInt) && CanHideDistanceWall)
    {
        WallDistance = Physics.Raycast(GetComponentInChildren<Camera>().transform.position, 
                                     transform.TransformDirection(Vector3.forward), 
                                     out ObjectCheck, HideDistance, LayerMaskInt);
        Items.ani.SetBool("Hide", WallDistance);
        Items.DefiniteHide = WallDistance;
    }
}

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            { 
                CanRunning = false;
                isClimbing = true;
                WalkingValue /= 2;
                Items.Hide(true);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            {
                moveDirection = new Vector3(0, Input.GetAxis("Vertical") * Speed * (-Camera.localRotation.x / 1.7f), 0);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            {
                CanRunning = true;
                isClimbing = false;
                WalkingValue *= 2;
                Items.ani.SetBool("Hide", false);
                Items.Hide(false);
            }
        }

    }
}