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
using Gs2.Gs2Limit.Domain.Iterator;
using Gs2.Gs2Limit.Request;
using Gs2.Gs2Limit.Result;
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

namespace Gs2.Gs2Limit.Domain.Model
{

    public partial class CounterDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2LimitRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly string _limitName;
        private readonly string _counterName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public string LimitName => _limitName;
        public string CounterName => _counterName;

        public CounterDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId,
            string limitName,
            string counterName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2LimitRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._limitName = limitName;
            this._counterName = counterName;
            this._parentKey = Gs2.Gs2Limit.Domain.Model.UserDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                this._userId != null ? this._userId.ToString() : null,
                "Counter"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Limit.Model.Counter> GetAsync(
            #else
        private IFuture<Gs2.Gs2Limit.Model.Counter> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Limit.Model.Counter> GetAsync(
        #endif
            GetCounterByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Limit.Model.Counter> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithLimitName(this._limitName)
                .WithCounterName(this._counterName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetCounterByUserIdFuture(
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
            var result = await this._client.GetCounterByUserIdAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                        request.LimitName != null ? request.LimitName.ToString() : null,
                        request.CounterName != null ? request.CounterName.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(result?.Item);
        #else
            return result?.Item;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Limit.Model.Counter>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Limit.Domain.Model.CounterDomain> CountUpAsync(
            #else
        public IFuture<Gs2.Gs2Limit.Domain.Model.CounterDomain> CountUp(
            #endif
        #else
        public async Task<Gs2.Gs2Limit.Domain.Model.CounterDomain> CountUpAsync(
        #endif
            CountUpByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Limit.Domain.Model.CounterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithLimitName(this._limitName)
                .WithCounterName(this._counterName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.CountUpByUserIdFuture(
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
            var result = await this._client.CountUpByUserIdAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                        request.LimitName != null ? request.LimitName.ToString() : null,
                        request.CounterName != null ? request.CounterName.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Limit.Domain.Model.CounterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Limit.Domain.Model.CounterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Limit.Domain.Model.CounterDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Limit.Domain.Model.CounterDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Limit.Domain.Model.CounterDomain> DeleteAsync(
        #endif
            DeleteCounterByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Limit.Domain.Model.CounterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithLimitName(this._limitName)
                .WithCounterName(this._counterName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteCounterByUserIdFuture(
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
            DeleteCounterByUserIdResult result = null;
            try {
                result = await this._client.DeleteCounterByUserIdAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException) {}
            #endif
            _cache.Delete<Gs2.Gs2Limit.Model.Counter>(
                _parentKey,
                Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                    request.LimitName != null ? request.LimitName.ToString() : null,
                    request.CounterName != null ? request.CounterName.ToString() : null
                )
            );
            Gs2.Gs2Limit.Domain.Model.CounterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Limit.Domain.Model.CounterDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string limitName,
            string counterName,
            string childType
        )
        {
            return string.Join(
                ":",
                "limit",
                namespaceName ?? "null",
                userId ?? "null",
                limitName ?? "null",
                counterName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string limitName,
            string counterName
        )
        {
            return string.Join(
                ":",
                limitName ?? "null",
                counterName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Limit.Model.Counter> Model() {
            #else
        public IFuture<Gs2.Gs2Limit.Model.Counter> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Limit.Model.Counter> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Limit.Model.Counter> self)
            {
        #endif
            Gs2.Gs2Limit.Model.Counter value = _cache.Get<Gs2.Gs2Limit.Model.Counter>(
                _parentKey,
                Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                    this.LimitName?.ToString(),
                    this.CounterName?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetCounterByUserIdRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException)
                        {
                            _cache.Delete<Gs2.Gs2Limit.Model.Counter>(
                            _parentKey,
                            Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                                this.LimitName?.ToString(),
                                this.CounterName?.ToString()
                            )
                        );
                        }
                        else
                        {
                            self.OnError(future.Error);
                            yield break;
                        }
                    }
        #else
                } catch(Gs2.Core.Exception.NotFoundException) {
                    _cache.Delete<Gs2.Gs2Limit.Model.Counter>(
                        _parentKey,
                        Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                            this.LimitName?.ToString(),
                            this.CounterName?.ToString()
                        )
                    );
                }
        #endif
                value = _cache.Get<Gs2.Gs2Limit.Model.Counter>(
                _parentKey,
                Gs2.Gs2Limit.Domain.Model.CounterDomain.CreateCacheKey(
                    this.LimitName?.ToString(),
                    this.CounterName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Limit.Model.Counter>(Impl);
        #endif
        }

    }
}