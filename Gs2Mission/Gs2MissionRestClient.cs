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
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Events;
using UnityEngine.Networking;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
    #endif
#else
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Mission.Request;
using Gs2.Gs2Mission.Result;
using Gs2.Util.LitJson;

namespace Gs2.Gs2Mission
{
	public class Gs2MissionRestClient : AbstractGs2Client
	{
#if UNITY_2017_1_OR_NEWER
		private readonly CertificateHandler _certificateHandler;
#endif

		public static string Endpoint = "mission";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		public Gs2MissionRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

#if UNITY_2017_1_OR_NEWER
		public Gs2MissionRestClient(Gs2RestSession gs2RestSession, CertificateHandler certificateHandler) : base(gs2RestSession)
		{
			_certificateHandler = certificateHandler;
		}
#endif


        public class DescribeCompletesTask : Gs2RestSessionTask<DescribeCompletesRequest, DescribeCompletesResult>
        {
            public DescribeCompletesTask(IGs2Session session, RestSessionRequestFactory factory, DescribeCompletesRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeCompletesRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/complete";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    sessionRequest.AddHeader("X-GS2-ACCESS-TOKEN", request.AccessToken);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeCompletes(
                Request.DescribeCompletesRequest request,
                UnityAction<AsyncResult<Result.DescribeCompletesResult>> callback
        )
		{
			var task = new DescribeCompletesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeCompletesResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeCompletesResult> DescribeCompletesFuture(
                Request.DescribeCompletesRequest request
        )
		{
			return new DescribeCompletesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeCompletesResult> DescribeCompletesAsync(
                Request.DescribeCompletesRequest request
        )
		{
            AsyncResult<Result.DescribeCompletesResult> result = null;
			await DescribeCompletes(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeCompletesTask DescribeCompletesAsync(
                Request.DescribeCompletesRequest request
        )
		{
			return new DescribeCompletesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeCompletesResult> DescribeCompletesAsync(
                Request.DescribeCompletesRequest request
        )
		{
			var task = new DescribeCompletesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeCompletesByUserIdTask : Gs2RestSessionTask<DescribeCompletesByUserIdRequest, DescribeCompletesByUserIdResult>
        {
            public DescribeCompletesByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, DescribeCompletesByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeCompletesByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeCompletesByUserId(
                Request.DescribeCompletesByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeCompletesByUserIdResult>> callback
        )
		{
			var task = new DescribeCompletesByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeCompletesByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeCompletesByUserIdResult> DescribeCompletesByUserIdFuture(
                Request.DescribeCompletesByUserIdRequest request
        )
		{
			return new DescribeCompletesByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeCompletesByUserIdResult> DescribeCompletesByUserIdAsync(
                Request.DescribeCompletesByUserIdRequest request
        )
		{
            AsyncResult<Result.DescribeCompletesByUserIdResult> result = null;
			await DescribeCompletesByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeCompletesByUserIdTask DescribeCompletesByUserIdAsync(
                Request.DescribeCompletesByUserIdRequest request
        )
		{
			return new DescribeCompletesByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeCompletesByUserIdResult> DescribeCompletesByUserIdAsync(
                Request.DescribeCompletesByUserIdRequest request
        )
		{
			var task = new DescribeCompletesByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class CompleteTask : Gs2RestSessionTask<CompleteRequest, CompleteResult>
        {
            public CompleteTask(IGs2Session session, RestSessionRequestFactory factory, CompleteRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(CompleteRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/complete/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.Config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    sessionRequest.AddHeader("X-GS2-ACCESS-TOKEN", request.AccessToken);
                }
                if (request.DuplicationAvoider != null)
                {
                    sessionRequest.AddHeader("X-GS2-DUPLICATION-AVOIDER", request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator Complete(
                Request.CompleteRequest request,
                UnityAction<AsyncResult<Result.CompleteResult>> callback
        )
		{
			var task = new CompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.CompleteResult>(task.Result, task.Error));
        }

		public IFuture<Result.CompleteResult> CompleteFuture(
                Request.CompleteRequest request
        )
		{
			return new CompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CompleteResult> CompleteAsync(
                Request.CompleteRequest request
        )
		{
            AsyncResult<Result.CompleteResult> result = null;
			await Complete(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public CompleteTask CompleteAsync(
                Request.CompleteRequest request
        )
		{
			return new CompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.CompleteResult> CompleteAsync(
                Request.CompleteRequest request
        )
		{
			var task = new CompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class CompleteByUserIdTask : Gs2RestSessionTask<CompleteByUserIdRequest, CompleteByUserIdResult>
        {
            public CompleteByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, CompleteByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(CompleteByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.Config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    sessionRequest.AddHeader("X-GS2-DUPLICATION-AVOIDER", request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CompleteByUserId(
                Request.CompleteByUserIdRequest request,
                UnityAction<AsyncResult<Result.CompleteByUserIdResult>> callback
        )
		{
			var task = new CompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.CompleteByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.CompleteByUserIdResult> CompleteByUserIdFuture(
                Request.CompleteByUserIdRequest request
        )
		{
			return new CompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CompleteByUserIdResult> CompleteByUserIdAsync(
                Request.CompleteByUserIdRequest request
        )
		{
            AsyncResult<Result.CompleteByUserIdResult> result = null;
			await CompleteByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public CompleteByUserIdTask CompleteByUserIdAsync(
                Request.CompleteByUserIdRequest request
        )
		{
			return new CompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.CompleteByUserIdResult> CompleteByUserIdAsync(
                Request.CompleteByUserIdRequest request
        )
		{
			var task = new CompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class ReceiveByUserIdTask : Gs2RestSessionTask<ReceiveByUserIdRequest, ReceiveByUserIdResult>
        {
            public ReceiveByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, ReceiveByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(ReceiveByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}/task/{missionTaskName}/receive";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    sessionRequest.AddHeader("X-GS2-DUPLICATION-AVOIDER", request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator ReceiveByUserId(
                Request.ReceiveByUserIdRequest request,
                UnityAction<AsyncResult<Result.ReceiveByUserIdResult>> callback
        )
		{
			var task = new ReceiveByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.ReceiveByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.ReceiveByUserIdResult> ReceiveByUserIdFuture(
                Request.ReceiveByUserIdRequest request
        )
		{
			return new ReceiveByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.ReceiveByUserIdResult> ReceiveByUserIdAsync(
                Request.ReceiveByUserIdRequest request
        )
		{
            AsyncResult<Result.ReceiveByUserIdResult> result = null;
			await ReceiveByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public ReceiveByUserIdTask ReceiveByUserIdAsync(
                Request.ReceiveByUserIdRequest request
        )
		{
			return new ReceiveByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.ReceiveByUserIdResult> ReceiveByUserIdAsync(
                Request.ReceiveByUserIdRequest request
        )
		{
			var task = new ReceiveByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCompleteTask : Gs2RestSessionTask<GetCompleteRequest, GetCompleteResult>
        {
            public GetCompleteTask(IGs2Session session, RestSessionRequestFactory factory, GetCompleteRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCompleteRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/complete/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    sessionRequest.AddHeader("X-GS2-ACCESS-TOKEN", request.AccessToken);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetComplete(
                Request.GetCompleteRequest request,
                UnityAction<AsyncResult<Result.GetCompleteResult>> callback
        )
		{
			var task = new GetCompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCompleteResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCompleteResult> GetCompleteFuture(
                Request.GetCompleteRequest request
        )
		{
			return new GetCompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCompleteResult> GetCompleteAsync(
                Request.GetCompleteRequest request
        )
		{
            AsyncResult<Result.GetCompleteResult> result = null;
			await GetComplete(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCompleteTask GetCompleteAsync(
                Request.GetCompleteRequest request
        )
		{
			return new GetCompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCompleteResult> GetCompleteAsync(
                Request.GetCompleteRequest request
        )
		{
			var task = new GetCompleteTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCompleteByUserIdTask : Gs2RestSessionTask<GetCompleteByUserIdRequest, GetCompleteByUserIdResult>
        {
            public GetCompleteByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, GetCompleteByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCompleteByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetCompleteByUserId(
                Request.GetCompleteByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetCompleteByUserIdResult>> callback
        )
		{
			var task = new GetCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCompleteByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCompleteByUserIdResult> GetCompleteByUserIdFuture(
                Request.GetCompleteByUserIdRequest request
        )
		{
			return new GetCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCompleteByUserIdResult> GetCompleteByUserIdAsync(
                Request.GetCompleteByUserIdRequest request
        )
		{
            AsyncResult<Result.GetCompleteByUserIdResult> result = null;
			await GetCompleteByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCompleteByUserIdTask GetCompleteByUserIdAsync(
                Request.GetCompleteByUserIdRequest request
        )
		{
			return new GetCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCompleteByUserIdResult> GetCompleteByUserIdAsync(
                Request.GetCompleteByUserIdRequest request
        )
		{
			var task = new GetCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteCompleteByUserIdTask : Gs2RestSessionTask<DeleteCompleteByUserIdRequest, DeleteCompleteByUserIdResult>
        {
            public DeleteCompleteByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, DeleteCompleteByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DeleteCompleteByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Delete(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    sessionRequest.AddHeader("X-GS2-DUPLICATION-AVOIDER", request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteCompleteByUserId(
                Request.DeleteCompleteByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteCompleteByUserIdResult>> callback
        )
		{
			var task = new DeleteCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteCompleteByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteCompleteByUserIdResult> DeleteCompleteByUserIdFuture(
                Request.DeleteCompleteByUserIdRequest request
        )
		{
			return new DeleteCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteCompleteByUserIdResult> DeleteCompleteByUserIdAsync(
                Request.DeleteCompleteByUserIdRequest request
        )
		{
            AsyncResult<Result.DeleteCompleteByUserIdResult> result = null;
			await DeleteCompleteByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DeleteCompleteByUserIdTask DeleteCompleteByUserIdAsync(
                Request.DeleteCompleteByUserIdRequest request
        )
		{
			return new DeleteCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteCompleteByUserIdResult> DeleteCompleteByUserIdAsync(
                Request.DeleteCompleteByUserIdRequest request
        )
		{
			var task = new DeleteCompleteByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class ReceiveByStampTaskTask : Gs2RestSessionTask<ReceiveByStampTaskRequest, ReceiveByStampTaskResult>
        {
            public ReceiveByStampTaskTask(IGs2Session session, RestSessionRequestFactory factory, ReceiveByStampTaskRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(ReceiveByStampTaskRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/stamp/receive";

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.StampTask != null)
                {
                    jsonWriter.WritePropertyName("stampTask");
                    jsonWriter.Write(request.StampTask);
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator ReceiveByStampTask(
                Request.ReceiveByStampTaskRequest request,
                UnityAction<AsyncResult<Result.ReceiveByStampTaskResult>> callback
        )
		{
			var task = new ReceiveByStampTaskTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.ReceiveByStampTaskResult>(task.Result, task.Error));
        }

		public IFuture<Result.ReceiveByStampTaskResult> ReceiveByStampTaskFuture(
                Request.ReceiveByStampTaskRequest request
        )
		{
			return new ReceiveByStampTaskTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.ReceiveByStampTaskResult> ReceiveByStampTaskAsync(
                Request.ReceiveByStampTaskRequest request
        )
		{
            AsyncResult<Result.ReceiveByStampTaskResult> result = null;
			await ReceiveByStampTask(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public ReceiveByStampTaskTask ReceiveByStampTaskAsync(
                Request.ReceiveByStampTaskRequest request
        )
		{
			return new ReceiveByStampTaskTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.ReceiveByStampTaskResult> ReceiveByStampTaskAsync(
                Request.ReceiveByStampTaskRequest request
        )
		{
			var task = new ReceiveByStampTaskTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeCounterModelMastersTask : Gs2RestSessionTask<DescribeCounterModelMastersRequest, DescribeCounterModelMastersResult>
        {
            public DescribeCounterModelMastersTask(IGs2Session session, RestSessionRequestFactory factory, DescribeCounterModelMastersRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeCounterModelMastersRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeCounterModelMasters(
                Request.DescribeCounterModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeCounterModelMastersResult>> callback
        )
		{
			var task = new DescribeCounterModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeCounterModelMastersResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeCounterModelMastersResult> DescribeCounterModelMastersFuture(
                Request.DescribeCounterModelMastersRequest request
        )
		{
			return new DescribeCounterModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeCounterModelMastersResult> DescribeCounterModelMastersAsync(
                Request.DescribeCounterModelMastersRequest request
        )
		{
            AsyncResult<Result.DescribeCounterModelMastersResult> result = null;
			await DescribeCounterModelMasters(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeCounterModelMastersTask DescribeCounterModelMastersAsync(
                Request.DescribeCounterModelMastersRequest request
        )
		{
			return new DescribeCounterModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeCounterModelMastersResult> DescribeCounterModelMastersAsync(
                Request.DescribeCounterModelMastersRequest request
        )
		{
			var task = new DescribeCounterModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class CreateCounterModelMasterTask : Gs2RestSessionTask<CreateCounterModelMasterRequest, CreateCounterModelMasterResult>
        {
            public CreateCounterModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, CreateCounterModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(CreateCounterModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(request.Name);
                }
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.Scopes != null)
                {
                    jsonWriter.WritePropertyName("scopes");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.Scopes)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ChallengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(request.ChallengePeriodEventId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateCounterModelMaster(
                Request.CreateCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateCounterModelMasterResult>> callback
        )
		{
			var task = new CreateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateCounterModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateCounterModelMasterResult> CreateCounterModelMasterFuture(
                Request.CreateCounterModelMasterRequest request
        )
		{
			return new CreateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateCounterModelMasterResult> CreateCounterModelMasterAsync(
                Request.CreateCounterModelMasterRequest request
        )
		{
            AsyncResult<Result.CreateCounterModelMasterResult> result = null;
			await CreateCounterModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public CreateCounterModelMasterTask CreateCounterModelMasterAsync(
                Request.CreateCounterModelMasterRequest request
        )
		{
			return new CreateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateCounterModelMasterResult> CreateCounterModelMasterAsync(
                Request.CreateCounterModelMasterRequest request
        )
		{
			var task = new CreateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCounterModelMasterTask : Gs2RestSessionTask<GetCounterModelMasterRequest, GetCounterModelMasterResult>
        {
            public GetCounterModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, GetCounterModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCounterModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetCounterModelMaster(
                Request.GetCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetCounterModelMasterResult>> callback
        )
		{
			var task = new GetCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCounterModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCounterModelMasterResult> GetCounterModelMasterFuture(
                Request.GetCounterModelMasterRequest request
        )
		{
			return new GetCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCounterModelMasterResult> GetCounterModelMasterAsync(
                Request.GetCounterModelMasterRequest request
        )
		{
            AsyncResult<Result.GetCounterModelMasterResult> result = null;
			await GetCounterModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCounterModelMasterTask GetCounterModelMasterAsync(
                Request.GetCounterModelMasterRequest request
        )
		{
			return new GetCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCounterModelMasterResult> GetCounterModelMasterAsync(
                Request.GetCounterModelMasterRequest request
        )
		{
			var task = new GetCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateCounterModelMasterTask : Gs2RestSessionTask<UpdateCounterModelMasterRequest, UpdateCounterModelMasterResult>
        {
            public UpdateCounterModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, UpdateCounterModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(UpdateCounterModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");

                var sessionRequest = Factory.Put(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.Scopes != null)
                {
                    jsonWriter.WritePropertyName("scopes");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.Scopes)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ChallengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(request.ChallengePeriodEventId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateCounterModelMaster(
                Request.UpdateCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCounterModelMasterResult>> callback
        )
		{
			var task = new UpdateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateCounterModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateCounterModelMasterResult> UpdateCounterModelMasterFuture(
                Request.UpdateCounterModelMasterRequest request
        )
		{
			return new UpdateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateCounterModelMasterResult> UpdateCounterModelMasterAsync(
                Request.UpdateCounterModelMasterRequest request
        )
		{
            AsyncResult<Result.UpdateCounterModelMasterResult> result = null;
			await UpdateCounterModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public UpdateCounterModelMasterTask UpdateCounterModelMasterAsync(
                Request.UpdateCounterModelMasterRequest request
        )
		{
			return new UpdateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateCounterModelMasterResult> UpdateCounterModelMasterAsync(
                Request.UpdateCounterModelMasterRequest request
        )
		{
			var task = new UpdateCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteCounterModelMasterTask : Gs2RestSessionTask<DeleteCounterModelMasterRequest, DeleteCounterModelMasterResult>
        {
            public DeleteCounterModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, DeleteCounterModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DeleteCounterModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");

                var sessionRequest = Factory.Delete(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteCounterModelMaster(
                Request.DeleteCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteCounterModelMasterResult>> callback
        )
		{
			var task = new DeleteCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteCounterModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteCounterModelMasterResult> DeleteCounterModelMasterFuture(
                Request.DeleteCounterModelMasterRequest request
        )
		{
			return new DeleteCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteCounterModelMasterResult> DeleteCounterModelMasterAsync(
                Request.DeleteCounterModelMasterRequest request
        )
		{
            AsyncResult<Result.DeleteCounterModelMasterResult> result = null;
			await DeleteCounterModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DeleteCounterModelMasterTask DeleteCounterModelMasterAsync(
                Request.DeleteCounterModelMasterRequest request
        )
		{
			return new DeleteCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteCounterModelMasterResult> DeleteCounterModelMasterAsync(
                Request.DeleteCounterModelMasterRequest request
        )
		{
			var task = new DeleteCounterModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeMissionGroupModelMastersTask : Gs2RestSessionTask<DescribeMissionGroupModelMastersRequest, DescribeMissionGroupModelMastersResult>
        {
            public DescribeMissionGroupModelMastersTask(IGs2Session session, RestSessionRequestFactory factory, DescribeMissionGroupModelMastersRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeMissionGroupModelMastersRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeMissionGroupModelMasters(
                Request.DescribeMissionGroupModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionGroupModelMastersResult>> callback
        )
		{
			var task = new DescribeMissionGroupModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeMissionGroupModelMastersResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeMissionGroupModelMastersResult> DescribeMissionGroupModelMastersFuture(
                Request.DescribeMissionGroupModelMastersRequest request
        )
		{
			return new DescribeMissionGroupModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeMissionGroupModelMastersResult> DescribeMissionGroupModelMastersAsync(
                Request.DescribeMissionGroupModelMastersRequest request
        )
		{
            AsyncResult<Result.DescribeMissionGroupModelMastersResult> result = null;
			await DescribeMissionGroupModelMasters(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeMissionGroupModelMastersTask DescribeMissionGroupModelMastersAsync(
                Request.DescribeMissionGroupModelMastersRequest request
        )
		{
			return new DescribeMissionGroupModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeMissionGroupModelMastersResult> DescribeMissionGroupModelMastersAsync(
                Request.DescribeMissionGroupModelMastersRequest request
        )
		{
			var task = new DescribeMissionGroupModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class CreateMissionGroupModelMasterTask : Gs2RestSessionTask<CreateMissionGroupModelMasterRequest, CreateMissionGroupModelMasterResult>
        {
            public CreateMissionGroupModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, CreateMissionGroupModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(CreateMissionGroupModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(request.Name);
                }
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.ResetType != null)
                {
                    jsonWriter.WritePropertyName("resetType");
                    jsonWriter.Write(request.ResetType);
                }
                if (request.ResetDayOfMonth != null)
                {
                    jsonWriter.WritePropertyName("resetDayOfMonth");
                    jsonWriter.Write(request.ResetDayOfMonth.ToString());
                }
                if (request.ResetDayOfWeek != null)
                {
                    jsonWriter.WritePropertyName("resetDayOfWeek");
                    jsonWriter.Write(request.ResetDayOfWeek);
                }
                if (request.ResetHour != null)
                {
                    jsonWriter.WritePropertyName("resetHour");
                    jsonWriter.Write(request.ResetHour.ToString());
                }
                if (request.CompleteNotificationNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("completeNotificationNamespaceId");
                    jsonWriter.Write(request.CompleteNotificationNamespaceId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateMissionGroupModelMaster(
                Request.CreateMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateMissionGroupModelMasterResult>> callback
        )
		{
			var task = new CreateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateMissionGroupModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateMissionGroupModelMasterResult> CreateMissionGroupModelMasterFuture(
                Request.CreateMissionGroupModelMasterRequest request
        )
		{
			return new CreateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateMissionGroupModelMasterResult> CreateMissionGroupModelMasterAsync(
                Request.CreateMissionGroupModelMasterRequest request
        )
		{
            AsyncResult<Result.CreateMissionGroupModelMasterResult> result = null;
			await CreateMissionGroupModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public CreateMissionGroupModelMasterTask CreateMissionGroupModelMasterAsync(
                Request.CreateMissionGroupModelMasterRequest request
        )
		{
			return new CreateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateMissionGroupModelMasterResult> CreateMissionGroupModelMasterAsync(
                Request.CreateMissionGroupModelMasterRequest request
        )
		{
			var task = new CreateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetMissionGroupModelMasterTask : Gs2RestSessionTask<GetMissionGroupModelMasterRequest, GetMissionGroupModelMasterResult>
        {
            public GetMissionGroupModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, GetMissionGroupModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetMissionGroupModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetMissionGroupModelMaster(
                Request.GetMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetMissionGroupModelMasterResult>> callback
        )
		{
			var task = new GetMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetMissionGroupModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetMissionGroupModelMasterResult> GetMissionGroupModelMasterFuture(
                Request.GetMissionGroupModelMasterRequest request
        )
		{
			return new GetMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetMissionGroupModelMasterResult> GetMissionGroupModelMasterAsync(
                Request.GetMissionGroupModelMasterRequest request
        )
		{
            AsyncResult<Result.GetMissionGroupModelMasterResult> result = null;
			await GetMissionGroupModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetMissionGroupModelMasterTask GetMissionGroupModelMasterAsync(
                Request.GetMissionGroupModelMasterRequest request
        )
		{
			return new GetMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetMissionGroupModelMasterResult> GetMissionGroupModelMasterAsync(
                Request.GetMissionGroupModelMasterRequest request
        )
		{
			var task = new GetMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateMissionGroupModelMasterTask : Gs2RestSessionTask<UpdateMissionGroupModelMasterRequest, UpdateMissionGroupModelMasterResult>
        {
            public UpdateMissionGroupModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, UpdateMissionGroupModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(UpdateMissionGroupModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Put(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.ResetType != null)
                {
                    jsonWriter.WritePropertyName("resetType");
                    jsonWriter.Write(request.ResetType);
                }
                if (request.ResetDayOfMonth != null)
                {
                    jsonWriter.WritePropertyName("resetDayOfMonth");
                    jsonWriter.Write(request.ResetDayOfMonth.ToString());
                }
                if (request.ResetDayOfWeek != null)
                {
                    jsonWriter.WritePropertyName("resetDayOfWeek");
                    jsonWriter.Write(request.ResetDayOfWeek);
                }
                if (request.ResetHour != null)
                {
                    jsonWriter.WritePropertyName("resetHour");
                    jsonWriter.Write(request.ResetHour.ToString());
                }
                if (request.CompleteNotificationNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("completeNotificationNamespaceId");
                    jsonWriter.Write(request.CompleteNotificationNamespaceId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateMissionGroupModelMaster(
                Request.UpdateMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateMissionGroupModelMasterResult>> callback
        )
		{
			var task = new UpdateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateMissionGroupModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateMissionGroupModelMasterResult> UpdateMissionGroupModelMasterFuture(
                Request.UpdateMissionGroupModelMasterRequest request
        )
		{
			return new UpdateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateMissionGroupModelMasterResult> UpdateMissionGroupModelMasterAsync(
                Request.UpdateMissionGroupModelMasterRequest request
        )
		{
            AsyncResult<Result.UpdateMissionGroupModelMasterResult> result = null;
			await UpdateMissionGroupModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public UpdateMissionGroupModelMasterTask UpdateMissionGroupModelMasterAsync(
                Request.UpdateMissionGroupModelMasterRequest request
        )
		{
			return new UpdateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateMissionGroupModelMasterResult> UpdateMissionGroupModelMasterAsync(
                Request.UpdateMissionGroupModelMasterRequest request
        )
		{
			var task = new UpdateMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteMissionGroupModelMasterTask : Gs2RestSessionTask<DeleteMissionGroupModelMasterRequest, DeleteMissionGroupModelMasterResult>
        {
            public DeleteMissionGroupModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, DeleteMissionGroupModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DeleteMissionGroupModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Delete(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteMissionGroupModelMaster(
                Request.DeleteMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteMissionGroupModelMasterResult>> callback
        )
		{
			var task = new DeleteMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteMissionGroupModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteMissionGroupModelMasterResult> DeleteMissionGroupModelMasterFuture(
                Request.DeleteMissionGroupModelMasterRequest request
        )
		{
			return new DeleteMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteMissionGroupModelMasterResult> DeleteMissionGroupModelMasterAsync(
                Request.DeleteMissionGroupModelMasterRequest request
        )
		{
            AsyncResult<Result.DeleteMissionGroupModelMasterResult> result = null;
			await DeleteMissionGroupModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DeleteMissionGroupModelMasterTask DeleteMissionGroupModelMasterAsync(
                Request.DeleteMissionGroupModelMasterRequest request
        )
		{
			return new DeleteMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteMissionGroupModelMasterResult> DeleteMissionGroupModelMasterAsync(
                Request.DeleteMissionGroupModelMasterRequest request
        )
		{
			var task = new DeleteMissionGroupModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeNamespacesTask : Gs2RestSessionTask<DescribeNamespacesRequest, DescribeNamespacesResult>
        {
            public DescribeNamespacesTask(IGs2Session session, RestSessionRequestFactory factory, DescribeNamespacesRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeNamespacesRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/";

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeNamespaces(
                Request.DescribeNamespacesRequest request,
                UnityAction<AsyncResult<Result.DescribeNamespacesResult>> callback
        )
		{
			var task = new DescribeNamespacesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeNamespacesResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeNamespacesResult> DescribeNamespacesFuture(
                Request.DescribeNamespacesRequest request
        )
		{
			return new DescribeNamespacesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeNamespacesResult> DescribeNamespacesAsync(
                Request.DescribeNamespacesRequest request
        )
		{
            AsyncResult<Result.DescribeNamespacesResult> result = null;
			await DescribeNamespaces(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeNamespacesTask DescribeNamespacesAsync(
                Request.DescribeNamespacesRequest request
        )
		{
			return new DescribeNamespacesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeNamespacesResult> DescribeNamespacesAsync(
                Request.DescribeNamespacesRequest request
        )
		{
			var task = new DescribeNamespacesTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class CreateNamespaceTask : Gs2RestSessionTask<CreateNamespaceRequest, CreateNamespaceResult>
        {
            public CreateNamespaceTask(IGs2Session session, RestSessionRequestFactory factory, CreateNamespaceRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(CreateNamespaceRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/";

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(request.Name);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.TransactionSetting != null)
                {
                    jsonWriter.WritePropertyName("transactionSetting");
                    request.TransactionSetting.WriteJson(jsonWriter);
                }
                if (request.MissionCompleteScript != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteScript");
                    request.MissionCompleteScript.WriteJson(jsonWriter);
                }
                if (request.CounterIncrementScript != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementScript");
                    request.CounterIncrementScript.WriteJson(jsonWriter);
                }
                if (request.ReceiveRewardsScript != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsScript");
                    request.ReceiveRewardsScript.WriteJson(jsonWriter);
                }
                if (request.CompleteNotification != null)
                {
                    jsonWriter.WritePropertyName("completeNotification");
                    request.CompleteNotification.WriteJson(jsonWriter);
                }
                if (request.LogSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    request.LogSetting.WriteJson(jsonWriter);
                }
                if (request.QueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("queueNamespaceId");
                    jsonWriter.Write(request.QueueNamespaceId);
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateNamespace(
                Request.CreateNamespaceRequest request,
                UnityAction<AsyncResult<Result.CreateNamespaceResult>> callback
        )
		{
			var task = new CreateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateNamespaceResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateNamespaceResult> CreateNamespaceFuture(
                Request.CreateNamespaceRequest request
        )
		{
			return new CreateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateNamespaceResult> CreateNamespaceAsync(
                Request.CreateNamespaceRequest request
        )
		{
            AsyncResult<Result.CreateNamespaceResult> result = null;
			await CreateNamespace(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public CreateNamespaceTask CreateNamespaceAsync(
                Request.CreateNamespaceRequest request
        )
		{
			return new CreateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateNamespaceResult> CreateNamespaceAsync(
                Request.CreateNamespaceRequest request
        )
		{
			var task = new CreateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetNamespaceStatusTask : Gs2RestSessionTask<GetNamespaceStatusRequest, GetNamespaceStatusResult>
        {
            public GetNamespaceStatusTask(IGs2Session session, RestSessionRequestFactory factory, GetNamespaceStatusRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetNamespaceStatusRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/status";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetNamespaceStatus(
                Request.GetNamespaceStatusRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> callback
        )
		{
			var task = new GetNamespaceStatusTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetNamespaceStatusResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetNamespaceStatusResult> GetNamespaceStatusFuture(
                Request.GetNamespaceStatusRequest request
        )
		{
			return new GetNamespaceStatusTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetNamespaceStatusResult> GetNamespaceStatusAsync(
                Request.GetNamespaceStatusRequest request
        )
		{
            AsyncResult<Result.GetNamespaceStatusResult> result = null;
			await GetNamespaceStatus(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetNamespaceStatusTask GetNamespaceStatusAsync(
                Request.GetNamespaceStatusRequest request
        )
		{
			return new GetNamespaceStatusTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetNamespaceStatusResult> GetNamespaceStatusAsync(
                Request.GetNamespaceStatusRequest request
        )
		{
			var task = new GetNamespaceStatusTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetNamespaceTask : Gs2RestSessionTask<GetNamespaceRequest, GetNamespaceResult>
        {
            public GetNamespaceTask(IGs2Session session, RestSessionRequestFactory factory, GetNamespaceRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetNamespaceRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetNamespace(
                Request.GetNamespaceRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceResult>> callback
        )
		{
			var task = new GetNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetNamespaceResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetNamespaceResult> GetNamespaceFuture(
                Request.GetNamespaceRequest request
        )
		{
			return new GetNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetNamespaceResult> GetNamespaceAsync(
                Request.GetNamespaceRequest request
        )
		{
            AsyncResult<Result.GetNamespaceResult> result = null;
			await GetNamespace(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetNamespaceTask GetNamespaceAsync(
                Request.GetNamespaceRequest request
        )
		{
			return new GetNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetNamespaceResult> GetNamespaceAsync(
                Request.GetNamespaceRequest request
        )
		{
			var task = new GetNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateNamespaceTask : Gs2RestSessionTask<UpdateNamespaceRequest, UpdateNamespaceResult>
        {
            public UpdateNamespaceTask(IGs2Session session, RestSessionRequestFactory factory, UpdateNamespaceRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(UpdateNamespaceRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Put(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.TransactionSetting != null)
                {
                    jsonWriter.WritePropertyName("transactionSetting");
                    request.TransactionSetting.WriteJson(jsonWriter);
                }
                if (request.MissionCompleteScript != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteScript");
                    request.MissionCompleteScript.WriteJson(jsonWriter);
                }
                if (request.CounterIncrementScript != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementScript");
                    request.CounterIncrementScript.WriteJson(jsonWriter);
                }
                if (request.ReceiveRewardsScript != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsScript");
                    request.ReceiveRewardsScript.WriteJson(jsonWriter);
                }
                if (request.CompleteNotification != null)
                {
                    jsonWriter.WritePropertyName("completeNotification");
                    request.CompleteNotification.WriteJson(jsonWriter);
                }
                if (request.LogSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    request.LogSetting.WriteJson(jsonWriter);
                }
                if (request.QueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("queueNamespaceId");
                    jsonWriter.Write(request.QueueNamespaceId);
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateNamespace(
                Request.UpdateNamespaceRequest request,
                UnityAction<AsyncResult<Result.UpdateNamespaceResult>> callback
        )
		{
			var task = new UpdateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateNamespaceResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateNamespaceResult> UpdateNamespaceFuture(
                Request.UpdateNamespaceRequest request
        )
		{
			return new UpdateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateNamespaceResult> UpdateNamespaceAsync(
                Request.UpdateNamespaceRequest request
        )
		{
            AsyncResult<Result.UpdateNamespaceResult> result = null;
			await UpdateNamespace(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public UpdateNamespaceTask UpdateNamespaceAsync(
                Request.UpdateNamespaceRequest request
        )
		{
			return new UpdateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateNamespaceResult> UpdateNamespaceAsync(
                Request.UpdateNamespaceRequest request
        )
		{
			var task = new UpdateNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteNamespaceTask : Gs2RestSessionTask<DeleteNamespaceRequest, DeleteNamespaceResult>
        {
            public DeleteNamespaceTask(IGs2Session session, RestSessionRequestFactory factory, DeleteNamespaceRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DeleteNamespaceRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Delete(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteNamespace(
                Request.DeleteNamespaceRequest request,
                UnityAction<AsyncResult<Result.DeleteNamespaceResult>> callback
        )
		{
			var task = new DeleteNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteNamespaceResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteNamespaceResult> DeleteNamespaceFuture(
                Request.DeleteNamespaceRequest request
        )
		{
			return new DeleteNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteNamespaceResult> DeleteNamespaceAsync(
                Request.DeleteNamespaceRequest request
        )
		{
            AsyncResult<Result.DeleteNamespaceResult> result = null;
			await DeleteNamespace(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DeleteNamespaceTask DeleteNamespaceAsync(
                Request.DeleteNamespaceRequest request
        )
		{
			return new DeleteNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteNamespaceResult> DeleteNamespaceAsync(
                Request.DeleteNamespaceRequest request
        )
		{
			var task = new DeleteNamespaceTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeCountersTask : Gs2RestSessionTask<DescribeCountersRequest, DescribeCountersResult>
        {
            public DescribeCountersTask(IGs2Session session, RestSessionRequestFactory factory, DescribeCountersRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeCountersRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/counter";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    sessionRequest.AddHeader("X-GS2-ACCESS-TOKEN", request.AccessToken);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeCounters(
                Request.DescribeCountersRequest request,
                UnityAction<AsyncResult<Result.DescribeCountersResult>> callback
        )
		{
			var task = new DescribeCountersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeCountersResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeCountersResult> DescribeCountersFuture(
                Request.DescribeCountersRequest request
        )
		{
			return new DescribeCountersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeCountersResult> DescribeCountersAsync(
                Request.DescribeCountersRequest request
        )
		{
            AsyncResult<Result.DescribeCountersResult> result = null;
			await DescribeCounters(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeCountersTask DescribeCountersAsync(
                Request.DescribeCountersRequest request
        )
		{
			return new DescribeCountersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeCountersResult> DescribeCountersAsync(
                Request.DescribeCountersRequest request
        )
		{
			var task = new DescribeCountersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeCountersByUserIdTask : Gs2RestSessionTask<DescribeCountersByUserIdRequest, DescribeCountersByUserIdResult>
        {
            public DescribeCountersByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, DescribeCountersByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeCountersByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeCountersByUserId(
                Request.DescribeCountersByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeCountersByUserIdResult>> callback
        )
		{
			var task = new DescribeCountersByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeCountersByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeCountersByUserIdResult> DescribeCountersByUserIdFuture(
                Request.DescribeCountersByUserIdRequest request
        )
		{
			return new DescribeCountersByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeCountersByUserIdResult> DescribeCountersByUserIdAsync(
                Request.DescribeCountersByUserIdRequest request
        )
		{
            AsyncResult<Result.DescribeCountersByUserIdResult> result = null;
			await DescribeCountersByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeCountersByUserIdTask DescribeCountersByUserIdAsync(
                Request.DescribeCountersByUserIdRequest request
        )
		{
			return new DescribeCountersByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeCountersByUserIdResult> DescribeCountersByUserIdAsync(
                Request.DescribeCountersByUserIdRequest request
        )
		{
			var task = new DescribeCountersByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class IncreaseCounterByUserIdTask : Gs2RestSessionTask<IncreaseCounterByUserIdRequest, IncreaseCounterByUserIdResult>
        {
            public IncreaseCounterByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, IncreaseCounterByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(IncreaseCounterByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Value != null)
                {
                    jsonWriter.WritePropertyName("value");
                    jsonWriter.Write(request.Value.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    sessionRequest.AddHeader("X-GS2-DUPLICATION-AVOIDER", request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }

            public override void OnError(Gs2.Core.Exception.Gs2Exception error)
            {
                if (error.Errors.Count(v => v.code == "counter.increase.conflict") > 0) {
                    base.OnError(new Exception.ConflictException(error));
                }
                else {
                    base.OnError(error);
                }
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator IncreaseCounterByUserId(
                Request.IncreaseCounterByUserIdRequest request,
                UnityAction<AsyncResult<Result.IncreaseCounterByUserIdResult>> callback
        )
		{
			var task = new IncreaseCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.IncreaseCounterByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.IncreaseCounterByUserIdResult> IncreaseCounterByUserIdFuture(
                Request.IncreaseCounterByUserIdRequest request
        )
		{
			return new IncreaseCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.IncreaseCounterByUserIdResult> IncreaseCounterByUserIdAsync(
                Request.IncreaseCounterByUserIdRequest request
        )
		{
            AsyncResult<Result.IncreaseCounterByUserIdResult> result = null;
			await IncreaseCounterByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public IncreaseCounterByUserIdTask IncreaseCounterByUserIdAsync(
                Request.IncreaseCounterByUserIdRequest request
        )
		{
			return new IncreaseCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.IncreaseCounterByUserIdResult> IncreaseCounterByUserIdAsync(
                Request.IncreaseCounterByUserIdRequest request
        )
		{
			var task = new IncreaseCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCounterTask : Gs2RestSessionTask<GetCounterRequest, GetCounterResult>
        {
            public GetCounterTask(IGs2Session session, RestSessionRequestFactory factory, GetCounterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCounterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    sessionRequest.AddHeader("X-GS2-ACCESS-TOKEN", request.AccessToken);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetCounter(
                Request.GetCounterRequest request,
                UnityAction<AsyncResult<Result.GetCounterResult>> callback
        )
		{
			var task = new GetCounterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCounterResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCounterResult> GetCounterFuture(
                Request.GetCounterRequest request
        )
		{
			return new GetCounterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCounterResult> GetCounterAsync(
                Request.GetCounterRequest request
        )
		{
            AsyncResult<Result.GetCounterResult> result = null;
			await GetCounter(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCounterTask GetCounterAsync(
                Request.GetCounterRequest request
        )
		{
			return new GetCounterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCounterResult> GetCounterAsync(
                Request.GetCounterRequest request
        )
		{
			var task = new GetCounterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCounterByUserIdTask : Gs2RestSessionTask<GetCounterByUserIdRequest, GetCounterByUserIdResult>
        {
            public GetCounterByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, GetCounterByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCounterByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetCounterByUserId(
                Request.GetCounterByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetCounterByUserIdResult>> callback
        )
		{
			var task = new GetCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCounterByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCounterByUserIdResult> GetCounterByUserIdFuture(
                Request.GetCounterByUserIdRequest request
        )
		{
			return new GetCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCounterByUserIdResult> GetCounterByUserIdAsync(
                Request.GetCounterByUserIdRequest request
        )
		{
            AsyncResult<Result.GetCounterByUserIdResult> result = null;
			await GetCounterByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCounterByUserIdTask GetCounterByUserIdAsync(
                Request.GetCounterByUserIdRequest request
        )
		{
			return new GetCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCounterByUserIdResult> GetCounterByUserIdAsync(
                Request.GetCounterByUserIdRequest request
        )
		{
			var task = new GetCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteCounterByUserIdTask : Gs2RestSessionTask<DeleteCounterByUserIdRequest, DeleteCounterByUserIdResult>
        {
            public DeleteCounterByUserIdTask(IGs2Session session, RestSessionRequestFactory factory, DeleteCounterByUserIdRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DeleteCounterByUserIdRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(request.UserId) ? request.UserId.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");

                var sessionRequest = Factory.Delete(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    sessionRequest.AddHeader("X-GS2-DUPLICATION-AVOIDER", request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteCounterByUserId(
                Request.DeleteCounterByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteCounterByUserIdResult>> callback
        )
		{
			var task = new DeleteCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteCounterByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteCounterByUserIdResult> DeleteCounterByUserIdFuture(
                Request.DeleteCounterByUserIdRequest request
        )
		{
			return new DeleteCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteCounterByUserIdResult> DeleteCounterByUserIdAsync(
                Request.DeleteCounterByUserIdRequest request
        )
		{
            AsyncResult<Result.DeleteCounterByUserIdResult> result = null;
			await DeleteCounterByUserId(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DeleteCounterByUserIdTask DeleteCounterByUserIdAsync(
                Request.DeleteCounterByUserIdRequest request
        )
		{
			return new DeleteCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteCounterByUserIdResult> DeleteCounterByUserIdAsync(
                Request.DeleteCounterByUserIdRequest request
        )
		{
			var task = new DeleteCounterByUserIdTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class IncreaseByStampSheetTask : Gs2RestSessionTask<IncreaseByStampSheetRequest, IncreaseByStampSheetResult>
        {
            public IncreaseByStampSheetTask(IGs2Session session, RestSessionRequestFactory factory, IncreaseByStampSheetRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(IncreaseByStampSheetRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/stamp/increase";

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.StampSheet != null)
                {
                    jsonWriter.WritePropertyName("stampSheet");
                    jsonWriter.Write(request.StampSheet);
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator IncreaseByStampSheet(
                Request.IncreaseByStampSheetRequest request,
                UnityAction<AsyncResult<Result.IncreaseByStampSheetResult>> callback
        )
		{
			var task = new IncreaseByStampSheetTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.IncreaseByStampSheetResult>(task.Result, task.Error));
        }

		public IFuture<Result.IncreaseByStampSheetResult> IncreaseByStampSheetFuture(
                Request.IncreaseByStampSheetRequest request
        )
		{
			return new IncreaseByStampSheetTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.IncreaseByStampSheetResult> IncreaseByStampSheetAsync(
                Request.IncreaseByStampSheetRequest request
        )
		{
            AsyncResult<Result.IncreaseByStampSheetResult> result = null;
			await IncreaseByStampSheet(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public IncreaseByStampSheetTask IncreaseByStampSheetAsync(
                Request.IncreaseByStampSheetRequest request
        )
		{
			return new IncreaseByStampSheetTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.IncreaseByStampSheetResult> IncreaseByStampSheetAsync(
                Request.IncreaseByStampSheetRequest request
        )
		{
			var task = new IncreaseByStampSheetTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class ExportMasterTask : Gs2RestSessionTask<ExportMasterRequest, ExportMasterResult>
        {
            public ExportMasterTask(IGs2Session session, RestSessionRequestFactory factory, ExportMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(ExportMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/export";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator ExportMaster(
                Request.ExportMasterRequest request,
                UnityAction<AsyncResult<Result.ExportMasterResult>> callback
        )
		{
			var task = new ExportMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.ExportMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.ExportMasterResult> ExportMasterFuture(
                Request.ExportMasterRequest request
        )
		{
			return new ExportMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.ExportMasterResult> ExportMasterAsync(
                Request.ExportMasterRequest request
        )
		{
            AsyncResult<Result.ExportMasterResult> result = null;
			await ExportMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public ExportMasterTask ExportMasterAsync(
                Request.ExportMasterRequest request
        )
		{
			return new ExportMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.ExportMasterResult> ExportMasterAsync(
                Request.ExportMasterRequest request
        )
		{
			var task = new ExportMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCurrentMissionMasterTask : Gs2RestSessionTask<GetCurrentMissionMasterRequest, GetCurrentMissionMasterResult>
        {
            public GetCurrentMissionMasterTask(IGs2Session session, RestSessionRequestFactory factory, GetCurrentMissionMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCurrentMissionMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetCurrentMissionMaster(
                Request.GetCurrentMissionMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentMissionMasterResult>> callback
        )
		{
			var task = new GetCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCurrentMissionMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCurrentMissionMasterResult> GetCurrentMissionMasterFuture(
                Request.GetCurrentMissionMasterRequest request
        )
		{
			return new GetCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCurrentMissionMasterResult> GetCurrentMissionMasterAsync(
                Request.GetCurrentMissionMasterRequest request
        )
		{
            AsyncResult<Result.GetCurrentMissionMasterResult> result = null;
			await GetCurrentMissionMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCurrentMissionMasterTask GetCurrentMissionMasterAsync(
                Request.GetCurrentMissionMasterRequest request
        )
		{
			return new GetCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCurrentMissionMasterResult> GetCurrentMissionMasterAsync(
                Request.GetCurrentMissionMasterRequest request
        )
		{
			var task = new GetCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateCurrentMissionMasterTask : Gs2RestSessionTask<UpdateCurrentMissionMasterRequest, UpdateCurrentMissionMasterResult>
        {
            public UpdateCurrentMissionMasterTask(IGs2Session session, RestSessionRequestFactory factory, UpdateCurrentMissionMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(UpdateCurrentMissionMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Put(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Settings != null)
                {
                    jsonWriter.WritePropertyName("settings");
                    jsonWriter.Write(request.Settings);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateCurrentMissionMaster(
                Request.UpdateCurrentMissionMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentMissionMasterResult>> callback
        )
		{
			var task = new UpdateCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateCurrentMissionMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateCurrentMissionMasterResult> UpdateCurrentMissionMasterFuture(
                Request.UpdateCurrentMissionMasterRequest request
        )
		{
			return new UpdateCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateCurrentMissionMasterResult> UpdateCurrentMissionMasterAsync(
                Request.UpdateCurrentMissionMasterRequest request
        )
		{
            AsyncResult<Result.UpdateCurrentMissionMasterResult> result = null;
			await UpdateCurrentMissionMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public UpdateCurrentMissionMasterTask UpdateCurrentMissionMasterAsync(
                Request.UpdateCurrentMissionMasterRequest request
        )
		{
			return new UpdateCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateCurrentMissionMasterResult> UpdateCurrentMissionMasterAsync(
                Request.UpdateCurrentMissionMasterRequest request
        )
		{
			var task = new UpdateCurrentMissionMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateCurrentMissionMasterFromGitHubTask : Gs2RestSessionTask<UpdateCurrentMissionMasterFromGitHubRequest, UpdateCurrentMissionMasterFromGitHubResult>
        {
            public UpdateCurrentMissionMasterFromGitHubTask(IGs2Session session, RestSessionRequestFactory factory, UpdateCurrentMissionMasterFromGitHubRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(UpdateCurrentMissionMasterFromGitHubRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/from_git_hub";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Put(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.CheckoutSetting != null)
                {
                    jsonWriter.WritePropertyName("checkoutSetting");
                    request.CheckoutSetting.WriteJson(jsonWriter);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateCurrentMissionMasterFromGitHub(
                Request.UpdateCurrentMissionMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentMissionMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentMissionMasterFromGitHubTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateCurrentMissionMasterFromGitHubResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateCurrentMissionMasterFromGitHubResult> UpdateCurrentMissionMasterFromGitHubFuture(
                Request.UpdateCurrentMissionMasterFromGitHubRequest request
        )
		{
			return new UpdateCurrentMissionMasterFromGitHubTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateCurrentMissionMasterFromGitHubResult> UpdateCurrentMissionMasterFromGitHubAsync(
                Request.UpdateCurrentMissionMasterFromGitHubRequest request
        )
		{
            AsyncResult<Result.UpdateCurrentMissionMasterFromGitHubResult> result = null;
			await UpdateCurrentMissionMasterFromGitHub(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public UpdateCurrentMissionMasterFromGitHubTask UpdateCurrentMissionMasterFromGitHubAsync(
                Request.UpdateCurrentMissionMasterFromGitHubRequest request
        )
		{
			return new UpdateCurrentMissionMasterFromGitHubTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateCurrentMissionMasterFromGitHubResult> UpdateCurrentMissionMasterFromGitHubAsync(
                Request.UpdateCurrentMissionMasterFromGitHubRequest request
        )
		{
			var task = new UpdateCurrentMissionMasterFromGitHubTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeCounterModelsTask : Gs2RestSessionTask<DescribeCounterModelsRequest, DescribeCounterModelsResult>
        {
            public DescribeCounterModelsTask(IGs2Session session, RestSessionRequestFactory factory, DescribeCounterModelsRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeCounterModelsRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/counter";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeCounterModels(
                Request.DescribeCounterModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeCounterModelsResult>> callback
        )
		{
			var task = new DescribeCounterModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeCounterModelsResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeCounterModelsResult> DescribeCounterModelsFuture(
                Request.DescribeCounterModelsRequest request
        )
		{
			return new DescribeCounterModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeCounterModelsResult> DescribeCounterModelsAsync(
                Request.DescribeCounterModelsRequest request
        )
		{
            AsyncResult<Result.DescribeCounterModelsResult> result = null;
			await DescribeCounterModels(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeCounterModelsTask DescribeCounterModelsAsync(
                Request.DescribeCounterModelsRequest request
        )
		{
			return new DescribeCounterModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeCounterModelsResult> DescribeCounterModelsAsync(
                Request.DescribeCounterModelsRequest request
        )
		{
			var task = new DescribeCounterModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetCounterModelTask : Gs2RestSessionTask<GetCounterModelRequest, GetCounterModelResult>
        {
            public GetCounterModelTask(IGs2Session session, RestSessionRequestFactory factory, GetCounterModelRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetCounterModelRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(request.CounterName) ? request.CounterName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetCounterModel(
                Request.GetCounterModelRequest request,
                UnityAction<AsyncResult<Result.GetCounterModelResult>> callback
        )
		{
			var task = new GetCounterModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetCounterModelResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetCounterModelResult> GetCounterModelFuture(
                Request.GetCounterModelRequest request
        )
		{
			return new GetCounterModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetCounterModelResult> GetCounterModelAsync(
                Request.GetCounterModelRequest request
        )
		{
            AsyncResult<Result.GetCounterModelResult> result = null;
			await GetCounterModel(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetCounterModelTask GetCounterModelAsync(
                Request.GetCounterModelRequest request
        )
		{
			return new GetCounterModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetCounterModelResult> GetCounterModelAsync(
                Request.GetCounterModelRequest request
        )
		{
			var task = new GetCounterModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeMissionGroupModelsTask : Gs2RestSessionTask<DescribeMissionGroupModelsRequest, DescribeMissionGroupModelsResult>
        {
            public DescribeMissionGroupModelsTask(IGs2Session session, RestSessionRequestFactory factory, DescribeMissionGroupModelsRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeMissionGroupModelsRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/group";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeMissionGroupModels(
                Request.DescribeMissionGroupModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionGroupModelsResult>> callback
        )
		{
			var task = new DescribeMissionGroupModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeMissionGroupModelsResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeMissionGroupModelsResult> DescribeMissionGroupModelsFuture(
                Request.DescribeMissionGroupModelsRequest request
        )
		{
			return new DescribeMissionGroupModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeMissionGroupModelsResult> DescribeMissionGroupModelsAsync(
                Request.DescribeMissionGroupModelsRequest request
        )
		{
            AsyncResult<Result.DescribeMissionGroupModelsResult> result = null;
			await DescribeMissionGroupModels(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeMissionGroupModelsTask DescribeMissionGroupModelsAsync(
                Request.DescribeMissionGroupModelsRequest request
        )
		{
			return new DescribeMissionGroupModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeMissionGroupModelsResult> DescribeMissionGroupModelsAsync(
                Request.DescribeMissionGroupModelsRequest request
        )
		{
			var task = new DescribeMissionGroupModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetMissionGroupModelTask : Gs2RestSessionTask<GetMissionGroupModelRequest, GetMissionGroupModelResult>
        {
            public GetMissionGroupModelTask(IGs2Session session, RestSessionRequestFactory factory, GetMissionGroupModelRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetMissionGroupModelRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetMissionGroupModel(
                Request.GetMissionGroupModelRequest request,
                UnityAction<AsyncResult<Result.GetMissionGroupModelResult>> callback
        )
		{
			var task = new GetMissionGroupModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetMissionGroupModelResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetMissionGroupModelResult> GetMissionGroupModelFuture(
                Request.GetMissionGroupModelRequest request
        )
		{
			return new GetMissionGroupModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetMissionGroupModelResult> GetMissionGroupModelAsync(
                Request.GetMissionGroupModelRequest request
        )
		{
            AsyncResult<Result.GetMissionGroupModelResult> result = null;
			await GetMissionGroupModel(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetMissionGroupModelTask GetMissionGroupModelAsync(
                Request.GetMissionGroupModelRequest request
        )
		{
			return new GetMissionGroupModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetMissionGroupModelResult> GetMissionGroupModelAsync(
                Request.GetMissionGroupModelRequest request
        )
		{
			var task = new GetMissionGroupModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeMissionTaskModelsTask : Gs2RestSessionTask<DescribeMissionTaskModelsRequest, DescribeMissionTaskModelsResult>
        {
            public DescribeMissionTaskModelsTask(IGs2Session session, RestSessionRequestFactory factory, DescribeMissionTaskModelsRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeMissionTaskModelsRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/group/{missionGroupName}/task";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeMissionTaskModels(
                Request.DescribeMissionTaskModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionTaskModelsResult>> callback
        )
		{
			var task = new DescribeMissionTaskModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeMissionTaskModelsResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeMissionTaskModelsResult> DescribeMissionTaskModelsFuture(
                Request.DescribeMissionTaskModelsRequest request
        )
		{
			return new DescribeMissionTaskModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeMissionTaskModelsResult> DescribeMissionTaskModelsAsync(
                Request.DescribeMissionTaskModelsRequest request
        )
		{
            AsyncResult<Result.DescribeMissionTaskModelsResult> result = null;
			await DescribeMissionTaskModels(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeMissionTaskModelsTask DescribeMissionTaskModelsAsync(
                Request.DescribeMissionTaskModelsRequest request
        )
		{
			return new DescribeMissionTaskModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeMissionTaskModelsResult> DescribeMissionTaskModelsAsync(
                Request.DescribeMissionTaskModelsRequest request
        )
		{
			var task = new DescribeMissionTaskModelsTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetMissionTaskModelTask : Gs2RestSessionTask<GetMissionTaskModelRequest, GetMissionTaskModelResult>
        {
            public GetMissionTaskModelTask(IGs2Session session, RestSessionRequestFactory factory, GetMissionTaskModelRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetMissionTaskModelRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetMissionTaskModel(
                Request.GetMissionTaskModelRequest request,
                UnityAction<AsyncResult<Result.GetMissionTaskModelResult>> callback
        )
		{
			var task = new GetMissionTaskModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetMissionTaskModelResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetMissionTaskModelResult> GetMissionTaskModelFuture(
                Request.GetMissionTaskModelRequest request
        )
		{
			return new GetMissionTaskModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetMissionTaskModelResult> GetMissionTaskModelAsync(
                Request.GetMissionTaskModelRequest request
        )
		{
            AsyncResult<Result.GetMissionTaskModelResult> result = null;
			await GetMissionTaskModel(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetMissionTaskModelTask GetMissionTaskModelAsync(
                Request.GetMissionTaskModelRequest request
        )
		{
			return new GetMissionTaskModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetMissionTaskModelResult> GetMissionTaskModelAsync(
                Request.GetMissionTaskModelRequest request
        )
		{
			var task = new GetMissionTaskModelTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DescribeMissionTaskModelMastersTask : Gs2RestSessionTask<DescribeMissionTaskModelMastersRequest, DescribeMissionTaskModelMastersResult>
        {
            public DescribeMissionTaskModelMastersTask(IGs2Session session, RestSessionRequestFactory factory, DescribeMissionTaskModelMastersRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DescribeMissionTaskModelMastersRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }
                if (request.PageToken != null) {
                    sessionRequest.AddQueryString("pageToken", $"{request.PageToken}");
                }
                if (request.Limit != null) {
                    sessionRequest.AddQueryString("limit", $"{request.Limit}");
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DescribeMissionTaskModelMasters(
                Request.DescribeMissionTaskModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionTaskModelMastersResult>> callback
        )
		{
			var task = new DescribeMissionTaskModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DescribeMissionTaskModelMastersResult>(task.Result, task.Error));
        }

		public IFuture<Result.DescribeMissionTaskModelMastersResult> DescribeMissionTaskModelMastersFuture(
                Request.DescribeMissionTaskModelMastersRequest request
        )
		{
			return new DescribeMissionTaskModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DescribeMissionTaskModelMastersResult> DescribeMissionTaskModelMastersAsync(
                Request.DescribeMissionTaskModelMastersRequest request
        )
		{
            AsyncResult<Result.DescribeMissionTaskModelMastersResult> result = null;
			await DescribeMissionTaskModelMasters(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DescribeMissionTaskModelMastersTask DescribeMissionTaskModelMastersAsync(
                Request.DescribeMissionTaskModelMastersRequest request
        )
		{
			return new DescribeMissionTaskModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DescribeMissionTaskModelMastersResult> DescribeMissionTaskModelMastersAsync(
                Request.DescribeMissionTaskModelMastersRequest request
        )
		{
			var task = new DescribeMissionTaskModelMastersTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class CreateMissionTaskModelMasterTask : Gs2RestSessionTask<CreateMissionTaskModelMasterRequest, CreateMissionTaskModelMasterResult>
        {
            public CreateMissionTaskModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, CreateMissionTaskModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(CreateMissionTaskModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");

                var sessionRequest = Factory.Post(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(request.Name);
                }
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.CounterName != null)
                {
                    jsonWriter.WritePropertyName("counterName");
                    jsonWriter.Write(request.CounterName);
                }
                if (request.TargetValue != null)
                {
                    jsonWriter.WritePropertyName("targetValue");
                    jsonWriter.Write(request.TargetValue.ToString());
                }
                if (request.CompleteAcquireActions != null)
                {
                    jsonWriter.WritePropertyName("completeAcquireActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.CompleteAcquireActions)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ChallengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(request.ChallengePeriodEventId);
                }
                if (request.PremiseMissionTaskName != null)
                {
                    jsonWriter.WritePropertyName("premiseMissionTaskName");
                    jsonWriter.Write(request.PremiseMissionTaskName);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateMissionTaskModelMaster(
                Request.CreateMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateMissionTaskModelMasterResult>> callback
        )
		{
			var task = new CreateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateMissionTaskModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateMissionTaskModelMasterResult> CreateMissionTaskModelMasterFuture(
                Request.CreateMissionTaskModelMasterRequest request
        )
		{
			return new CreateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateMissionTaskModelMasterResult> CreateMissionTaskModelMasterAsync(
                Request.CreateMissionTaskModelMasterRequest request
        )
		{
            AsyncResult<Result.CreateMissionTaskModelMasterResult> result = null;
			await CreateMissionTaskModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public CreateMissionTaskModelMasterTask CreateMissionTaskModelMasterAsync(
                Request.CreateMissionTaskModelMasterRequest request
        )
		{
			return new CreateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateMissionTaskModelMasterResult> CreateMissionTaskModelMasterAsync(
                Request.CreateMissionTaskModelMasterRequest request
        )
		{
			var task = new CreateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class GetMissionTaskModelMasterTask : Gs2RestSessionTask<GetMissionTaskModelMasterRequest, GetMissionTaskModelMasterResult>
        {
            public GetMissionTaskModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, GetMissionTaskModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(GetMissionTaskModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");

                var sessionRequest = Factory.Get(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetMissionTaskModelMaster(
                Request.GetMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetMissionTaskModelMasterResult>> callback
        )
		{
			var task = new GetMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetMissionTaskModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetMissionTaskModelMasterResult> GetMissionTaskModelMasterFuture(
                Request.GetMissionTaskModelMasterRequest request
        )
		{
			return new GetMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetMissionTaskModelMasterResult> GetMissionTaskModelMasterAsync(
                Request.GetMissionTaskModelMasterRequest request
        )
		{
            AsyncResult<Result.GetMissionTaskModelMasterResult> result = null;
			await GetMissionTaskModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public GetMissionTaskModelMasterTask GetMissionTaskModelMasterAsync(
                Request.GetMissionTaskModelMasterRequest request
        )
		{
			return new GetMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetMissionTaskModelMasterResult> GetMissionTaskModelMasterAsync(
                Request.GetMissionTaskModelMasterRequest request
        )
		{
			var task = new GetMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateMissionTaskModelMasterTask : Gs2RestSessionTask<UpdateMissionTaskModelMasterRequest, UpdateMissionTaskModelMasterResult>
        {
            public UpdateMissionTaskModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, UpdateMissionTaskModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(UpdateMissionTaskModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");

                var sessionRequest = Factory.Put(url);

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata);
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description);
                }
                if (request.CounterName != null)
                {
                    jsonWriter.WritePropertyName("counterName");
                    jsonWriter.Write(request.CounterName);
                }
                if (request.TargetValue != null)
                {
                    jsonWriter.WritePropertyName("targetValue");
                    jsonWriter.Write(request.TargetValue.ToString());
                }
                if (request.CompleteAcquireActions != null)
                {
                    jsonWriter.WritePropertyName("completeAcquireActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.CompleteAcquireActions)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ChallengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(request.ChallengePeriodEventId);
                }
                if (request.PremiseMissionTaskName != null)
                {
                    jsonWriter.WritePropertyName("premiseMissionTaskName");
                    jsonWriter.Write(request.PremiseMissionTaskName);
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    sessionRequest.Body = body;
                }
                sessionRequest.AddHeader("Content-Type", "application/json");

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateMissionTaskModelMaster(
                Request.UpdateMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateMissionTaskModelMasterResult>> callback
        )
		{
			var task = new UpdateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateMissionTaskModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateMissionTaskModelMasterResult> UpdateMissionTaskModelMasterFuture(
                Request.UpdateMissionTaskModelMasterRequest request
        )
		{
			return new UpdateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateMissionTaskModelMasterResult> UpdateMissionTaskModelMasterAsync(
                Request.UpdateMissionTaskModelMasterRequest request
        )
		{
            AsyncResult<Result.UpdateMissionTaskModelMasterResult> result = null;
			await UpdateMissionTaskModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public UpdateMissionTaskModelMasterTask UpdateMissionTaskModelMasterAsync(
                Request.UpdateMissionTaskModelMasterRequest request
        )
		{
			return new UpdateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateMissionTaskModelMasterResult> UpdateMissionTaskModelMasterAsync(
                Request.UpdateMissionTaskModelMasterRequest request
        )
		{
			var task = new UpdateMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteMissionTaskModelMasterTask : Gs2RestSessionTask<DeleteMissionTaskModelMasterRequest, DeleteMissionTaskModelMasterResult>
        {
            public DeleteMissionTaskModelMasterTask(IGs2Session session, RestSessionRequestFactory factory, DeleteMissionTaskModelMasterRequest request) : base(session, factory, request)
            {
            }

            protected override IGs2SessionRequest CreateRequest(DeleteMissionTaskModelMasterRequest request)
            {
                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(request.NamespaceName) ? request.NamespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(request.MissionGroupName) ? request.MissionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(request.MissionTaskName) ? request.MissionTaskName.ToString() : "null");

                var sessionRequest = Factory.Delete(url);
                if (request.ContextStack != null)
                {
                    sessionRequest.AddQueryString("contextStack", request.ContextStack);
                }

                if (request.RequestId != null)
                {
                    sessionRequest.AddHeader("X-GS2-REQUEST-ID", request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    sessionRequest
                );

                return sessionRequest;
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteMissionTaskModelMaster(
                Request.DeleteMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteMissionTaskModelMasterResult>> callback
        )
		{
			var task = new DeleteMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteMissionTaskModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteMissionTaskModelMasterResult> DeleteMissionTaskModelMasterFuture(
                Request.DeleteMissionTaskModelMasterRequest request
        )
		{
			return new DeleteMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
                request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteMissionTaskModelMasterResult> DeleteMissionTaskModelMasterAsync(
                Request.DeleteMissionTaskModelMasterRequest request
        )
		{
            AsyncResult<Result.DeleteMissionTaskModelMasterResult> result = null;
			await DeleteMissionTaskModelMaster(
                request,
                r => result = r
            );
            if (result.Error != null)
            {
                throw result.Error;
            }
            return result.Result;
        }
    #else
		public DeleteMissionTaskModelMasterTask DeleteMissionTaskModelMasterAsync(
                Request.DeleteMissionTaskModelMasterRequest request
        )
		{
			return new DeleteMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new UnityRestSessionRequest(_certificateHandler)),
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteMissionTaskModelMasterResult> DeleteMissionTaskModelMasterAsync(
                Request.DeleteMissionTaskModelMasterRequest request
        )
		{
			var task = new DeleteMissionTaskModelMasterTask(
                Gs2RestSession,
                new RestSessionRequestFactory(() => new DotNetRestSessionRequest()),
			    request
            );
			return await task.Invoke();
        }
#endif
	}
}