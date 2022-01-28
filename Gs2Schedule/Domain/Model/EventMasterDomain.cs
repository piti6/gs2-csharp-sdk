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
using Gs2.Gs2Schedule.Domain.Iterator;
using Gs2.Gs2Schedule.Request;
using Gs2.Gs2Schedule.Result;
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

namespace Gs2.Gs2Schedule.Domain.Model
{

    public partial class EventMasterDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2ScheduleRestClient _client;
        private readonly string _namespaceName;
        private readonly string _eventName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string EventName => _eventName;

        public EventMasterDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string eventName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2ScheduleRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._eventName = eventName;
            this._parentKey = Gs2.Gs2Schedule.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                "EventMaster"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Schedule.Model.EventMaster> GetAsync(
            #else
        private IFuture<Gs2.Gs2Schedule.Model.EventMaster> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Schedule.Model.EventMaster> GetAsync(
        #endif
            GetEventMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Schedule.Model.EventMaster> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithEventName(this._eventName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetEventMasterFuture(
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
            var result = await this._client.GetEventMasterAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                        request.EventName != null ? request.EventName.ToString() : null
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
            return new Gs2InlineFuture<Gs2.Gs2Schedule.Model.EventMaster>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> UpdateAsync(
        #endif
            UpdateEventMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithEventName(this._eventName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateEventMasterFuture(
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
            var result = await this._client.UpdateEventMasterAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                        request.EventName != null ? request.EventName.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Schedule.Domain.Model.EventMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> DeleteAsync(
        #endif
            DeleteEventMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithEventName(this._eventName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteEventMasterFuture(
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
            DeleteEventMasterResult result = null;
            try {
                result = await this._client.DeleteEventMasterAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException) {}
            #endif
            _cache.Delete<Gs2.Gs2Schedule.Model.EventMaster>(
                _parentKey,
                Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                    request.EventName != null ? request.EventName.ToString() : null
                )
            );
            Gs2.Gs2Schedule.Domain.Model.EventMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Schedule.Domain.Model.EventMasterDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string eventName,
            string childType
        )
        {
            return string.Join(
                ":",
                "schedule",
                namespaceName ?? "null",
                eventName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string eventName
        )
        {
            return string.Join(
                ":",
                eventName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Schedule.Model.EventMaster> Model() {
            #else
        public IFuture<Gs2.Gs2Schedule.Model.EventMaster> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Schedule.Model.EventMaster> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Schedule.Model.EventMaster> self)
            {
        #endif
            Gs2.Gs2Schedule.Model.EventMaster value = _cache.Get<Gs2.Gs2Schedule.Model.EventMaster>(
                _parentKey,
                Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                    this.EventName?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetEventMasterRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException)
                        {
                            _cache.Delete<Gs2.Gs2Schedule.Model.EventMaster>(
                            _parentKey,
                            Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                                this.EventName?.ToString()
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
                    _cache.Delete<Gs2.Gs2Schedule.Model.EventMaster>(
                        _parentKey,
                        Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                            this.EventName?.ToString()
                        )
                    );
                }
        #endif
                value = _cache.Get<Gs2.Gs2Schedule.Model.EventMaster>(
                _parentKey,
                Gs2.Gs2Schedule.Domain.Model.EventMasterDomain.CreateCacheKey(
                    this.EventName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Schedule.Model.EventMaster>(Impl);
        #endif
        }

    }
}