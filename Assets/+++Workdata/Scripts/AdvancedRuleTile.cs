using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Custom/Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor> {
    
    public bool alwaysConnect;
    public TileBase[] tilesToConnect;
    public bool checkSelf;

    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int This = 1;
        public const int NotThis = 2;
        public const int Null = 3;
        public const int NotNull = 4;
        public const int Any = 5;
        public const int Specified = 6;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This: return Check_This(tile);
            case Neighbor.NotThis: return Check_NotThis(tile);
            case Neighbor.Null: return Check_Null(tile);
            case Neighbor.NotNull: return Check_NotNull(tile);
            case Neighbor.Any: return Check_Any(tile);
            case Neighbor.Specified: return Check_Specified(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }

    private bool Check_This(TileBase tile)
    {
        if (!alwaysConnect) return tile == this;
        return tilesToConnect.Contains(tile) || tile == this;
    }

    private bool Check_NotThis(TileBase tile)
    {
        return tile != this;
    }

    private bool Check_Null(TileBase tile)
    {
        return tile == null;
    }
    
    private bool Check_NotNull(TileBase tile)
    {
        return tile != null;
    }

    private bool Check_Any(TileBase tile)
    {
        if (checkSelf) return tile != null;
        return tile != null && tile != this;
    }

    private bool Check_Specified(TileBase tile)
    {
        return tilesToConnect.Contains(tile);
    }
}