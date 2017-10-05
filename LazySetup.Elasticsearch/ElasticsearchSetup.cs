using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace LazySetup.Elasticsearch
{
    public static class ElasticsearchSetup
    {
        /// <summary>
        /// Setup connection to single node
        /// </summary>
        public static void AddElasticsearch(this IServiceCollection services, string url)
        {
            services.AddSingleton<IElasticClient>(provider => new ElasticClient(new Uri(url)));
        }

        /// <summary>
        /// Setup connection to single node
        /// </summary>
        public static void AddElasticsearch(this IServiceCollection services, string url, string username, string password)
        {
            services.AddSingleton<IElasticClient>(provider => new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri(url))).BasicAuthentication(username, password)));
        }

        /// <summary>
        /// Setup connection to multiple nodes
        /// </summary>
        public static void AddElasticsearch(this IServiceCollection services, IEnumerable<string> urls)
        {
            services.AddSingleton<IElasticClient>(provider => new ElasticClient(new ConnectionSettings(new StaticConnectionPool(urls.Select(url => new Uri(url))))));
        }

        /// <summary>
        /// Setup connection to multiple nodes
        /// </summary>
        public static void AddElasticsearch(this IServiceCollection services, IEnumerable<string> urls, string username, string password)
        {
            services.AddSingleton<IElasticClient>(provider => new ElasticClient(new ConnectionSettings(new StaticConnectionPool(urls.Select(url => new Uri(url)))).BasicAuthentication(username, password)));
        }
    }
}