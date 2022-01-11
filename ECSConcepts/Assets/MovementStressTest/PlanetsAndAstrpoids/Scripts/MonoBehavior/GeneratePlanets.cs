using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class GeneratePlanets : MonoBehaviour
{
    public float Radius;
    [Range(1,99)]
    public int Density;
    public int Size;
    Vector3[] vertices;

    private float _stepSize;

    private void Start() {
        vertices = new Vector3[(Density*2)];
        Vector2[] uv; 

        _stepSize = Size/(Density - 1);
        int _numberTriangleTriples =((Density-1)*2)*3;
        Vector3 a = new Vector3(0,0,0);
        Vector3 c = new Vector3(0,_stepSize, 0);
        Vector3 b = new Vector3(_stepSize,_stepSize,0);
        Vector3 d = new Vector3(_stepSize,0,0);

        int[] triangles = new int[_numberTriangleTriples];
        
        vertices[0] = a;
        vertices[1] = b;
        vertices[2] = c;
        vertices[3] = d;
        
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 0;
        triangles[4] = 1;
        triangles[5] = 3;

        triangles[6] = 3;
        triangles[7] = 1;

        for (int i = 2; i < Density;i++)
        {
        b = new Vector3(i * _stepSize,_stepSize,0);
        d = new Vector3(i * _stepSize,0,0);
        
        vertices[i+2] = b;
        vertices[i+3] = d;

        int shiftedI = (i-1)*6;
        triangles[shiftedI] = triangles[shiftedI - 1];//a
        triangles[shiftedI+1] = triangles[shiftedI - 2];//c
        triangles[shiftedI+2] = i+2;//b

        triangles[shiftedI+3] = triangles[shiftedI - 1];//a

        shiftedI = shiftedI + 3;
        
        triangles[shiftedI+1] = i+2;//b
        triangles[shiftedI+2] = i+3;//d
               
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
        }
}

}
