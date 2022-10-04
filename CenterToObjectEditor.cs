using UnityEngine;
using UnityEditor;
using System.Linq;
namespace ThaIntesect.EditorUtils
{
    public class CenterToObjectEditor : Editor {

        [MenuItem("GameObject/ThaIntersect/CenterMeshWise")]
        public static void CenteredEmtpyParent(){
            Vector3 centerofMass;
            GameObject activeGameObject = Selection.activeGameObject;
            
            if( activeGameObject.GetComponentsInChildren<MeshRenderer>() != null ){
                MeshRenderer[] childMeshes = activeGameObject.GetComponentsInChildren<MeshRenderer>();
                var size = childMeshes.Length;
                var totalSum = new Vector3();
                
                foreach (MeshRenderer childMesh in childMeshes)
                {
                    totalSum += childMesh.bounds.center;
                }
                centerofMass = totalSum/size;
                GameObject goParent = new GameObject($"{activeGameObject.name}.Parent");
                goParent.transform.position = centerofMass;
                goParent.transform.rotation = activeGameObject.transform.rotation;
                if( activeGameObject.GetComponentInParent<Transform>() != null ){
                    goParent.transform.parent = activeGameObject.transform.parent;
                    activeGameObject.transform.parent = goParent.transform;  
                }else{
                    activeGameObject.transform.parent = goParent.transform;
                }
                
            }

        }

    }
    
}