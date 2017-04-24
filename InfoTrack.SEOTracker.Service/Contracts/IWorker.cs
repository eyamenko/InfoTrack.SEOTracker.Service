using System;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Service.Contracts
{
    public interface IWorker
    {
        Task Run();
    }
}