using System;
using UnityEngine;
using Verse;
namespace Leoltron.GeneBankIndicators;

[StaticConstructorOnStartup]
internal static class GeneBankStateExtensions
{
    private static readonly Material PoweredFullMaterial = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0, 0.7137255f, 0.9372549f));
    private static readonly Material PoweredNotFullMaterial = SolidColorMaterials.SimpleSolidColorMaterial(Color.green);
    private static readonly Material UnpoweredEmptyMaterial = SolidColorMaterials.SimpleSolidColorMaterial(Color.yellow);
    private static readonly Material UnpoweredNotEmptyMaterial = SolidColorMaterials.SimpleSolidColorMaterial(Color.red);

    public static Material GetIndicatorMaterial(this GeneBankState state) => state switch
    {
        GeneBankState.PoweredFull => PoweredFullMaterial,
        GeneBankState.PoweredNotFull => PoweredNotFullMaterial,
        GeneBankState.UnpoweredEmpty => UnpoweredEmptyMaterial,
        GeneBankState.UnpoweredNotEmpty => UnpoweredNotEmptyMaterial,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
    };
}