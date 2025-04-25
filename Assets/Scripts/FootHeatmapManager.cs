using UnityEngine;

public class FootHeatmapManager : MonoBehaviour
{
    public static FootHeatmapManager Instance { get; private set; }

    [Header("히트맵 설정")]
    public int textureSize = 1024;
    public RenderTexture heatmapTexture;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // 히트맵 텍스처 생성
        heatmapTexture = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.ARGB32);
        heatmapTexture.enableRandomWrite = true;
        heatmapTexture.Create();
    }

    private void OnDestroy()
    {
        if (heatmapTexture != null)
            heatmapTexture.Release();
    }
}
