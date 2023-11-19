using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string Scene;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(Scene);
    }
}
