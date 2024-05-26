using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    private MovementData movementData;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private Vector3 _move = new Vector3(0, 0, 0);
    private Vector3 _rotate = new Vector3(0, 0, 0);
    public float _moveSpeed; // 移動速度の変数
    public float _rotationSpeed; // 回転速度の変数
    private bool _isUDPControlActive;

    public void InitMovementData(MovementData data)
    {
        movementData = data;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("walking", false);
        _animator.SetBool("flying", false);
    }

    public void SetMove(float move_z){
        _move.z = move_z;
    }

    public void SetRotate(float rotate_y){
        _rotate.y = rotate_y;
    }

    public bool IsUDPControlActive
    {
        get { return _isUDPControlActive; }
        set { _animator.SetBool("walking", false);
            _animator.SetBool("flying", value); 
            _isUDPControlActive = value; }
    }

    void Update()
    {
        Move(_move);
        Rotate(_rotate);
    }

    public void Move(Vector3 movement)
    {
        Vector3 move = _transform.TransformDirection(movement) * movementData.moveDirection.magnitude * Time.deltaTime *_moveSpeed ;
        _rigidbody.AddForce(2000 * move, ForceMode.Force); // 力を加える
        if(!_isUDPControlActive){
            if(movement == Vector3.zero)
                _animator.SetBool("walking", false);
            else
                _animator.SetBool("walking", true);
        }
        //Debug.Log("Move method executed");
    }

    public void Rotate(Vector3 rotation)
    {
        float rotationAmount = movementData.rotateDirection.magnitude * rotation.y * Time.deltaTime *_rotationSpeed;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationAmount);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        _transform.Rotate(Vector3.up * rotationAmount); 
        _rigidbody.angularVelocity = Vector3.zero; // 角速度をリセチE��
        //Debug.Log("Rotate method executed");
    }
}