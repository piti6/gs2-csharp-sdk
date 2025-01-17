
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

namespace Gs2.Gs2Matchmaking.Domain.Iterator
{

    #if UNITY_2017_1_OR_NEWER
    public class DoMatchmakingByUserIdIterator : Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> {
    #else
    public class DoMatchmakingByUserIdIterator : IAsyncEnumerable<Gs2.Gs2Matchmaking.Model.Gathering> {
    #endif
        private readonly CacheDatabase _cache;
        private readonly Gs2MatchmakingRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;
        private readonly Gs2.Gs2Matchmaking.Model.Player _player;
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;
        public Gs2.Gs2Matchmaking.Model.Player Player => _player;
        private string _matchmakingContextToken;
        private bool _isCacheChecked;
        private bool _last;
        private Gs2.Gs2Matchmaking.Model.Gathering[] _result;

        int? fetchSize;

        public DoMatchmakingByUserIdIterator(
            CacheDatabase cache,
            Gs2MatchmakingRestClient client,
            string namespaceName,
            string userId,
            Gs2.Gs2Matchmaking.Model.Player player
        ) {
            this._cache = cache;
            this._client = client;
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._player = player;
            this._matchmakingContextToken = null;
            this._last = false;
            this._result = new Gs2.Gs2Matchmaking.Model.Gathering[]{};

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

            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.DoMatchmakingByUserIdFuture(
            #else
            var r = await this._client.DoMatchmakingByUserIdAsync(
            #endif
                new Gs2.Gs2Matchmaking.Request.DoMatchmakingByUserIdRequest()
                    .WithNamespaceName(this._namespaceName)
                    .WithUserId(this._userId)
                    .WithPlayer(this._player)
                    .WithMatchmakingContextToken(this._matchmakingContextToken)
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
            this._result = new []{
                r.Item
            };
            this._matchmakingContextToken = r.MatchmakingContextToken;
            this._last = this._matchmakingContextToken == null;
            this._cache.ClearListCache<Gs2.Gs2Matchmaking.Model.Gathering>(
                Gs2.Gs2Matchmaking.Domain.Model.UserDomain.CreateCacheParentKey(
                    this.NamespaceName,
                    "Singleton",
                    "Gathering"
                )
            );
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
            Action<AsyncResult<Gs2.Gs2Matchmaking.Model.Gathering>> callback
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
                        Gs2.Gs2Matchmaking.Model.Gathering ret = this._result[0];
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
            callback.Invoke(new AsyncResult<Gs2.Gs2Matchmaking.Model.Gathering>(
                Current,
                error
            ));
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Matchmaking.Model.Gathering> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
            #else

        protected override IEnumerator Next(
            Action<AsyncResult<Gs2.Gs2Matchmaking.Model.Gathering>> callback
            #endif
        #else
        public async IAsyncEnumerator<Gs2.Gs2Matchmaking.Model.Gathering> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
        #endif
        )
        {
        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Gs2Matchmaking.Model.Gathering>(async (writer, token) =>
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
                    callback.Invoke(new AsyncResult<Gs2.Gs2Matchmaking.Model.Gathering>(
                        Current,
                        Error
                    ));
                    yield break;
        #else
                    break;
        #endif
                }
                Gs2.Gs2Matchmaking.Model.Gathering ret = this._result[0];
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
                callback.Invoke(new AsyncResult<Gs2.Gs2Matchmaking.Model.Gathering>(
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
