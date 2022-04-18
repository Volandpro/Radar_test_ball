using UnityEngine;

namespace Spawn.InteractableObjects
{
    public interface IInteractableObjectPositionChooser
    {
        Vector3 GetPosition();
    }
}