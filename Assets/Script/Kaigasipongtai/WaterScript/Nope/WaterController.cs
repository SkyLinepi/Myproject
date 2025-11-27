using UnityEngine;

public class WaterController : MonoBehaviour
{
    public int nodes = 30; // จำนวนจุดน้ำ
    public float width = 10f;
    public float springStrength = 0.1f;
    public float damping = 0.04f;
    public float spread = 0.05f;

    float[] xPos;
    float[] yPos;
    float[] velocities;
    float[] accelerations;

    float baseHeight;

    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = nodes;

        xPos = new float[nodes];
        yPos = new float[nodes];
        velocities = new float[nodes];
        accelerations = new float[nodes];

        baseHeight = transform.position.y;

        float step = width / (nodes - 1);

        for (int i = 0; i < nodes; i++)
        {
            xPos[i] = transform.position.x + i * step;
            yPos[i] = baseHeight;
            line.SetPosition(i, new Vector3(xPos[i], yPos[i], 0));
        }
    }

    void Update()
    {
        // 1. Update spring physics
        for (int i = 0; i < nodes; i++)
        {
            float force = springStrength * (baseHeight - yPos[i]) - velocities[i] * damping;
            accelerations[i] += force;
            yPos[i] += velocities[i];
            velocities[i] += accelerations[i];
            accelerations[i] = 0;

            line.SetPosition(i, new Vector3(xPos[i], yPos[i], 0));
        }

        // 2. Spread wave between neighbors
        float[] leftDeltas = new float[nodes];
        float[] rightDeltas = new float[nodes];

        for (int j = 0; j < 8; j++) // ทำหลายรอบให้คลื่นเนียนขึ้น
        {
            for (int i = 0; i < nodes; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = spread * (yPos[i] - yPos[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }

                if (i < nodes - 1)
                {
                    rightDeltas[i] = spread * (yPos[i] - yPos[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }
        }
    }

    public void Splash(float posX, float force)
    {
        for (int i = 0; i < nodes; i++)
        {
            float dist = Mathf.Abs(xPos[i] - posX);
            if (dist < 0.5f) // เฉพาะจุดใกล้ตำแหน่งตกน้ำ
            {
                velocities[i] += force;
            }
        }
    }
}