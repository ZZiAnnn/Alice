using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.name == "gameScene_1")
            transform.position -= new Vector3(Time.deltaTime * TilemapMove.Speed, 0, 0);
        if (currentScene.name == "gameScene_2")
            transform.position -= new Vector3(Time.deltaTime * bridgeMove.Speed * 2, 0, 0);
    }
}
