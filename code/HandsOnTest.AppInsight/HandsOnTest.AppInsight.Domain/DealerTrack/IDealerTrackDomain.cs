using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HandsOnTest.AppInsight.Domain.DTO;

namespace HandsOnTest.AppInsight.Domain.DealerTrack
{
    public interface IDealerTrackDomain
    {
        Task<IEnumerable<DealerTrackerDTO>> ProcessFile(IFormFile file);
    }
}
