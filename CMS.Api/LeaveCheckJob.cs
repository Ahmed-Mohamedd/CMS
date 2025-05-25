using CMS.Domain.Interfaces;

namespace CMS.Api
{
    public class LeaveCheckJob
    {
        private readonly ILeaveService _updater;

        public LeaveCheckJob(ILeaveService updater)
        {
            _updater = updater;
        }

        public async Task ExecuteAsync()
        {
            await _updater.UpdateLeaveStatusAsync(CancellationToken.None);
        }
    }
}
