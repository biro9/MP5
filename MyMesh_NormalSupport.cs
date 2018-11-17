using UnityEngine;

public partial class MyMesh : MonoBehaviour 
{
    LineSegment[] mNormals;

    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.05f);
            mNormals[i].transform.SetParent(transform);
        }
        UpdateNormals(v, n);
    }

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 1.0f * n[i]);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    void ComputeNormals(Vector3[] v, Vector3[] n)
    {
        //trinorms for each normal according to the corresponding tri pos [the fuck is this even help me please]
        Vector3[] triNormal = new Vector3[v.Length - 1];

        for (int i = 0; i < triNormal.Length; i++)
        {
            
        }

        //put in above for loop for scalability
        triNormal[0] = FaceNormal(v, 3, 4, 0);
        triNormal[1] = FaceNormal(v, 0, 4, 1);
        triNormal[2] = FaceNormal(v, 4, 5, 1);
        triNormal[3] = FaceNormal(v, 1, 5, 2);
        triNormal[4] = FaceNormal(v, 6, 7, 3);
        triNormal[5] = FaceNormal(v, 3, 7, 4);
        triNormal[6] = FaceNormal(v, 7, 8, 4);
        triNormal[7] = FaceNormal(v, 4, 8, 5);


        for (int i = 0; i < n.Length; i++)
        {
            
        }

        //put in above for loop for scalability
        n[0] = (triNormal[0] + triNormal[1]).normalized;
        n[1] = (triNormal[1] + triNormal[2] + triNormal[3]).normalized;
        n[2] = triNormal[3].normalized;
        n[3] = (triNormal[0] + triNormal[4] + triNormal[5]).normalized;
        n[4] = (triNormal[0] + triNormal[1] + triNormal[2] + triNormal[5] + triNormal[6] + triNormal[7]).normalized;
        n[5] = (triNormal[2] + triNormal[3]).normalized;
        n[6] = triNormal[4].normalized;
        n[7] = (triNormal[4] + triNormal[5] + triNormal[6]).normalized;
        n[8] = (triNormal[6] + triNormal[7]).normalized;

        UpdateNormals(v, n);
    }
}