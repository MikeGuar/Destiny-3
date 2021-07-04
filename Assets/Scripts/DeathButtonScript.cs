using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathButtonScript : MonoBehaviour
{

    RectTransform deathScreen;

    void Start() {
       deathScreen = GetComponent<RectTransform>();
       deathScreen.gameObject.SetActive(false);
    }

    private string sceneNumber;

    public void OnButtonPress() {
        sceneNumber = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneNumber);
    }

}
