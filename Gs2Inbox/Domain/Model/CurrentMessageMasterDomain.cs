/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantUsingDirective
// ReSharper disable CheckNamespace
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ArrangeThisQualifier
// ReSharper disable NotAccessedField.Local

#pragma warning disable 1998

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Inbox.Domain.Iterator;
using Gs2.Gs2Inbox.Request;
using Gs2.Gs2Inbox.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Util;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
using System.Collections;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
    #endif
#else
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Gs2Inbox.Domain.Model
{

    public partial class CurrentMessageMasterDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2InboxRestClient _client;
        private readonly string _namespaceName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;

        public CurrentMessageMasterDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2InboxRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._parentKey = Gs2.Gs2Inbox.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this.NamespaceName,
                "CurrentMessageMaster"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> ExportMasterAsync(
            #else
        public IFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> ExportMaster(
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> ExportMasterAsync(
        #endif
            ExportMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.ExportMasterFuture(
                request
            );
            yield return future;
            if (future.Error != null)
            {
                self.OnError(future.Error);
                yield break;
            }
            var result = future.Result;
            #else
            var result = await this._client.ExportMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "CurrentMessageMaster"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
            }
            Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Inbox.Model.CurrentMessageMaster> GetAsync(
            #else
        private IFuture<Gs2.Gs2Inbox.Model.CurrentMessageMaster> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Inbox.Model.CurrentMessageMaster> GetAsync(
        #endif
            GetCurrentMessageMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Model.CurrentMessageMaster> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetCurrentMessageMasterFuture(
                request
            );
            yield return future;
            if (future.Error != null)
            {
                self.OnError(future.Error);
                yield break;
            }
            var result = future.Result;
            #else
            var result = await this._client.GetCurrentMessageMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "CurrentMessageMaster"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(result?.Item);
        #else
            return result?.Item;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Model.CurrentMessageMaster>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> UpdateAsync(
        #endif
            UpdateCurrentMessageMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateCurrentMessageMasterFuture(
                request
            );
            yield return future;
            if (future.Error != null)
            {
                self.OnError(future.Error);
                yield break;
            }
            var result = future.Result;
            #else
            var result = await this._client.UpdateCurrentMessageMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "CurrentMessageMaster"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
            }
            Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> UpdateFromGitHubAsync(
            #else
        public IFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> UpdateFromGitHub(
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> UpdateFromGitHubAsync(
        #endif
            UpdateCurrentMessageMasterFromGitHubRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateCurrentMessageMasterFromGitHubFuture(
                request
            );
            yield return future;
            if (future.Error != null)
            {
                self.OnError(future.Error);
                yield break;
            }
            var result = future.Result;
            #else
            var result = await this._client.UpdateCurrentMessageMasterFromGitHubAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "CurrentMessageMaster"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
            }
            Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string childType
        )
        {
            return string.Join(
                ":",
                "inbox",
                namespaceName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
        )
        {
            return "Singleton";
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Model.CurrentMessageMaster> Model() {
            #else
        public IFuture<Gs2.Gs2Inbox.Model.CurrentMessageMaster> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Model.CurrentMessageMaster> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Model.CurrentMessageMaster> self)
            {
        #endif
            var (value, find) = _cache.Get<Gs2.Gs2Inbox.Model.CurrentMessageMaster>(
                _parentKey,
                Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                )
            );
            if (!find) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetCurrentMessageMasterRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            var key = Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                                );
                            _cache.Put<Gs2.Gs2Inbox.Model.CurrentMessageMaster>(
                                _parentKey,
                                key,
                                null,
                                UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );

                            if (e.errors[0].component != "currentMessageMaster")
                            {
                                self.OnError(future.Error);
                            }
                        }
                        else
                        {
                            self.OnError(future.Error);
                            yield break;
                        }
                    }
        #else
                } catch(Gs2.Core.Exception.NotFoundException e) {
                    var key = Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                        );
                    _cache.Put<Gs2.Gs2Inbox.Model.CurrentMessageMaster>(
                        _parentKey,
                        key,
                        null,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                    if (e.errors[0].component != "currentMessageMaster")
                    {
                        throw e;
                    }
                }
        #endif
                (value, find) = _cache.Get<Gs2.Gs2Inbox.Model.CurrentMessageMaster>(
                    _parentKey,
                    Gs2.Gs2Inbox.Domain.Model.CurrentMessageMasterDomain.CreateCacheKey(
                    )
                );
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(value);
            yield return null;
        #else
            return value;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Model.CurrentMessageMaster>(Impl);
        #endif
        }

    }
}
