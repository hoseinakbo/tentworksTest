using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCollider : MonoBehaviour
{
    Interactable interactable;
    private void Start()
    {
        interactable = transform.parent.GetComponent<Interactable>();
    }

    public void OnInteract()
    {
        if (interactable != null)
            interactable.OnInteract();
    }

}
