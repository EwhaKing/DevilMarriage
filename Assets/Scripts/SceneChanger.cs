using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    [Header("설정 팝업 UI 오브젝트")]
    [SerializeField] private GameObject settingPopup; // 유니티에서 설정창 UI를 여기에 드래그앤드롭

    /// <summary>
    /// 1. 지정한 인덱스의 씬으로 이동 (게임시작, 도감 등)
    /// </summary>
    public void ChangeSceneByIndex(int sceneIndex)
    {
        Debug.Log(sceneIndex + "번 씬으로 이동합니다.");
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// 2. 설정 팝업창 열기
    /// </summary>
    public void OpenSettingsPopup()
    {
        if (settingPopup != null)
        {
            settingPopup.SetActive(true); // 설정창 켜기
        }
    }

    /// <summary>
    /// 3. 설정 팝업창 닫기
    /// </summary>
    public void CloseSettingsPopup()
    {
        if (settingPopup != null)
        {
            settingPopup.SetActive(false); // 설정창 끄기
        }
    }

    /// <summary>
    /// 4. 게임 종료
    /// </summary>
    public void ClickExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}