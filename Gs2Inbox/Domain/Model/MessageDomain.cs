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
using Gs2.Gs2Inbox.Domain.Iterator;
using Gs2.Gs2Inbox.Request;
using Gs2.Gs2Inbox.Result;
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

namespace Gs2.Gs2Inbox.Domain.Model
{

    public partial class MessageDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2InboxRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly string _messageName;

        private readonly String _parentKey;
        public string TransactionId { get; set; }
        public bool? AutoRunStampSheet { get; set; }
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public string MessageName => _messageName;

        public MessageDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId,
            string messageName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2InboxRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._messageName = messageName;
            this._parentKey = Gs2.Gs2Inbox.Domain.Model.UserDomain.CreateCacheParentKey(
                this.NamespaceName,
                this.UserId,
                "Message"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Inbox.Model.Message> GetAsync(
            #else
        private IFuture<Gs2.Gs2Inbox.Model.Message> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Inbox.Model.Message> GetAsync(
        #endif
            GetMessageByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Model.Message> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMessageName(this.MessageName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetMessageByUserIdFuture(
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
            var result = await this._client.GetMessageByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Message"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        resultModel.Item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
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
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Model.Message>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Domain.Model.MessageDomain> OpenAsync(
            #else
        public IFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain> Open(
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Domain.Model.MessageDomain> OpenAsync(
        #endif
            OpenMessageByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMessageName(this.MessageName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.OpenMessageByUserIdFuture(
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
            OpenMessageByUserIdResult result = null;
            try {
                result = await this._client.OpenMessageByUserIdAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException e) {
                if (e.errors[0].component == "message")
                {
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        request.MessageName.ToString()
                    );
                    _cache.Put<Gs2.Gs2Inbox.Model.Message>(
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
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Message"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Delete<Gs2.Gs2Inbox.Model.Message>(parentKey, key);
                    cache.ClearListCache<Gs2.Gs2Inbox.Model.Message>(
                        parentKey
                    );
                }
            }
            Gs2.Gs2Inbox.Domain.Model.MessageDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Domain.Model.MessageDomain> ReadAsync(
            #else
        public IFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain> Read(
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Domain.Model.MessageDomain> ReadAsync(
        #endif
            ReadMessageByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMessageName(this.MessageName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.ReadMessageByUserIdFuture(
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
            var result = await this._client.ReadMessageByUserIdAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Message"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        resultModel.Item.ExpiresAt ?? UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }
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
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Domain.Model.MessageDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Domain.Model.MessageDomain> DeleteAsync(
        #endif
            DeleteMessageByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithUserId(this.UserId)
                .WithMessageName(this.MessageName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteMessageByUserIdFuture(
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
            DeleteMessageByUserIdResult result = null;
            try {
                result = await this._client.DeleteMessageByUserIdAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException e) {
                if (e.errors[0].component == "message")
                {
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        request.MessageName.ToString()
                    );
                    _cache.Put<Gs2.Gs2Inbox.Model.Message>(
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
                    var parentKey = Gs2.Gs2Inbox.Domain.Model.UserDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        this.UserId,
                        "Message"
                    );
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Delete<Gs2.Gs2Inbox.Model.Message>(parentKey, key);
                }
            }
            Gs2.Gs2Inbox.Domain.Model.MessageDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Domain.Model.MessageDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string messageName,
            string childType
        )
        {
            return string.Join(
                ":",
                "inbox",
                namespaceName ?? "null",
                userId ?? "null",
                messageName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string messageName
        )
        {
            return string.Join(
                ":",
                messageName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Inbox.Model.Message> Model() {
            #else
        public IFuture<Gs2.Gs2Inbox.Model.Message> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Inbox.Model.Message> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Inbox.Model.Message> self)
            {
        #endif
            var (value, find) = _cache.Get<Gs2.Gs2Inbox.Model.Message>(
                _parentKey,
                Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                    this.MessageName?.ToString()
                )
            );
            if (!find) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetMessageByUserIdRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                                    this.MessageName?.ToString()
                                );
                            _cache.Put<Gs2.Gs2Inbox.Model.Message>(
                                _parentKey,
                                key,
                                null,
                                UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );

                            if (e.errors[0].component != "message")
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
                    var key = Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                            this.MessageName?.ToString()
                        );
                    _cache.Put<Gs2.Gs2Inbox.Model.Message>(
                        _parentKey,
                        key,
                        null,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                    if (e.errors[0].component != "message")
                    {
                        throw e;
                    }
                }
        #endif
                (value, find) = _cache.Get<Gs2.Gs2Inbox.Model.Message>(
                    _parentKey,
                    Gs2.Gs2Inbox.Domain.Model.MessageDomain.CreateCacheKey(
                        this.MessageName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Inbox.Model.Message>(Impl);
        #endif
        }

    }
}
