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
using Gs2.Gs2MegaField.Domain.Iterator;
using Gs2.Gs2MegaField.Request;
using Gs2.Gs2MegaField.Result;
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

namespace Gs2.Gs2MegaField.Domain.Model
{

    public partial class LayerModelMasterDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2MegaFieldRestClient _client;
        private readonly string _namespaceName;
        private readonly string _areaModelName;
        private readonly string _layerModelName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string AreaModelName => _areaModelName;
        public string LayerModelName => _layerModelName;

        public LayerModelMasterDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string areaModelName,
            string layerModelName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2MegaFieldRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._areaModelName = areaModelName;
            this._layerModelName = layerModelName;
            this._parentKey = Gs2.Gs2MegaField.Domain.Model.AreaModelMasterDomain.CreateCacheParentKey(
                this._namespaceName?.ToString() ?? null,
                this._areaModelName?.ToString() ?? null,
                "LayerModelMaster"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2MegaField.Model.LayerModelMaster> GetAsync(
            #else
        private IFuture<Gs2.Gs2MegaField.Model.LayerModelMaster> Get(
            #endif
        #else
        private async Task<Gs2.Gs2MegaField.Model.LayerModelMaster> GetAsync(
        #endif
            GetLayerModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2MegaField.Model.LayerModelMaster> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithAreaModelName(this._areaModelName)
                .WithLayerModelName(this._layerModelName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetLayerModelMasterFuture(
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
            var result = await this._client.GetLayerModelMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;

            {
                var parentKey = Gs2.Gs2MegaField.Domain.Model.AreaModelMasterDomain.CreateCacheParentKey(
                    this._namespaceName?.ToString() ?? null,
                    this._areaModelName?.ToString() ?? null,
                    "LayerModelMaster"
                );
                var key = Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                    resultModel.Item.Name.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
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
            return new Gs2InlineFuture<Gs2.Gs2MegaField.Model.LayerModelMaster>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> UpdateAsync(
        #endif
            UpdateLayerModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithAreaModelName(this._areaModelName)
                .WithLayerModelName(this._layerModelName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateLayerModelMasterFuture(
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
            var result = await this._client.UpdateLayerModelMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;

            {
                var parentKey = Gs2.Gs2MegaField.Domain.Model.AreaModelMasterDomain.CreateCacheParentKey(
                    this._namespaceName?.ToString() ?? null,
                    this._areaModelName?.ToString() ?? null,
                    "LayerModelMaster"
                );
                var key = Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                    resultModel.Item.Name.ToString()
                );
                cache.Put(
                    parentKey,
                    key,
                    resultModel.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> DeleteAsync(
        #endif
            DeleteLayerModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithAreaModelName(this._areaModelName)
                .WithLayerModelName(this._layerModelName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteLayerModelMasterFuture(
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
            DeleteLayerModelMasterResult result = null;
            try {
                result = await this._client.DeleteLayerModelMasterAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException e) {
                if (e.errors[0].component == "layerModelMaster")
                {
                    var parentKey = Gs2.Gs2MegaField.Domain.Model.AreaModelMasterDomain.CreateCacheParentKey(
                    this._namespaceName?.ToString() ?? null,
                    this._areaModelName?.ToString() ?? null,
                    "LayerModelMaster"
                );
                    var key = Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                        request.LayerModelName.ToString()
                    );
                    _cache.Delete<Gs2.Gs2MegaField.Model.LayerModelMaster>(parentKey, key);
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

            {
                var parentKey = Gs2.Gs2MegaField.Domain.Model.AreaModelMasterDomain.CreateCacheParentKey(
                    this._namespaceName?.ToString() ?? null,
                    this._areaModelName?.ToString() ?? null,
                    "LayerModelMaster"
                );
                var key = Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                    resultModel.Item.Name.ToString()
                );
                cache.Delete<Gs2.Gs2MegaField.Model.LayerModelMaster>(parentKey, key);
            }
            Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string areaModelName,
            string layerModelName,
            string childType
        )
        {
            return string.Join(
                ":",
                "megaField",
                namespaceName ?? "null",
                areaModelName ?? "null",
                layerModelName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string layerModelName
        )
        {
            return string.Join(
                ":",
                layerModelName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2MegaField.Model.LayerModelMaster> Model() {
            #else
        public IFuture<Gs2.Gs2MegaField.Model.LayerModelMaster> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2MegaField.Model.LayerModelMaster> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2MegaField.Model.LayerModelMaster> self)
            {
        #endif
            Gs2.Gs2MegaField.Model.LayerModelMaster value = _cache.Get<Gs2.Gs2MegaField.Model.LayerModelMaster>(
                _parentKey,
                Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                    this.LayerModelName?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetLayerModelMasterRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            if (e.errors[0].component == "layerModelMaster")
                            {
                                _cache.Delete<Gs2.Gs2MegaField.Model.LayerModelMaster>(
                                    _parentKey,
                                    Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                                        this.LayerModelName?.ToString()
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
                    if (e.errors[0].component == "layerModelMaster")
                    {
                        _cache.Delete<Gs2.Gs2MegaField.Model.LayerModelMaster>(
                            _parentKey,
                            Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                                this.LayerModelName?.ToString()
                            )
                        );
                    }
                    else
                    {
                        throw e;
                    }
                }
        #endif
                value = _cache.Get<Gs2.Gs2MegaField.Model.LayerModelMaster>(
                    _parentKey,
                    Gs2.Gs2MegaField.Domain.Model.LayerModelMasterDomain.CreateCacheKey(
                        this.LayerModelName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2MegaField.Model.LayerModelMaster>(Impl);
        #endif
        }

    }
}
