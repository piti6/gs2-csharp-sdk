
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
using Gs2.Core.Domain;
using Gs2.Core.Util;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
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
        #if GS2_ENABLE_UNITASK
    public class CountIssueStampSheetLogIterator {
        #else
    public class CountIssueStampSheetLogIterator : Gs2Iterator<Gs2.Gs2Log.Model.IssueStampSheetLogCount> {
        #endif
    #else
    public class CountIssueStampSheetLogIterator : IAsyncEnumerable<Gs2.Gs2Log.Model.IssueStampSheetLogCount> {
    #endif
        private readonly CacheDatabase _cache;
        private readonly Gs2LogRestClient _client;
        private readonly string _namespaceName;
        private readonly bool? _service;
        private readonly bool? _method;
        private readonly bool? _userId;
        private readonly bool? _action;
        private readonly long? _begin;
        private readonly long? _end;
        private readonly bool? _longTerm;
        private string _pageToken;
        private bool _last;
        private Gs2.Gs2Log.Model.IssueStampSheetLogCount[] _result;

        int? fetchSize;

        public CountIssueStampSheetLogIterator(
            CacheDatabase cache,
            Gs2LogRestClient client,
            string namespaceName,
            bool? service,
            bool? method,
            bool? userId,
            bool? action,
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
            this._action = action;
            this._begin = begin;
            this._end = end;
            this._longTerm = longTerm;
            this._pageToken = null;
            this._last = false;
            this._result = new Gs2.Gs2Log.Model.IssueStampSheetLogCount[]{};

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

            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.CountIssueStampSheetLogFuture(
            #else
            var r = await this._client.CountIssueStampSheetLogAsync(
            #endif
                new Gs2.Gs2Log.Request.CountIssueStampSheetLogRequest()
                    .WithNamespaceName(this._namespaceName)
                    .WithService(this._service)
                    .WithMethod(this._method)
                    .WithUserId(this._userId)
                    .WithAction(this._action)
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
        }

        private bool _hasNext()
        {
            return this._result.Length != 0 || !this._last;
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Log.Model.IssueStampSheetLogCount> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
            #else

        public override bool HasNext()
        {
            if (Error != null) return false;
            return _hasNext();
        }

        protected override IEnumerator Next(
            Action<Gs2.Gs2Log.Model.IssueStampSheetLogCount> callback
            #endif
        #else
        public async IAsyncEnumerator<Gs2.Gs2Log.Model.IssueStampSheetLogCount> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
        #endif
        )
        {
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Gs2Log.Model.IssueStampSheetLogCount>(async (writer, token) =>
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
                    yield break;
        #else
                    break;
        #endif
                }
                Gs2.Gs2Log.Model.IssueStampSheetLogCount ret = this._result[0];
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
