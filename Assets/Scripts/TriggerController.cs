using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerController : MonoBehaviour
{
    private string targetSceneName = "bossScene"; // 目标场景的名称

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) // 只对与标签为"Player"的物体发生碰撞的情况做出反应，你可以根据需要修改标签或碰撞条件
        {
            Debug.Log("进入boss场景");
            SceneManager.LoadScene(targetSceneName); // 加载目标场景
        }
    }
}
