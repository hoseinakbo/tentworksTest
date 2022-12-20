using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public IngredientUI ingredientUI;

    public Ingredient equippedIngredient;

    public float interactDistance = 2.5f;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            InteractButtonPressed();
        }
    }

    void InteractButtonPressed()
    {
        int layerMask = 1 << 6;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactDistance, layerMask))
        {
            InteractableCollider col = hit.collider.GetComponent<InteractableCollider>();
            if (col != null)
                col.OnInteract();
        }
        else
        {
            //Debug.Log("Did not Hit");
        }
    }

    public void EquipIngredient(Ingredient toEquip)
    {
        equippedIngredient = toEquip;
        UpdateUI();
    }
    public void RemoveEquippedIngredient()
    {
        equippedIngredient = Ingredient.None;
        UpdateUI();
    }

    void UpdateUI()
    {
        UIManager.Instance.UpdatePlayerIngredientUI();
    }
}


public enum Ingredient
{
    None,
    Vegetable,
    ChoppedVegetable,
    Meat,
    CookedMeat,
    Cheese
}