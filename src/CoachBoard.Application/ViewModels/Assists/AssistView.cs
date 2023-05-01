using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Assists;

public record AssistView(
    long Id,
    long PlayerAssistedId)
{
    public static AssistView Map(Assist assist) =>
        new AssistView(
            assist.Id,
            assist.PlayerAssistedId
        );
};