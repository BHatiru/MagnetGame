using UnityEngine;

public class MagnetField : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _units;
    [SerializeField] private AudioSource _source;
    private bool _enable = true;

    private int _state=1;

    private int State
    {
        get { return _state; }
        set
        {
            _state = value;
            if (_state > 1)
            {
                _state = 0;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            State++;

            if (State == 1)
            {
                _enable = true;
            }
            else
            {
                _enable = false;
            }
        }


        if (_enable)
        {
            _particleSystem.Play();
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
            if (colliders.Length == 0)
            {
                return;
            }

            foreach (Collider collider in colliders)
            {
                IMagnetInteractable interactable = collider.GetComponent<IMagnetInteractable>();
                if (interactable == null)
                {
                    continue;
                }

                interactable.Interact(transform);
            }
        }
        else
        {
            _source.Play();
            _particleSystem.Stop();
            IMagnetInteractable[] interactables = _units.GetComponentsInChildren<IMagnetInteractable>();
            foreach (IMagnetInteractable interactable in interactables)
            {
                interactable.StopInteract();
            }
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
