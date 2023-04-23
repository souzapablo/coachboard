using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Transfer : BaseEntity
{
    public Transfer(Player playerTransferred, TransferStatus status, decimal fee, decimal salary, int contractYears)
    {
        PlayerTransferred = playerTransferred;
        Status = status;
        Fee = fee;
        Salary = salary;
        ContractYears = contractYears;
    }

    public Player PlayerTransferred { get; private set; }
    public TransferStatus Status { get; private set; }
    public decimal Fee { get; private set; }
    public decimal Salary { get; private set; }
    public int ContractYears { get; private set; }
}