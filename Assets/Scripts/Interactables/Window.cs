using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Window : Interactable
{
    public List<Ingredient> currentIngredients;

    public TextMeshProUGUI timerText;
    public WindowOrder windowOrder;

    public List<Image> ingredientImages;
    public List<Ingredient> targetIngredients;

    public TextMeshProUGUI scoreText;

    float timeStarted;

    void Start()
    {
        GenerateNewOrder();
    }

    public override void OnInteract()
    {
        base.OnInteract();

        Ingredient playerIngredient = GameManager.Instance.playerInventory.equippedIngredient;

        if (playerIngredient != Ingredient.None)
        {
            if (currentIngredients.Count == 3)
            {
                GameManager.Instance.playerInventory.EquipIngredient(currentIngredients[2]);
                currentIngredients.RemoveAt(2);
            }

            bool existsInTarget = false;
            int neededCount = 0;
            for (int i = 0; i < targetIngredients.Count; i++)
            {
                if (targetIngredients[i] == playerIngredient)
                {
                    existsInTarget = true;
                    neededCount++;
                }
            }
            int currentCount = 0;
            for (int i = 0; i < currentIngredients.Count; i++)
                if (currentIngredients[i] == playerIngredient)
                    currentCount++;

            if (existsInTarget && currentCount < neededCount)
            {
                currentIngredients.Add(playerIngredient);
                GameManager.Instance.playerInventory.RemoveEquippedIngredient();
            }
            else
            {
                UIManager.Instance.ShowToastMessage("This ingredient isn't for this window!");
            }
        }
        else
        {
            if (currentIngredients.Count != 0)
            {
                int lastItem = currentIngredients.Count - 1;

                GameManager.Instance.playerInventory.EquipIngredient(currentIngredients[lastItem]);
                currentIngredients.RemoveAt(lastItem);
            }
        }

        UpdateCurrentUI();

        //Check order complete:
        if (currentIngredients.Count == targetIngredients.Count)
        {
            List<Ingredient> tempTargets = new List<Ingredient>(targetIngredients);
            for (int i = 0; i < currentIngredients.Count; i++)
            {
                for (int j = 0; j < tempTargets.Count; j++)
                {
                    if (currentIngredients[i] == tempTargets[j])
                    {
                        tempTargets.RemoveAt(j);
                        break;
                    }
                }
            }
            if (tempTargets.Count == 0) //Order is complete
                StartCoroutine("OrderComplete");

        }
    }

    IEnumerator OrderComplete()
    {
        windowOrder.ShowDelivered();
        CalculateReward();
        currentIngredients = new List<Ingredient>();
        UpdateCurrentUI();

        yield return new WaitForSeconds(GameManager.Instance.newOrderWaitTime);

        GenerateNewOrder();
    }

    void CalculateReward()
    {
        int score = 0;

        for (int i = 0; i < targetIngredients.Count; i++)
        {
            if (targetIngredients[i] == Ingredient.ChoppedVegetable)
                score += GameManager.Instance.vegetableScore;
            else if (targetIngredients[i] == Ingredient.CookedMeat)
                score += GameManager.Instance.meatScore;
            else if (targetIngredients[i] == Ingredient.Cheese)
                score += GameManager.Instance.cheeseScore;
        }

        score -= Mathf.FloorToInt(Time.time - timeStarted);

        UIManager.Instance.ShowScoreMessage(scoreText, score);
        GameManager.Instance.AddScore(score);
    }

    void GenerateNewOrder()
    {
        StopCoroutine("OrderTimer");

        int numberOfIngredients = Random.value < 0.5 ? 2 : 3;
        targetIngredients = new List<Ingredient>();
        for (int i = 0; i < numberOfIngredients; i++)
        {
            int rand = Random.Range(0, 3);
            Ingredient ing = Ingredient.None;
            if (rand == 0)
                ing = Ingredient.ChoppedVegetable;
            else if (rand == 1)
                ing = Ingredient.CookedMeat;
            else if (rand == 2)
                ing = Ingredient.Cheese;
            targetIngredients.Add(ing);
        }
        windowOrder.ShowIngredients(targetIngredients);
        UpdateCurrentUI();

        StartCoroutine("OrderTimer");
    }

    IEnumerator OrderTimer()
    {
        timeStarted = Time.time;

        while (true)
        {
            timerText.text = "Time: " + ((int)(Time.time - timeStarted));

            yield return new WaitForEndOfFrame();
        }
    }

    void UpdateCurrentUI()
    {
        if (currentIngredients.Count > 0)
            ingredientImages[0].transform.parent.gameObject.SetActive(true);
        else
            ingredientImages[0].transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            if (i < currentIngredients.Count)
            {
                UIManager.Instance.UpdateIngredientUI(ingredientImages[i], currentIngredients[i]);
                ingredientImages[i].gameObject.SetActive(true);
            }
            else
            {
                ingredientImages[i].gameObject.SetActive(false);
            }
        }
    }


}
