using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeatBox.System.ObjectPooling
{
    public class ObjectPoolingManager : MonoBehaviour
    {
        public List<GameObject> poolObjects = new List<GameObject>();
        public Transform targetParent;
        public GameObject targetObject;

        /** RESET THE OBJECT POOLS */
        private void Awake()
        {
            if (targetObject == null) return;
            
            for (int i = 0; i < 10; i++) {
                try
                {
                    var obj = Instantiate(targetObject, targetParent);
                    var objOPO = obj.GetComponent<ObjectPoolingObject>();

                    objOPO.parent = this;
                    obj.SetActive(false);
                
                    poolObjects.Add(obj);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("idk why this fucking error occured but its okey 'cause game working well in now");
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /** THIS FUNCTION GIVES OBJECT IN OBJECT POLL. */
        public GameObject TakeOut()
        {
            GameObject obj;
            if (poolObjects.Count > 0) {
                obj = GetObjectFromPool();
            } else {
                obj = CreateObject();
            }

            obj.SetActive(true);
            obj.GetComponent<ObjectPoolingObject>().parent = this;
            return obj;
        }

        /** THIS FUNCTION TAKES OBJECT TO OBJECT POOL. */
        public void TakeIn(GameObject obj)
        {
            obj.SetActive(false);
            poolObjects.Add(obj);
        }

        /** CALLED WHEN OBJECT POOL IS EMPTY AND TakeOut() CALLED. */
        public GameObject CreateObject() {
            var obj = Instantiate(targetObject, targetParent);
            return obj;
        }
        
        /** CALLED WHEN OBJECT POOL ISN'T EMPTY AND TakeOut() CALLED. */
        public GameObject GetObjectFromPool() {
            var obj = poolObjects[0];
            poolObjects.RemoveAt(0);
            return obj;
        }
        
    }
}
