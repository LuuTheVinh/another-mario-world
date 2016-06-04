using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OverMenuController : MonoBehaviour {

	public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void OnMenuButtonClick()
    {

    }
}
