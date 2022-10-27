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
using Gs2.Gs2Formation.Domain.Iterator;
using Gs2.Gs2Formation.Request;
using Gs2.Gs2Formation.Result;
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

namespace Gs2.Gs2Formation.Domain.Model
{

    public partial class FormModelMasterDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2FormationRestClient _client;
        private readonly string _namespaceName;
        private readonly string _formModelName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string FormModelName => _formModelName;

        public FormModelMasterDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string formModelName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2FormationRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._formModelName = formModelName;
            this._parentKey = Gs2.Gs2Formation.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this.NamespaceName,
                "FormModelMaster"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Formation.Model.FormModelMaster> GetAsync(
            #else
        private IFuture<Gs2.Gs2Formation.Model.FormModelMaster> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Formation.Model.FormModelMaster> GetAsync(
        #endif
            GetFormModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Formation.Model.FormModelMaster> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithFormModelName(this.FormModelName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetFormModelMasterFuture(
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
            var result = await this._client.GetFormModelMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Formation.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "FormModelMaster"
                    );
                    var key = Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
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
            return new Gs2InlineFuture<Gs2.Gs2Formation.Model.FormModelMaster>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> UpdateAsync(
        #endif
            UpdateFormModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithFormModelName(this.FormModelName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdateFormModelMasterFuture(
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
            var result = await this._client.UpdateFormModelMasterAsync(
                request
            );
            #endif
            var requestModel = request;
            var resultModel = result;
            var cache = _cache;
            if (resultModel != null) {
                
                if (resultModel.Item != null) {
                    var parentKey = Gs2.Gs2Formation.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "FormModelMaster"
                    );
                    var key = Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
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
            Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> DeleteAsync(
        #endif
            DeleteFormModelMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this.NamespaceName)
                .WithFormModelName(this.FormModelName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeleteFormModelMasterFuture(
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
            DeleteFormModelMasterResult result = null;
            try {
                result = await this._client.DeleteFormModelMasterAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException e) {
                if (e.errors[0].component == "formModelMaster")
                {
                    var parentKey = Gs2.Gs2Formation.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                    this.NamespaceName,
                    "FormModelMaster"
                );
                    var key = Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
                        request.FormModelName.ToString()
                    );
                    _cache.Delete<Gs2.Gs2Formation.Model.FormModelMaster>(parentKey, key);
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
                    var parentKey = Gs2.Gs2Formation.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                        this.NamespaceName,
                        "FormModelMaster"
                    );
                    var key = Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
                        resultModel.Item.Name.ToString()
                    );
                    cache.Delete<Gs2.Gs2Formation.Model.FormModelMaster>(parentKey, key);
                }
            }
            Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string formModelName,
            string childType
        )
        {
            return string.Join(
                ":",
                "formation",
                namespaceName ?? "null",
                formModelName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string formModelName
        )
        {
            return string.Join(
                ":",
                formModelName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Formation.Model.FormModelMaster> Model() {
            #else
        public IFuture<Gs2.Gs2Formation.Model.FormModelMaster> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Formation.Model.FormModelMaster> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Formation.Model.FormModelMaster> self)
            {
        #endif
            Gs2.Gs2Formation.Model.FormModelMaster value = _cache.Get<Gs2.Gs2Formation.Model.FormModelMaster>(
                _parentKey,
                Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
                    this.FormModelName?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetFormModelMasterRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException e)
                        {
                            if (e.errors[0].component == "formModelMaster")
                            {
                                _cache.Delete<Gs2.Gs2Formation.Model.FormModelMaster>(
                                    _parentKey,
                                    Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
                                        this.FormModelName?.ToString()
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
                    if (e.errors[0].component == "formModelMaster")
                    {
                        _cache.Delete<Gs2.Gs2Formation.Model.FormModelMaster>(
                            _parentKey,
                            Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
                                this.FormModelName?.ToString()
                            )
                        );
                    }
                    else
                    {
                        throw e;
                    }
                }
        #endif
                value = _cache.Get<Gs2.Gs2Formation.Model.FormModelMaster>(
                    _parentKey,
                    Gs2.Gs2Formation.Domain.Model.FormModelMasterDomain.CreateCacheKey(
                        this.FormModelName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Formation.Model.FormModelMaster>(Impl);
        #endif
        }

    }
}
