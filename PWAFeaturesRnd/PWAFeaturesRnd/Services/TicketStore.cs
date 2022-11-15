using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Services
{
    public class TicketStore : ITicketStore
    {
        #region Private Variables
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<TicketStore> _logger;
        #endregion

        #region Constructors
        public TicketStore(IMemoryCache memoryCache, ILogger<TicketStore> logger)
        {
            this.memoryCache = memoryCache;
            _logger = logger;
        }
        #endregion

        #region Methods
        public Task RemoveAsync(string key)
        {
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            var ticket = memoryCache.Get<AuthenticationTicket>(key);
            return Task.FromResult(ticket);
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            memoryCache.Set(key, ticket);

            return Task.CompletedTask;
        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = ticket.Principal.Identity.Name + "_pwa";

            memoryCache.Set(key, ticket);

            return Task.FromResult(key);
        }
        #endregion

    }
}
