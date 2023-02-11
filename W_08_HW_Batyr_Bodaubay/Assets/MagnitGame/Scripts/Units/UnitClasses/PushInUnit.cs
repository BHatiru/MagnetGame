using UnityEngine;

public class PushInUnit : InteractableUnitBase, IMagnetInteractable
{
    [SerializeField] private float _speed;

    private Transform _magnet;
    private bool _isSleeping;
    private bool _isInteractable = true;

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
        outOfMagnitVector.y = 0;
        float distance = outOfMagnitVector.magnitude;

        if (distance > _pushDistance)
        {
            _isSleeping = true;
            return;
        }

        float pushForce = 1 - distance / _pushDistance;
        pushForce *= _pushForce;

        /*Vector3 nextPosition = transform.position + outOfMagnitVector.normalized * pushForce;
        transform.position = Vector3.Lerp(transform.position, nextPosition, _speed * Time.deltaTime);*/

        gameObject.GetComponent<Rigidbody>().AddForce(outOfMagnitVector.normalized * pushForce , ForceMode.Force);
    }
}
