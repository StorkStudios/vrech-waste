using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField]
    private SerializedSet<Resource> resources;

    public IEnumerable<Resource> Resources => resources.Select(res => res.Item);

    public event System.Action<Resource> ResourceChange;
    public event System.Action<Resource> ResourceReachedBound;

    private void Start()
    {
        foreach (var resource in resources)
        {
            resource.Item.ValueChanged += OnResourceChange;
            resource.Item.ValueReachedBound += OnResourceReachedBound;
        }
    }

    private void OnResourceChange(Resource obj)
    {
        ResourceChange?.Invoke(obj);
    }

    private void OnResourceReachedBound(Resource obj)
    {
        ResourceReachedBound?.Invoke(obj);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach (var resource in resources)
        {
            resource.Item.UpdateResource(deltaTime);
        }
    }

    public Resource GetRandomResource()
    {
        return resources.ElementAt(Random.Range(0, resources.Count));
    }
}
