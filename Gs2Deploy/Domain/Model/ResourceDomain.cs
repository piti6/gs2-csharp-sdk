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
using Gs2.Gs2Deploy.Domain.Iterator;
using Gs2.Gs2Deploy.Request;
using Gs2.Gs2Deploy.Result;
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

namespace Gs2.Gs2Deploy.Domain.Model
{

    public partial class ResourceDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2DeployRestClient _client;
        private readonly string _stackName;
        private readonly string _resourceName;

        private readonly String _parentKey;
        public string StackName => _stackName;
        public string ResourceName => _resourceName;

        public ResourceDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string stackName,
            string resourceName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2DeployRestClient(
                session
            );
            this._stackName = stackName;
            this._resourceName = resourceName;
            this._parentKey = Gs2.Gs2Deploy.Domain.Model.StackDomain.CreateCacheParentKey(
                this.StackName,
                "Resource"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Deploy.Model.Resource> GetAsync(
            #else
        private IFuture<Gs2.Gs2Deploy.Model.Resource> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Deploy.Model.Resource> GetAsync(
        #endif
            GetResourceRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Deploy.Model.Resource> self)
            {
        #endif
            request
                .WithStackName(this.StackName)
                .WithResourceName(this.ResourceName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetResourceFuture(
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
            var result = await this._client.GetResourceAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Deploy.Domain.Model.StackDomain.CreateCacheParentKey(
                        this.StackName,
                        "Resource"
                    );
                    var key = Gs2.Gs2Deploy.Domain.Model.ResourceDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Put(
                        parentKey,
                        key,
                        resultModel.Item,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
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
            return new Gs2InlineFuture<Gs2.Gs2Deploy.Model.Resource>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string stackName,
            string resourceName,
            string childType
        )
        {
            return string.Join(
                ":",
                "deploy",
                stackName ?? "null",
                resourceName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string resourceName
        )
        {
            return string.Join(
                ":",
                resourceName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Deploy.Model.Resource> Model() {
            #else
        public IFuture<Gs2.Gs2Deploy.Model.Resource> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Deploy.Model.Resource> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Deploy.Model.Resource> self)
            {
        #endif
            var (value, find) = _cache.Get<Gs2.Gs2Deploy.Model.Resource>(
                _parentKey,
                Gs2.Gs2Deploy.Domain.Model.ResourceDomain.CreateCacheKey(
                    this.ResourceName?.ToString()
                )
            );
            if (!find) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetResourceRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            var key = Gs2.Gs2Deploy.Domain.Model.ResourceDomain.CreateCacheKey(
                                    this.ResourceName?.ToString()
                                );
                            _cache.Put<Gs2.Gs2Deploy.Model.Resource>(
                                _parentKey,
                                key,
                                null,
                                UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                            );

                            if (e.errors[0].component != "resource")
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
                    var key = Gs2.Gs2Deploy.Domain.Model.ResourceDomain.CreateCacheKey(
                            this.ResourceName?.ToString()
                        );
                    _cache.Put<Gs2.Gs2Deploy.Model.Resource>(
                        _parentKey,
                        key,
                        null,
                        UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                    if (e.errors[0].component != "resource")
                    {
                        throw e;
                    }
                }
        #endif
                (value, find) = _cache.Get<Gs2.Gs2Deploy.Model.Resource>(
                    _parentKey,
                    Gs2.Gs2Deploy.Domain.Model.ResourceDomain.CreateCacheKey(
                        this.ResourceName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Deploy.Model.Resource>(Impl);
        #endif
        }

    }
}
