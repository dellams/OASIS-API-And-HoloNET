﻿//using System.Collections.Generic;
//using System.Threading.Tasks;
//using NextGenSoftware.OASIS.API.Core.Enums;
//using NextGenSoftware.OASIS.API.Core.Helpers;
//using NextGenSoftware.OASIS.API.Core.Interfaces;
//using NextGenSoftware.OASIS.API.Core.Interfaces.STAR;
//using NextGenSoftware.OASIS.STAR.CelestialBodies;

//namespace NextGenSoftware.OASIS.STAR.DNATemplates.CSharpTemplates
//{
//    public class PlanetDNATemplate : Planet, IPlanet
//    {
//        public PlanetDNATemplate(Dictionary<ProviderType, string> providerKey) : base(providerKey)
//        {

//        }

//        public PlanetDNATemplate() : base()
//        {

//        }

//        /*
//        public PlanetDNATemplate(string holochainConductorURI, HoloNETClientType type, string providerKey) : base(holochainConductorURI, type, providerKey)
//        {

//        }

//        public PlanetDNATemplate(string holochainConductorURI, HoloNETClientType type) : base(holochainConductorURI, type)
//        {

//        }

//        public PlanetDNATemplate(HoloNETClientBase holoNETClient, string providerKey) : base(holoNETClient, providerKey)
//        {

//        }

//        public PlanetDNATemplate(HoloNETClientBase holoNETClient) : base(holoNETClient)
//        {

//        }*/



//        //public async Task<IHolon> LoadHOLONAsync(string hcEntryAddressHash)
//        //{
//        //    return await CelestialBodyCore.LoadHolonAsync(hcEntryAddressHash);
//        //}

//        public async Task<OASISResult<IHolon>> LoadHOLONAsync(Dictionary<ProviderType, string> providerKey)
//        {
//            return await CelestialBodyCore.LoadHolonAsync(providerKey);
//        }

//        public async Task<OASISResult<IHolon>> SaveHOLONAsync(IHolon holon)
//        {
//            return await CelestialBodyCore.SaveHolonAsync(holon);
//            //return await CelestialBodyCore.SaveHolonAsync("{holon}", holon);
//        }

//        /*
//        //TODO: Do we still need these now? Nice to call the method what the holon type is I guess...
//        public async Task<IHolon> LoadHOLONAsync(string hcEntryAddressHash)
//        {
//            return await base.LoadHolonAsync(hcEntryAddressHash);
//        }

//        public async Task<IHolon> SaveHOLONAsync(IHolon holon)
//        {
//            //return await base.SaveHolonAsync("{holon}", holon);
//            return await base.SaveHolonAsync(holon);
//        }*/
//    }
//}
