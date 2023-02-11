using System.Linq;
using UnityEngine;

public class MagnetMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _foorLayer;


    private void Update()
    {

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, 100f, _foorLayer.value);

        if (!hitSomething)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, hit.point, _speed * Time.deltaTime);

    }
}
