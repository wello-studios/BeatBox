using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    public List<MenuComponent> components = new List<MenuComponent>();
    public int componentIndex = 0;
    public int startIndex = 0;
    public bool isActive;

    public void Activate()
    {
        isActive = true;
        componentIndex = startIndex;
        components[componentIndex].Activate();
    }

    public void Deactivate()
    {
        isActive = false;
        components[componentIndex].Deactivate();
    }

    public void AddIndex()
    {
        if (componentIndex == components.Count-1) return;
        
        components[componentIndex].Deactivate();
        componentIndex += 1;
        components[componentIndex].Activate();
    }
    
    public void RemoveIndex()
    {
        if (componentIndex == 0) return;
        
        components[componentIndex].Deactivate();
        componentIndex -= 1;
        components[componentIndex].Activate();
    }
}
