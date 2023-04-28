namespace Server.Models.Authentication
{
    public class AccountDTO
    {
        public long AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public int OrderCount { get; set; }

        public AccountDTO(Account account)
        {
            AccountId = account.AccountId;
            FirstName = account.FirstName;
            LastName = account.LastName;
            Email = account.Email;
            PhoneNumber = account.PhoneNumber;
            IsVerified = account.IsVerified;
            OrderCount = account.OrderCount;
        }
    }
}
