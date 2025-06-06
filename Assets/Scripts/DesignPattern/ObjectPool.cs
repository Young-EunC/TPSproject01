using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern 
{ 
    public class ObjectPool
    {
        private Stack<PooledObject> _stack;
        private PooledObject _targetPrefab;
        private GameObject _poolObject;

        public ObjectPool(PooledObject targetPrefab, Transform parent, int initSize = 5) {
            Init(targetPrefab, parent, initSize);
        }

        private void Init(PooledObject targetPrefab, Transform parent, int initSize) {
            _stack = new Stack<PooledObject>(initSize);
            _targetPrefab = targetPrefab;
            _poolObject = new GameObject($"{targetPrefab.name} Pool");
            _poolObject.transform.parent = parent;

            for (int cnt = 0; cnt < initSize; cnt++)
            {
                CreatePooledObject();
            }
        }
        public PooledObject Get() {
            if (_stack.TryPop(out PooledObject pooledObject))
            {
                pooledObject.gameObject.SetActive(true);
            }
            else 
            {
                CreatePooledObject();
                pooledObject = _stack.Pop();
            }
            return pooledObject;
        }
        public void ReturnPool(PooledObject target) {
            target.transform.parent = _poolObject.transform;
            target.gameObject.SetActive(false);
            _stack.Push(target);
        }
        private void CreatePooledObject() {
            PooledObject pooledObject = MonoBehaviour.Instantiate(_targetPrefab);
            pooledObject.PooledInit(this);
            ReturnPool(pooledObject);
        }
    }
}