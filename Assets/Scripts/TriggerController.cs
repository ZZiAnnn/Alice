using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerController : MonoBehaviour
{
    private string targetSceneName = "bossScene"; // Ŀ�곡��������

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����boss����");
        if (other.CompareTag("player")) // ֻ�����ǩΪ"Player"�����巢����ײ�����������Ӧ������Ը�����Ҫ�޸ı�ǩ����ײ����
        {
            Debug.Log("����boss����");
            SceneManager.LoadScene(targetSceneName); // ����Ŀ�곡��
        }
    }
}
