using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowOrder : MonoBehaviour
{
    public GameObject deliveredParent;
    public GameObject ingredientsParent;
    public TextMeshProUGUI timerText;

    public Image[] images;

    public void ShowDelivered()
    {
        deliveredParent.SetActive(true);
        ingredientsParent.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    public void ShowIngredients(List<Ingredient> ingredientsToShow)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < ingredientsToShow.Count)
            {
                UIManager.Instance.UpdateIngredientUI(images[i], ingredientsToShow[i]);
                images[i].gameObject.SetActive(true);
            }
            else
                images[i].gameObject.SetActive(false);
        }

        timerText.text = "Time: 0";

        deliveredParent.SetActive(false);
        ingredientsParent.SetActive(true);
        timerText.gameObject.SetActive(true);
    }
}
