
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
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Util;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
    #if GS2_ENABLE_UNITASK
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
    #else
using System.Collections;
using UnityEngine.Events;
using Gs2.Core;
using Gs2.Core.Exception;
    #endif
#else
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Gs2Log.Domain.Iterator
{

    #if UNITY_2017_1_OR_NEWER
    public class QueryAccessLogIterator : Gs2Iterator<Gs2.Gs2Log.Model.AccessLog> {
    #else
    public class QueryAccessLogIterator : IAsyncEnumerable<Gs2.Gs2Log.Model.AccessLog> {
    #endif
        private readonly CacheDatabase _cache;
        private readonly Gs2LogRestClient _client;
        private readonly string _namespaceName;
        private readonly string _service;
        private readonly string _method;
        private readonly string _userId;
        private readonly long? _begin;
        private readonly long? _end;
        private readonly bool? _longTerm;
        public string NamespaceName => _namespaceName;
        public string Service => _service;
        public string Method => _method;
        public string UserId => _userId;
        public long? Begin => _begin;
        public long? End => _end;
        public bool? LongTerm => _longTerm;
        private string _pageToken;
        private bool _isCacheChecked;
        private bool _last;
        private Gs2.Gs2Log.Model.AccessLog[] _result;

        int? fetchSize;

        public QueryAccessLogIterator(
            CacheDatabase cache,
            Gs2LogRestClient client,
            string namespaceName,
            string service,
            string method,
            string userId,
            long? begin,
            long? end,
            bool? longTerm
        ) {
            this._cache = cache;
            this._client = client;
            this._namespaceName = namespaceName;
            this._service = service;
            this._method = method;
            this._userId = userId;
            this._begin = begin;
            this._end = end;
            this._longTerm = longTerm;
            this._pageToken = null;
            this._last = false;
            this._result = new Gs2.Gs2Log.Model.AccessLog[]{};

            this.fetchSize = null;
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        private async UniTask _load() {
            #else
        private IEnumerator _load() {
            #endif
        #else
        private async Task _load() {
        #endif
            var isCacheChecked = this._isCacheChecked;
            this._isCacheChecked = true;
            var parentKey = Gs2.Gs2Log.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this.NamespaceName,
                "AccessLog"
            );
            if (!isCacheChecked && this._cache.TryGetList<Gs2.Gs2Log.Model.AccessLog>
            (
                    parentKey,
                    out var list
            )) {
                this._result = list
                    .Where(item => this._service == null || item.Service == this._service)
                    .Where(item => this._method == null || item.Method == this._method)
                    .Where(item => this._userId == null || item.UserId == this._userId)
                    .Where(item => this._begin == null || item.Timestamp >= this._begin)
                    .Where(item => this._end == null || item.Timestamp <= this._end)
                    .ToArray();
                this._pageToken = null;
                this._last = true;
            } else {

                #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                var future = this._client.QueryAccessLogFuture(
                #else
                var r = await this._client.QueryAccessLogAsync(
                #endif
                    new Gs2.Gs2Log.Request.QueryAccessLogRequest()
                        .WithNamespaceName(this._namespaceName)
                        .WithService(this._service)
                        .WithMethod(this._method)
                        .WithUserId(this._userId)
                        .WithBegin(this._begin)
                        .WithEnd(this._end)
                        .WithLongTerm(this._longTerm)
                        .WithPageToken(this._pageToken)
                        .WithLimit(this.fetchSize)
                );
                #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                yield return future;
                if (future.Error != null)
                {
                    Error = future.Error;
                    yield break;
                }
                var r = future.Result;
                #endif
                this._result = r.Items;
                this._pageToken = r.NextPageToken;
                this._last = this._pageToken == null;
                foreach (var item in this._result) {
                    this._cache.Put(
                            parentKey,
                            Gs2.Gs2Log.Domain.Model.AccessLogDomain.CreateCacheKey(
                            ),
                            item,
                            UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                    );
                }

                if (this._last) {
                    this._cache.SetListCached<Gs2.Gs2Log.Model.AccessLog>(
                            parentKey
                    );
                }
            }
        }

        private bool _hasNext()
        {
            return this._result.Length != 0 || !this._last;
        }

        #if UNITY_2017_1_OR_NEWER
        public override bool HasNext()
        {
            if (Error != null) return false;
            return _hasNext();
        }
        #endif

        #if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK

        protected override System.Collections.IEnumerator Next(
            Action<AsyncResult<Gs2.Gs2Log.Model.AccessLog>> callback
        )
        {
            Gs2Exception error = null;
            yield return UniTask.ToCoroutine(
                async () => {
                    try {
                        if (this._result.Length == 0 && !this._last) {
                            await this._load();
                        }
                        if (this._result.Length == 0) {
                            Current = null;
                            return;
                        }
                        Gs2.Gs2Log.Model.AccessLog ret = this._result[0];
                        this._result = this._result.ToList().GetRange(1, this._result.Length - 1).ToArray();
                        if (this._result.Length == 0 && !this._last) {
                            await this._load();
                        }
                        Current = ret;
                    }
                    catch (Gs2Exception e) {
                        Current = null;
                        error = e;
                    }
                }
            );
            callback.Invoke(new AsyncResult<Gs2.Gs2Log.Model.AccessLog>(
                Current,
                error
            ));
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Log.Model.AccessLog> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
            #else

        protected override IEnumerator Next(
            Action<AsyncResult<Gs2.Gs2Log.Model.AccessLog>> callback
            #endif
        #else
        public async IAsyncEnumerator<Gs2.Gs2Log.Model.AccessLog> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
        #endif
        )
        {
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Gs2Log.Model.AccessLog>(async (writer, token) =>
            {
            #endif
        #endif
        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            while(this._hasNext()) {
        #endif
                if (this._result.Length == 0 && !this._last) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return this._load();
        #else
                    await this._load();
        #endif
                }
                if (this._result.Length == 0) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    Current = null;
                    callback.Invoke(new AsyncResult<Gs2.Gs2Log.Model.AccessLog>(
                        Current,
                        Error
                    ));
                    yield break;
        #else
                    break;
        #endif
                }
                Gs2.Gs2Log.Model.AccessLog ret = this._result[0];
                this._result = this._result.ToList().GetRange(1, this._result.Length - 1).ToArray();
                if (this._result.Length == 0 && !this._last) {
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
                    yield return this._load();
        #else
                    await this._load();
        #endif
                }
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
                await writer.YieldAsync(ret);
            #else
                Current = ret;
                callback.Invoke(new AsyncResult<Gs2.Gs2Log.Model.AccessLog>(
                    Current,
                    Error
                ));
            #endif
        #else
                yield return ret;
        #endif
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            }
            });
            #endif
        #else
            }
        #endif
        }
    }
}
