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
using Gs2.Gs2Mission.Domain.Iterator;
using Gs2.Gs2Mission.Request;
using Gs2.Gs2Mission.Result;
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

namespace Gs2.Gs2Mission.Domain.Model
{

    public partial class CompleteDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2MissionRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly string _missionGroupName;

        private readonly String _parentKey;
        public string TransactionId { get; set; }
        public bool? AutoRunStampSheet { get; set; }
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public string MissionGroupName => _missionGroupName;

        public CompleteDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId,
            string missionGroupName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2MissionRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._missionGroupName = missionGroupName;
            this._parentKey = Gs2.Gs2Mission.Domain.Model.UserDomain.CreateCacheParentKey(
                this.NamespaceName,
                this.UserId,
                "Complete"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Mission.Domain.Model.CompleteDomain> CompleteAsync(
            #else
        public IFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain> Complete(
            #endif
        #else
        public async Task<Gs2.Gs2Mission.Domain.Model.CompleteDomain> CompleteAsync(
        #endif
            CompleteByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMissionGroupName(this.MissionGroupName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.CompleteByUserIdFuture(
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
            var result = await this._client.CompleteByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
            }
            if (result?.StampSheet != null)
            {
                Gs2.Core.Domain.StampSheetDomain stampSheet = new Gs2.Core.Domain.StampSheetDomain(
                        _cache,
                        _jobQueueDomain,
                        _session,
                        result?.StampSheet,
                        result?.StampSheetEncryptionKeyId,
                        _stampSheetConfiguration.NamespaceName,
                        _stampSheetConfiguration.StampTaskEventHandler,
                        _stampSheetConfiguration.StampSheetEventHandler
                );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                yield return stampSheet.Run();
        #else
                try {
                    await stampSheet.RunAsync();
                } catch (Gs2.Core.Exception.Gs2Exception e) {
                    throw new Gs2.Core.Exception.TransactionException(stampSheet, e);
                }
        #endif
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(this);
        #else
            return this;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Mission.Domain.Model.CompleteDomain> ReceiveAsync(
            #else
        public IFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain> Receive(
            #endif
        #else
        public async Task<Gs2.Gs2Mission.Domain.Model.CompleteDomain> ReceiveAsync(
        #endif
            ReceiveByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMissionGroupName(this.MissionGroupName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.ReceiveByUserIdFuture(
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
            var result = await this._client.ReceiveByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Mission.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Complete"
                    );
                    var key = Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                        resultModel.Item.MissionGroupName.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        resultModel.Item.NextResetAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
            }
            Gs2.Gs2Mission.Domain.Model.CompleteDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Mission.Model.Complete> GetAsync(
            #else
        private IFuture<Gs2.Gs2Mission.Model.Complete> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Mission.Model.Complete> GetAsync(
        #endif
            GetCompleteByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Mission.Model.Complete> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMissionGroupName(this.MissionGroupName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetCompleteByUserIdFuture(
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
            var result = await this._client.GetCompleteByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Mission.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Complete"
                    );
                    var key = Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                        resultModel.Item.MissionGroupName.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        resultModel.Item.NextResetAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
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
            return new Gs2InlineFuture<Gs2.Gs2Mission.Model.Complete>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Mission.Domain.Model.CompleteDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Mission.Domain.Model.CompleteDomain> DeleteAsync(
        #endif
            DeleteCompleteByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMissionGroupName(this.MissionGroupName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteCompleteByUserIdFuture(
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
            DeleteCompleteByUserIdResult result = null;
            try {
                result = await this._client.DeleteCompleteByUserIdAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException e) {
                if (e.errors[0].component == "complete")
                {
                    var key = Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                        request.MissionGroupName.ToString()
                    );
                    _cache.Put<Gs2.Gs2Mission.Model.Complete>(
                        _parentKey,
                        key,
                        null,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
                else
                {
                    throw e;
                }
            }
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Mission.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Complete"
                    );
                    var key = Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                        resultModel.Item.MissionGroupName.ToString()
                    );
                    cache.Delete<Gs2.Gs2Mission.Model.Complete>(parentKey, key);
                }
            }
            Gs2.Gs2Mission.Domain.Model.CompleteDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Mission.Domain.Model.CompleteDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string missionGroupName,
            string childType
        )
        {
            return string.Join(
                ":",
                "mission",
                namespaceName ?? "null",
                userId ?? "null",
                missionGroupName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string missionGroupName
        )
        {
            return string.Join(
                ":",
                missionGroupName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Mission.Model.Complete> Model() {
            #else
        public IFuture<Gs2.Gs2Mission.Model.Complete> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Mission.Model.Complete> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Mission.Model.Complete> self)
            {
        #endif
            var (value, find) = _cache.Get<Gs2.Gs2Mission.Model.Complete>(
                _parentKey,
                Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                    this.MissionGroupName?.ToString()
                )
            );
            if (!find) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetCompleteByUserIdRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            var key = Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                                    this.MissionGroupName?.ToString()
                                );
                            _cache.Put<Gs2.Gs2Mission.Model.Complete>(
                                _parentKey,
                                key,
                                null,
                                UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );

                            if (e.errors[0].component != "complete")
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
                    var key = Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                            this.MissionGroupName?.ToString()
                        );
                    _cache.Put<Gs2.Gs2Mission.Model.Complete>(
                        _parentKey,
                        key,
                        null,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                    if (e.errors[0].component != "complete")
                    {
                        throw e;
                    }
                }
        #endif
                (value, find) = _cache.Get<Gs2.Gs2Mission.Model.Complete>(
                    _parentKey,
                    Gs2.Gs2Mission.Domain.Model.CompleteDomain.CreateCacheKey(
                        this.MissionGroupName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Mission.Model.Complete>(Impl);
        #endif
        }

    }
}
