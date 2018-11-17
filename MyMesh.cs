using System;
using UnityEngine;

[DisallowMultipleComponent]
public partial class MyMesh : MonoBehaviour 
{
	void Start () 
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;

        //delete the current mesh
        theMesh.Clear();

        //the "resolution" of image [number of vertices on one dimension]
        int res = GameObject.Find("GM").GetComponent<GameManager>().Resolution;

        //number of segments that are between vertices on one dimension
        int seg = res - 1;

        //distance between vertex on one dimension
        float dist = 2.0f / seg;

        //number of vertices on the mesh [number is equal to res^2 because res is n of v on one dimension]
        Vector3[] v = new Vector3[res * res];

        //V [vertex postitions on the mesh]
        float a = -1.0f, b = -1.0f;
        for (int i = 0; i < v.Length; i++)
        {
            v[i] = new Vector3(a, 0.0f, b);
            a += dist;
            if (a > 1.0f)
            {
                a = -1.0f;
                b += dist;
            }
        }

        theMesh.vertices = v;

        //normals of each vertice on the mesh [default should be new Vec3(0, 1, 0)]
        Vector3[] n = new Vector3[v.Length];

        //N [normals of each vertice]
        for (int i = 0; i < n.Length; i++)
        {
            n[i] = new Vector3(0, 1, 0);
        }

        theMesh.normals = n;

        //i really dont know tf this is but should be same n of v
        Vector2[] uv = new Vector2[v.Length];

        //UV [your guess is as good as mine tf is this LOL]
        int x = 0, y = 0;
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2((x * 0.5f), (y * 0.5f));
            x++;
            if (x > 2)
            {
                x = 0;
                y++;
            }
        }

        theMesh.uv = uv;

        //number of vertices for triangles [number of triangles is equal to (((2^res) * res) / 3)]
        int[] t = new int[(int)Math.Pow(2, res) * res];

        //just go with it FUCKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
        int c = 1, d = 0, z = 0;
        for (int i = 0; i < t.Length; i++)
        {
            switch (d)
            {
                case 0:
                    c = z;
                    t[i] = c;
                    break;
                case 1:
                    t[i] = c + 3;
                    break;
                case 2:
                    t[i] = c + 4;
                    break;
                case 3:
                    t[i] = c;
                    break;
                case 4:
                    t[i] = c + 4;
                    break;
                case 5:
                    t[i] = c + 1;
                    break;
            }
            d++;
            if (d > 5)
            {
                d = 0;
                if ((z + 2) % 3 == 0)
                {
                    z += 2;
                }
                else
                {
                    z++;
                }
            }
        }

        /*
        t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle

        t[6] = 1; t[7] = 4; t[8] = 5;  // 2nd triangle
        t[9] = 1; t[10] = 5; t[11] = 2;  // 3rd triangle

        t[12] = 3; t[13] = 6; t[14] = 7;  // 4th triangle
        t[15] = 3; t[16] = 7; t[17] = 4;  // 5th triangle

        t[18] = 4; t[19] = 7; t[20] = 8;  // 6th triangle
        t[21] = 4; t[22] = 8; t[23] = 5;  // 7th triangle
        */

        theMesh.triangles = t;

        InitControllers(v);
        InitNormals(v, n);
    }

    void Update () 
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;

        for (int i = 0; i < mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, n);

        theMesh.vertices = v;
        theMesh.normals = n;
	}
}