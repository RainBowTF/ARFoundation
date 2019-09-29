using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


// 필수로 있어야 되는 컴포넌트를 지정
[RequireComponent(typeof(ARPlaneMeshVisualizer), typeof(MeshRenderer), typeof(ARPlane))]
public class ARFeatheredPlane : MonoBehaviour
{
    [Tooltip("바닥 표시 텍스처의 크기")]
    [SerializeField] float _featherWidth = 0.2f;

    ARPlaneMeshVisualizer _planeMeshVisualizer;
    ARPlane _plane;
    Material _featheredMaterial;


    static List<Vector3> _featheringUVs = new List<Vector3>();
    static List<Vector3> _vertices = new List<Vector3>();

    private void Awake()
    {
        _planeMeshVisualizer = GetComponent<ARPlaneMeshVisualizer>();
        _featheredMaterial = GetComponent<MeshRenderer>().material;
        _plane = GetComponent<ARPlane>();
    }

    private void OnEnable()
    {
        _plane.boundaryChanged += ARPlaneBoundaryUpdated;
    }

    private void OnDisable()
    {
        _plane.boundaryChanged -= ARPlaneBoundaryUpdated;
    }

    void ARPlaneBoundaryUpdated(ARPlaneBoundaryChangedEventArgs eventArgs)
    {
        GenerateBoundaryUVs(_planeMeshVisualizer.mesh);
    }


    // 
    void GenerateBoundaryUVs(Mesh mesh)
    {
        int vertexCount = mesh.vertexCount;

        _featheringUVs.Clear();
        // list 오브젝트의 용량이, 총개수보다 작다면 용량을 총개수로 늘려준다 
        if( _featheringUVs.Capacity < vertexCount ) { _featheringUVs.Capacity = vertexCount; }

        mesh.GetVertices(_vertices);

        Vector3 centerInPlaneSpace = _vertices[_vertices.Count - 1];    // 가운데 vertex
        Vector3 uv = new Vector3(0, 0, 0);
        float shortestUVMapping = float.MaxValue;

        // 마지막 vertex가 가운데에 나타나야 되는 vertex이다
        for(int i = 0; i < vertexCount -1; i++)
        {
            float vertexDist = Vector3.Distance(_vertices[i], centerInPlaneSpace);

            // Remap the UV so that a UV of "1" marks the feathering boudary.
            // The ratio of featherBoundaryDistance/edgeDistance is the same as featherUV/edgeUV.
            // Rearrange to get the edge UV.
            float uvMapping = vertexDist / Mathf.Max(vertexDist - _featherWidth, 0.001f);
            uv.x = uvMapping;


            // All the UV mappings will be different. In the shader we need to know the UV value we need to fade out by.
            // Choose the shortest UV to guarentee we fade out before the border.
            // This means the feathering widths will be slightly different, we again rely on a fairly uniform plane.
            if ( shortestUVMapping > uvMapping) { shortestUVMapping = uvMapping; }

            _featheringUVs.Add(uv);
        }

        _featheredMaterial.SetFloat("_ShortestUVMapping", shortestUVMapping);

        // 
        uv.Set(0, 0, 0);
        _featheringUVs.Add(uv);

        mesh.SetUVs(1, _featheringUVs);
        mesh.UploadMeshData(false);
    }
}
