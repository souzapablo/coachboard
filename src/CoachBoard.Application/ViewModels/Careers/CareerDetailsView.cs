using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Careers;

public record CareerDetailsView(
    long Id,
    string Manager,
    IEnumerable<string> Teams,
    DateTime LastUpdate)
{
    public static CareerDetailsView Map(Career career) =>
        new(
            career.Id,
            career.ManagerName,
            career.Teams.Select(team => team.Name),
            career.LastUpdate);
};