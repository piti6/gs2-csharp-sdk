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
using Gs2.Gs2Friend.Domain.Iterator;
using Gs2.Gs2Friend.Request;
using Gs2.Gs2Friend.Result;
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

namespace Gs2.Gs2Friend.Domain.Model
{

    public partial class UserDomain {
        private readonly CacheDatabase _cache;
        private readonly JobQueueDomain _jobQueueDomain;
        private readonly StampSheetConfiguration _stampSheetConfiguration;
        private readonly Gs2RestSession _session;
        private readonly Gs2FriendRestClient _client;
        private readonly string _namespaceName;
        private readonly string _userId;

        private readonly String _parentKey;
        public string NextPageToken { get; set; }
        public string NamespaceName => _namespaceName;
        public string UserId => _userId;

        public UserDomain(
            CacheDatabase cache,
            JobQueueDomain jobQueueDomain,
            StampSheetConfiguration stampSheetConfiguration,
            Gs2RestSession session,
            string namespaceName,
            string userId
        ) {
            this._cache = cache;
            this._jobQueueDomain = jobQueueDomain;
            this._stampSheetConfiguration = stampSheetConfiguration;
            this._session = session;
            this._client = new Gs2FriendRestClient(
                session
            );
            this._namespaceName = namespaceName;
            this._userId = userId;
            this._parentKey = Gs2.Gs2Friend.Domain.Model.NamespaceDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                "User"
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Friend.Domain.Model.FriendRequestDomain> SendRequestAsync(
            #else
        public IFuture<Gs2.Gs2Friend.Domain.Model.FriendRequestDomain> SendRequest(
            #endif
        #else
        public async Task<Gs2.Gs2Friend.Domain.Model.FriendRequestDomain> SendRequestAsync(
        #endif
            SendRequestByUserIdRequest request
        ) {

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            IEnumerator Impl(IFuture<Gs2.Gs2Friend.Domain.Model.FriendRequestDomain> self)
            {
        #endif
            request
                .WithNamespaceName(this._namespaceName)
                .WithUserId(this._userId);
            #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            var future = this._client.SendRequestByUserIdFuture(
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
            var result = await this._client.SendRequestByUserIdAsync(
                request
            );
            #endif
            string parentKey = Gs2.Gs2Friend.Domain.Model.UserDomain.CreateCacheParentKey(
                this._namespaceName != null ? this._namespaceName.ToString() : null,
                this._userId != null ? this._userId.ToString() : null,
                "FriendRequest"
            );
                    
            if (result.Item != null) {
                _cache.Put(
                    parentKey,
                    Gs2.Gs2Friend.Domain.Model.FriendRequestDomain.CreateCacheKey(
                        request.TargetUserId != null ? request.TargetUserId.ToString() : null
                    ),
                    result.Item,
                    UnixTime.ToUnixTime(DateTime.Now) + 1000 * 60 * Gs2.Core.Domain.Gs2.DefaultCacheMinutes
                );
            }
            Gs2.Gs2Friend.Domain.Model.FriendRequestDomain domain = new Gs2.Gs2Friend.Domain.Model.FriendRequestDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                request.NamespaceName,
                result?.Item?.UserId,
                result?.Item?.TargetUserId
            );

        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            self.OnComplete(domain);
            yield return null;
        #else
            return domain;
        #endif
        #if UNITY_2017_1_OR_NEWER && !GS2_ENABLE_UNITASK
            }
            return new Gs2InlineFuture<Gs2.Gs2Friend.Domain.Model.FriendRequestDomain>(Impl);
        #endif
        }

        public Gs2.Gs2Friend.Domain.Model.ProfileDomain Profile(
        ) {
            return new Gs2.Gs2Friend.Domain.Model.ProfileDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId
            );
        }

        public Gs2.Gs2Friend.Domain.Model.PublicProfileDomain PublicProfile(
        ) {
            return new Gs2.Gs2Friend.Domain.Model.PublicProfileDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<string> BlackLists(
            #else
        public Gs2Iterator<string> BlackLists(
            #endif
        #else
        public DescribeBlackListByUserIdIterator BlackLists(
        #endif
        )
        {
            return new DescribeBlackListByUserIdIterator(
                this._cache,
                this._client,
                this._namespaceName,
                this._userId
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

        public Gs2.Gs2Friend.Domain.Model.BlackListDomain BlackList(
        ) {
            return new Gs2.Gs2Friend.Domain.Model.BlackListDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Friend.Model.FollowUser> Follows(
            #else
        public Gs2Iterator<Gs2.Gs2Friend.Model.FollowUser> Follows(
            #endif
        #else
        public DescribeFollowsByUserIdIterator Follows(
        #endif
            bool? withProfile
        )
        {
            return new DescribeFollowsByUserIdIterator(
                this._cache,
                this._client,
                this._namespaceName,
                this._userId,
                withProfile
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

        public Gs2.Gs2Friend.Domain.Model.FollowDomain Follow(
            bool? withProfile
        ) {
            return new Gs2.Gs2Friend.Domain.Model.FollowDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId,
                withProfile
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Friend.Model.FriendUser> Friends(
            #else
        public Gs2Iterator<Gs2.Gs2Friend.Model.FriendUser> Friends(
            #endif
        #else
        public DescribeFriendsByUserIdIterator Friends(
        #endif
            bool? withProfile
        )
        {
            return new DescribeFriendsByUserIdIterator(
                this._cache,
                this._client,
                this._namespaceName,
                this._userId,
                withProfile
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

        public Gs2.Gs2Friend.Domain.Model.FriendDomain Friend(
            bool? withProfile
        ) {
            return new Gs2.Gs2Friend.Domain.Model.FriendDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId,
                withProfile
            );
        }

        public Gs2.Gs2Friend.Domain.Model.SendBoxDomain SendBox(
        ) {
            return new Gs2.Gs2Friend.Domain.Model.SendBoxDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId
            );
        }

        public Gs2.Gs2Friend.Domain.Model.FriendRequestDomain FriendRequest(
            string targetUserId
        ) {
            return new Gs2.Gs2Friend.Domain.Model.FriendRequestDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId,
                targetUserId
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Friend.Model.FriendRequest> SendRequests(
            #else
        public Gs2Iterator<Gs2.Gs2Friend.Model.FriendRequest> SendRequests(
            #endif
        #else
        public DescribeSendRequestsByUserIdIterator SendRequests(
        #endif
        )
        {
            return new DescribeSendRequestsByUserIdIterator(
                this._cache,
                this._client,
                this._namespaceName,
                this._userId
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

        public Gs2.Gs2Friend.Domain.Model.SendFriendRequestDomain SendFriendRequest(
            string targetUserId
        ) {
            return new Gs2.Gs2Friend.Domain.Model.SendFriendRequestDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId,
                targetUserId
            );
        }

        public Gs2.Gs2Friend.Domain.Model.InboxDomain Inbox(
        ) {
            return new Gs2.Gs2Friend.Domain.Model.InboxDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId
            );
        }

        #if UNITY_2017_1_OR_NEWER
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Gs2Friend.Model.FriendRequest> ReceiveRequests(
            #else
        public Gs2Iterator<Gs2.Gs2Friend.Model.FriendRequest> ReceiveRequests(
            #endif
        #else
        public DescribeReceiveRequestsByUserIdIterator ReceiveRequests(
        #endif
        )
        {
            return new DescribeReceiveRequestsByUserIdIterator(
                this._cache,
                this._client,
                this._namespaceName,
                this._userId
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

        public Gs2.Gs2Friend.Domain.Model.ReceiveFriendRequestDomain ReceiveFriendRequest(
            string fromUserId
        ) {
            return new Gs2.Gs2Friend.Domain.Model.ReceiveFriendRequestDomain(
                this._cache,
                this._jobQueueDomain,
                this._stampSheetConfiguration,
                this._session,
                this._namespaceName,
                this._userId,
                fromUserId
            );
        }

        public static string CreateCacheParentKey(
            string namespaceName,
            string userId,
            string childType
        )
        {
            return string.Join(
                ":",
                "friend",
                namespaceName ?? "null",
                userId ?? "null",
                childType
            );
        }

        public static string CreateCacheKey(
            string userId
        )
        {
            return string.Join(
                ":",
                userId ?? "null"
            );
        }

    }
}