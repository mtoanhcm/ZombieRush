using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ZRCore.Pool
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> availableObjects;
        private readonly T prefab;
        private readonly Transform parentTransform;
        private readonly int sizeIncrease;

        private const int SIZE_NEED_INCREASE = 5;

        public ObjectPool(T prefab, int size, Transform parentTransform = null)
        {
            this.prefab = prefab;
            this.parentTransform = parentTransform;
            sizeIncrease = size;
            availableObjects = new Queue<T>();

            ExpandPool(size);
        }

        public T GetObject(bool isActiveObject = true)
        {
            if (availableObjects.Count <= SIZE_NEED_INCREASE)
            {
                ExpandPool(sizeIncrease);
            }

            T obj = availableObjects.Dequeue();

            if (isActiveObject) {
                obj.gameObject.SetActive(true);
            }
            
            return obj;
        }

        public void ReturnObject(T obj)
        {
            if (obj == null) {
                return;
            } 
            
            obj.gameObject.SetActive(false);
            availableObjects.Enqueue(obj);
        }

        private async void ExpandPool(int size) {
            for (var i = 0; i < size; i++)
            {
                T newObject = Object.Instantiate(prefab, parentTransform);
                newObject.gameObject.SetActive(false);
                availableObjects.Enqueue(newObject);

                await Task.Delay(5);
            }
        }
    }
}
