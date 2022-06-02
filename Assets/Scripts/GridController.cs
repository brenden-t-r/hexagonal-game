using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap extrasMap;
    [SerializeField] private Light2D lightHightlightValid;
    [SerializeField] private Light2D lightHighlightInvalid;
    [SerializeField] private Light2D lightPlayer;
    [SerializeField] private Tile validHoverTile;
    [SerializeField] private Tile invalidHoverTile;
    [SerializeField] private Tile shopTile;
    [SerializeField] private Tile encounterTile;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Canvas shopUi;
    [SerializeField] private GameObject play;
    private Vector3Int playerPos;
    private Vector3Int highlightedTilePos;
    private GameTile highlightedTile;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem.Stop();
        playerPos = grid.WorldToCell(player.transform.position);
        lightHighlightInvalid.enabled = false;
        lightHightlightValid.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!play.activeSelf) return;
        Vector3Int mousePos = GetMousePosition();
        Vector3Int hoverPos = new Vector3Int(mousePos.x, mousePos.y, 0);
        hoverHighlight(hoverPos);
        clickEvent();
    }

    private void hoverHighlight(Vector3Int hoverPos)
    {
        if (hoverPos == highlightedTilePos) return;
        if (highlightedTile != null)
        {
            extrasMap.SetTile(highlightedTilePos, null);
            lightHighlightInvalid.enabled = false;
            lightHightlightValid.enabled = false;
            particleSystem.Stop();
        }
        highlightedTilePos = hoverPos;
        // if (hoverPos == playerPos) return;
        TileBase hoverTile = interactiveMap.GetTile(hoverPos);
        if (hoverTile is GameTile gTile)
        {
            if (gTile.type != GameTile.TileTypes.UI)
            {
                highlightedTile = gTile;
                bool isValid = isValidTile(gTile, hoverPos);
                Tile validTile = gTile.type == GameTile.TileTypes.SHOP ? shopTile
                    : gTile.type == GameTile.TileTypes.ENCOUNTER ? encounterTile
                    : validHoverTile;
                extrasMap.SetTile(hoverPos, isValid ? validTile : invalidHoverTile);
                Light2D light = isValid ? lightHightlightValid : lightHighlightInvalid;
                light.enabled = true;
                light.transform.position = interactiveMap.CellToWorld(highlightedTilePos);
                if (isValid) {
                    particleSystem.transform.position = interactiveMap.CellToWorld(highlightedTilePos);
                    particleSystem.Play();
                }
            }
        } else highlightedTile = null;
    }

    private void clickEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (highlightedTile == null) return;
            Debug.Log("is a " + highlightedTile.type);
            if (isValidTile(highlightedTile, highlightedTilePos))
            {
                if (GameTile.TileTypes.SHOP == highlightedTile.type)
                {
                    // Do Shop
                    Debug.Log("Do shop");
                    play.SetActive(false);
                    shopUi.rootCanvas.enabled = true;
                }
                else if (GameTile.TileTypes.ENCOUNTER == highlightedTile.type)
                {
                    // Do encounter
                    Debug.Log("Do encounter");
                }
                else
                {
                    // Do move
                    playerPos = highlightedTilePos;
                    // player.transform.position = interactiveMap.CellToWorld(highlightedTilePos);
                    play.SetActive(false);
                    StartCoroutine(MoveOverSeconds(player.transform, interactiveMap.CellToWorld(highlightedTilePos), 0.5f, play));
                    lightPlayer.transform.position = player.transform.position;
                }
            }
        }
    }

    bool isValidTile(GameTile hoverTile, Vector3Int hoverPos)
    {
        if (!isNeighbor(playerPos, hoverPos)) return false;
        if (hoverTile.type == GameTile.TileTypes.GRASS)
        {
            return true;
        }
        if (hoverTile.type == GameTile.TileTypes.SHOP)
        {
            return true;
        }
        if (hoverTile.type == GameTile.TileTypes.ENCOUNTER)
        {
            return true;
        }
        if (hoverTile.type == GameTile.TileTypes.WATER)
        {
            return false;
        }
        return false;
    }

    Vector3Int GetMousePosition () {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    static Vector3Int 
    LEFT = new Vector3Int(-1, 0, 0),
    RIGHT = new Vector3Int(1, 0, 0),
    DOWN = new Vector3Int(0, -1, 0),
    DOWNLEFT = new Vector3Int(-1, -1, 0),
    DOWNRIGHT = new Vector3Int(1, -1, 0),
    UP = new Vector3Int(0, 1, 0),
    UPLEFT = new Vector3Int(-1, 1, 0),
    UPRIGHT = new Vector3Int(1, 1, 0);
    
    static Vector3Int[] directions_when_y_is_even = 
        { LEFT, RIGHT, DOWN, DOWNLEFT, UP, UPLEFT };
    static Vector3Int[] directions_when_y_is_odd = 
        { LEFT, RIGHT, DOWN, DOWNRIGHT, UP, UPRIGHT };
    
    
    public Vector3Int Neighbors(Vector3Int node) {
        Vector3Int[] directions = (node.y % 2) == 0? 
            directions_when_y_is_even: 
            directions_when_y_is_odd;
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction;
            return neighborPos;
        }
        return Vector3Int.zero;
    }

    public bool isNeighbor(Vector3Int node, Vector3Int other)
    {
        Vector3Int[] directions = (node.y % 2) == 0? 
            directions_when_y_is_even: 
            directions_when_y_is_odd;
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction;
            if (neighborPos == other) return true;
        }
        return false;
    }
    
    public static IEnumerator MoveOverSeconds(Transform objectToMove, Vector3 end, float seconds, GameObject play)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
        play.SetActive(true);
    }
    
    /* Raycast
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    Vector3Int pos = interactiveMap.WorldToCell(hit.point);*/
}
