using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int difficultyValue;
    
    // Start is called before the first frame update
    void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(setDifficulty);
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void setDifficulty()
    {
        //
        gameManager.startGame(difficultyValue);
        Debug.Log(button.gameObject.name+"was clicked");
    }
}
