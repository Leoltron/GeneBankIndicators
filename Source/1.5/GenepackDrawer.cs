using System;
using UnityEngine;
using Verse;
namespace Leoltron.GeneBankIndicators;

[StaticConstructorOnStartup]
internal static class GenepackDrawer
{
    public static void Draw(Thing thing, bool isGeneBankHorizontal, float fullnessPercentage)
    {
        var graphic = (isGeneBankHorizontal ? GenepackGraphicHorizontal : GenepackGraphicVertical).Value;
        var genepacksOffsets = isGeneBankHorizontal ? horizontalGenepacksPositions : verticalGenepacksPositions;
        var genepacksToRender = (int)(fullnessPercentage * genepacksOffsets.Length);

        for (var i = 0; i < genepacksToRender; i++)
        {
            graphic.Draw(thing.DrawPos + genepacksOffsets[i], Rot4.North, thing);
        }
    }
    static GenepackDrawer()
    {
        InitHorizontalGenepacksPositions();
        InitVerticalGenepacksPositions();
    }

    private static readonly Lazy<Graphic> GenepackGraphicHorizontal =
        new(() =>
            GraphicDatabase.Get<Graphic_Single>("GeneBankIndicators/Genebank_genepack_horizontal", ShaderDatabase.DefaultShader, new Vector2(1f / 2 / 32 * 18, 1f / 2 / 32 * 12), Color.white));
    private static readonly Lazy<Graphic> GenepackGraphicVertical =
        new(() =>
            GraphicDatabase.Get<Graphic_Single>("GeneBankIndicators/Genebank_genepack_vertical", ShaderDatabase.DefaultShader, new Vector2(1f / 2 / 32 * 7, 1f / 2 / 32 * 19), Color.white));


    private static Vector3[] horizontalGenepacksPositions;
    private static void InitHorizontalGenepacksPositions()
    {
        const float topRowZ = 1f / 78 * 18;
        const float bottomRowZ = 1f / 78 * 5;

        const float x1 = -2f / 128 * 31;
        const float x2 = -2f / 128 * 10;
        const float x3 = -2f / 128 * -10;
        const float x4 = -2f / 128 * -31;

        horizontalGenepacksPositions =
        [
            new Vector3(x1, 1, topRowZ),
            new Vector3(x2, 1, topRowZ),
            new Vector3(x3, 1, topRowZ),
            new Vector3(x4, 1, topRowZ),
            new Vector3(x1, 1, bottomRowZ),
            new Vector3(x2, 1, bottomRowZ),
            new Vector3(x3, 1, bottomRowZ),
            new Vector3(x4, 1, bottomRowZ)
        ];
    }
    private static Vector3[] verticalGenepacksPositions;
    private static void InitVerticalGenepacksPositions()
    {
        const float leftColumnX = -1f / 78 * 7.5f;
        const float rightColumnX = -1f / 78 * -7.5f;

        const float dz = 17;

        const float z1 = 2f / 128 * 36;
        const float z2 = 2f / 128 * (36 - dz);
        const float z3 = 2f / 128 * (36 - dz * 2);
        const float z4 = 2f / 128 * (36 - dz * 3);

        verticalGenepacksPositions =
        [
            new Vector3(leftColumnX, 1, z1),
            new Vector3(rightColumnX, 1, z1),
            new Vector3(leftColumnX, 1, z2),
            new Vector3(rightColumnX, 1, z2),
            new Vector3(leftColumnX, 1, z3),
            new Vector3(rightColumnX, 1, z3),
            new Vector3(leftColumnX, 1, z4),
            new Vector3(rightColumnX, 1, z4),
        ];
    }
}