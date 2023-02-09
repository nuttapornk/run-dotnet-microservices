using Ordering.Domain.Common;

namespace Ordering.Application.Common.Interfaces;

public interface IEmailService
{
    Task<bool> SendMailAsync(Email email);
}
