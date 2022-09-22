using System;

namespace Prism.Services.Dialogs;

public static class IDialogServiceExtensions
{
    public static void ShowPatternCompareDialog(this IDialogService dialogService, string patternA, string patternB, Action<IDialogResult> callback)
    {
        var p = new DialogParameters
        {
            { "patternA", patternA },
            { "patternB", patternB }
        };

        dialogService.ShowDialog("PatternCompareDialog", p, callback);
    }

    public static void ShowMovementDialog(this IDialogService dialogService, string patternA, string patternB, Action<IDialogResult> callback)
    {
        var p = new DialogParameters
        {
            { "patternA", patternA },
            { "patternB", patternB }
        };

        dialogService.ShowDialog("MovementDialog", p, callback);
    }
}
