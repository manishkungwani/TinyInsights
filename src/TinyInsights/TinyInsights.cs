﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyInsightsLib
{
    public static class TinyInsights
    {
        private static List<ITinyInsightsProvider> insightsProviders = new List<ITinyInsightsProvider>();

        public static void Configure(ITinyInsightsProvider provider)
        {
            insightsProviders.Add(provider);
        }

        public static void Configure(params ITinyInsightsProvider[] providers)
        {
            insightsProviders.AddRange(providers.ToList());
        }

        public static async Task TrackErrorAsync(Exception ex)
        {
            var tasks = new List<Task>();
            
            foreach(var provider in insightsProviders)
            {
                if(provider.IsTrackErrorsEnabled)
                {
                    var task = provider.TrackErrorAsync(ex);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }

        public static async Task TrackPageViewAsync(string viewName)
        {
            var tasks = new List<Task>();

            foreach (var provider in insightsProviders)
            {
                if (provider.IsTrackPageViewsEnabled)
                {
                    var task = provider.TrackPageViewAsync(viewName);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }

        public static async Task TrackPageViewAsync(string viewName, Dictionary<string, string> properties)
        {
            var tasks = new List<Task>();

            foreach (var provider in insightsProviders)
            {
                if (provider.IsTrackPageViewsEnabled)
                {
                    var task = provider.TrackPageViewAsync(viewName, properties);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }

        public static async Task TrackEventAsync(string eventName)
        {
            var tasks = new List<Task>();

            foreach (var provider in insightsProviders)
            {
                if (provider.IsTrackEventsEnabled)
                {
                    var task = provider.TrackEventAsync(eventName);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }

        public static async Task TrackEventAsync(string eventName, Dictionary<string, string> properties)
        {
            var tasks = new List<Task>();

            foreach (var provider in insightsProviders)
            {
                if (provider.IsTrackEventsEnabled)
                {
                    var task = provider.TrackEventAsync(eventName, properties);
                    tasks.Add(task);
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
