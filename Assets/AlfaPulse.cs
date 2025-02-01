using UnityEngine;
using UnityEngine.UI;

public class AlphaPulse : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float pulseSpeed = 1f;
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 1f;

    private Color _originalColor;

    private void Start()
    {
        targetImage = GetComponent<Image>();
        _originalColor = targetImage.color;
    }

    private void Update()
    {
        if (targetImage == null) return;
        float newAlpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        targetImage.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, newAlpha);
    }
}