﻿using NextGenSoftware.OASIS.API.Core.Enums;
using System;
using System.Collections.Generic;

namespace NextGenSoftware.OASIS.API.Core.Interfaces.NFT
{
    public interface IOASISNFT
    {
        Guid Id { get; set; }
        Guid MintedByAvatarId { get; set; }
        string MintedByAddress { get; set; }
        string Hash { get; set; }  
        decimal Price { get; set; }
        decimal Discount { get; set; }
        byte[] Thumbnail { get; set; }
        string ThumbnailUrl { get; set; }
        public string Token { get; set; } //TODO: Should be dervied from the OnChainProvider so may not need this?
        Dictionary<string, object> MetaData { get; set; }
        ProviderType OffChainProvider { get; set; }
        ProviderType OnChainProvider { get; set; }
    }
}