using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Careers;

public record CareerView(
    long Id,
    string Manager,
    IEnumerable<string> Teams)
{
    public static CareerView Map(Career career) =>
        new CareerView(
            career.Id,
            career.ManagerName,
            career.Teams.Select(team => team.Name));
};