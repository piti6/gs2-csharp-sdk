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
using Gs2.Gs2Matchmaking.Domain.Iterator;
using Gs2.Gs2Matchmaking.Request;
using Gs2.Gs2Matchmaking.Result;
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

namespace Gs2.Gs2Matchmaking.Domain.Model
{

    public partial class GatheringAccessTokenDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2MatchmakingRestClient _client;
        private readonly string _namespaceName;
        private AccessToken _accessToken;
        public AccessToken AccessToken => _accessToken;
        private readonly string _gatheringName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string UserId => _accessToken?.UserId;
        public string GatheringName => _gatheringName;

        public GatheringAccessTokenDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            AccessToken accessToken,
            string gatheringName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2MatchmakingRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._accessToken = accessToken;
            this._gatheringName = gatheringName;
            this._parentKey = Gs2.Gs2Matchmaking.Domain.Model.UserDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                "Singleton",
                "Gathering"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> UpdateAsync(
        #endif
            UpdateGatheringRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithAccessToken(this._accessToken?.Token)
                .WithGatheringName(this._gatheringName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateGatheringFuture(
                request
            );
            yield return future;
            if (future.Error != null)
            {
                self.OnError(future.Error);
                yield break;
            }
            var result = future.Result;
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
              
            {
                var parentKey = Gs2.Gs2Matchmaking.Domain.Model.UserDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                    "Singleton",
                        "Gathering"
                );
                var key = Gs2.Gs2Matchmaking.Domain.Model.GatheringDomain.CreateCacheKey(
                    resultModel.Item.Name.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    resultModel.Item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            #else
            var result = await this._client.UpdateGatheringAsync(
                request
            );
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
              
            {
                var parentKey = Gs2.Gs2Matchmaking.Domain.Model.UserDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                    "Singleton",
                        "Gathering"
                );
                var key = Gs2.Gs2Matchmaking.Domain.Model.GatheringDomain.CreateCacheKey(
                    resultModel.Item.Name.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    resultModel.Item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            #endif
            Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> CancelMatchmakingAsync(
            #else
        public IFuture<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> CancelMatchmaking(
            #endif
        #else
        public async Task<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> CancelMatchmakingAsync(
        #endif
            CancelMatchmakingRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithAccessToken(this._accessToken?.Token)
                .WithGatheringName(this._gatheringName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.CancelMatchmakingFuture(
                request
            );
            yield return future;
            if (future.Error != null)
            {
                self.OnError(future.Error);
                yield break;
            }
            var result = future.Result;
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
              
            {
                var parentKey = Gs2.Gs2Matchmaking.Domain.Model.UserDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                    "Singleton",
                        "Gathering"
                );
                var key = Gs2.Gs2Matchmaking.Domain.Model.GatheringDomain.CreateCacheKey(
                    resultModel.Item.Name.ToString()
                );
                cache.Delete<Gs2.Gs2Matchmaking.Model.Gathering>(parentKey, key);
            }
            #else
            CancelMatchmakingResult result = null;
            try {
                result = await this._client.CancelMatchmakingAsync(
                    request
                );
                var requestModel = request;
                var resultModel = result;
                var cache = _cache;
              
                {
                    var parentKey = Gs2.Gs2Matchmaking.Domain.Model.UserDomain.CreateCacheParentKey(
                        _namespaceName.ToString(),
                        "Singleton",
                            "Gathering"
                    );
                    var key = Gs2.Gs2Matchmaking.Domain.Model.GatheringDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Delete<Gs2.Gs2Matchmaking.Model.Gathering>(parentKey, key);
                }
            } catch(Gs2.Core.Exception.NotFoundException) {}
            #endif
            Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string gatheringName,
            string childType
        )
        {
            return string.Join(
                ":",
                "matchmaking",
                namespaceName ?? "null",
                userId ?? "null",
                gatheringName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string gatheringName
        )
        {
            return string.Join(
                ":",
                gatheringName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Matchmaking.Model.Gathering> Model() {
            #else
        public IFuture<Gs2.Gs2Matchmaking.Model.Gathering> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Matchmaking.Model.Gathering> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Matchmaking.Model.Gathering> self)
            {
        #endif
            Gs2.Gs2Matchmaking.Model.Gathering value = _cache.Get<Gs2.Gs2Matchmaking.Model.Gathering>(
                _parentKey,
                Gs2.Gs2Matchmaking.Domain.Model.GatheringDomain.CreateCacheKey(
                    this.GatheringName?.ToString()
                )
            );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(value);
            yield return null;
        #else
            return value;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Matchmaking.Model.Gathering>(Impl);
        #endif
        }

    }
}
