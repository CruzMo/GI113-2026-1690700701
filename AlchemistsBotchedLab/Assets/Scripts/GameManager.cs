
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Store ingredient amounts
    public int[] ingredientCounts = new int[8];

    // Store the UI text for each ingredient
    public TextMeshProUGUI[] countTexts;

    // Add ingredient amount
    public void AddIngredient(int index)
    {
        if (index >= 0 && index < ingredientCounts.Length)
        {
            ingredientCounts[index]++;

            countTexts[index].text = ingredientCounts[index].ToString();
        }
    }
}
