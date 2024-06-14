using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{

    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance = 0;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    public int CurrentBalance { get { return currentBalance; } }
    // Start is called before the first frame update
    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();

    }

    void UpdateDisplay()
    {
        textMeshProUGUI.text = "Golds: " + CurrentBalance;
    }
   public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        if(currentBalance < 0)
        {
            ReloadScene();
          
        }
        UpdateDisplay();
    }
   void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);

    }
}
