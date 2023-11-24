using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static void ChangeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void ChangeSceneDelay(string nextScene)
    {
        StartCoroutine(WaitToChangeScene(nextScene));
    }

    private IEnumerator WaitToChangeScene(string nextScene)
    {
        yield return new WaitForSeconds(1.5f);

        ChangeScene(nextScene);
    }
}
