using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void ReplayGame()
    {
        GameStartController.shouldStartCountdown = false; // ī��Ʈ�ٿ� �������� ����
        SceneFader.DisableNextFadeOut();
        SceneManager.LoadScene("PlayScene");
    }

    public void MoveToGarden()
    {
        SceneManager.LoadScene("GardenScene");
    }
}
