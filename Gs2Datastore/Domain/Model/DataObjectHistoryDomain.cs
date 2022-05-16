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
using Gs2.Gs2Datastore.Domain.Iterator;
using Gs2.Gs2Datastore.Request;
using Gs2.Gs2Datastore.Result;
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

namespace Gs2.Gs2Datastore.Domain.Model
{

    public partial class DataObjectHistoryDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2DatastoreRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly string _dataObjectName;
        private readonly string _generation;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public string DataObjectName => _dataObjectName;
        public string Generation => _generation;

        public DataObjectHistoryDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId,
            string dataObjectName,
            string generation
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2DatastoreRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._dataObjectName = dataObjectName;
            this._generation = generation;
            this._parentKey = Gs2.Gs2Datastore.Domain.Model.DataObjectDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                this._userId != null ? this._userId.ToString() : null,
                this._dataObjectName != null ? this._dataObjectName.ToString() : null,
                "DataObjectHistory"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Datastore.Model.DataObjectHistory> GetAsync(
            #else
        private IFuture<Gs2.Gs2Datastore.Model.DataObjectHistory> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Datastore.Model.DataObjectHistory> GetAsync(
        #endif
            GetDataObjectHistoryByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Datastore.Model.DataObjectHistory> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId)
                .WithDataObjectName(this._dataObjectName)
                .WithGeneration(this._generation);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetDataObjectHistoryByUserIdFuture(
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
                var parentKey = Gs2.Gs2Datastore.Domain.Model.DataObjectDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                    _userId.ToString(),
                    resultModel.Item.DataObjectName.ToString(),
                    "DataObjectHistory"
                );
                var key = Gs2.Gs2Datastore.Domain.Model.DataObjectHistoryDomain.CreateCacheKey(
                    resultModel.Item.Generation.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            #else
            var result = await this._client.GetDataObjectHistoryByUserIdAsync(
                request
            );
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
              
            {
                var parentKey = Gs2.Gs2Datastore.Domain.Model.DataObjectDomain.CreateCacheParentKey(
                    _namespaceName.ToString(),
                    _userId.ToString(),
                    resultModel.Item.DataObjectName.ToString(),
                    "DataObjectHistory"
                );
                var key = Gs2.Gs2Datastore.Domain.Model.DataObjectHistoryDomain.CreateCacheKey(
                    resultModel.Item.Generation.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(result?.Item);
        #else
            return result?.Item;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Datastore.Model.DataObjectHistory>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string dataObjectName,
            string generation,
            string childType
        )
        {
            return string.Join(
                ":",
                "datastore",
                namespaceName ?? "null",
                userId ?? "null",
                dataObjectName ?? "null",
                generation ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string generation
        )
        {
            return string.Join(
                ":",
                generation ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Datastore.Model.DataObjectHistory> Model() {
            #else
        public IFuture<Gs2.Gs2Datastore.Model.DataObjectHistory> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Datastore.Model.DataObjectHistory> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Datastore.Model.DataObjectHistory> self)
            {
        #endif
            Gs2.Gs2Datastore.Model.DataObjectHistory value = _cache.Get<Gs2.Gs2Datastore.Model.DataObjectHistory>(
                _parentKey,
                Gs2.Gs2Datastore.Domain.Model.DataObjectHistoryDomain.CreateCacheKey(
                    this.Generation?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetDataObjectHistoryByUserIdRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            if (e.errors[0].component == "dataObjectHistory")
                            {
                                _cache.Delete<Gs2.Gs2Datastore.Model.DataObjectHistory>(
                                    _parentKey,
                                    Gs2.Gs2Datastore.Domain.Model.DataObjectHistoryDomain.CreateCacheKey(
                                        this.Generation?.ToString()
                                    )
                                );
                            }
                            else
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
                    if (e.errors[0].component == "dataObjectHistory")
                    {
                    _cache.Delete<Gs2.Gs2Datastore.Model.DataObjectHistory>(
                            _parentKey,
                            Gs2.Gs2Datastore.Domain.Model.DataObjectHistoryDomain.CreateCacheKey(
                                this.Generation?.ToString()
                            )
                        );
                    }
                    else
                    {
                        throw e;
                    }
                }
        #endif
                value = _cache.Get<Gs2.Gs2Datastore.Model.DataObjectHistory>(
                _parentKey,
                Gs2.Gs2Datastore.Domain.Model.DataObjectHistoryDomain.CreateCacheKey(
                    this.Generation?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Datastore.Model.DataObjectHistory>(Impl);
        #endif
        }

    }
}
