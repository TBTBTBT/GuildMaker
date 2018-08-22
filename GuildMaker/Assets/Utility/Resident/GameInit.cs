using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit {

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        Application.targetFrameRate = 60;
        string current = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Resident");
        SceneManager.LoadScene(current);
    }
}
