using UnityEngine;
using TMPro;

public class CollectablesManager : MonoBehaviour
{
    public TMP_Text collectablesText;

    private int totalCollectables;
    private int collectedCount;
    

    void Start()
    {
        // Find all collectables in the scene at the beginning
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
        totalCollectables = collectables.Length;
        collectedCount = 0;

        // Subscribe to the collect event of each collectable
        foreach (GameObject collectable in collectables)
        {
            collectable.GetComponent<CollectableScript>().OnCollected += OnCollectableCollected;
        }

        // Update UI text
        UpdateUI();
    }

    void OnCollectableCollected()
    {
        // Increase the collected count
        collectedCount++;

        // Update UI text
        UpdateUI();

        // Check if all collectables are collected
        if (collectedCount == totalCollectables)
        {
            Debug.Log("All collectables collected! You win!");
            GetComponent<LevelManagerScript>().endingPanel.SetActive(true);
            
            

            // Add any additional actions for when all collectables are collected
        }

        
    }

    void UpdateUI()
    {
        // Update the UI text to show collected and remaining collectables in fraction format
        collectablesText.text = " " + collectedCount + "/" + totalCollectables;
    }
}