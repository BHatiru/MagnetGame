using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private BoxCollider _collider;

    private Bounds _bounds;

    private void Awake()
    {
        _bounds = _collider.bounds;
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 nextPosition = transform.position + Vector3.right * horizontal + Vector3.forward * vertical;
        
        if(!_bounds.Contains(nextPosition))
        {
            nextPosition = _bounds.ClosestPoint(nextPosition);
        }

        transform.position = Vector3.Lerp(transform.position, nextPosition, _speed * Time.deltaTime);
    }
}