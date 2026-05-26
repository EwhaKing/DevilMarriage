using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneChanger : MonoBehaviour
{
    [Header("설정 팝업 UI 오브젝트")]
    [SerializeField] private GameObject settingPopup; // 유니티에서 설정창 UI를 여기에 드래그앤드롭

    public DialogueRunner dialogueRunner;

    private void Start()
    {
        // 게임이 시작될 때 'LoadScene'이라는 명령어를 직접 강제로 등록합니다.
        if (dialogueRunner != null)
        {
            dialogueRunner.AddCommandHandler<string>("LoadScene", ChangeSceneByName);
            Debug.Log("LoadScene 명령어 등록됨");
        }
        else
        {
            Debug.LogError("SceneChanger에 Dialogue Runner가 연결되지 않았습니다!");
        }
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

    // 얀 스크립트에서 <<LoadScene 씬이름>> 을 호출하면 이름으로 씬 이동
    public void ChangeSceneByName(string sceneName)
    {
        Debug.Log($"얀 스크립트 명령: {sceneName} 씬으로 이동합니다.");
        SceneManager.LoadScene(sceneName);
    }
}