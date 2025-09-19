using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV_Station.Application.Users.DTOs.Requests
{
    public record UserRefreshTokenDto
    (
        Guid userId,
        string refreshToken
    );
}
