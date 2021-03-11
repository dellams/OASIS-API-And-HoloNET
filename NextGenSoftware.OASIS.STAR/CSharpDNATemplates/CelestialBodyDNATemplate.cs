﻿
using NextGenSoftware.OASIS.API.Core;
using NextGenSoftware.OASIS.API.Core.Interfaces;
using System.Threading.Tasks;

namespace NextGenSoftware.OASIS.STAR.CSharpTemplates
{
    public class CelestialBodyDNATemplate : CelestialBody, ICelestialBody
    {
        public CelestialBodyDNATemplate(string providerKey) : base(providerKey, GenesisType.CelestialBody)
        {

        }

        public CelestialBodyDNATemplate() : base(GenesisType.CelestialBody)
        {

        }

        /*
        public CelestialBodyDNATemplate(string holochainConductorURI, HoloNETClientType type, string providerKey) : base(holochainConductorURI, type, providerKey, GenesisType.Star)
        {

        }

        public CelestialBodyDNATemplate(string holochainConductorURI, HoloNETClientType type) : base(holochainConductorURI, type, GenesisType.Star)
        {

        }

        public CelestialBodyDNATemplate(HoloNETClientBase holoNETClient, string providerKey) : base(holoNETClient, providerKey, GenesisType.Star)
        {

        }

        public CelestialBodyDNATemplate(HoloNETClientBase holoNETClient) : base(holoNETClient, GenesisType.Star)
        {

        }*/


        public async Task<IHolon> LoadHOLONAsync(string hcEntryAddressHash)
        {
            //return await base.CelestialBodyCore.LoadHolonAsync("{holon}", hcEntryAddressHash);
            return await base.CelestialBodyCore.LoadHolonAsync(hcEntryAddressHash);
        }

        public async Task<IHolon> SaveHOLONAsync(IHolon holon)
        {
            //return await base.CelestialBodyCore.SaveHolonAsync("{holon}", holon);
            return await base.CelestialBodyCore.SaveHolonAsync(holon);
        }

        /*
        //TODO: Do we still need these now? Nice to call the method what the holon type is I guess...
        public async Task<IHolon> LoadHOLONAsync(string hcEntryAddressHash)
        {
            return await base.LoadHolonAsync(hcEntryAddressHash);
        }

        public async Task<IHolon> SaveHOLONAsync(IHolon holon)
        {
            //return await base.SaveHolonAsync("{holon}", holon);
            return await base.SaveHolonAsync(holon);
        }*/
    }
}
