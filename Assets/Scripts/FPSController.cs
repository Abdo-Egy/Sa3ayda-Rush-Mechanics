using System.Collections;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] Transform PlayerBody;
    ///////////////////////////////////////////////////
    [Header("Movement Settings")]
    [SerializeField] float WalkSpeed = 5f;
    [SerializeField] float RunSpeed = 10f;
    float _Horizontal;
    float _Vertical;
    Vector3 _moveDir;
    float _currentSpeed;
    ///////////////////////////////////////////////////
    [Header("Mouse Settings")]
    [SerializeField] Transform _camera;
    [SerializeField] float MouseSensitivity = 300;
    [SerializeField] float VerticalClamp = 85;
    float _MouseX;
    float _MouseY;
    float xRotation;
    float yRotation;
    ///////////////////////////////////////////////////
    [Header("Jump Settings")]
    [SerializeField] float JumpForce = 5;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float GroundRadius = 0.2f;
    [SerializeField] LayerMask GroundMask;
    bool _isGrounded;
    ///////////////////////////////////////////////////
    [Header("Landing Effect")]
    [SerializeField] float LandingForce = .4f;
    [SerializeField] float LandingSpeed = 5;
    bool _wasGrounded;
    bool _isLandingEffectPlaying;
    ///////////////////////////////////////////////////
    [Header("Camera Bob")]
    [SerializeField] float BobFrequency = 6f;
    [SerializeField] float BobAmplitude = 0.05f;
    [SerializeField] float RunBobMultiplier = 1.5f;
    [SerializeField] float BobStopSpeed = 5;
    float _bobTimer;
    Vector3 _camDefaultPos;
    ///////////////////////////////////////////////////
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camDefaultPos = _camera.localPosition;
    }
    private void Update()
    {
        HandleInput();
        MouseRotation();
        CameraBob();
        Jump();
        LandingEffect();
    }
    private void HandleInput()
    {
        _Horizontal = Input.GetAxis("Horizontal");
        _Vertical = Input.GetAxis("Vertical");
        _moveDir = new Vector3(_Horizontal, 0, _Vertical);

        _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;
        _moveDir = transform.TransformDirection(_moveDir);
    }
    void MouseRotation()
    {
        _MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        _MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        yRotation += _MouseX;
        xRotation -= _MouseY;
        xRotation = Mathf.Clamp(xRotation, -VerticalClamp, VerticalClamp);

        _camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        /// PlayerBody.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    void CameraBob()
    {
        if (_moveDir.magnitude != 0 && _isGrounded)
        {
            _bobTimer += Time.deltaTime * BobFrequency * (_currentSpeed == RunSpeed ? RunBobMultiplier : 1f);
            float bobOffset = Mathf.Sin(_bobTimer) * BobAmplitude;
            _camera.localPosition = _camDefaultPos + new Vector3(0, bobOffset, 0);
        }
        else
        {
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, _camDefaultPos, Time.deltaTime * BobStopSpeed);
            _bobTimer = 0;
        }
    }
    void Jump()
    {
        _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundRadius, GroundMask);
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }
    }
    void LandingEffect()
    {
        if (!_wasGrounded && _isGrounded && !_isLandingEffectPlaying)
        {
            StartCoroutine(StartLandingEffect());
        }
        _wasGrounded = _isGrounded;
    }
    private void FixedUpdate()
    {
        if (_moveDir.magnitude != 0)
            Rigidbody.MovePosition(transform.position + _moveDir * _currentSpeed * Time.deltaTime);
    }
    IEnumerator StartLandingEffect()
    {
        _isLandingEffectPlaying = true;

        Vector3 OriginalCameraPos = _camera.localPosition;
        Vector3 TargetCameraPos = OriginalCameraPos + Vector3.down * LandingForce;

        float t = 0;

        // Down
        while (t < 1)
        {
            t += Time.deltaTime * LandingSpeed;
            _camera.localPosition = Vector3.Lerp(OriginalCameraPos, TargetCameraPos, t);
            yield return null;
        }
        // Up
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * LandingSpeed;
            _camera.localPosition = Vector3.Lerp(TargetCameraPos, OriginalCameraPos, t);
            yield return null;
        }
        _camera.localPosition = OriginalCameraPos;
        _isLandingEffectPlaying = false;
    }
}
