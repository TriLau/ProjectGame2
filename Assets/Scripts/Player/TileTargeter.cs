using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTargeter : MonoBehaviour
{
    [SerializeField]
    private Tilemap[] _tilemaps; // All Tilemaps to check (Ground, Decorations, etc.)
    public Tilemap[] Tilemaps
    {
        get { return _tilemaps; }
        set { _tilemaps = value; }
    }

    [SerializeField]
    private Tilemap _highlightTilemap;
    public Tilemap HighlightTilemap
    {
        get { return _highlightTilemap; }
        set { _highlightTilemap = value; }
    }

    [SerializeField]
    private AnimatedTile _highlightTile;
    public AnimatedTile HighlightTile
    {
        get { return _highlightTile; }
        set { _highlightTile = value; }
    }

    [SerializeField]
    private int highlightRange = 1;

    private Vector3 _mouseWorldPosition;
    private Vector3Int _previousTilePos;
    private Vector3Int _mouseTilePosition;
    private Vector3Int _playerTilePosition;
    private Vector3Int _clampedTilePosition;

    [SerializeField] private bool _canHoe = false;
    public bool CanHoe
    {
        get { return _canHoe; }
        set { _canHoe = value; }
    }
    [SerializeField] private List<Tilemap> tilemapCheck = new List<Tilemap>();
    [SerializeField] PlayerController playerController;

    [SerializeField] RuleTile HoeTile;

    void Update()
    {
        TargetTile();
    }

    void TargetTile()
    {
        // Get mouse position in world coordinates
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0; // Ensure it's on the correct plane

        // Convert mouse world position to tile position
        _mouseTilePosition = HighlightTilemap.WorldToCell(_mouseWorldPosition);

        // Get player position in tile coordinates
        _playerTilePosition = HighlightTilemap.WorldToCell(transform.position);

        // Ensure the highlight stays within 1 tile range of the player
        _clampedTilePosition = new Vector3Int(
            Mathf.Clamp(_mouseTilePosition.x, _playerTilePosition.x - highlightRange, _playerTilePosition.x + highlightRange),
            Mathf.Clamp(_mouseTilePosition.y, _playerTilePosition.y - highlightRange, _playerTilePosition.y + highlightRange),
            _mouseTilePosition.z
        );

        // Only update if tile position has changed
        if (_clampedTilePosition != _previousTilePos)
        {
            tilemapCheck.Clear();
            HighlightTilemap.SetTile(_previousTilePos, null); // Remove previous highlight

            foreach (Tilemap tilemap in Tilemaps)
            {
                if (tilemap.HasTile(_clampedTilePosition)) // Check if a tile exists in any Tilemap
                {
                    tilemapCheck.Add(tilemap);
                }
            }

            HighlightTilemap.SetTile(_clampedTilePosition, HighlightTile); // Place highlight on the separate Tilemap
            _previousTilePos = _clampedTilePosition;

            // Check if tile is walkable and can be hoed
            CanHoe = (tilemapCheck.Count == 1 && tilemapCheck[0].name == "Walkfront");
        }
    }

    public void UseTool(string tool)
    {
        switch (tool)
        {
            default:
                {
                    Debug.Log("Do nothing");
                    break;
                }
            case "Axe":
                {
                    ChangePlayerFacingDirection();
                    break;
                }
            case "Hoe":
                {
                    ChangePlayerFacingDirection();
                    UseHoe();
                    break;
                }
        }
        
    }

    private void ChangePlayerFacingDirection()
    {
        if (_clampedTilePosition.x < _playerTilePosition.x)
        {
            playerController.LastMovement = Vector2.left;
            playerController.IsFacingRight = false;
        }
        else if (_clampedTilePosition.x > _playerTilePosition.x)
        {
            playerController.LastMovement = Vector2.right;
            playerController.IsFacingRight = true;
        }

        if (_clampedTilePosition.y > _playerTilePosition.y)
        {
            playerController.LastMovement = Vector2.up;
        }
        else if (_clampedTilePosition.y < _playerTilePosition.y)
        {
            playerController.LastMovement = Vector2.down;
        }
    }
    private void UseHoe()
    {
        
        if (CanHoe)
        {
            if (!TileManager.Instance.HoeTiles.Contains(_clampedTilePosition))
            {
                Tilemaps[2].SetTile(_clampedTilePosition, HoeTile);
                TileManager.Instance.AddHoeTile(_clampedTilePosition);
            }
            else
            {
                Debug.Log("Already hoe");
            }
            
            
        }
        else Debug.Log("Cant Hoe here");
    }

   


}
