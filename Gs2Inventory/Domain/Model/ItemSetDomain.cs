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
 *
 * deny overwrite
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

    public partial class ItemSetDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2InventoryRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly string _inventoryName;
        private readonly string _itemName;
        private readonly string _itemSetName;

        private readonly String _parentKey;
        public string Body { get; set; }
        public string Signature { get; set; }
        public long? OverflowCount { get; set; }
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public string InventoryName => _inventoryName;
        public string ItemName => _itemName;
        public string ItemSetName => _itemSetName;

        public ItemSetDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId,
            string inventoryName,
            string itemName,
            string itemSetName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2InventoryRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._inventoryName = inventoryName;
            this._itemName = itemName;
            this._itemSetName = itemSetName;
            this._parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                this.NamespaceName,
                this.UserId,
                this.InventoryName,
                "ItemSet"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Inventory.Model.ItemSet[]> GetAsync(
            #else
        private IFuture<Gs2.Gs2Inventory.Model.ItemSet[]> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Inventory.Model.ItemSet[]> GetAsync(
        #endif
            GetItemSetByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Model.ItemSet[]> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithInventoryName(this.InventoryName)
                .WithItemName(this.ItemName)
                .WithItemSetName(this.ItemSetName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetItemSetByUserIdFuture(
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
            var result = await this._client.GetItemSetByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        this.InventoryName,
                        "ItemSet"
                    );
                    foreach (var item in resultModel.Items) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            item.ItemName.ToString(),
                            item.Name.ToString()
                        );
                        if (item.Count == 0) {
                            cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                                parentKey,
                                key
                            );
                        }
                        else
                        {
                            cache.Put(
                                parentKey,
                                key,
                                item,
                                item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );
                        }
                    }
                    if (resultModel.Items.Length == 0) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            ItemName,
                            null
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Items,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                    }
                    var _ = resultModel.Items.GroupBy(v => v.ItemName).Select(group =>
                    {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            group.Key,
                            null
                        );
                        var items = group.ToArray();
                        cache.Put(
                            parentKey,
                            key,
                            items,
                            items == null || items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                        return items;
                    }).ToArray();
                }
                if (resultModel.ItemModel != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.InventoryName,
                        "ItemModel"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemModelDomain.CreateCacheKey(
                        resultModel.ItemModel.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.ItemModel,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
                if (resultModel.Inventory != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Inventory"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheKey(
                        resultModel.Inventory.InventoryName.ToString()
                    );
                    var (item, find) = cache.Get<Gs2.Gs2Inventory.Model.Inventory>(
                        parentKey,
                        key
                    );
                    if (item == null || item.Revision < resultModel.Inventory.Revision)
                    {
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Inventory,
                            resultModel.Items == null || resultModel.Items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : resultModel.Items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                    }
                }
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(result?.Items);
        #else
            return result?.Items;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Model.ItemSet[]>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> GetItemWithSignatureAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> GetItemWithSignature(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> GetItemWithSignatureAsync(
        #endif
            GetItemWithSignatureByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithInventoryName(this.InventoryName)
                .WithItemName(this.ItemName)
                .WithItemSetName(this.ItemSetName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetItemWithSignatureByUserIdFuture(
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
            var result = await this._client.GetItemWithSignatureByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        this.InventoryName,
                        "ItemSet"
                    );
                    foreach (var item in resultModel.Items) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            item.ItemName.ToString(),
                            item.Name.ToString()
                        );
                        if (item.Count == 0) {
                            cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                                parentKey,
                                key
                            );
                        }
                        else
                        {
                            cache.Put(
                                parentKey,
                                key,
                                item,
                                item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );
                        }
                    }
                    if (resultModel.Items.Length == 0) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            ItemName,
                            null
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Items,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                    }
                    var _ = resultModel.Items.GroupBy(v => v.ItemName).Select(group =>
                    {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            group.Key,
                            null
                        );
                        var items = group.ToArray();
                        cache.Put(
                            parentKey,
                            key,
                            items,
                            items == null || items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                        return items;
                    }).ToArray();
                }
                if (resultModel.ItemModel != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.InventoryName,
                        "ItemModel"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemModelDomain.CreateCacheKey(
                        resultModel.ItemModel.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.ItemModel,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
                if (resultModel.Inventory != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Inventory"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheKey(
                        resultModel.Inventory.InventoryName.ToString()
                    );
                    var (item, find) = cache.Get<Gs2.Gs2Inventory.Model.Inventory>(
                        parentKey,
                        key
                    );
                    if (item == null || item.Revision < resultModel.Inventory.Revision)
                    {
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Inventory,
                            resultModel.Items == null || resultModel.Items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : resultModel.Items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                    }
                }
            }
            Gs2.Gs2Inventory.Domain.Model.ItemSetDomain domain = null;
            if (result?.Items.Length > 0) {
                domain = new Gs2.Gs2Inventory.Domain.Model.ItemSetDomain(
                    this._cache,
                    this._jobQueueDomain,
                    this._stampSheetConfiguration,
                    this._session,
                    request.NamespaceName,
                    result?.Items[0]?.UserId,
                    result?.Items[0]?.InventoryName,
                    result?.Items[0]?.ItemName,
                    null
                );
            } else {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(this);
            yield return null;
        #else
                return this;
        #endif
            }
            this.Body = domain.Body = result?.Body;
            this.Signature = domain.Signature = result?.Signature;
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> AcquireAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> Acquire(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> AcquireAsync(
        #endif
            AcquireItemSetByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithInventoryName(this.InventoryName)
                .WithItemName(this.ItemName)
                .WithItemSetName(this.ItemSetName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.AcquireItemSetByUserIdFuture(
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
            var result = await this._client.AcquireItemSetByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        this.InventoryName,
                        "ItemSet"
                    );
                    foreach (var item in resultModel.Items) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            item.ItemName.ToString(),
                            item.Name.ToString()
                        );
                        if (item.Count == 0) {
                            cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                                parentKey,
                                key
                            );
                        }
                        else
                        {
                            cache.Put(
                                parentKey,
                                key,
                                item,
                                item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );
                        }
                    }
                    if (resultModel.Items.Length == 0) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            ItemName,
                            null
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Items,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                    }
                    var _ = resultModel.Items.GroupBy(v => v.ItemName).Select(group =>
                    {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            group.Key,
                            null
                        );
                        var items = group.ToArray();
                        cache.Put(
                            parentKey,
                            key,
                            items,
                            items == null || items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                        return items;
                    }).ToArray();
                }
                if (resultModel.ItemModel != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.InventoryName,
                        "ItemModel"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemModelDomain.CreateCacheKey(
                        resultModel.ItemModel.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.ItemModel,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
                if (resultModel.Inventory != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Inventory"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheKey(
                        resultModel.Inventory.InventoryName.ToString()
                    );
                    var (item, find) = cache.Get<Gs2.Gs2Inventory.Model.Inventory>(
                        parentKey,
                        key
                    );
                    if (item == null || item.Revision < resultModel.Inventory.Revision)
                    {
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Inventory,
                            resultModel.Items == null || resultModel.Items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : resultModel.Items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                    }
                }
            }
            Gs2.Gs2Inventory.Domain.Model.ItemSetDomain domain = null;
            if (result?.Items.Length > 0) {
                domain = new Gs2.Gs2Inventory.Domain.Model.ItemSetDomain(
                    this._cache,
                    this._jobQueueDomain,
                    this._stampSheetConfiguration,
                    this._session,
                    request.NamespaceName,
                    result?.Items[0]?.UserId,
                    result?.Items[0]?.InventoryName,
                    result?.Items[0]?.ItemName,
                    null
                );
            } else {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(this);
            yield return null;
        #else
                return this;
        #endif
            }
            this.OverflowCount = domain.OverflowCount = result?.OverflowCount;
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> ConsumeAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> Consume(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> ConsumeAsync(
        #endif
            ConsumeItemSetByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithInventoryName(this.InventoryName)
                .WithItemName(this.ItemName)
                .WithItemSetName(this.ItemSetName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.ConsumeItemSetByUserIdFuture(
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
            var result = await this._client.ConsumeItemSetByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        this.InventoryName,
                        "ItemSet"
                    );
                    foreach (var item in resultModel.Items) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            item.ItemName.ToString(),
                            item.Name.ToString()
                        );
                        if (item.Count == 0) {
                            cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                                parentKey,
                                key
                            );
                        }
                        else
                        {
                            cache.Put(
                                parentKey,
                                key,
                                item,
                                item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );
                        }
                    }
                    if (resultModel.Items.Length == 0) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            ItemName,
                            null
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Items,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                    }
                    var _ = resultModel.Items.GroupBy(v => v.ItemName).Select(group =>
                    {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            group.Key,
                            null
                        );
                        var items = group.ToArray();
                        cache.Put(
                            parentKey,
                            key,
                            items,
                            items == null || items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                        return items;
                    }).ToArray();
                }
                if (resultModel.ItemModel != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.InventoryName,
                        "ItemModel"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemModelDomain.CreateCacheKey(
                        resultModel.ItemModel.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.ItemModel,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
                if (resultModel.Inventory != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Inventory"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheKey(
                        resultModel.Inventory.InventoryName.ToString()
                    );
                    var (item, find) = cache.Get<Gs2.Gs2Inventory.Model.Inventory>(
                        parentKey,
                        key
                    );
                    if (item == null || item.Revision < resultModel.Inventory.Revision)
                    {
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Inventory,
                            resultModel.Items == null || resultModel.Items.Length == 0 ? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes : resultModel.Items.Min(v => v.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes)
                        );
                    }
                }
            }
            Gs2.Gs2Inventory.Domain.Model.ItemSetDomain domain = null;
            if (result?.Items.Length > 0) {
                domain = new Gs2.Gs2Inventory.Domain.Model.ItemSetDomain(
                    this._cache,
                    this._jobQueueDomain,
                    this._stampSheetConfiguration,
                    this._session,
                    request.NamespaceName,
                    result?.Items[0]?.UserId,
                    result?.Items[0]?.InventoryName,
                    result?.Items[0]?.ItemName,
                    null
                );
            } else {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(this);
            yield return null;
        #else
                return this;
        #endif
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> DeleteAsync(
        #endif
            DeleteItemSetByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithInventoryName(this.InventoryName)
                .WithItemName(this.ItemName)
                .WithItemSetName(this.ItemSetName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteItemSetByUserIdFuture(
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
            DeleteItemSetByUserIdResult result = null;
            try {
                result = await this._client.DeleteItemSetByUserIdAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException e) {
                if (e.errors[0].component == "itemSet")
                {
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                        request.ItemName.ToString(),
                        request.ItemSetName.ToString()
                    );
                    _cache.Put<Gs2.Gs2Inventory.Model.ItemSet>(
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
                {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        this.InventoryName,
                        "ItemSet"
                    );
                    foreach (var item in resultModel.Items) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            item.ItemName.ToString(),
                            item.Name.ToString()
                        );
                        cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                            parentKey,
                            key
                        );
                    }
                    if (resultModel.Items.Length == 0) {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            ItemName,
                            null
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Items,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                    }
                    var _ = resultModel.Items.GroupBy(v => v.ItemName).Select(group =>
                    {
                        var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            group.Key,
                            null
                        );
                        var items = group.ToArray();
                        cache.Delete<Gs2.Gs2Inventory.Model.ItemSet[]>(
                            parentKey,
                            key
                        );
                        return items;
                    }).ToArray();
                }
                if (resultModel.ItemModel != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.InventoryName,
                        "ItemModel"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemModelDomain.CreateCacheKey(
                        resultModel.ItemModel.Name.ToString()
                    );
                    cache.Delete<Gs2.Gs2Inventory.Model.ItemModel>(parentKey, key);
                }
                if (resultModel.Inventory != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Inventory"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheKey(
                        resultModel.Inventory.InventoryName.ToString()
                    );
                    cache.Delete<Gs2.Gs2Inventory.Model.Inventory>(parentKey, key);
                }
            }
            Gs2.Gs2Inventory.Domain.Model.ItemSetDomain domain = null;
            if (result?.Items.Length > 0) {
                domain = new Gs2.Gs2Inventory.Domain.Model.ItemSetDomain(
                    this._cache,
                    this._jobQueueDomain,
                    this._stampSheetConfiguration,
                    this._session,
                    request.NamespaceName,
                    result?.Items[0]?.UserId,
                    result?.Items[0]?.InventoryName,
                    result?.Items[0]?.ItemName,
                    null
                );
            } else {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(this);
            yield return null;
        #else
                return this;
        #endif
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.ItemSetDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain[]> AddReferenceOfAsync(
            #else
        public IFuture<Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain[]> AddReferenceOf(
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain[]> AddReferenceOfAsync(
        #endif
            AddReferenceOfByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain[]> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithInventoryName(this.InventoryName)
                .WithItemName(this.ItemName)
                .WithItemSetName(this.ItemSetName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.AddReferenceOfByUserIdFuture(
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
            var result = await this._client.AddReferenceOfByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.ItemSet != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        this.InventoryName,
                        "ItemSet"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                        resultModel.ItemSet.ItemName.ToString(),
                        resultModel.ItemSet.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.ItemSet,
                        resultModel.ItemSet.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
                if (resultModel.ItemModel != null) {
                    var parentKey = Gs2.Gs2Inventory.Domain.Model.InventoryModelDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.InventoryName,
                        "ItemModel"
                    );
                    var key = Gs2.Gs2Inventory.Domain.Model.ItemModelDomain.CreateCacheKey(
                        resultModel.ItemModel.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.ItemModel,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
            }
            var domain = new Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain[result?.Item.Length ?? 0];
            for (int i=0; i<result?.Item.Length; i++)
            {
                domain[i] = new Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain(
                    this._cache,
                    this._jobQueueDomain,
                    this._stampSheetConfiguration,
                    this._session,
                    request.NamespaceName,
                    request.UserId,
                    request.InventoryName,
                    request.ItemName,
                    request.ItemSetName,
                    request.ReferenceOf
                );
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain[]>(Impl);
        #endif
        }
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public Gs2Iterator<string> ReferenceOves(
        )
        {
            return new DescribeReferenceOfByUserIdIterator(
                this._cache,
                this._client,
                this.NamespaceName,
                this.InventoryName,
                this.UserId,
                this.ItemName,
                this.ItemSetName
            );
        }

        public IUniTaskAsyncEnumerable<string> ReferenceOvesAsync(
            #else
        public Gs2Iterator<string> ReferenceOves(
            #endif
        #else
        public DescribeReferenceOfByUserIdIterator ReferenceOves(
        #endif
        )
        {
            return new DescribeReferenceOfByUserIdIterator(
                this._cache,
                this._client,
                this.NamespaceName,
                this.InventoryName,
                this.UserId,
                this.ItemName,
                this.ItemSetName
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

        public Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain ReferenceOf(
            string referenceOf
        ) {
            return new Gs2.Gs2Inventory.Domain.Model.ReferenceOfDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this.NamespaceName,
                this.UserId,
                this.InventoryName,
                this.ItemName,
                this.ItemSetName,
                referenceOf
            );
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string inventoryName,
            string itemName,
            string itemSetName,
            string childType
        )
        {
            return string.Join(
                ":",
                "inventory",
                namespaceName ?? "null",
                userId ?? "null",
                inventoryName ?? "null",
                itemName ?? "null",
                itemSetName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string itemName,
            string itemSetName
        )
        {
            return string.Join(
                ":",
                itemName ?? "null",
                itemSetName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inventory.Model.ItemSet[]> Model() {
            #else
        public IFuture<Gs2.Gs2Inventory.Model.ItemSet[]> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Inventory.Model.ItemSet[]> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inventory.Model.ItemSet[]> self)
            {
        #endif
            Gs2.Gs2Inventory.Model.ItemSet[] value;
            bool find = false;
            if (!string.IsNullOrEmpty(this.ItemSetName)) {
                value = new []{
                    _cache.Get<Gs2.Gs2Inventory.Model.ItemSet>(
                        _parentKey,
                        Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            this.ItemName?.ToString(),
                            this.ItemSetName?.ToString()
                        )
                    ).Item1
                };
                find = true;
            }
            else 
            {
                var (v, _) = _cache.Get<Gs2.Gs2Inventory.Model.ItemSet>(
                    _parentKey,
                    Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                        this.ItemName?.ToString(),
                        this.ItemSetName?.ToString()
                    )
                );
                if (v == null) {
                    value = null;
                }
                else {
                    value = new[] {v};
                    find = true;
                }
            }
            if (!find) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetItemSetByUserIdRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            if (e.errors[0].component == "itemSet")
                            {
                                _cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                                    _parentKey,
                                    Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                                        this.ItemName?.ToString(),
                                        this.ItemSetName?.ToString()
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
                    if (e.errors[0].component == "itemSet")
                    {
                        _cache.Delete<Gs2.Gs2Inventory.Model.ItemSet>(
                            _parentKey,
                            Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                                this.ItemName?.ToString(),
                                this.ItemSetName?.ToString()
                            )
                        );
                    }
                    else
                    {
                        throw e;
                    }
                }
        #endif
                if (!string.IsNullOrEmpty(this.ItemSetName)) {
                    var (v, find2) = _cache.Get<Gs2.Gs2Inventory.Model.ItemSet>(
                        _parentKey,
                        Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            this.ItemName?.ToString(),
                            this.ItemSetName?.ToString()
                        )
                    );
                    if (v == null) {
                        value = null;
                    }
                    else {
                        value = new[] {v};
                    }
                }
                else 
                {
                    (value, _) = _cache.Get<Gs2.Gs2Inventory.Model.ItemSet[]>(
                        _parentKey,
                        Gs2.Gs2Inventory.Domain.Model.ItemSetDomain.CreateCacheKey(
                            this.ItemName?.ToString(),
                            null
                        )
                    );
                }
            }
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(value);
            yield return null;
        #else
            return value;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inventory.Model.ItemSet[]>(Impl);
        #endif
        }

    }
}
