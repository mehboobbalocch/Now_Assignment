using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class UserBalanceModel
    {
        public Guid UserId { get; set; }
        public double Balance { get; set; }
    }
}
