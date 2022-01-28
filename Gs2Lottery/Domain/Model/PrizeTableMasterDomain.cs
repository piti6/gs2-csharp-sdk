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
using Gs2.Gs2Lottery.Domain.Iterator;
using Gs2.Gs2Lottery.Request;
using Gs2.Gs2Lottery.Result;
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

namespace Gs2.Gs2Lottery.Domain.Model
{

    public partial class PrizeTableMasterDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2LotteryRestClient _client;
        private readonly string _namespaceName;
        private readonly string _prizeTableName;

        private readonly String _parentKey;
        public string NamespaceName => _namespaceName;
        public string PrizeTableName => _prizeTableName;

        public PrizeTableMasterDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string prizeTableName
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2LotteryRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._prizeTableName = prizeTableName;
            this._parentKey = Gs2.Gs2Lottery.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                "PrizeTableMaster"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask<Gs2.Gs2Lottery.Model.PrizeTableMaster> GetAsync(
            #else
        private IFuture<Gs2.Gs2Lottery.Model.PrizeTableMaster> Get(
            #endif
        #else
        private async Task<Gs2.Gs2Lottery.Model.PrizeTableMaster> GetAsync(
        #endif
            GetPrizeTableMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lottery.Model.PrizeTableMaster> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithPrizeTableName(this._prizeTableName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.GetPrizeTableMasterFuture(
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
            var result = await this._client.GetPrizeTableMasterAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                        request.PrizeTableName != null ? request.PrizeTableName.ToString() : null
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
            return new Gs2InlineFuture<Gs2.Gs2Lottery.Model.PrizeTableMaster>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> UpdateAsync(
            #else
        public IFuture<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> Update(
            #endif
        #else
        public async Task<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> UpdateAsync(
        #endif
            UpdatePrizeTableMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithPrizeTableName(this._prizeTableName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.UpdatePrizeTableMasterFuture(
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
            var result = await this._client.UpdatePrizeTableMasterAsync(
                request
            );
            #endif
                    
            if (result.Item != null) {
                _cache.Put(
                    _parentKey,
                    Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                        request.PrizeTableName != null ? request.PrizeTableName.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain>(Impl);
        #endif
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> DeleteAsync(
            #else
        public IFuture<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> Delete(
            #endif
        #else
        public async Task<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> DeleteAsync(
        #endif
            DeletePrizeTableMasterRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithPrizeTableName(this._prizeTableName);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DeletePrizeTableMasterFuture(
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
            DeletePrizeTableMasterResult result = null;
            try {
                result = await this._client.DeletePrizeTableMasterAsync(
                    request
                );
            } catch(Gs2.Core.Exception.NotFoundException) {}
            #endif
            _cache.Delete<Gs2.Gs2Lottery.Model.PrizeTableMaster>(
                _parentKey,
                Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                    request.PrizeTableName != null ? request.PrizeTableName.ToString() : null
                )
            );
            Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain domain = this;

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain>(Impl);
        #endif
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string prizeTableName,
            string childType
        )
        {
            return string.Join(
                ":",
                "lottery",
                namespaceName ?? "null",
                prizeTableName ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string prizeTableName
        )
        {
            return string.Join(
                ":",
                prizeTableName ?? "null"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Lottery.Model.PrizeTableMaster> Model() {
            #else
        public IFuture<Gs2.Gs2Lottery.Model.PrizeTableMaster> Model() {
            #endif
        #else
        public async Task<Gs2.Gs2Lottery.Model.PrizeTableMaster> Model() {
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Lottery.Model.PrizeTableMaster> self)
            {
        #endif
            Gs2.Gs2Lottery.Model.PrizeTableMaster value = _cache.Get<Gs2.Gs2Lottery.Model.PrizeTableMaster>(
                _parentKey,
                Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                    this.PrizeTableName?.ToString()
                )
            );
            if (value == null) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    var future = this.Get(
        #else
                try {
                    await this.GetAsync(
        #endif
                        new GetPrizeTableMasterRequest()
                    );
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is Gs2.Core.Exception.NotFoundException)
                        {
                            _cache.Delete<Gs2.Gs2Lottery.Model.PrizeTableMaster>(
                            _parentKey,
                            Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                                this.PrizeTableName?.ToString()
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
                    _cache.Delete<Gs2.Gs2Lottery.Model.PrizeTableMaster>(
                        _parentKey,
                        Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                            this.PrizeTableName?.ToString()
                        )
                    );
                }
        #endif
                value = _cache.Get<Gs2.Gs2Lottery.Model.PrizeTableMaster>(
                _parentKey,
                Gs2.Gs2Lottery.Domain.Model.PrizeTableMasterDomain.CreateCacheKey(
                    this.PrizeTableName?.ToString()
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
            return new Gs2InlineFuture<Gs2.Gs2Lottery.Model.PrizeTableMaster>(Impl);
        #endif
        }

    }
}