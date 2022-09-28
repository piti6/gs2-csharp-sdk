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
using Gs2.Gs2SerialKey.Domain.Iterator;
using Gs2.Gs2SerialKey.Request;
using Gs2.Gs2SerialKey.Result;
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

namespace Gs2.Gs2SerialKey.Domain.Model
{

    public partial class SerialKeyDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2SerialKeyRestClient _client;
        private readonly string _namespaceName;
        private readonly string _serialKeyCode;

        private readonly String _parentKey;
        public string Url { get; set; }
        public string NamespaceName => _namespaceName;
        public string SerialKeyCode => _serialKeyCode;

        public SerialKeyDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string serialKeyCode
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2SerialKeyRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._serialKeyCode = serialKeyCode;
            this._parentKey = Gs2.Gs2SerialKey.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                "SerialKey"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain> UseAsync(
            #else
        public IFuture<Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain> Use(
            #endif
        #else
        public async Task<Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain> UseAsync(
        #endif
            UseByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UseByUserIdFuture(
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
                var parentKey = Gs2.Gs2SerialKey.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                        "SerialKey"
                );
                var key = Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain.CreateCacheKey(
                    resultModel.Item.Code.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            {
                var parentKey = Gs2.Gs2SerialKey.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                        "CampaignModel"
                );
                var key = Gs2.Gs2SerialKey.Domain.Model.CampaignModelDomain.CreateCacheKey(
                    resultModel.CampaignModel.Name.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.CampaignModel,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            #else
            var result = await this._client.UseByUserIdAsync(
                request
            );
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
              
            {
                var parentKey = Gs2.Gs2SerialKey.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                        "SerialKey"
                );
                var key = Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain.CreateCacheKey(
                    resultModel.Item.Code.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            {
                var parentKey = Gs2.Gs2SerialKey.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                        "CampaignModel"
                );
                var key = Gs2.Gs2SerialKey.Domain.Model.CampaignModelDomain.CreateCacheKey(
                    resultModel.CampaignModel.Name.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.CampaignModel,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            #endif
            Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string serialKeyCode,
            string childType
        )
        {
            return string.Join(
                ":",
                "serialKey",
                namespaceName ?? "null",
                serialKeyCode ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string serialKeyCode
        )
        {
            return string.Join(
                ":",
                serialKeyCode ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2SerialKey.Model.SerialKey> Model() {
            #else
        public IFuture<Gs2.Gs2SerialKey.Model.SerialKey> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2SerialKey.Model.SerialKey> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2SerialKey.Model.SerialKey> self)
            {
        #endif
            Gs2.Gs2SerialKey.Model.SerialKey value = _cache.Get<Gs2.Gs2SerialKey.Model.SerialKey>(
                _parentKey,
                Gs2.Gs2SerialKey.Domain.Model.SerialKeyDomain.CreateCacheKey(
                    this.SerialKeyCode?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2SerialKey.Model.SerialKey>(Impl);
        #endif
        }

    }
}