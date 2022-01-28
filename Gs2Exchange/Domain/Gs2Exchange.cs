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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using Gs2.Gs2Exchange.Domain.Iterator;
using Gs2.Gs2Exchange.Domain.Model;
using Gs2.Gs2Exchange.Request;
using Gs2.Gs2Exchange.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
#if UNITY_2017_1_OR_NEWER
using System.Collections;
using UnityEngine.Scripting;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
    #endif
#else
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Gs2Exchange.Domain
{

    public class Gs2Exchange {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2ExchangeRestClient _client;

        private readonly String _parentKey;

        public Gs2Exchange(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2ExchangeRestClient(
                session
            );
            this._parentKey = "exchange";
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Exchange.Domain.Model.NamespaceDomain> CreateNamespaceAsync(
            #else
        public IFuture<Gs2.Gs2Exchange.Domain.Model.NamespaceDomain> CreateNamespace(
            #endif
        #else
        public async Task<Gs2.Gs2Exchange.Domain.Model.NamespaceDomain> CreateNamespaceAsync(
        #endif
            CreateNamespaceRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Exchange.Domain.Model.NamespaceDomain> self)
            {
        #endif
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.CreateNamespaceFuture(
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
            var result = await this._client.CreateNamespaceAsync(
                request
            );
            #endif
            string parentKey = "exchange:Gs2.Gs2Exchange.Model.Namespace";
                    
            if (result.Item != null) {
                _cache.Put(
                    parentKey,
                    Gs2.Gs2Exchange.Domain.Model.NamespaceDomain.CreateCacheKey(
                        result.Item?.Name?.ToString()
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Exchange.Domain.Model.NamespaceDomain domain = new Gs2.Gs2Exchange.Domain.Model.NamespaceDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
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
            return new Gs2InlineFuture<Gs2.Gs2Exchange.Domain.Model.NamespaceDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Exchange.Model.Namespace> Namespaces(
            #else
        public Gs2Iterator<Gs2.Gs2Exchange.Model.Namespace> Namespaces(
            #endif
        #else
        public DescribeNamespacesIterator Namespaces(
        #endif
        )
        {
            return new DescribeNamespacesIterator(
                this._cache,
                this._client
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

        public Gs2.Gs2Exchange.Domain.Model.NamespaceDomain Namespace(
            string namespaceName
        ) {
            return new Gs2.Gs2Exchange.Domain.Model.NamespaceDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                namespaceName
            );
        }

        public static void UpdateCacheFromStampSheet(
                CacheDatabase cache,
                string method,
                string request,
                string result
        ) {
                switch (method) {
                    case "ExchangeByUserId": {
                        ExchangeByUserIdRequest requestModel = ExchangeByUserIdRequest.FromJson(JsonMapper.ToObject(request));
                        ExchangeByUserIdResult resultModel = ExchangeByUserIdResult.FromJson(JsonMapper.ToObject(result));
                        string parentKey = Gs2.Gs2Exchange.Domain.Model.UserDomain.CreateCacheParentKey(
                            requestModel.NamespaceName.ToString(),
                            requestModel.UserId.ToString(),
                            "Exchange"
                        );
                        string key = Gs2.Gs2Exchange.Domain.Model.ExchangeDomain.CreateCacheKey(
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Item,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                        break;
                    }
                    case "CreateAwaitByUserId": {
                        CreateAwaitByUserIdRequest requestModel = CreateAwaitByUserIdRequest.FromJson(JsonMapper.ToObject(request));
                        CreateAwaitByUserIdResult resultModel = CreateAwaitByUserIdResult.FromJson(JsonMapper.ToObject(result));
                        string parentKey = Gs2.Gs2Exchange.Domain.Model.UserDomain.CreateCacheParentKey(
                            requestModel.NamespaceName.ToString(),
                            resultModel.Item.UserId.ToString(),
                            "Await"
                        );
                        string key = Gs2.Gs2Exchange.Domain.Model.AwaitDomain.CreateCacheKey(
                            resultModel.Item.Name.ToString(),
                            resultModel.Item.RateName.ToString()
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Item,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                        break;
                    }
                }
        }

        public static void UpdateCacheFromStampTask(
                CacheDatabase cache,
                string method,
                string request,
                string result
        ) {
                switch (method) {
                    case "DeleteAwaitByUserId": {
                        DeleteAwaitByUserIdRequest requestModel = DeleteAwaitByUserIdRequest.FromJson(JsonMapper.ToObject(request));
                        DeleteAwaitByUserIdResult resultModel = DeleteAwaitByUserIdResult.FromJson(JsonMapper.ToObject(result));
                        string parentKey = Gs2.Gs2Exchange.Domain.Model.UserDomain.CreateCacheParentKey(
                            requestModel.NamespaceName.ToString(),
                            resultModel.Item.UserId.ToString(),
                            "Await"
                        );
                        string key = Gs2.Gs2Exchange.Domain.Model.AwaitDomain.CreateCacheKey(
                            resultModel.Item.Name.ToString(),
                            resultModel.Item.RateName.ToString()
                        );
                        cache.Delete<Gs2.Gs2Exchange.Model.Await>(parentKey, key);
                        break;
                    }
                }
        }

        public static void UpdateCacheFromJobResult(
                CacheDatabase cache,
                string method,
                Gs2.Gs2JobQueue.Model.Job job,
                Gs2.Gs2JobQueue.Model.JobResultBody result
        ) {
                switch (method) {
                    case "exchange_by_user_id": {
                        ExchangeByUserIdRequest requestModel = ExchangeByUserIdRequest.FromJson(JsonMapper.ToObject(job.Args));
                        ExchangeByUserIdResult resultModel = ExchangeByUserIdResult.FromJson(JsonMapper.ToObject(result.Result));
                        string parentKey = Gs2.Gs2Exchange.Domain.Model.UserDomain.CreateCacheParentKey(
                            requestModel.NamespaceName.ToString(),
                            requestModel.UserId.ToString(),
                            "Exchange"
                        );
                        string key = Gs2.Gs2Exchange.Domain.Model.ExchangeDomain.CreateCacheKey(
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Item,
                              UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                        break;
                    }
                    case "create_await_by_user_id": {
                        CreateAwaitByUserIdRequest requestModel = CreateAwaitByUserIdRequest.FromJson(JsonMapper.ToObject(job.Args));
                        CreateAwaitByUserIdResult resultModel = CreateAwaitByUserIdResult.FromJson(JsonMapper.ToObject(result.Result));
                        string parentKey = Gs2.Gs2Exchange.Domain.Model.UserDomain.CreateCacheParentKey(
                            requestModel.NamespaceName.ToString(),
                            resultModel.Item.UserId.ToString(),
                            "Await"
                        );
                        string key = Gs2.Gs2Exchange.Domain.Model.AwaitDomain.CreateCacheKey(
                            resultModel.Item.Name.ToString(),
                            resultModel.Item.RateName.ToString()
                        );
                        cache.Put(
                            parentKey,
                            key,
                            resultModel.Item,
                              UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                        );
                        break;
                    }
                }
        }
    }
}