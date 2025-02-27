using UnityEngine;

namespace BeatBox.System.Manager
{
    public class PlayerCustomDataManager : MonoBehaviour
    {
        public static float noteSpeedMult = 5;
        
        public void InitData(
            float NoteSpeedMultiplier
        )
        {
            noteSpeedMult = NoteSpeedMultiplier;
            
            GetComponent<PreLoadManager>().FinishInitialization();
        }
    }
}