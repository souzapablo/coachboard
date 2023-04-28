using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Careers;

public record CareerView(
    long Id,
    string Manager,
    DateTime LastUpdate)
{
    public static CareerView Map(Career career) =>
        new(
            career.Id,
            career.ManagerName,
            career.LastUpdate);
};