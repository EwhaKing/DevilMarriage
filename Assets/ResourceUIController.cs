using UnityEngine;
using UnityEngine.UI; // 유니티 UI 컴포넌트를 제어하기 위해 필수입니다.

public class ResourceUIController : MonoBehaviour
{
    [Header("UI 이미지 연결")]
    [Tooltip("쥐의 피를 표시할 UI Image (Image Type이 반드시 'Filled'여야 합니다)")]
    public Image bloodGauge;
    
    [Tooltip("정신력을 표시할 UI Image")]
    public Image sanityGauge;

    /// <summary>
    /// 쥐의 피 수치가 변할 때마다 PuzzleController가 이 함수를 호출합니다.
    /// </summary>
    /// <param name="currentBlood">현재 쥐의 피</param>
    /// <param name="maxBlood">최대 쥐의 피</param>
    public void UpdateBloodGauge(float currentBlood, float maxBlood)
    {
        if (bloodGauge != null && maxBlood > 0)
        {
            // Image.fillAmount는 0.0(빈 상태) ~ 1.0(가득 찬 상태) 사이의 값을 받습니다.
            bloodGauge.fillAmount = currentBlood / maxBlood;
        }
    }

    /// <summary>
    /// 정신력 수치가 변할 때마다 PuzzleController가 이 함수를 호출합니다.
    /// </summary>
    /// <param name="currentSanity">현재 정신력</param>
    /// <param name="maxSanity">최대 정신력</param>
    public void UpdateSanityGauge(int currentSanity, int maxSanity)
    {
        if (sanityGauge != null && maxSanity > 0)
        {
            // 주의: int 연산은 소수점을 버리므로, 정확한 비율(float)을 얻기 위해 명시적 형변환이 필요합니다.
            sanityGauge.fillAmount = (float)currentSanity / maxSanity;
        }
    }
}