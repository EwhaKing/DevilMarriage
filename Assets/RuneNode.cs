using UnityEngine;

// 이 스크립트는 맵에 배치될 각각의 룬(타일) 오브젝트에 붙습니다.
public class RuneNode : MonoBehaviour
{
    private void OnMouseDown()
    {
        // 마우스로 이 룬을 클릭하면, 씬에 있는 PuzzleController 매니저를 찾습니다.
        PuzzleController manager = FindFirstObjectByType<PuzzleController>();
        
        if (manager != null)
        {
            // 컨트롤러에게 클릭된 룬(자기 자신)을 전달합니다.
            manager.OnRuneClicked(this);
        }
    }
}