using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioSource pushSound;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button PictureBookButton;
    [SerializeField] private Button RankingButton;
    private void Awake()
    {
        StartButton.onClick.AddListener(pushSound.Play);
        PictureBookButton.onClick.AddListener(pushSound.Play);
        RankingButton.onClick.AddListener(pushSound.Play);
        StartButton.onClick.AddListener(MainGameStart);
        PictureBookButton.onClick.AddListener(PictureBookStart);
        RankingButton.onClick.AddListener(RankingStart);
    }
    private void MainGameStart()
    {
        SceneManager.LoadScene("MainGame");
    }
    private void PictureBookStart()
    {
        SceneManager.LoadScene("PictureBook");
    }
    private void RankingStart()
    {
        SceneManager.LoadScene("Ranking");
    }
}
