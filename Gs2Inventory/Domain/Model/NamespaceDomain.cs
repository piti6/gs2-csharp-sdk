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
using Gs2.Gs2Inventory.Domain.Iterator;
using Gs2.Gs2Inventory.Request;
using Gs2.Gs2Inventory.Result;
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

namespace Gs2.Gs2Inventory.Domain.Model
{

    public partial class NamespaceDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2InventoryRestClient _client;
        private readonly string _namespaceName;

        private readonly String _parentKey;
        public string Status { get; set; }
        public string NextPageToken { get; set; }
        public string NamespaceName => _namespaceName;

        public NamespaceDomain(
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
            this._client = new Gs2InventoryRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._parentKey = "inventory:Gs2.Gs2Inventory.Model.Namespace";
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> GetStatusAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> GetStatus(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> GetStatusAsync(
        #endif
            GetNamespaceStatusRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
          IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> self)
          {
        #endif
            request
                .WithNamespaceName(this._namespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetNamespaceStatusFuture(
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
            var result = await this._client.GetNamespaceStatusAsync(
                request
            );
            #endif
            Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain = this;
            this.Status = domain.Status = result?.Status;
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Inventory.Model.Namespace> GetAsync(
            #else
        private IFuture<Gs2.Gs2Inventory.Model.Namespace> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Inventory.Model.Namespace> GetAsync(
        #endif
            GetNamespaceRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
          IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Model.Namespace> self)
          {
        #endif
            request
                .WithNamespaceName(this._namespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetNamespaceFuture(
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
            var result = await this._client.GetNamespaceAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                        request.NamespaceName != null ? request.NamespaceName.ToString() : null
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
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Model.Namespace>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> UpdateAsync(
        #endif
            UpdateNamespaceRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
          IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> self)
          {
        #endif
            request
                .WithNamespaceName(this._namespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateNamespaceFuture(
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
            var result = await this._client.UpdateNamespaceAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                        request.NamespaceName != null ? request.NamespaceName.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> DeleteAsync(
        #endif
            DeleteNamespaceRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
          IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain> self)
          {
        #endif
            request
                .WithNamespaceName(this._namespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteNamespaceFuture(
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
            DeleteNamespaceResult result = null;
            try {
                result = await this._client.DeleteNamespaceAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException) {}
            #endif
            _cache.Delete<Gs2.Gs2Inventory.Model.Namespace>(
                _parentKey,
                Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                    request.NamespaceName != null ? request.NamespaceName.ToString() : null
                )
            );
            Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.NamespaceDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain> CreateInventoryModelMasterAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain> CreateInventoryModelMaster(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain> CreateInventoryModelMasterAsync(
        #endif
            CreateInventoryModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
          IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain> self)
          {
        #endif
            request
                .WithNamespaceName(this._namespaceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.CreateInventoryModelMasterFuture(
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
            var result = await this._client.CreateInventoryModelMasterAsync(
                request
            );
            #endif
            string parentKey = Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                "InventoryModelMaster"
            );
                    
            if (result.Item != null) {
                _cache.Put(
                    parentKey,
                    Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain.CreateCacheKey(
                        result.Item?.Name?.ToString()
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain domain = new Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                request.NamespaceName,
                result?.Item?.Name
            );

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain>(Impl);
        #endif
        }

        public Gs2.Gs2Inventory.Domain.Model.CurrentItemModelMasterDomain CurrentItemModelMaster(
        ) {
            return new Gs2.Gs2Inventory.Domain.Model.CurrentItemModelMasterDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Inventory.Model.InventoryModel> InventoryModels(
            #else
        public Gs2Iterator<Gs2.Gs2Inventory.Model.InventoryModel> InventoryModels(
            #endif
        #else
        public DescribeInventoryModelsIterator InventoryModels(
        #endif
        )
        {
            return new DescribeInventoryModelsIterator(
                this._cache,
                this._client,
                this._namespaceName
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            ).GetAsyncEnumerator();
            #else
            );
            #endif
        #else
            );
        #endif
        }

        public Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain InventoryModel(
            string inventoryName
        ) {
            return new Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                inventoryName
            );
        }

        public Gs2.Gs2Inventory.Domain.Model.UserDomain User(
            string userId
        ) {
            return new Gs2.Gs2Inventory.Domain.Model.UserDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                userId
            );
        }

        public UserAccessTokenDomain AccessToken(
            AccessToken accessToken
        ) {
            return new UserAccessTokenDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                accessToken
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Inventory.Model.InventoryModelMaster> InventoryModelMasters(
            #else
        public Gs2Iterator<Gs2.Gs2Inventory.Model.InventoryModelMaster> InventoryModelMasters(
            #endif
        #else
        public DescribeInventoryModelMastersIterator InventoryModelMasters(
        #endif
        )
        {
            return new DescribeInventoryModelMastersIterator(
                this._cache,
                this._client,
                this._namespaceName
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            ).GetAsyncEnumerator();
            #else
            );
            #endif
        #else
            );
        #endif
        }

        public Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain InventoryModelMaster(
            string inventoryName
        ) {
            return new Gs2.Gs2Inventory.Domain.Model.InventoryModelMasterDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                inventoryName
            );
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string childType
        )
        {
            return string.Join(
                ":",
                "inventory",
                namespaceName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string namespaceName
        )
        {
            return string.Join(
                ":",
                namespaceName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Model.Namespace> Model() {
            #else
        public IFuture<Gs2.Gs2Inventory.Model.Namespace> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Model.Namespace> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Model.Namespace> self)
            {
        #endif
            Gs2.Gs2Inventory.Model.Namespace value = _cache.Get<Gs2.Gs2Inventory.Model.Namespace>(
                _parentKey,
                Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                    this.NamespaceName?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetNamespaceRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException)
                        {
                            _cache.Delete<Gs2.Gs2Inventory.Model.Namespace>(
                            _parentKey,
                            Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                                this.NamespaceName?.ToString()
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
                    _cache.Delete<Gs2.Gs2Inventory.Model.Namespace>(
                        _parentKey,
                        Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                            this.NamespaceName?.ToString()
                        )
                    );
                }
        #endif
                value = _cache.Get<Gs2.Gs2Inventory.Model.Namespace>(
                _parentKey,
                Gs2.Gs2Inventory.Domain.Model.NamespaceDomain.CreateCacheKey(
                    this.NamespaceName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Model.Namespace>(Impl);
        #endif
        }

    }
}
