using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OverMenuController : MonoBehaviour {

    public GameObject SelectIcon;

    public Vector2[] indexPosition = new Vector2[] {
        new Vector2(-100, -45),
        new Vector2(-100, -90)
    };

    private int currentIndex = 0;

	public void OnPlayAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void OnMenuButtonClick()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown("up"))
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = indexPosition.Length - 1;

            SelectIcon.transform.localPosition = indexPosition[currentIndex];
        }
        else if (Input.GetKeyDown("down"))
        {
            currentIndex++;
            if (currentIndex > indexPosition.Length - 1)
                currentIndex = 0;

            SelectIcon.transform.localPosition = indexPosition[currentIndex];
        }

        if(Input.GetButtonDown("Jump"))
        {
            switch (currentIndex)
            {
                case 0:
                    {
                        OnPlayAgainButtonClick();
                        break;
                    }
                case 1:
                    {
                        OnMenuButtonClick();
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
