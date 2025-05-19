using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReferenceRegistry
{
    private static Dictionary<GameObject, ReferenceProvider> _providers = new();

    public static void Register(ReferenceProvider referenceProvider) 
    {
        if (!_providers.ContainsKey(referenceProvider.gameObject))
        {
            _providers.Add(referenceProvider.gameObject, referenceProvider);
        }
    }
    public static void Unregister(ReferenceProvider referenceProvider)
    {
        if (_providers.ContainsKey(referenceProvider.gameObject))
        {
            _providers.Remove(referenceProvider.gameObject);
        }

    }
    public static void Clear() 
    {
        _providers.Clear();
    }
    public static ReferenceProvider GetProvider(GameObject gameObject) 
    {
        if (_providers.ContainsKey(gameObject))
        {
            return _providers[gameObject];
        }
        return null;
    }
}
