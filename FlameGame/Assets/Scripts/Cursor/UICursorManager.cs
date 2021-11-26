using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UICursorManager : MonoBehaviour
{

    private enum CursorState
    { 
        IDLE, 
        OVER_MATERIAL_A, 
        OVER_MATERIAL_B, 
        OVER_WORKER, 
        START_SELECTION, 
        SELECTED_MATERIAL, 
        SELECTED_WORKER, 
        WORKER_MOVE, 
        WORKER_HARVEST, 
        WORKER_DROP, 
        SCROLL, 
        DRAW_GRASS, 
        DRAW_WOOD,
    };
    private CursorState _currentCursorState;

    [HideInInspector]
    public enum Direction {N, NW, W, SW, S, SE, E, NE }
    
    [Header ("Hover Cursors")]

        [Space(10)]

        [SerializeField]
        private CursorObject _idleCursor;
        [SerializeField]
        private CursorObject _overMaterialACursor;
        [SerializeField]
        private CursorObject _overMaterialBCursor;
        [SerializeField]
        private CursorObject _overWorkerCursor;
        [SerializeField]
        private CursorObject _drawWoodCursor;
        [SerializeField]
        private CursorObject _drawGrassCursor;

    [Header ("Selection Cursors")]

        [Space(10)]

        [SerializeField]
        private CursorObject _selectedMaterialCursor;
        [SerializeField]
        private CursorObject _selectedWorkerCursor;
        [SerializeField]
        private CursorObject _startSelectionCursor;

    [Header ("Action Cursors")]

        [Space(10)]

        [SerializeField]
        private CursorObject _workerMoveCursor;
        [SerializeField]
        private CursorObject _workerHarvestCursor;
        [SerializeField]
        private CursorObject _workerDropCursor;

    [Header ("Scroll Cursors")]

        [Space(10)]

        [SerializeField]
        private CursorObject _ScrollNorthCursor;
        [SerializeField]
        private CursorObject _ScrollNorthWestCursor;
        [SerializeField]
        private CursorObject _ScrollWestCursor;
        [SerializeField]
        private CursorObject _ScrollSouthWestCursor;
        [SerializeField]
        private CursorObject _ScrollSouthCursor;
        [SerializeField]
        private CursorObject _ScrollSouthEastCursor;
        [SerializeField]
        private CursorObject _ScrollEastCursor;
        [SerializeField]
        private CursorObject _ScrollNorthEastCursor;


    private CursorObject _prevCursor;
    private CursorState _prevCursorState;
    //cursor variables
    private CursorObject _currentCursor;
    private Texture2D _currentTexture;
    private List<Texture2D> _cursorTextures; 
    private Vector2 _cursorOffset;


    //animation variables
    private bool _animationIsActive; 
    private float _timeSinceLastFrame;
    private int _currentFrame;
    private int _totalAnimationFrames;
    private float _timePerAnimationFrame;
    private IEnumerator _animationCoroutine;
    private bool _isScrollCursor; 

    private void OnEnable()
    {
        UIAction.OnCursorIdle += SetCursorToIdle;
        UIAction.OnCursorDrawWood += SetCursorToDrawWood;
        UIAction.OnCursorDrawGrass += SetCursorToDrawGrass;

        //UIAction.OnCursorOverMaterialTypeA += SetCursorToOverMaterialA;
        //UIAction.OnCursorOverMaterialTypeB += SetCursorToOverMaterialB;
        //UIAction.OnCursorOverWorker += SetCursorToOverWorker;
        UIAction.OnCursorWorkerSelected += SetCursorToWorkerSelected;
        //UIAction.OnCursorMaterialSelected += SetCursorToMaterialSelected;
        UIAction.OnCursorWorkerMove += SetCursorToWorkerMove;
        //UIAction.OnCursorWorkerDrop += SetCursorToWorkerDrop;
        //UIAction.OnCursorWorkerHarvest += SetCursorToWorkerHarvest;
        UIAction.OnCursorScroll += SetCursorToScroll;
        UIAction.OnCursorStartSelection += SetCursorToStartSelection;
        UIAction.OnCursorScrollStop += StopScrolling;
    }

    private void OnDisable()
    {
        UIAction.OnCursorIdle -= SetCursorToIdle;
        UIAction.OnCursorDrawWood -= SetCursorToDrawWood;
        UIAction.OnCursorDrawGrass -= SetCursorToDrawGrass;

        //UIAction.OnCursorOverMaterialTypeA -= SetCursorToOverMaterialA;
        //UIAction.OnCursorOverMaterialTypeB -= SetCursorToOverMaterialB;
        //UIAction.OnCursorOverWorker -= SetCursorToOverWorker;
        UIAction.OnCursorWorkerSelected -= SetCursorToWorkerSelected;
       // UIAction.OnCursorMaterialSelected -= SetCursorToMaterialSelected;
        UIAction.OnCursorWorkerMove -= SetCursorToWorkerMove;
        //UIAction.OnCursorWorkerDrop -= SetCursorToWorkerDrop;
        //UIAction.OnCursorWorkerHarvest -= SetCursorToWorkerHarvest;
        UIAction.OnCursorScroll -= SetCursorToScroll;
        UIAction.OnCursorStartSelection -= SetCursorToStartSelection;
        UIAction.OnCursorScrollStop -= StopScrolling;

    }

    void Start()
    {
        SetCursorToIdle();
        _animationIsActive = false;
        _animationCoroutine = CursorAnimation();
    }

    private void SetCursorToIdle()
    {
        ChangeCursor(CursorState.IDLE, _idleCursor, false);
    }

    private void SetCursorToDrawGrass()
    {
        ChangeCursor(CursorState.DRAW_GRASS, _drawGrassCursor, false);

    }

    private void SetCursorToDrawWood()
    {
        ChangeCursor(CursorState.DRAW_WOOD, _drawWoodCursor, false);

    }

    private void SetCursorToOverMaterialA()
    {
        ChangeCursor( CursorState.OVER_MATERIAL_A, _overMaterialACursor, false);

    }

    private void SetCursorToOverMaterialB()
    {
        ChangeCursor(CursorState.OVER_MATERIAL_B, _overMaterialBCursor, false);

    }

    private void SetCursorToMaterialSelected()
    {
        ChangeCursor(CursorState.SELECTED_MATERIAL, _selectedMaterialCursor, false);

    }

    private void SetCursorToOverWorker()
    {
        ChangeCursor(CursorState.OVER_WORKER, _overWorkerCursor, false);
    }

    private void SetCursorToWorkerSelected()
    {
        ChangeCursor(CursorState.SELECTED_WORKER, _selectedWorkerCursor, false);

    }

    private void SetCursorToStartSelection()
    {
        ChangeCursor(CursorState.START_SELECTION, _startSelectionCursor, false);

    }

    private void SetCursorToWorkerMove()
    {
        ChangeCursor(CursorState.WORKER_MOVE, _workerMoveCursor, false);

    }

    private void SetCursorToWorkerHarvest()
    {
        ChangeCursor(CursorState.WORKER_HARVEST, _workerHarvestCursor, false);

    }

    private void SetCursorToWorkerDrop()
    {
        ChangeCursor(CursorState.WORKER_DROP, _workerDropCursor, false);

    }

   private void SetCursorToScroll(Direction dir)
    {
        CursorObject _scrollCursorToSet = null;
        switch (dir)
        {
            case Direction.N:
                _scrollCursorToSet = _ScrollNorthCursor;
                break;
            case Direction.NW:
                _scrollCursorToSet = _ScrollNorthWestCursor;
                break;
            case Direction.W:
                _scrollCursorToSet = _ScrollWestCursor;
                break;
            case Direction.SW:
                _scrollCursorToSet = _ScrollSouthWestCursor;
                break;
            case Direction.S:
                _scrollCursorToSet = _ScrollSouthCursor;
                break;
            case Direction.SE:
                _scrollCursorToSet = _ScrollSouthEastCursor;
                break;
            case Direction.E:
                _scrollCursorToSet = _ScrollEastCursor;
                break;
            case Direction.NE:
                _scrollCursorToSet = _ScrollNorthEastCursor;
                break;
            default:
                _scrollCursorToSet = _idleCursor;
                break;
        }

        ChangeCursor(CursorState.SCROLL, _scrollCursorToSet, true);

    }

    private void StopScrolling()
    {
        _isScrollCursor = false;
        if (_prevCursor != null)
        ChangeCursor(_prevCursorState,_prevCursor,false);
    }

    private void ChangeCursor(CursorState newCursorState, CursorObject newCursor, bool IsScrollCursor)
    {

        if (_currentCursorState == newCursorState) return; 

        if (!IsScrollCursor && _isScrollCursor)
        {
            return;
        }
        _isScrollCursor = IsScrollCursor;
        if (_currentCursor != null)
        {
            _currentCursor.OnCursorStop();
            _prevCursor = _currentCursor;
            _prevCursorState = _currentCursorState;
        }

        if (_animationIsActive)
        {
            StopCoroutine(_animationCoroutine);
            _animationIsActive = false;

        }

        _currentCursorState = newCursorState;
        _currentCursor = newCursor; 
        _currentCursor.OnCursorStart();

        _currentTexture = _currentCursor.GetCursorTexture();
        _cursorOffset = _currentCursor.GetOffset();

        ReloadCursor();

        if (_currentCursor.GetIsAnimated())
        {
            StartAnimation();
        }
    }

    private void ReloadCursor()
    {
        //changes the cursor
        Cursor.SetCursor(_currentTexture, _cursorOffset, CursorMode.Auto);

    }


    private void StartAnimation()
    {
        _timePerAnimationFrame = _currentCursor.GetTimePerAnimationFrame();
        _totalAnimationFrames = _currentCursor.GetNumerOfAnimationFrames();
        _cursorTextures = _currentCursor.GetCursorTextures();
        _currentFrame = 1;
        _timeSinceLastFrame = 0.0f;
        _animationIsActive = true;
        StartCoroutine(_animationCoroutine);

    }


    private IEnumerator CursorAnimation()
    {

        while (true)
        {
            yield return null;
            _timeSinceLastFrame += Time.deltaTime;

            if (_timeSinceLastFrame > _timePerAnimationFrame) GoToNextFrame();

        }
    }


    private void GoToNextFrame()
    {
        if (_currentFrame == _totalAnimationFrames && _currentCursor.GetAnimateOnlyOnce())
        {
            StopCoroutine(_animationCoroutine);
            _animationIsActive = false;
        }
        _currentFrame = (_currentFrame % _totalAnimationFrames) + 1;
        _currentTexture = _cursorTextures[_currentFrame - 1];
        _timeSinceLastFrame = 0.0f;
        ReloadCursor();
    }



}
