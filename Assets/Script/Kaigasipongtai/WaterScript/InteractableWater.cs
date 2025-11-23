using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.XR;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractableWater : MonoBehaviour
{
    [Header("ระบบสปริงน้ำ")]
    // ค่าคงที่สปริง
    [SerializeField] private float _springConstant = 1.4f;
    // ค่าการลดเเรงสั่น
    [SerializeField] private float _damping = 1.1f;
    // ค่าการเเพร่กระจายคลื่น
    [SerializeField] private float _spread = 6.5f;
    // ค่าการโต้ตอบการแพร่กระจายคลื่น
    [SerializeField, Range(1, 10)] private float _wavePropagationInteraction = 8f;
    //ค่่าตัวคูณการเเพร่กระจายคลื่น
    [SerializeField, Range(0f, 20f)] private float _spreadMult = 5.5f;

    [Header("เเรง")]

    // ตัวคูณเเรงที่กระทำต่อน้ำ
    public float ForceMultiplier = 0.2f;
    // เเรงสูงสุดที่กระทำต่อน้ำ 
    [Range(1f, 50f)] public float MaxForce = 5f;

    // การชนกันของวัตถุกับน้ำ
    [Header("การชนกันของวัตถุกับน้ำ")]
    // ตัวคูณรัศมีการชนของผู้เล่น
    [SerializeField, Range(1f, 10f)] private float _playerCollisionRadiusMultiplier = 4.15f;




    // ข้อมูลการสร้างตาข่ายน้ำ
    [Header("สร้างตาข่ายน้ำ")]
    [Range(2, 500)] public int NumOfXVertices = 70;

    public float width = 10f;
    public float Height = 4f;
    public Material WaterMaterial;
    private const int NUM_OF_Y_VERTICES = 2;

    [Header("สีกรอบน้ำ")]
    public Color GizmoColor = Color.red;

    private Mesh _mesh;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Vector3[] _vertices;
    private int[] _topVerticesIndex;

    private EdgeCollider2D _coll;

    public class WaterPoint
    {
        public float velcity, acceleration, pos, targetHeight;
    }

    private List<WaterPoint> _waterPoints = new List<WaterPoint>();





    private void Start()
    {
        _coll = GetComponent<EdgeCollider2D>();

        GenerateMesh();
        CraetWaterPoints();
        Debug.Log("eiei");
    }

    private void Reset()
    {
        _coll = GetComponent<EdgeCollider2D>();
        _coll.isTrigger = true;
    }

    public void FixUpdates()
    {
        // อัปเดทสปริงน้ำทุกตำเเหน่ง
        for (int i = 1; i < _waterPoints.Count - 1; i++)
        {
            WaterPoint point = _waterPoints[i];

            float x = point.pos - point.targetHeight;
            float acceleration = -_springConstant * x - _damping * point.velcity;
            point.pos += point.velcity * _spread * Time.fixedDeltaTime;
            _vertices[_topVerticesIndex[i]].y = point.pos;
            point.velcity += acceleration * _spreadMult * Time.fixedDeltaTime;
        }

        // อัปเดทการโต้ตอบการแพร่กระจายคลื่น
        for (int j = 0; j < _wavePropagationInteraction; j++)
        {
            float[] leftDeltas = new float[_waterPoints.Count];
            float[] rightDeltas = new float[_waterPoints.Count];

            for (int i = 1; i < _waterPoints.Count - 1; i++)
            {
                leftDeltas[i] = _spread * (_waterPoints[i].pos - _waterPoints[i - 1].pos) * Time.fixedDeltaTime;
                rightDeltas[i] = _spread * (_waterPoints[i].pos - _waterPoints[i + 1].pos) * Time.fixedDeltaTime;
            }

            for (int i = 1; i < _waterPoints.Count - 1; i++)
            {
                _waterPoints[i - 1].velcity += leftDeltas[i];
                _waterPoints[i + 1].velcity += rightDeltas[i];
            }
        }


        // อัปเดทตาข่ายน้ำ
        _mesh.vertices = _vertices;
    }

    public void Splash(Collider2D collider, float force)
    {
        float radius = collider.bounds.extents.x * _playerCollisionRadiusMultiplier;
        Vector2 center = collider.transform.position;

        for (int i = 0; i < _waterPoints.Count; i++)
        {
            Vector2 vertexWorldsPos = transform.TransformPoint(_vertices[_topVerticesIndex[i]]);

            if (IsPointInsideCircle(vertexWorldsPos, center, radius))
            {
                _waterPoints[i].velcity += force;
            }
        }
    }

    private bool IsPointInsideCircle(Vector2 point, Vector2 center, float radius)
    {
        float distanceSquared = (point - center).sqrMagnitude;
        return distanceSquared <= radius * radius;
    }

    public void ResetEdgeCollider()
    {
        _coll = GetComponent<EdgeCollider2D>();

        // Edge Collider ต้องมีจำนวนจุดเท่ากับจำนวนเวอร์เท็กซ์ในแถวบนสุด
        // ซึ่งคือ NumOfXVertices จุด ไม่ใช่แค่ 2 จุด
        Vector2[] newPoints = new Vector2[NumOfXVertices];

        for (int i = 0; i < NumOfXVertices; i++)
        {
            // ใช้เวอร์เท็กซ์ทั้งหมดในแถวบนสุด
            newPoints[i] = _vertices[_topVerticesIndex[i]];
        }

        _coll.offset = Vector2.zero;
        _coll.points = newPoints;
    }

    public void GenerateMesh()
    {
        _mesh = new Mesh();
        _mesh.Clear(); // --- FIX: ป้องกัน Mesh เก่าค้าง ---

        _vertices = new Vector3[NumOfXVertices * NUM_OF_Y_VERTICES];
        _topVerticesIndex = new int[NumOfXVertices];

        // *** แก้ไข: ลูป Y ต้องวนตาม NUM_OF_Y_VERTICES (คือ 2) ***
        for (int y = 0; y < NUM_OF_Y_VERTICES; y++)
        {
            // ลูป X วนตามจำนวนคอลัมน์ของเวอร์เท็กซ์ (NumOfXVertices)
            for (int x = 0; x < NumOfXVertices; x++)
            {
                float xPos = (x / (float)(NumOfXVertices - 1)) * width - width / 2f;

                // *** แก้ไข: คำนวณ Y position ให้เป็นแถวล่าง (-Height/2) และแถวบน (+Height/2) ***
                // y = 0 คือแถวล่าง, y = 1 คือแถวบน (เพราะ NUM_OF_Y_VERTICES = 2)
                float yPos = (y == 0) ? -Height / 2f : Height / 2f;

                int a = y * NumOfXVertices + x; // Index ถูกต้องแล้ว

                // บรรทัด 75 (แก้ไขแล้ว): ตอนนี้ Index จะอยู่ภายในขอบเขตของ Array (สูงสุด 139)
                _vertices[a] = new Vector3(xPos, yPos, 0f);

                // *** แก้ไข: ตรวจสอบว่าเป็นแถวบนสุด (y = NUM_OF_Y_VERTICES - 1 หรือ y = 1) ***
                if (y == NUM_OF_Y_VERTICES - 1)
                {
                    _topVerticesIndex[x] = a;
                }
            }
        }
        int[] triangles = new int[(NumOfXVertices - 1) * (NUM_OF_Y_VERTICES - 1) * 6];
        int index = 0;

        for (int y = 0; y < NUM_OF_Y_VERTICES - 1; y++)
        {
            for (int x = 0; x < NumOfXVertices - 1; x++)
            {
                int bottomLeft = y * NumOfXVertices + x;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + NumOfXVertices;
                int topRight = topLeft + 1;

                triangles[index++] = bottomLeft;
                triangles[index++] = topRight;
                triangles[index++] = bottomRight;

                triangles[index++] = bottomLeft;
                triangles[index++] = topLeft;
                triangles[index++] = topRight;
            }
        }

        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            uvs[i] = new Vector2(_vertices[i].x / width + 0.5f, _vertices[i].y / Height + 0.5f);

        }
        //  FIX: ให้ MeshRenderer และ MeshFilter ถูกกำหนดแน่นอน ก่อนใช้งาน
        if (_meshRenderer == null)
        {
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (_meshRenderer == null)
                _meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        if (_meshFilter == null)
        {
            _meshFilter = gameObject.GetComponent<MeshFilter>();
            if (_meshFilter == null)
                _meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        _meshRenderer.material = WaterMaterial;

        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _meshFilter.mesh = _mesh;
    }

    private void CraetWaterPoints()
    {
        _waterPoints.Clear();
        for (int i = 0; i < _topVerticesIndex.Length; i++)
        {
            _waterPoints.Add(new WaterPoint { pos = _vertices[_topVerticesIndex[i]].y, targetHeight = _vertices[_topVerticesIndex[i]].y, });
        }
    }
}


[CustomEditor(typeof(InteractableWater))]
public class InteractableWaterEditor : Editor
{
    private InteractableWater _water;
    private void OnEnable()
    {
        _water = (InteractableWater)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        root.Add(new VisualElement { style = { height = 10 } });
        Button generateMeshButton = new Button(() => _water.GenerateMesh())
        {
            text = "Generate Mesh"

        };
        root.Add(generateMeshButton);

        Button placeEdgeColliderButton = new Button(() => _water.ResetEdgeCollider())
        {
            text = "Reset Edge Collider"
        };
        root.Add(placeEdgeColliderButton);
        return root;

    }
    private void ChangeDimensions(ref float width, ref float height, float calculatedWidthMax, float calculatedHeightMax)
    {
        width = Mathf.Max(0.1f, calculatedWidthMax);
        height = Mathf.Max(0.1f, calculatedHeightMax);
    }

    private void OnSceneGUI()
    {
        Handles.color = _water.GizmoColor;
        Vector3 center = _water.transform.position;
        Vector3 size = new Vector3(_water.width, _water.Height, 0.1f);
        Handles.DrawWireCube(center, size);

        float handleSize = HandleUtility.GetHandleSize(center) * 0.1f;
        Vector3 snap = Vector3.one * 0.1f;

        Vector3[] corners = new Vector3[4];
        corners[0] = center + new Vector3(-_water.width / 2, -_water.Height / 2, 0);  // Bottom Left
        corners[1] = center + new Vector3(_water.width / 2, -_water.Height / 2, 0);   // Bottom Right
        corners[2] = center + new Vector3(-_water.width / 2, _water.Height / 2, 0);   // Top Left
        corners[3] = center + new Vector3(_water.width / 2, _water.Height / 2, 0);    // Top Right

        // === Bottom Left ===
        EditorGUI.BeginChangeCheck();
        Vector3 newBL = Handles.FreeMoveHandle(corners[0], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            _water.width = corners[1].x - newBL.x;
            _water.Height = corners[2].y - newBL.y;
            _water.transform.position += (newBL - corners[0]) / 2;
        }

        // === Bottom Right ===
        EditorGUI.BeginChangeCheck();
        Vector3 newBR = Handles.FreeMoveHandle(corners[1], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            _water.width = newBR.x - corners[0].x;
            _water.Height = corners[3].y - newBR.y;
            _water.transform.position += (newBR - corners[1]) / 2;
        }

        // === Top Left ===
        EditorGUI.BeginChangeCheck();
        Vector3 newTL = Handles.FreeMoveHandle(corners[2], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            _water.width = corners[3].x - newTL.x;
            _water.Height = newTL.y - corners[0].y;
            _water.transform.position += (newTL - corners[2]) / 2;
        }

        // === Top Right ===
        EditorGUI.BeginChangeCheck();
        Vector3 newTR = Handles.FreeMoveHandle(corners[3], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            _water.width = newTR.x - corners[2].x;
            _water.Height = newTR.y - corners[1].y;
            _water.transform.position += (newTR - corners[3]) / 2;
        }

        if (GUI.changed)
        {
            Undo.RecordObject(_water, "Resize Water Mesh");
            EditorUtility.SetDirty(_water);
            _water.GenerateMesh();
        }
    }

}
