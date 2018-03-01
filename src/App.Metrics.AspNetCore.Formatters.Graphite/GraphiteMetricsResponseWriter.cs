﻿// <copyright file="GraphiteMetricsResponseWriter.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace App.Metrics.Formatters.Graphite
{
    public class GraphiteMetricsResponseWriter : IMetricsResponseWriter
    {
        /// <inheritdoc />
        public string ContentType => "application/vnd.app.metrics.v1.metrics.graphite; graphite=plain-text-0.10.0;";

        public Task WriteAsync(HttpContext context, MetricsDataValueSource metricsData, CancellationToken token = default(CancellationToken))
        {
            var payloadBuilder = new GraphitePayloadBuilder();

            var formatter = new MetricDataValueSourceFormatter();
            formatter.Build(metricsData, payloadBuilder);

            return context.Response.WriteAsync(payloadBuilder.PayloadFormatted(), token);
        }
    }
}