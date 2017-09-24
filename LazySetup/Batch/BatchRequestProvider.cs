﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LazySetup.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;

namespace LazySetup.Batch
{
    public class BatchRequestProvider
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextFactory _factory;
        private readonly BatchRequestOptions _options;

        public BatchRequestProvider(RequestDelegate next, IHttpContextFactory factory, BatchRequestOptions options)
        {
            _next = next;
            _factory = factory;
            _options = options;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }
            return ProcessBatchRequest(context);
        }

        private async Task ProcessBatchRequest(HttpContext context)
        {
            if (context.Request.Method.Equals("POST"))
            {
                context.Request.EnableRewind();

                var streamHelper = new StreamHelper();

                var json = await streamHelper.StreamToJson(context.Request.Body);

                var requests = JsonConvert.DeserializeObject<IEnumerable<RequestModel>>(json);

                var responseBody = new List<ResponseModel>();

                foreach (var request in requests)
                {
                    var newRequest = new HttpRequestFeature
                    {
                        Body = request.Body != null ? new MemoryStream(Encoding.ASCII.GetBytes(request.Body)) : null,
                        Headers = context.Request.Headers,
                        Method = request.Method,
                        Path = request.RelativeUrl,
                        PathBase = string.Empty,
                        Protocol = context.Request.Protocol,
                        Scheme = context.Request.Scheme,
                        QueryString = context.Request.QueryString.Value
                    };

                    var newRespone = new HttpResponseFeature();
                    
                    var features = CreateDefaultFeatures(context.Features);
                    features.Set<IHttpRequestFeature>(newRequest);
                    features.Set<IHttpResponseFeature>(newRespone);
                    
                    var requestLifetimeFeature = new HttpRequestLifetimeFeature();
                    features.Set<IHttpRequestLifetimeFeature>(requestLifetimeFeature);

                    var innerContext = _factory.Create(features);
                    await _next(innerContext);
                    
                    var responseJson = await streamHelper.StreamToJson(innerContext.Response.Body);
                    responseBody.Add(new ResponseModel
                    {
                        StatusCode = innerContext.Response.StatusCode,
                        Headers = innerContext.Response.Headers.ToDictionary(x => x.Key, x => x.Value.ToString()),
                        Body = JsonConvert.DeserializeObject(responseJson)
                    });
                }

                var binFormatter = new BinaryFormatter();
                var mStream = new MemoryStream();
                binFormatter.Serialize(mStream, responseBody);

                context.Response.StatusCode = 200;
                context.Response.Body = mStream;
                return;
            }

            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("This endpoint only accepts POST");
        }

        private FeatureCollection CreateDefaultFeatures(IFeatureCollection input)
        {
            var output = new FeatureCollection();
            output.Set(input.Get<IServiceProvidersFeature>());
            output.Set(input.Get<IHttpRequestIdentifierFeature>());
            output.Set(input.Get<IAuthenticationFeature>());
            output.Set(input.Get<IHttpAuthenticationFeature>());
            output.Set<IItemsFeature>(new ItemsFeature()); // per request?
            return output;
        }
    }
}
