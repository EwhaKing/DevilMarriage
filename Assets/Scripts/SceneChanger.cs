using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("이동할 씬 이름")]
    [SerializeField] private string IntroSceneName = "IntroScene"; // 게임시작 시 이동할 씬
    [SerializeField] private string CollectionSceneName = "CollectionScene";     // 악마도감 이동 시 이동할 씬

    [Header("설정 팝업 UI (있을 경우만 연결)")]
    [SerializeField] private GameObject settingPopup;

    /// <summary>
    /// 1. 게임 시작 버튼 (인게임/스토리 씬으로 이동)
    /// </summary>
    public void ClickStartGame()
    {
        Debug.Log("게임 시작! 씬 전환: " + IntroSceneName);
        SceneManager.LoadScene(IntroSceneName);
    }

    /// <summary>
    /// 2. 악마 도감 버튼 (도감 씬으로 이동)
    /// </summary>
    public void ClickOpenCollection()
    {
        Debug.Log("악마 도감 열기! 씬 전환: " + CollectionSceneName);
        SceneManager.LoadScene(CollectionSceneName);
    }

    /// <summary>
    /// 3. 설정 버튼 (창 띄우기)
    /// </summary>
    public void ClickOpenSettings()
    {
        Debug.Log("설정 창 켜기");
        if (settingPopup != null)
        {
            // 설정 팝업 오브젝트가 연결되어 있다면 활성화
            settingPopup.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Setting Popup 오브젝트가 인스펙터에 연결되지 않았습니다.");
        }
    }

    /// <summary>
    /// 4. 게임 종료 버튼
    /// </summary>
    public void ClickExitGame()
    {
        Debug.Log("게임 종료");

#if UNITY_EDITOR
        // 유니티 에디터 상에서 테스트할 때 꺼지도록 처리
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 실제 게임이 종료됨
        Application.Quit();
#endif
    }
}