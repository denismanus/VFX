using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class StaticModelOutline : MonoBehaviour
{
    private const string MaterialName = "Outline";

    [SerializeField] private Color _outLineColor;
    [SerializeField] private bool _enabledOnStart;

    private static Material _material;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private GameObject _outlineGeometry;

    public bool IsOutlineEnabled => _outlineGeometry != null && _outlineGeometry.activeSelf;

    private Material OutlineMaterial
    {
        get
        {
            if (_material == null)
            {
                _material = Resources.Load<Material>(MaterialName);
            }

            return _material;
        }
    }

    private void Awake()
    {
        if (_enabledOnStart)
        {
            SetOutline(true);
        }
    }

    public void SetOutline(bool value)
    {
        if (value)
            StartCoroutine(Rotate());
        else
        {
            StopAllCoroutines();
        }

        if (_outlineGeometry == null)
        {
            CreateOutline();
        }

        _outlineGeometry.SetActive(value);
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 7);
            yield return null;
        }
    }

    public void SetOutlineColor(Color color)
    {
        _outLineColor = color;
    }

    private void CreateOutline()
    {
        _outlineGeometry = new GameObject {name = transform.name + "_outline"};
        _outlineGeometry.transform.SetParent(transform);
        _outlineGeometry.transform.localPosition = Vector3.zero;
        _outlineGeometry.transform.localRotation = quaternion.identity;

        _meshRenderer = _outlineGeometry.AddComponent<MeshRenderer>();
        _meshFilter = _outlineGeometry.AddComponent<MeshFilter>();

        _meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        _meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        _meshRenderer.material = OutlineMaterial;
    }
}