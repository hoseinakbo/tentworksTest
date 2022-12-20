using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Table : Interactable
{
    public Ingredient currentIngredient;
    public Image ingredientImage;
    public GameObject timer;
    public Image timerFill;

    public override void OnInteract()
    {
        base.OnInteract();
        
        StopCoroutine("DoChopping");
        timer.SetActive(false);

        Ingredient playerIngredient = GameManager.Instance.playerInventory.equippedIngredient;
        Ingredient tableIngredient = currentIngredient;

        currentIngredient = playerIngredient;
        GameManager.Instance.playerInventory.EquipIngredient(tableIngredient);
        UIManager.Instance.UpdateIngredientUI(ingredientImage, currentIngredient);

        if (playerIngredient != Ingredient.Vegetable && playerIngredient != Ingredient.ChoppedVegetable && playerIngredient != Ingredient.None)
            UIManager.Instance.ShowToastMessage("You can only chop vegetables!");
            
        if (playerIngredient == Ingredient.Vegetable)
            StartCoroutine("DoChopping");
    }

    IEnumerator DoChopping()
    {
        timerFill.fillAmount = 0;
        timer.SetActive(true);
        float totalTime = GameManager.Instance.vegetableChoppingTime;
        float timeRemaining = totalTime;

        while(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerFill.fillAmount = (totalTime - timeRemaining) / totalTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        timer.SetActive(false);
        currentIngredient = Ingredient.ChoppedVegetable;
        UIManager.Instance.UpdateIngredientUI(ingredientImage, currentIngredient);
    }
}
