using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class UserInformationResponse
    {
        public Guid Id { get; set; }
        public string SecurityCode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public RoleResponse Role { get; set; } = null!;
        public string? Status { get; set; }
        public string LocationName { get; set; } = null!;
    }

    public class UserInformationDetailResponse : UserInformationResponse
    {
        public ContractDetailResponse UserContract { get; set; } = null!;

        public List<TransactionGeneralResponse> UserTransaction { get; set; } = null !;
    }
}
