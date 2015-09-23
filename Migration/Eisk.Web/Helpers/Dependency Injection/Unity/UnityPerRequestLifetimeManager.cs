using System;
using Microsoft.Practices.Unity;

//===================================================================================
// Microsoft patterns & practices
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

namespace Eisk.Helpers
{
    public class UnityPerRequestLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly IPerRequestStore contextStore;
        private readonly Guid key = Guid.NewGuid();

        /// <summary>
        ///     Initializes a new instance of UnityPerRequestLifetimeManager with a per-request store.
        /// </summary>
        /// <param name="contextStore"></param>
        public UnityPerRequestLifetimeManager(IPerRequestStore contextStore)
        {
            this.contextStore = contextStore;
            this.contextStore.EndRequest += EndRequestHandler;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override object GetValue()
        {
            return contextStore.GetValue(key);
        }

        public override void SetValue(object newValue)
        {
            contextStore.SetValue(key, newValue);
        }

        public override void RemoveValue()
        {
            var oldValue = contextStore.GetValue(key);
            contextStore.RemoveValue(key);

            var disposable = oldValue as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            RemoveValue();
        }

        private void EndRequestHandler(object sender, EventArgs e)
        {
            RemoveValue();
        }
    }
}