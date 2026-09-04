using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject EndGameText;
    public TMP_Text bricksCounter;
    public TMP_Text livesCounter;
    public TMP_Text levelCounter;
    public TMP_Text winText;

    //Instancja naszego Game Managera
    public static GameManager instance;
    //Lista cegie³ek
    public List<GameObject> bricks = new List<GameObject>();
    //Skrypt naszej kulki
    public ArcanoidBall ball;
    //czy gra trwa
    public bool gameRun = false;

    public int lives;

    int currentLevel;
    public int maxLevel = 3;
    public BricksGenerator brickList;


    void Awake()
    {
        //Przypisanie obiektu do instancji, stworzeie Singletona
        if(instance == null)
        {
            instance = this;
        }
        currentLevel = 1;
        lives = 3;   
        
        EndGameText.SetActive(false);
        
    }

    void Update()
    {
        if(gameRun && lives <= 0)
        {
            EndGame(false);
        }

        //Je¿eli gra siê nie rozpocze³a i klikniemy spacjê to uruchamiamy pi³kê.
        if (Input.GetKeyDown(KeyCode.Space) && !gameRun)
        {
            ball.RunBall();
            gameRun = true;
        }

        //Je¿eli znikn¹ wszystkie cegie³ki koñczyy grê
        if(gameRun && bricks.Count == 0)
        {
            EndGame(true);
        }

        //Je¿eli gra skoñczona, to po naciœniêciu klawisza R zresetujemy scenê
        if(!gameRun)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        // koniec w przypadku zbicia wszystkich bloczkow na ostatnim poziomie
        if(gameRun && bricks.Count == 0 && currentLevel == maxLevel)
        {
            EndGame(true);
        }

        if ((gameRun && bricks.Count == 0 && currentLevel < maxLevel))
        {
            currentLevel++;
            ball.StopBall();
            brickList.StartLevel(currentLevel);
        }
    }

    //Funkcja koñcz¹ca grê
    public void EndGame(bool win)
    {
        EndGameText.SetActive(true);
        gameRun = false;
        string txt = win ? "Wygrana!" : "Przegrana!";
        winText.text = win ? "Wygrana!" : "Przegrana!";
        Debug.Log(txt);
        ball.StopBall();
    }

    public void UpdateUI()
    {
        bricksCounter.text = "Bricks to destroy: " + bricks.Count;
        livesCounter.text = "Lives: " + lives.ToString();
        levelCounter.text = "Level: " + currentLevel.ToString();
    }

}
