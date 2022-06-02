using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Canvas shopUi;
    [SerializeField] private GameObject play;
    [SerializeField] private Camera mainCamera;
    
    // Lighting
    [SerializeField] private Light2D lightHighlightValid;
    [SerializeField] private Light2D lightHighlightInvalid;
    [SerializeField] private Light2D lightPlayer;
    
    // Tiles
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap extrasMap;
    [SerializeField] private Tile validHoverTile;
    [SerializeField] private Tile invalidHoverTile;
    [SerializeField] private Tile shopTile;
    [SerializeField] private Tile encounterTile;
    
    private Vector3Int playerPos;
    private Vector3Int highlightedTilePos;
    private GameTile highlightedTile;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem.Stop();
        playerPos = grid.WorldToCell(player.transform.position);
        lightHighlightInvalid.enabled = false;
        lightHighlightValid.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!play.activeSelf) return;
        Vector3Int mousePos = GetMousePosition();
        Vector3Int hoverPos = new Vector3Int(mousePos.x, mousePos.y, 0);
        HoverHighlight(hoverPos);
        ClickEvent();
    }
    
    private Vector3Int GetMousePosition () {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    private void HoverHighlight(Vector3Int hoverPos)
    {
        if (hoverPos == highlightedTilePos) return;
        if (highlightedTile != null)
        {
            ResetHoverHighlight();
        }
        highlightedTilePos = hoverPos;
        if (interactiveMap.GetTile(hoverPos) is GameTile gameTile)
        {
            if (gameTile.type != GameTile.TileTypes.UI)
            {
                DoHoverHighlight(hoverPos, gameTile);
            }
        } else highlightedTile = null;
    }

    private void DoHoverHighlight(Vector3Int hoverPos, GameTile gTile)
    {
        highlightedTile = gTile;
        var isValid = IsValidTile(gTile, hoverPos);
        Tile validTile = gTile.type == GameTile.TileTypes.SHOP ? shopTile
            : gTile.type == GameTile.TileTypes.ENCOUNTER ? encounterTile
            : validHoverTile;
        extrasMap.SetTile(hoverPos, isValid ? validTile : invalidHoverTile);
        Light2D lighting = isValid ? lightHighlightValid : lightHighlightInvalid;
        lighting.enabled = true;
        lighting.transform.position = interactiveMap.CellToWorld(highlightedTilePos);
        if (isValid)
        {
            particleSystem.transform.position = interactiveMap.CellToWorld(highlightedTilePos);
            particleSystem.Play();
        }
    }

    private void ResetHoverHighlight()
    {
        extrasMap.SetTile(highlightedTilePos, null);
        lightHighlightInvalid.enabled = false;
        lightHighlightValid.enabled = false;
        particleSystem.Stop();
    }

    private bool IsValidTile(GameTile hoverTile, Vector3Int hoverPos)
    {
        if (!IsNeighbor(playerPos, hoverPos)) return false;
        switch (hoverTile.type)
        {
            case GameTile.TileTypes.GRASS:
            case GameTile.TileTypes.SHOP:
            case GameTile.TileTypes.ENCOUNTER:
                return true;
            case GameTile.TileTypes.WATER:
                return false;
            default:
                return false;
        }
    }

    private void ClickEvent()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (highlightedTile == null) return;
        if (!IsValidTile(highlightedTile, highlightedTilePos)) return;
        switch (highlightedTile.type)
        {
            case GameTile.TileTypes.SHOP:
                DoShop();
                break;
            case GameTile.TileTypes.ENCOUNTER:
                DoEncounter();
                break;
            case GameTile.TileTypes.GRASS:
            case GameTile.TileTypes.WATER:
            case GameTile.TileTypes.UI:
            default:
            {
                DoMove();
                break;
            }
        }
    }

    private void DoShop()
    {
        play.SetActive(false);
        shopUi.rootCanvas.enabled = true;
    }

    private void DoEncounter()
    {
        return;
    }
    
    private void DoMove()
    {
        play.SetActive(false);
        playerPos = highlightedTilePos;
        Vector3 cellToWorld = interactiveMap.CellToWorld(highlightedTilePos);
        StartCoroutine(Utils.StaticUtils.MoveOverSeconds(player.transform, cellToWorld, 0.5f, play));
        lightPlayer.transform.position = player.transform.position;
    }

    private static readonly Vector3Int 
        Left = new Vector3Int(-1, 0, 0), 
        Right = new Vector3Int(1, 0, 0),
        Down = new Vector3Int(0, -1, 0), 
        DownLeft = new Vector3Int(-1, -1, 0), 
        DownRight = new Vector3Int(1, -1, 0), 
        Up = new Vector3Int(0, 1, 0), 
        UpLeft = new Vector3Int(-1, 1, 0), 
        UpRight = new Vector3Int(1, 1, 0);
    
    private readonly Vector3Int[] directions_when_y_is_even = { Left, Right, Down, DownLeft, Up, UpLeft };
    private readonly Vector3Int[] directions_when_y_is_odd = { Left, Right, Down, DownRight, Up, UpRight };

    private bool IsNeighbor(Vector3Int node, Vector3Int other)
    {
        Vector3Int[] directions = (node.y % 2) == 0 
            ? directions_when_y_is_even 
            : directions_when_y_is_odd;
        return directions
            .Select(direction => node + direction)
            .Any(neighborPos => neighborPos == other);
    }
}
