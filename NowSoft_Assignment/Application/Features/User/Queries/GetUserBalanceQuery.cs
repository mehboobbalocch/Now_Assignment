using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public class GetUserBalanceQuery : IRequest<UserBalanceModel>
    {
        public string UserId { get; set; }
    }
}
