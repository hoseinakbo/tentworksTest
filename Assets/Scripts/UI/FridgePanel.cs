using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgePanel : MonoBehaviour
{

    public void ShowPanel()
    {
        UIManager.Instance.inGameUI.SetActive(false);
        gameObject.SetActive(true);
        UIManager.Instance.hasActivePanel = true;
    }

    public void ClosePanel()
    {
        UIManager.Instance.inGameUI.SetActive(true);
        gameObject.SetActive(false);
        UIManager.Instance.hasActivePanel = false;
    }

    public void VegetablesButtonOnPress()
    {
        GameManager.Instance.playerInventory.EquipIngredient(Ingredient.Vegetable);
        ClosePanel();
    }
    public void CheeseButtonOnPress()
    {
        GameManager.Instance.playerInventory.EquipIngredient(Ingredient.Cheese);
        ClosePanel();
    }
    public void MeatButtonOnPress()
    {
        GameManager.Instance.playerInventory.EquipIngredient(Ingredient.Meat);
        ClosePanel();
    }

}
