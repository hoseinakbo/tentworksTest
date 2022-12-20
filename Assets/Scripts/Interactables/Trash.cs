using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();

        GameManager.Instance.playerInventory.RemoveEquippedIngredient();
    }
}
