using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int round;
    public int ante;
    public int base_score => 100 * ante * ante;
    public BossManager blindMng;
    public void NextRound()
    {
        round++;
    }
    public void NextAnte()
    {
        ante++;
        blindMng.NewAnte(base_score, ante);
    }
    public void NewRun()
    {
        round = 1;
        ante = 1;
        blindMng.NewAnte(base_score, ante);

    }
    public void LoadHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
