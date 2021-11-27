using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cursor", menuName = "FlameGame/Cursor", order = 52)]
public class CursorObject : ScriptableObject
{
    [Header("Type of Cursor")]

        [Space(10)]

        [Tooltip("Is the cursor Animated")]
        [SerializeField]
        private bool _isAnimated;

        //[Tooltip("Not coded")]
        //[SerializeField]
        //private bool _hasIntroAnimation;

        //[Tooltip("Not coded")]
        //[SerializeField]
        //private bool _hasExitAnimation;

    [Header("List of all Cursor Textures")]
    
        [Space(10)]

        [Tooltip("If the cursor is not animated the first list item will be the cursor texture. Otherwise all the cursor textures will be animated")]
        [SerializeField]
        private List <Texture2D> _cursorTextures;


    [Header("Position of the Cursor")]
    
        [Space(10)]

        [Tooltip("Offset of the cursor texture from the mouse position. Default is x = 0, y = 0")]
        [SerializeField]
        private Vector2 _offsetFromMousePosition = Vector2.zero;
    

    [Header("Animation Information")]

        [Space(10)]

        [Tooltip("Total time in seconds for the complete animation")]
        [SerializeField]
        [Range(0.0f, 5.0f)]
        private float _animationTimeInSeconds = 1.0f;

        [SerializeField]
        private bool _animateOnlyOnce = false; 
        private IEnumerator _animationCoroutine; 

    public void OnCursorStart()
    {

    }

    public void OnCursorStop()
    {
    }

    public Texture2D GetCursorTexture()
    {
        return _cursorTextures[0];
    }

    public Vector2 GetOffset()
    {
        return _offsetFromMousePosition;
    }

    public List <Texture2D> GetCursorTextures()
    {
        return _cursorTextures;
    }

    public bool GetIsAnimated()
    {
        return _isAnimated;
    }
    public int GetNumerOfAnimationFrames()
    {
        return _cursorTextures.Count;
    }

    public float GetTimePerAnimationFrame()
    {
        return (_animationTimeInSeconds / _cursorTextures.Count);
    }

    public bool GetAnimateOnlyOnce()
    {
        return _animateOnlyOnce;
    }
}
