﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Enum;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Infrastructure.Builder;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Infrastructure.Exceptions;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Infrastructure.Factory.TokenStorage;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Infrastructure.Interfaces;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Models.Cargo;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Models.Common;

namespace NextGenSoftware.OASIS.API.Providers.CargoOASIS.Infrastructure.Handlers.Queries
{
    public class GetContractsHandler : IHandle<Response<GetContractsResponseModel>, GetContractsRequestHandler>
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public GetContractsHandler()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(1),
                BaseAddress = new Uri("https://api2.cargo.build/")
            };
            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            
            _tokenStorage = TokenStorageFactory.GetMemoryCacheTokenStorage();
        }
        
        /// <summary>
        /// Get contracts
        /// More information: https://docs.cargo.build/cargo-js/cargo.api#get-a-list-of-collections-on-cargo
        /// </summary>
        /// <param name="request">Request parameters</param>
        /// <returns>Contracts</returns>
        public async Task<Response<GetContractsResponseModel>> Handle(GetContractsRequestHandler request)
        {
            var response = new Response<GetContractsResponseModel>();
            try
            {
                var queryBuilder = new UrlQueryBuilder();
                queryBuilder.AppendParameter("limit", request.Limit);
                queryBuilder.AppendParameter("owned", request.Owned.ToString());
                queryBuilder.AppendParameter("page", request.Page);
                queryBuilder.AppendParameter("address", request.Address);
                queryBuilder.AppendParameter("cargoContract", request.CargoContract.ToString());
                queryBuilder.AppendParameter("hasTokens", request.HasTokens.ToString());
                queryBuilder.AppendParameter("showcaseId", request.ShowcaseId);
                queryBuilder.AppendParameter("useAuthToken", request.UseAuthToken.ToString());

                var urlQuery = $"v3/get-contracts{queryBuilder.GetQuery()}";
                var httRequest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_httpClient.BaseAddress + urlQuery),
                };
                if (request.SkipAuth != null && !request.SkipAuth.Value)
                {
                    var accessToken = await _tokenStorage.GetToken();
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
                var httpResponse = await _httpClient.SendAsync(httRequest);
                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UserNotAuthorizedException();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Message = httpResponse.ReasonPhrase;
                    response.ResponseStatus = ResponseStatus.Fail;
                    return response;
                }
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GetContractsResponseModel>(responseString);
                response.Payload = data;
                return response;
            }
            catch (UserNotAuthorizedException e)
            {
                response.ResponseStatus = ResponseStatus.Unauthorized;
                response.Message = e.Message;
                return response;
            }
            catch (UserNotRegisteredException e)
            {
                response.ResponseStatus = ResponseStatus.NotRegistered;
                response.Message = e.Message;
                return response;
            }
            catch (Exception e)
            {
                response.ResponseStatus = ResponseStatus.Fail;
                response.Message = e.Message;
                return response;
            }
        }
    }

    public class GetContractsRequestHandler
    {
        /// <summary>
        /// Optional. String. Page of results to display. Defaults to 1.
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// Optional. String. Limit of collections per page. Defaults to 10.
        /// </summary>
        public string Limit { get; set; }

        /// <summary>
        /// Optional. String. Limit results to show only collections in the given showcase.
        /// </summary>
        public string ShowcaseId { get; set; }

        /// <summary>
        /// Optional. Boolean. Show only collections that the current authenticated user owns.
        /// </summary>
        public bool? Owned { get; set; }

        /// <summary>
        /// Optional. String. Ethereum wallet address. If specified will only return collections for a given user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Optional. Boolean. Will not use current logged in users address if true.
        /// </summary>
        public bool? SkipAuth { get; set; }

        /// <summary>
        /// Optional. Boolean. Will only return collection if the user owns at least one NFT within that collection.
        /// </summary>
        public bool? HasTokens { get; set; }

        /// <summary>
        /// Optional. Boolean. Return only collections created on Cargo.
        /// </summary>
        public bool? CargoContract { get; set; }

        /// <summary>
        /// Optional. Boolean. Show only collections that the current authenticated user either owns,
        /// or has collectibles in.
        /// Collections within response will contain an additional contractTokens property stating how many collectibles
        /// the user owns within that collection. This takes precedence over the owned parameter.
        /// </summary>
        public bool? UseAuthToken { get; set; }
    }

    public class GetContractsResponseModel
    {
        public class GetContractsData
        {
            [JsonProperty("limit")]
            public string Limit { get; set; }
            [JsonProperty("totalPages")]
            public string TotalPages { get; set; }
            [JsonProperty("total")]
            public string Total { get; set; }
            [JsonProperty("page")]
            public string Page { get; set; }

            [JsonProperty("results")]
            public IEnumerable<ContractV3> Results { get; set; }
        }
        [JsonProperty("err")]
        public bool Error { get; set; }

        [JsonProperty("status")] 
        public int Status { get; set; }

        [JsonProperty("data")] 
        public GetContractsData Data { get; set; }
    }
}