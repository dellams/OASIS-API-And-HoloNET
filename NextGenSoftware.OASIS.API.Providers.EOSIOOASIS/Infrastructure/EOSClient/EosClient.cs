﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Managers;
using NextGenSoftware.OASIS.API.Providers.EOSIOOASIS.Entities;

namespace NextGenSoftware.OASIS.API.Providers.EOSIOOASIS.Infrastructure.EOSClient
{
    public class EosClient : IEosClient
    {
        private readonly Uri _eosHostNodeUri;
        private readonly HttpClient _httpClient;

        public EosClient(Uri eosHostNodeUri)
        {
            _eosHostNodeUri = eosHostNodeUri;

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10),
                BaseAddress = _eosHostNodeUri
            };
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        public async Task<GetNodeInfoResponseDto> GetNodeInfo()
        {
            return await SendRequest<GetNodeInfoResponseDto, object>(null, HttpMethod.Get,
                new Uri(_eosHostNodeUri.Host + ""));
        }

        public async Task<GetTableRowsResponseDto> GetTableRows(GetTableRowsRequestDto getTableRowsRequest)
        {
            return await SendRequest<GetTableRowsResponseDto, GetTableRowsRequestDto>(getTableRowsRequest,
                HttpMethod.Post, new Uri(_eosHostNodeUri.Host + ""));
        }

        public async Task<AbiJsonToBinResponseDto> AbiJsonToBin(AbiJsonToBinRequestDto abiJsonToBinRequestDto)
        {
            return await SendRequest<AbiJsonToBinResponseDto, AbiJsonToBinRequestDto>(abiJsonToBinRequestDto,
                HttpMethod.Post, new Uri(_eosHostNodeUri.Host + ""));
        }

        public async Task<string> AbiBinToJson(AbiBinToJsonRequestDto abiJsonToBinRequestDto)
        {
            return await SendRequest<string, AbiBinToJsonRequestDto>(abiJsonToBinRequestDto, HttpMethod.Post,
                new Uri(_eosHostNodeUri.Host + ""));
        }

        /// <summary>
        ///     Generic method for sending http requests
        /// </summary>
        /// <param name="request">Request object</param>
        /// <param name="httpMethod">Http methods</param>
        /// <param name="uri">Host endpoint</param>
        /// <typeparam name="TResponse">Generic object as http-payload response</typeparam>
        /// <typeparam name="TRequest">Generic object as http-body request</typeparam>
        /// <returns>Received object from host</returns>
        /// <exception cref="ArgumentNullException">If some of input parameter is null</exception>
        private async Task<TResponse> SendRequest<TResponse, TRequest>(TRequest request, HttpMethod httpMethod, Uri uri)
        {
            // Validate input parameters
            if (httpMethod == null)
                throw new ArgumentNullException(nameof(httpMethod));
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            try
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = uri,
                    Headers = {{"Content-Type", "application/json"}}
                };

                if (request != null)
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(request));

                // Send request into EOS-node endpoint
                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
                if (!httpResponseMessage.IsSuccessStatusCode)
                    throw new HttpRequestException(
                        $"Provider: EOS. Incorrect response was received from endpoint! Endpoint: {uri.AbsoluteUri}.");

                var httpResponseBodyContent = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(httpResponseBodyContent);
            }
            catch (Exception e)
            {
                LoggingManager.Log(
                    $"Provider: EOS. Error was happened while performing the eos-request! Endpoint: {uri.AbsoluteUri}. Message: " +
                    e.Message, LogType.Error);
                throw;
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _httpClient.CancelPendingRequests();
            _httpClient.Dispose();
        }

        ~EosClient()
        {
            ReleaseUnmanagedResources();
        }
    }
}