using UnityEngine;

public interface IMagnetInteractable
{
    void Interact(Transform transform);
    void StopInteract();
}