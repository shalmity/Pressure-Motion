using UnityEngine;

public class FootPressureDrawer : MonoBehaviour
{
    public RenderTexture heatmapTexture;
    public Material drawMaterial;
    public LayerMask groundLayer;

    public float rayDistance = 0.1f;

    [Range(0f, 1f)]
    public float pressureStrength = 1.0f;

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, groundLayer))
        {
            Vector2 uv;
            if (TryGetUV(hit, out uv))
            {
                DrawPressureAtUV(uv, pressureStrength);
            }
        }
    }

    bool TryGetUV(RaycastHit hit, out Vector2 uv)
    {
        uv = Vector2.zero;
        if (hit.collider is MeshCollider meshCollider && meshCollider.sharedMesh != null)
        {
            Mesh mesh = meshCollider.sharedMesh;
            int index = hit.triangleIndex * 3;
            Vector2 uv0 = mesh.uv[mesh.triangles[index]];
            Vector2 uv1 = mesh.uv[mesh.triangles[index + 1]];
            Vector2 uv2 = mesh.uv[mesh.triangles[index + 2]];
            uv = BarycentricLerp(uv0, uv1, uv2, hit.barycentricCoordinate);
            return true;
        }
        return false;
    }

    Vector2 BarycentricLerp(Vector2 a, Vector2 b, Vector2 c, Vector3 bary)
    {
        return a * bary.x + b * bary.y + c * bary.z;
    }

    void DrawPressureAtUV(Vector2 uv, float pressure)
    {
        drawMaterial.SetVector("_UV", new Vector4(uv.x, uv.y, 0, 0));
        drawMaterial.SetFloat("_Strength", pressure);

        RenderTexture temp = RenderTexture.GetTemporary(heatmapTexture.width, heatmapTexture.height, 0);
        Graphics.Blit(heatmapTexture, temp);
        Graphics.Blit(temp, heatmapTexture, drawMaterial);
        RenderTexture.ReleaseTemporary(temp);
    }
}
