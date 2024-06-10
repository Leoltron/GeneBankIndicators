using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;
namespace Leoltron.GeneBankIndicators;

[UsedImplicitly]
[StaticConstructorOnStartup]
public class GenepackContainerWithIndicatorComp : ThingComp
{
    private CompGenepackContainer Genes => parent.GetComp<CompGenepackContainer>();
    private int GenesCount => Genes.ContainedGenepacks.Count;
    private int GenesMaxCapacity => Genes.Props.maxCapacity;

    public override void PostDraw()
    {
        var genesCount = GenesCount;
        var genesMaxCapacity = GenesMaxCapacity;

        var fullnessPercentage = Mathf.Clamp01((float)genesCount / genesMaxCapacity);
        GenepackDrawer.Draw(parent, !parent.Rotation.IsHorizontal, fullnessPercentage);
        DrawIndicators(genesCount, genesMaxCapacity);
    }

    private void DrawIndicators(int genesCount, int genesMaxCapacity)
    {
        const float pixelWidth = 2f / 128;
        const float pixelHeight = 1f / 78;

        var indicatorMaterial = GetState(genesCount, genesMaxCapacity).GetIndicatorMaterial();
        switch (parent.Rotation.AsInt)
        {
            case Rot4.NorthInt:
                DrawMaterial(indicatorMaterial, pixelWidth * -44, pixelHeight * 8, pixelWidth * 2, pixelHeight * 4);
                DrawMaterial(indicatorMaterial, pixelWidth * 45, pixelHeight * 8, pixelWidth * 2, pixelHeight * 4);
                DrawMaterial(indicatorMaterial, pixelWidth * 45, pixelHeight * 17, pixelWidth * 2, pixelHeight * 9);
                break;
            case Rot4.SouthInt:
                DrawMaterial(indicatorMaterial, pixelWidth * 45, pixelHeight * 8, pixelWidth * 2, pixelHeight * 4);
                DrawMaterial(indicatorMaterial, pixelWidth * -44, pixelHeight * 8, pixelWidth * 2, pixelHeight * 4);
                DrawMaterial(indicatorMaterial, pixelWidth * -44, pixelHeight * 17, pixelWidth * 2, pixelHeight * 9);
                break;
            case Rot4.WestInt:
                DrawMaterial(indicatorMaterial, pixelWidth * -6, pixelHeight * 57, pixelWidth * 1, pixelHeight * 10);
                DrawMaterial(indicatorMaterial, pixelWidth * 2, pixelHeight * 57, pixelWidth * 1, pixelHeight * 4);
                DrawMaterial(indicatorMaterial, pixelWidth * 2, pixelHeight * -38, pixelWidth * 1, pixelHeight * 4);
                break;
            case Rot4.EastInt:
                DrawMaterial(indicatorMaterial, pixelWidth * 6, pixelHeight * 57, pixelWidth * 1, pixelHeight * 10);
                DrawMaterial(indicatorMaterial, pixelWidth * -2, pixelHeight * 57, pixelWidth * 1, pixelHeight * 4);
                DrawMaterial(indicatorMaterial, pixelWidth * -2, pixelHeight * -38, pixelWidth * 1, pixelHeight * 4);
                break;
        }
    }
    private void DrawMaterial(Material material, float x, float z, float width, float height)
    {
        var size = new Vector3(width, 1f, height);
        var position = (parent.DrawPos + new Vector3(x, 0, z)) with
        {
            y = parent.def.altitudeLayer.AltitudeFor() + 0.03846154f
        };
        var matrix = new Matrix4x4();
        matrix.SetTRS(position, parent.Rotation.AsQuat, size);
        Graphics.DrawMesh(MeshPool.plane10, matrix, material, 0);
    }


    private GeneBankState GetState(int genesCount, int genesMaxCapacity)
    {
        var powered = parent.GetComp<CompPowerTrader>()?.PowerOn ?? true;
        var full = genesCount >= genesMaxCapacity;
        var empty = genesCount == 0;

        if (powered)
        {
            return full ? GeneBankState.PoweredFull : GeneBankState.PoweredNotFull;
        }

        return empty ? GeneBankState.UnpoweredEmpty : GeneBankState.UnpoweredNotEmpty;
    }
}