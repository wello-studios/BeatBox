using System;
using System.Collections.Generic;
using UnityEngine;

/* ObjectPoolingManager
 * 오브젝트 풀을 제어함.
 *
 * [VARIABLE]
 * List<GameObject> poolObjects
 *   풀
 * Transform targetParent
 *   풀 속 오브젝트가 존재할 오브젝트
 * GameObject targetObject
 *   오브젝트 풀링의 주체 프리펩
 * 
 * [METHOD]
 * GameObject TakeOut
 *   풀에서 오브젝트 꺼내기
 * void TakeIn (GameObject obj)
 *   obj를 풀에 넣기
 * 
 * [UNITY EVENT]
 * AWAKE - 풀 초기화
 * START - 
 * UPDATE - 
 */

namespace BeatBox.System.ObjectPooling
{
    public class ObjectPoolingManager : MonoBehaviour
    {
        public List<GameObject> poolObjects = new List<GameObject>();
        public Transform targetParent;
        public GameObject targetObject;
        
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
                    Debug.LogWarning("idk why this fucking error occured but its okey 'cause game working well at now");
                }
            }
        }
    }
}
