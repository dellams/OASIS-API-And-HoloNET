﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextGenSoftware.OASIS.API.Core
{
    // This interface is responsbile for persisting data/state to storage, this could be a local DB or other local 
    // storage or through a distributed/decentralised provider such as IPFS or Holochain (these two implementations 
    // will be implemented soon (IPFSOASIS & HoloOASIS).
    public interface IOASISSTORAGE
    {
        Task<IProfile> LoadProfileAsync(string profileEntryHash);
        //Task<IProfile> LoadProfileAsync(Guid Id);
        //Task<bool> SaveProfileAsync(IProfile profile);
        Task SaveProfileAsync(IProfile profile);
        Task<bool> AddKarmaToProfileAsync(IProfile profile, int karma);
        Task<bool> RemoveKarmaFromProfileAsync(IProfile profile, int karma);

        //TODO: Lots more to come! ;-)
    }
}
