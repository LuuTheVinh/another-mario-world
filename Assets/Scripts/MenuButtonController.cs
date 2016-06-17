using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class MenuButtonController :  OverMenuController
    {
        public override void CheckBtn()
        {
            if (Input.GetButtonDown("Jump"))
            {
                switch (currentIndex)
                {
                    case 0:
                        {
                            OnPlayButtonClick();
                            break;
                        }
                    case 1:
                        {
                            OnExitButtonClick();
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        public void OnPlayButtonClick()
        {
            SceneManager.LoadScene("Stage0", LoadSceneMode.Single);
        }

        public void OnExitButtonClick()
        {
            
        }
    }
}
