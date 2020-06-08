using System;

namespace ASB.Microservices.APIService.Client.Commands
{
    public class OrderDetailsResult
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public int ItemsCount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}