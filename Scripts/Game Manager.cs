using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;//liberary for restart of scene/game;
using UnityEngine.UI;//to interact with button
using UnityEngine;
using TMPro;
using UnityEditor;//for UI

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip swingsound;
    public AudioClip bombsound;


    public List<GameObject>Target;
    public GameObject pausedScreen;
    public bool paused;
    public Button restartButton;
    float spwanRate=2f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOver;
    public bool isGameActive;
    public GameObject tittleScreen;
    public TextMeshProUGUI TimeDisplay;
    public GameObject creator;
    private int score=0;
    public int lives=3;
    private int timevalue=60;
    public GameObject Volume;
    // private int hi=1;
    // Start is called before the first frame update
    void Start()
    {
      audioSource=GetComponent<AudioSource>();

    }
    
    void checkForPaused()
    {
        if(!paused)
        {
            paused=true;
            pausedScreen.SetActive(true);
            Time.timeScale=0;

        }
        else
        {
            paused=false;
            pausedScreen.SetActive(false);
            Time.timeScale=1;
        }
    }
    IEnumerator Timecount()
    {
       while(isGameActive)
       {
        Debug.Log(timevalue);
           yield return new WaitForSeconds(1);
           timevalue--;
           TimeDisplay.text="Time:"+timevalue;
           if(timevalue<=0)
           {
            GameOver();
           }

       }
    }
    IEnumerator spwanTargets()
    {
        StartCoroutine(Timecount());

        while(isGameActive)
        {
     
        yield return new WaitForSeconds(spwanRate);
        int index=Random.Range(0,Target.Count);//list length;
        Instantiate(Target[index]);

               
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
        checkForPaused();
        }
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        isGameActive=false;
        gameOver.gameObject.SetActive(true);
         
    }
    public void upDateScore(int ScoreToAdd)
    {

        score+=ScoreToAdd;
        scoreText.text="Score: "+score;
        
    }
    public void upDateLives(int LiveToMinus)
    {
        lives=lives-LiveToMinus;
        livesText.text="Lives:"+lives;
        
        
    }
    public void soundCall(float soundIndicator)
    {
        if(soundIndicator==1)
        {
           audioSource.PlayOneShot(swingsound,1.0f);

        }
        else{
            audioSource.PlayOneShot(bombsound,1.0f);
        }
         
    }
    public void restartGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void startGame(int difficultyValue)
    {
        spwanRate/=difficultyValue;

        tittleScreen.gameObject.SetActive(false);
        // Volume.gameObject.SetActive(false);
        
        creator.gameObject.SetActive(false);
        isGameActive=true;
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        TimeDisplay.gameObject.SetActive(true);

        upDateScore(0);
        upDateLives(0);
        StartCoroutine(spwanTargets());
    }
}
