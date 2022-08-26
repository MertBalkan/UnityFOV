using UnityEditor;
using UnityEngine;

namespace fov
{
    public class FOVSettings : MonoBehaviour
    {
        [System.Serializable]
        public struct FOVStruct
        {
            [Range(-1f, 0f)] 
            public float fovValue;
            public float playerRadius;
            public LineVector lineVector;
            public GameObject player;

        }

        [System.Serializable]
        public struct LineVector
        {
            public Vector3 lineVector1;
            public Vector3 lineVector2;
        }
            
        [SerializeField] private FOVStruct fovSettings;
        
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            SetFOV();
            
            Handles.color = Color.white;
            Handles.DrawWireDisc(fovSettings.player.transform.position, transform.up, fovSettings.playerRadius);

            var playerPos = fovSettings.player.transform.localPosition;
            var linePos = fovSettings.lineVector.lineVector1 * fovSettings.playerRadius;
            Handles.color = Color.green;
            
            var forwardVector = (playerPos + linePos);
            Handles.DrawLine(playerPos, forwardVector, 5.0f);
            
            fovSettings.lineVector.lineVector1.z = Mathf.Sqrt(Mathf.Pow(fovSettings.playerRadius, 2) - Mathf.Pow(linePos.x, 2)) / fovSettings.playerRadius;
            var linePos2 =  fovSettings.lineVector.lineVector2 * fovSettings.playerRadius;
            
            Handles.color =  Color.red;
            var forwardVector2 = (playerPos + linePos2);
            
            Handles.DrawLine(playerPos, forwardVector2, 5.0f);
            
            fovSettings.lineVector.lineVector2.z = Mathf.Sqrt(Mathf.Pow(fovSettings.playerRadius, 2) - Mathf.Pow(linePos2.x, 2)) / fovSettings.playerRadius;

            var dotProduct = Vector3.Dot( fovSettings.lineVector.lineVector1,  fovSettings.lineVector.lineVector2);
            var angleRadian = Mathf.Acos(dotProduct);
            
            Handles.Label(playerPos + Vector3.forward * 0.8f, (angleRadian * Mathf.Rad2Deg).ToString());
            Handles.color = Color.yellow;
            Handles.DrawWireArc(playerPos, Vector3.up,  fovSettings.lineVector.lineVector1, angleRadian * Mathf.Rad2Deg, 1.0f, 2.0f);
            Handles.color = Color.white;
        }
#endif
        private void SetFOV()
        {
            fovSettings.lineVector.lineVector1.x =  fovSettings.fovValue;
            fovSettings.lineVector.lineVector2.x = -fovSettings.fovValue;
        }
    }
}