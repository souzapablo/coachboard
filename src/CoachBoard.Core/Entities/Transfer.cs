using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Transfer : BaseEntity
{
    public Transfer(long teamId, long playerTransferredId, TransferStatus status, decimal fee, decimal salary,
        int contractYears)
    {
        TeamId = teamId;
        PlayerTransferredId = playerTransferredId;
        Status = status;
        Fee = fee;
        Salary = salary;
        ContractYears = contractYears;
    }

    public long TeamId { get; private set; }
    public long PlayerTransferredId { get; private set; }
    public TransferStatus Status { get; private set; }
    public decimal Fee { get; private set; }
    public decimal Salary { get; private set; }
    public int ContractYears { get; private set; }
}