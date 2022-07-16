using UnityEngine;
using UnityEngine.UI;

public class UIHeartDisplay : MonoBehaviour
{
    [SerializeField] Image _slidingImage = default;
    [SerializeField] Image _backgroundImage = default;

    public void SetImage(float percent)
    {
        _slidingImage.fillAmount = percent;
        if (percent == 0f)
        {
            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 0.5f);
        }
        else
        {
            _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, _backgroundImage.color.b, 1f);
        }
    }
}