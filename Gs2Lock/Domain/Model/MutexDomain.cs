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
using Gs2.Gs2Lock.Domain.Iterator;
using Gs2.Gs2Lock.Request;
using Gs2.Gs2Lock.Result;
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

namespace Gs2.Gs2Lock.Domain.Model
{

    public partial class MutexDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2LockRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly string _propertyId;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public string PropertyId => _propertyId;

        public MutexDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId,
            string propertyId
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2LockRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._propertyId = propertyId;
            this._parentKey = Gs2.Gs2Lock.Domain.Model.UserDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                this._userId != null ? this._userId.ToString() : null,
                "Mutex"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lock.Domain.Model.MutexDomain> LockAsync(
            #else
        public IFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain> Lock(
            #endif
        #else
        public async Task<Gs2.Gs2Lock.Domain.Model.MutexDomain> LockAsync(
        #endif
            LockByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithPropertyId(this._propertyId);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.LockByUserIdFuture(
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
            var result = await this._client.LockByUserIdAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                        request.PropertyId != null ? request.PropertyId.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Lock.Domain.Model.MutexDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lock.Domain.Model.MutexDomain> UnlockAsync(
            #else
        public IFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain> Unlock(
            #endif
        #else
        public async Task<Gs2.Gs2Lock.Domain.Model.MutexDomain> UnlockAsync(
        #endif
            UnlockByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithPropertyId(this._propertyId);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UnlockByUserIdFuture(
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
            var result = await this._client.UnlockByUserIdAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                        request.PropertyId != null ? request.PropertyId.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Lock.Domain.Model.MutexDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Lock.Model.Mutex> GetAsync(
            #else
        private IFuture<Gs2.Gs2Lock.Model.Mutex> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Lock.Model.Mutex> GetAsync(
        #endif
            GetMutexByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lock.Model.Mutex> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithPropertyId(this._propertyId);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetMutexByUserIdFuture(
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
            var result = await this._client.GetMutexByUserIdAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                        request.PropertyId != null ? request.PropertyId.ToString() : null
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
            return new Gs2InlineFuture<Gs2.Gs2Lock.Model.Mutex>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lock.Domain.Model.MutexDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Lock.Domain.Model.MutexDomain> DeleteAsync(
        #endif
            DeleteMutexByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithPropertyId(this._propertyId);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteMutexByUserIdFuture(
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
            DeleteMutexByUserIdResult result = null;
            try {
                result = await this._client.DeleteMutexByUserIdAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException) {}
            #endif
            _cache.Delete<Gs2.Gs2Lock.Model.Mutex>(
                _parentKey,
                Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                    request.PropertyId != null ? request.PropertyId.ToString() : null
                )
            );
            Gs2.Gs2Lock.Domain.Model.MutexDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Lock.Domain.Model.MutexDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string propertyId,
            string childType
        )
        {
            return string.Join(
                ":",
                "lock",
                namespaceName ?? "null",
                userId ?? "null",
                propertyId ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string propertyId
        )
        {
            return string.Join(
                ":",
                propertyId ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lock.Model.Mutex> Model() {
            #else
        public IFuture<Gs2.Gs2Lock.Model.Mutex> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Lock.Model.Mutex> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lock.Model.Mutex> self)
            {
        #endif
            Gs2.Gs2Lock.Model.Mutex value = _cache.Get<Gs2.Gs2Lock.Model.Mutex>(
                _parentKey,
                Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                    this.PropertyId?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetMutexByUserIdRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException)
                        {
                            _cache.Delete<Gs2.Gs2Lock.Model.Mutex>(
                            _parentKey,
                            Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                                this.PropertyId?.ToString()
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
                    _cache.Delete<Gs2.Gs2Lock.Model.Mutex>(
                        _parentKey,
                        Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                            this.PropertyId?.ToString()
                        )
                    );
                }
        #endif
                value = _cache.Get<Gs2.Gs2Lock.Model.Mutex>(
                _parentKey,
                Gs2.Gs2Lock.Domain.Model.MutexDomain.CreateCacheKey(
                    this.PropertyId?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Lock.Model.Mutex>(Impl);
        #endif
        }

    }
}