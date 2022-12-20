using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stove : Interactable
{
    public Ingredient currentIngredient;
    public Image ingredientImage;
    public GameObject timer;
    public Image timerFill;

    public override void OnInteract()
    {
        base.OnInteract();

        StopCoroutine("DoCooking");
        timer.SetActive(false);

        Ingredient playerIngredient = GameManager.Instance.playerInventory.equippedIngredient;
        Ingredient tableIngredient = currentIngredient;
        currentIngredient = playerIngredient;
        GameManager.Instance.playerInventory.EquipIngredient(tableIngredient);
        UIManager.Instance.UpdateIngredientUI(ingredientImage, currentIngredient);

        if (playerIngredient != Ingredient.Meat && playerIngredient != Ingredient.CookedMeat && playerIngredient != Ingredient.None)
            UIManager.Instance.ShowToastMessage("You can only cook meat!");

        if (playerIngredient == Ingredient.Meat)
            StartCoroutine("DoCooking");
    }

    IEnumerator DoCooking()
    {
        timerFill.fillAmount = 0;
        timer.SetActive(true);
        float totalTime = GameManager.Instance.meatCookingTime;
        float timeRemaining = totalTime;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerFill.fillAmount = (totalTime - timeRemaining) / totalTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        timer.SetActive(false);
        currentIngredient = Ingredient.CookedMeat;
        UIManager.Instance.UpdateIngredientUI(ingredientImage, currentIngredient);
    }
}
