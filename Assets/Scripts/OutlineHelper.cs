using UnityEngine;

public class OutlineHelper
{
    public int layerMask;
    uint lightmaskYellow, lightmaskWhite, lightmaskDefault;
    uint noLine, yellowLine, whiteLine;

    public OutlineHelper()
    {
        setLines();
    }

    public void SetIndex(GameObject hit, bool Line)
    {
        Renderer tempRenderer = null;
        uint tempIndex = 0;

        switch (hit.tag)
        {
            case "Tower":
                tempRenderer = hit.transform.GetComponent<Renderer>();
                tempIndex = yellowLine;
                break;
            case "Placeable":
                tempRenderer = hit.GetComponent<Renderer>();
                tempIndex = whiteLine;
                break;
        }

        if (Line)
        {
            tempRenderer.renderingLayerMask = tempIndex;
        }
        else
        {
            tempRenderer.renderingLayerMask = noLine;
        }

    }
    public void setLines()
    {
        layerMask = LayerMask.GetMask("FloorCheck");
        lightmaskWhite = RenderingLayerMask.GetMask("Light Layer 1");
        lightmaskYellow = RenderingLayerMask.GetMask("Light Layer 2");
        lightmaskDefault = RenderingLayerMask.GetMask("Default");

        noLine = lightmaskDefault;
        whiteLine = lightmaskDefault | lightmaskWhite;
        yellowLine = lightmaskDefault | lightmaskYellow;
    }

}
