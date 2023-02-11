using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PushInUpUnit : InteractableUnitBase, IMagnetInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private float deltaY_Coefficient=1f;

    private bool _isSleeping;
    private bool _isInteractable = true;
    private Transform _magnet;

    public void Interact(Transform magnet)
    {
        _isInteractable = true;
        _magnet = magnet;
        _isSleeping = false;
    }

    public void StopInteract()
    {
        _isInteractable = false;
    }

    private void Update()
    {
        if (_isSleeping || _magnet == null || _isInteractable == false)
        {
            return;
        }

        Vector3 outOfMagnitVector = _magnet.position - transform.position;
        

        float distance = outOfMagnitVector.magnitude;

        if (distance > _pushDistance)
        {
            _isSleeping = true;
            return;
        }
        
        float pushForce = 1 - distance / _pushDistance;
        pushForce *= _pushForce;

        float deltaY = pushForce * deltaY_Coefficient;

        outOfMagnitVector.y += deltaY; 
        
        
        gameObject.GetComponent<Rigidbody>().AddForce(outOfMagnitVector.normalized * pushForce , ForceMode.Force);
    }
}