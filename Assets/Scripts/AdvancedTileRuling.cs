using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class AdvancedTileRuling : RuleTile<AdvancedTileRuling.Neighbor> {
    public bool customField;

    public TileBase[] tileOption;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Option1 = 3;
        public const int Option2 = 4;
        public const int Option3 = 5;
        public const int Option4 = 6;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.Option1:
                if(tileOption.Length <= 0) return false;
                return tileOption[0] == tile;
            case Neighbor.Option2:
                if (tileOption.Length <= 1) return false;
                return tileOption[1] == tile;
            case Neighbor.Option3:
                if (tileOption.Length <= 2) return false;
                return tileOption[2] == tile;
            case Neighbor.Option4:
                if (tileOption.Length <= 3) return false;
                return tileOption[3] == tile;
        }
        return base.RuleMatch(neighbor, tile);
    }
}