using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Interactable
{

    public override void OnInteract()
    {
        base.OnInteract();

        if (GameManager.Instance.playerInventory.equippedIngredient == Ingredient.None)
            UIManager.Instance.fridgePanel.ShowPanel();
        else
            UIManager.Instance.ShowToastMessage("Your hands are full!");
    }

}
