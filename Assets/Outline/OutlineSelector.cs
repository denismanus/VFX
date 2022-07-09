using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSelector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Interact(hit.transform);
            }
        }
    }

    private void Interact(Transform obj)
    {
        StaticModelOutline outline;
        if (obj.transform.TryGetComponent(out outline))
        {
            outline.SetOutline(!outline.IsOutlineEnabled);
        }
    }
}