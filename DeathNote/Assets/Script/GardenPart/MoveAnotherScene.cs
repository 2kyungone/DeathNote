using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToDoGam : MonoBehaviour
{
    public string targetSceneName;

    public void GoToTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
