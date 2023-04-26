using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Careers;

public record CareerView(
    long Id,
    string Manager)
{
    public static CareerView Map(Career career) =>
        new CareerView(
            career.Id,
            career.ManagerName);
};