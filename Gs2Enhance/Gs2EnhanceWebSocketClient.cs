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
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
    #endif
#else
using System.Threading.Tasks;
using System.Threading;
#endif

namespace Gs2.Gs2Enhance
{
	public class Gs2EnhanceWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "enhance";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		public Gs2EnhanceWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

		}


        public class CreateNamespaceTask : Gs2WebSocketSessionTask<Request.CreateNamespaceRequest, Result.CreateNamespaceResult>
        {
	        public CreateNamespaceTask(IGs2Session session, Request.CreateNamespaceRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.CreateNamespaceRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(request.Name.ToString());
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description.ToString());
                }
                if (request.EnableDirectEnhance != null)
                {
                    jsonWriter.WritePropertyName("enableDirectEnhance");
                    jsonWriter.Write(request.EnableDirectEnhance.ToString());
                }
                if (request.TransactionSetting != null)
                {
                    jsonWriter.WritePropertyName("transactionSetting");
                    request.TransactionSetting.WriteJson(jsonWriter);
                }
                if (request.EnhanceScript != null)
                {
                    jsonWriter.WritePropertyName("enhanceScript");
                    request.EnhanceScript.WriteJson(jsonWriter);
                }
                if (request.LogSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    request.LogSetting.WriteJson(jsonWriter);
                }
                if (request.QueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("queueNamespaceId");
                    jsonWriter.Write(request.QueueNamespaceId.ToString());
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "namespace",
                    "createNamespace",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateNamespace(
                Request.CreateNamespaceRequest request,
                UnityAction<AsyncResult<Result.CreateNamespaceResult>> callback
        )
		{
			var task = new CreateNamespaceTask(
			    Gs2WebSocketSession,
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
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateNamespaceResult> CreateNamespaceAsync(
            Request.CreateNamespaceRequest request
        )
		{
		    var task = new CreateNamespaceTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public CreateNamespaceTask CreateNamespaceAsync(
                Request.CreateNamespaceRequest request
        )
		{
			return new CreateNamespaceTask(
                Gs2WebSocketSession,
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
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class GetNamespaceTask : Gs2WebSocketSessionTask<Request.GetNamespaceRequest, Result.GetNamespaceResult>
        {
	        public GetNamespaceTask(IGs2Session session, Request.GetNamespaceRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.GetNamespaceRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "namespace",
                    "getNamespace",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetNamespace(
                Request.GetNamespaceRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceResult>> callback
        )
		{
			var task = new GetNamespaceTask(
			    Gs2WebSocketSession,
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
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetNamespaceResult> GetNamespaceAsync(
            Request.GetNamespaceRequest request
        )
		{
		    var task = new GetNamespaceTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public GetNamespaceTask GetNamespaceAsync(
                Request.GetNamespaceRequest request
        )
		{
			return new GetNamespaceTask(
                Gs2WebSocketSession,
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
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateNamespaceTask : Gs2WebSocketSessionTask<Request.UpdateNamespaceRequest, Result.UpdateNamespaceResult>
        {
	        public UpdateNamespaceTask(IGs2Session session, Request.UpdateNamespaceRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.UpdateNamespaceRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description.ToString());
                }
                if (request.EnableDirectEnhance != null)
                {
                    jsonWriter.WritePropertyName("enableDirectEnhance");
                    jsonWriter.Write(request.EnableDirectEnhance.ToString());
                }
                if (request.TransactionSetting != null)
                {
                    jsonWriter.WritePropertyName("transactionSetting");
                    request.TransactionSetting.WriteJson(jsonWriter);
                }
                if (request.EnhanceScript != null)
                {
                    jsonWriter.WritePropertyName("enhanceScript");
                    request.EnhanceScript.WriteJson(jsonWriter);
                }
                if (request.LogSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    request.LogSetting.WriteJson(jsonWriter);
                }
                if (request.QueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("queueNamespaceId");
                    jsonWriter.Write(request.QueueNamespaceId.ToString());
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "namespace",
                    "updateNamespace",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateNamespace(
                Request.UpdateNamespaceRequest request,
                UnityAction<AsyncResult<Result.UpdateNamespaceResult>> callback
        )
		{
			var task = new UpdateNamespaceTask(
			    Gs2WebSocketSession,
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
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateNamespaceResult> UpdateNamespaceAsync(
            Request.UpdateNamespaceRequest request
        )
		{
		    var task = new UpdateNamespaceTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public UpdateNamespaceTask UpdateNamespaceAsync(
                Request.UpdateNamespaceRequest request
        )
		{
			return new UpdateNamespaceTask(
                Gs2WebSocketSession,
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
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteNamespaceTask : Gs2WebSocketSessionTask<Request.DeleteNamespaceRequest, Result.DeleteNamespaceResult>
        {
	        public DeleteNamespaceTask(IGs2Session session, Request.DeleteNamespaceRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.DeleteNamespaceRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "namespace",
                    "deleteNamespace",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteNamespace(
                Request.DeleteNamespaceRequest request,
                UnityAction<AsyncResult<Result.DeleteNamespaceResult>> callback
        )
		{
			var task = new DeleteNamespaceTask(
			    Gs2WebSocketSession,
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
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteNamespaceResult> DeleteNamespaceAsync(
            Request.DeleteNamespaceRequest request
        )
		{
		    var task = new DeleteNamespaceTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public DeleteNamespaceTask DeleteNamespaceAsync(
                Request.DeleteNamespaceRequest request
        )
		{
			return new DeleteNamespaceTask(
                Gs2WebSocketSession,
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
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class GetRateModelTask : Gs2WebSocketSessionTask<Request.GetRateModelRequest, Result.GetRateModelResult>
        {
	        public GetRateModelTask(IGs2Session session, Request.GetRateModelRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.GetRateModelRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.RateName != null)
                {
                    jsonWriter.WritePropertyName("rateName");
                    jsonWriter.Write(request.RateName.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "rateModel",
                    "getRateModel",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetRateModel(
                Request.GetRateModelRequest request,
                UnityAction<AsyncResult<Result.GetRateModelResult>> callback
        )
		{
			var task = new GetRateModelTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetRateModelResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetRateModelResult> GetRateModelFuture(
                Request.GetRateModelRequest request
        )
		{
			return new GetRateModelTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetRateModelResult> GetRateModelAsync(
            Request.GetRateModelRequest request
        )
		{
		    var task = new GetRateModelTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public GetRateModelTask GetRateModelAsync(
                Request.GetRateModelRequest request
        )
		{
			return new GetRateModelTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetRateModelResult> GetRateModelAsync(
            Request.GetRateModelRequest request
        )
		{
		    var task = new GetRateModelTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class CreateRateModelMasterTask : Gs2WebSocketSessionTask<Request.CreateRateModelMasterRequest, Result.CreateRateModelMasterResult>
        {
	        public CreateRateModelMasterTask(IGs2Session session, Request.CreateRateModelMasterRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.CreateRateModelMasterRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(request.Name.ToString());
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description.ToString());
                }
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata.ToString());
                }
                if (request.TargetInventoryModelId != null)
                {
                    jsonWriter.WritePropertyName("targetInventoryModelId");
                    jsonWriter.Write(request.TargetInventoryModelId.ToString());
                }
                if (request.AcquireExperienceSuffix != null)
                {
                    jsonWriter.WritePropertyName("acquireExperienceSuffix");
                    jsonWriter.Write(request.AcquireExperienceSuffix.ToString());
                }
                if (request.MaterialInventoryModelId != null)
                {
                    jsonWriter.WritePropertyName("materialInventoryModelId");
                    jsonWriter.Write(request.MaterialInventoryModelId.ToString());
                }
                if (request.AcquireExperienceHierarchy != null)
                {
                    jsonWriter.WritePropertyName("acquireExperienceHierarchy");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.AcquireExperienceHierarchy)
                    {
                        jsonWriter.Write(item);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ExperienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(request.ExperienceModelId.ToString());
                }
                if (request.BonusRates != null)
                {
                    jsonWriter.WritePropertyName("bonusRates");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.BonusRates)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "rateModelMaster",
                    "createRateModelMaster",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateRateModelMaster(
                Request.CreateRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateRateModelMasterResult>> callback
        )
		{
			var task = new CreateRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateRateModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateRateModelMasterResult> CreateRateModelMasterFuture(
                Request.CreateRateModelMasterRequest request
        )
		{
			return new CreateRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateRateModelMasterResult> CreateRateModelMasterAsync(
            Request.CreateRateModelMasterRequest request
        )
		{
		    var task = new CreateRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public CreateRateModelMasterTask CreateRateModelMasterAsync(
                Request.CreateRateModelMasterRequest request
        )
		{
			return new CreateRateModelMasterTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateRateModelMasterResult> CreateRateModelMasterAsync(
            Request.CreateRateModelMasterRequest request
        )
		{
		    var task = new CreateRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class GetRateModelMasterTask : Gs2WebSocketSessionTask<Request.GetRateModelMasterRequest, Result.GetRateModelMasterResult>
        {
	        public GetRateModelMasterTask(IGs2Session session, Request.GetRateModelMasterRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.GetRateModelMasterRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.RateName != null)
                {
                    jsonWriter.WritePropertyName("rateName");
                    jsonWriter.Write(request.RateName.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "rateModelMaster",
                    "getRateModelMaster",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetRateModelMaster(
                Request.GetRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetRateModelMasterResult>> callback
        )
		{
			var task = new GetRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetRateModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetRateModelMasterResult> GetRateModelMasterFuture(
                Request.GetRateModelMasterRequest request
        )
		{
			return new GetRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetRateModelMasterResult> GetRateModelMasterAsync(
            Request.GetRateModelMasterRequest request
        )
		{
		    var task = new GetRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public GetRateModelMasterTask GetRateModelMasterAsync(
                Request.GetRateModelMasterRequest request
        )
		{
			return new GetRateModelMasterTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetRateModelMasterResult> GetRateModelMasterAsync(
            Request.GetRateModelMasterRequest request
        )
		{
		    var task = new GetRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class UpdateRateModelMasterTask : Gs2WebSocketSessionTask<Request.UpdateRateModelMasterRequest, Result.UpdateRateModelMasterResult>
        {
	        public UpdateRateModelMasterTask(IGs2Session session, Request.UpdateRateModelMasterRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.UpdateRateModelMasterRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.RateName != null)
                {
                    jsonWriter.WritePropertyName("rateName");
                    jsonWriter.Write(request.RateName.ToString());
                }
                if (request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(request.Description.ToString());
                }
                if (request.Metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(request.Metadata.ToString());
                }
                if (request.TargetInventoryModelId != null)
                {
                    jsonWriter.WritePropertyName("targetInventoryModelId");
                    jsonWriter.Write(request.TargetInventoryModelId.ToString());
                }
                if (request.AcquireExperienceSuffix != null)
                {
                    jsonWriter.WritePropertyName("acquireExperienceSuffix");
                    jsonWriter.Write(request.AcquireExperienceSuffix.ToString());
                }
                if (request.MaterialInventoryModelId != null)
                {
                    jsonWriter.WritePropertyName("materialInventoryModelId");
                    jsonWriter.Write(request.MaterialInventoryModelId.ToString());
                }
                if (request.AcquireExperienceHierarchy != null)
                {
                    jsonWriter.WritePropertyName("acquireExperienceHierarchy");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.AcquireExperienceHierarchy)
                    {
                        jsonWriter.Write(item);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ExperienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(request.ExperienceModelId.ToString());
                }
                if (request.BonusRates != null)
                {
                    jsonWriter.WritePropertyName("bonusRates");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.BonusRates)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "rateModelMaster",
                    "updateRateModelMaster",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator UpdateRateModelMaster(
                Request.UpdateRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateRateModelMasterResult>> callback
        )
		{
			var task = new UpdateRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.UpdateRateModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.UpdateRateModelMasterResult> UpdateRateModelMasterFuture(
                Request.UpdateRateModelMasterRequest request
        )
		{
			return new UpdateRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.UpdateRateModelMasterResult> UpdateRateModelMasterAsync(
            Request.UpdateRateModelMasterRequest request
        )
		{
		    var task = new UpdateRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public UpdateRateModelMasterTask UpdateRateModelMasterAsync(
                Request.UpdateRateModelMasterRequest request
        )
		{
			return new UpdateRateModelMasterTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.UpdateRateModelMasterResult> UpdateRateModelMasterAsync(
            Request.UpdateRateModelMasterRequest request
        )
		{
		    var task = new UpdateRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteRateModelMasterTask : Gs2WebSocketSessionTask<Request.DeleteRateModelMasterRequest, Result.DeleteRateModelMasterResult>
        {
	        public DeleteRateModelMasterTask(IGs2Session session, Request.DeleteRateModelMasterRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.DeleteRateModelMasterRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.RateName != null)
                {
                    jsonWriter.WritePropertyName("rateName");
                    jsonWriter.Write(request.RateName.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "rateModelMaster",
                    "deleteRateModelMaster",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteRateModelMaster(
                Request.DeleteRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteRateModelMasterResult>> callback
        )
		{
			var task = new DeleteRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteRateModelMasterResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteRateModelMasterResult> DeleteRateModelMasterFuture(
                Request.DeleteRateModelMasterRequest request
        )
		{
			return new DeleteRateModelMasterTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteRateModelMasterResult> DeleteRateModelMasterAsync(
            Request.DeleteRateModelMasterRequest request
        )
		{
		    var task = new DeleteRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public DeleteRateModelMasterTask DeleteRateModelMasterAsync(
                Request.DeleteRateModelMasterRequest request
        )
		{
			return new DeleteRateModelMasterTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteRateModelMasterResult> DeleteRateModelMasterAsync(
            Request.DeleteRateModelMasterRequest request
        )
		{
		    var task = new DeleteRateModelMasterTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class CreateProgressByUserIdTask : Gs2WebSocketSessionTask<Request.CreateProgressByUserIdRequest, Result.CreateProgressByUserIdResult>
        {
	        public CreateProgressByUserIdTask(IGs2Session session, Request.CreateProgressByUserIdRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.CreateProgressByUserIdRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.UserId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(request.UserId.ToString());
                }
                if (request.RateName != null)
                {
                    jsonWriter.WritePropertyName("rateName");
                    jsonWriter.Write(request.RateName.ToString());
                }
                if (request.TargetItemSetId != null)
                {
                    jsonWriter.WritePropertyName("targetItemSetId");
                    jsonWriter.Write(request.TargetItemSetId.ToString());
                }
                if (request.Materials != null)
                {
                    jsonWriter.WritePropertyName("materials");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in request.Materials)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (request.Force != null)
                {
                    jsonWriter.WritePropertyName("force");
                    jsonWriter.Write(request.Force.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "progress",
                    "createProgressByUserId",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateProgressByUserId(
                Request.CreateProgressByUserIdRequest request,
                UnityAction<AsyncResult<Result.CreateProgressByUserIdResult>> callback
        )
		{
			var task = new CreateProgressByUserIdTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateProgressByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateProgressByUserIdResult> CreateProgressByUserIdFuture(
                Request.CreateProgressByUserIdRequest request
        )
		{
			return new CreateProgressByUserIdTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateProgressByUserIdResult> CreateProgressByUserIdAsync(
            Request.CreateProgressByUserIdRequest request
        )
		{
		    var task = new CreateProgressByUserIdTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public CreateProgressByUserIdTask CreateProgressByUserIdAsync(
                Request.CreateProgressByUserIdRequest request
        )
		{
			return new CreateProgressByUserIdTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateProgressByUserIdResult> CreateProgressByUserIdAsync(
            Request.CreateProgressByUserIdRequest request
        )
		{
		    var task = new CreateProgressByUserIdTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class GetProgressTask : Gs2WebSocketSessionTask<Request.GetProgressRequest, Result.GetProgressResult>
        {
	        public GetProgressTask(IGs2Session session, Request.GetProgressRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.GetProgressRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.AccessToken != null)
                {
                    jsonWriter.WritePropertyName("accessToken");
                    jsonWriter.Write(request.AccessToken.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(request.AccessToken);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "progress",
                    "getProgress",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetProgress(
                Request.GetProgressRequest request,
                UnityAction<AsyncResult<Result.GetProgressResult>> callback
        )
		{
			var task = new GetProgressTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetProgressResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetProgressResult> GetProgressFuture(
                Request.GetProgressRequest request
        )
		{
			return new GetProgressTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetProgressResult> GetProgressAsync(
            Request.GetProgressRequest request
        )
		{
		    var task = new GetProgressTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public GetProgressTask GetProgressAsync(
                Request.GetProgressRequest request
        )
		{
			return new GetProgressTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetProgressResult> GetProgressAsync(
            Request.GetProgressRequest request
        )
		{
		    var task = new GetProgressTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class GetProgressByUserIdTask : Gs2WebSocketSessionTask<Request.GetProgressByUserIdRequest, Result.GetProgressByUserIdResult>
        {
	        public GetProgressByUserIdTask(IGs2Session session, Request.GetProgressByUserIdRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.GetProgressByUserIdRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.UserId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(request.UserId.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "progress",
                    "getProgressByUserId",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator GetProgressByUserId(
                Request.GetProgressByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetProgressByUserIdResult>> callback
        )
		{
			var task = new GetProgressByUserIdTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.GetProgressByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.GetProgressByUserIdResult> GetProgressByUserIdFuture(
                Request.GetProgressByUserIdRequest request
        )
		{
			return new GetProgressByUserIdTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.GetProgressByUserIdResult> GetProgressByUserIdAsync(
            Request.GetProgressByUserIdRequest request
        )
		{
		    var task = new GetProgressByUserIdTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public GetProgressByUserIdTask GetProgressByUserIdAsync(
                Request.GetProgressByUserIdRequest request
        )
		{
			return new GetProgressByUserIdTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.GetProgressByUserIdResult> GetProgressByUserIdAsync(
            Request.GetProgressByUserIdRequest request
        )
		{
		    var task = new GetProgressByUserIdTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteProgressTask : Gs2WebSocketSessionTask<Request.DeleteProgressRequest, Result.DeleteProgressResult>
        {
	        public DeleteProgressTask(IGs2Session session, Request.DeleteProgressRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.DeleteProgressRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.AccessToken != null)
                {
                    jsonWriter.WritePropertyName("accessToken");
                    jsonWriter.Write(request.AccessToken.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }
                if (request.AccessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(request.AccessToken);
                }
                if (request.DuplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "progress",
                    "deleteProgress",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteProgress(
                Request.DeleteProgressRequest request,
                UnityAction<AsyncResult<Result.DeleteProgressResult>> callback
        )
		{
			var task = new DeleteProgressTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteProgressResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteProgressResult> DeleteProgressFuture(
                Request.DeleteProgressRequest request
        )
		{
			return new DeleteProgressTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteProgressResult> DeleteProgressAsync(
            Request.DeleteProgressRequest request
        )
		{
		    var task = new DeleteProgressTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public DeleteProgressTask DeleteProgressAsync(
                Request.DeleteProgressRequest request
        )
		{
			return new DeleteProgressTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteProgressResult> DeleteProgressAsync(
            Request.DeleteProgressRequest request
        )
		{
		    var task = new DeleteProgressTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class DeleteProgressByUserIdTask : Gs2WebSocketSessionTask<Request.DeleteProgressByUserIdRequest, Result.DeleteProgressByUserIdResult>
        {
	        public DeleteProgressByUserIdTask(IGs2Session session, Request.DeleteProgressByUserIdRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.DeleteProgressByUserIdRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.NamespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(request.NamespaceName.ToString());
                }
                if (request.UserId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(request.UserId.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }
                if (request.DuplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(request.DuplicationAvoider);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "progress",
                    "deleteProgressByUserId",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator DeleteProgressByUserId(
                Request.DeleteProgressByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteProgressByUserIdResult>> callback
        )
		{
			var task = new DeleteProgressByUserIdTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.DeleteProgressByUserIdResult>(task.Result, task.Error));
        }

		public IFuture<Result.DeleteProgressByUserIdResult> DeleteProgressByUserIdFuture(
                Request.DeleteProgressByUserIdRequest request
        )
		{
			return new DeleteProgressByUserIdTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.DeleteProgressByUserIdResult> DeleteProgressByUserIdAsync(
            Request.DeleteProgressByUserIdRequest request
        )
		{
		    var task = new DeleteProgressByUserIdTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public DeleteProgressByUserIdTask DeleteProgressByUserIdAsync(
                Request.DeleteProgressByUserIdRequest request
        )
		{
			return new DeleteProgressByUserIdTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.DeleteProgressByUserIdResult> DeleteProgressByUserIdAsync(
            Request.DeleteProgressByUserIdRequest request
        )
		{
		    var task = new DeleteProgressByUserIdTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif


        public class CreateProgressByStampSheetTask : Gs2WebSocketSessionTask<Request.CreateProgressByStampSheetRequest, Result.CreateProgressByStampSheetResult>
        {
	        public CreateProgressByStampSheetTask(IGs2Session session, Request.CreateProgressByStampSheetRequest request) : base(session, request)
	        {
	        }

            protected override IGs2SessionRequest CreateRequest(Request.CreateProgressByStampSheetRequest request)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (request.StampSheet != null)
                {
                    jsonWriter.WritePropertyName("stampSheet");
                    jsonWriter.Write(request.StampSheet.ToString());
                }
                if (request.KeyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(request.KeyId.ToString());
                }
                if (request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(request.ContextStack.ToString());
                }
                if (request.RequestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(request.RequestId);
                }

                AddHeader(
                    Session.Credential,
                    "enhance",
                    "progress",
                    "createProgressByStampSheet",
                    jsonWriter
                );

                jsonWriter.WriteObjectEnd();

                return WebSocketSessionRequestFactory.New<WebSocketSessionRequest>(stringBuilder.ToString());
            }
        }

#if UNITY_2017_1_OR_NEWER
		public IEnumerator CreateProgressByStampSheet(
                Request.CreateProgressByStampSheetRequest request,
                UnityAction<AsyncResult<Result.CreateProgressByStampSheetResult>> callback
        )
		{
			var task = new CreateProgressByStampSheetTask(
			    Gs2WebSocketSession,
			    request
            );
            yield return task;
            callback.Invoke(new AsyncResult<Result.CreateProgressByStampSheetResult>(task.Result, task.Error));
        }

		public IFuture<Result.CreateProgressByStampSheetResult> CreateProgressByStampSheetFuture(
                Request.CreateProgressByStampSheetRequest request
        )
		{
			return new CreateProgressByStampSheetTask(
			    Gs2WebSocketSession,
			    request
			);
        }

    #if GS2_ENABLE_UNITASK
		public async UniTask<Result.CreateProgressByStampSheetResult> CreateProgressByStampSheetAsync(
            Request.CreateProgressByStampSheetRequest request
        )
		{
		    var task = new CreateProgressByStampSheetTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
    #else
		public CreateProgressByStampSheetTask CreateProgressByStampSheetAsync(
                Request.CreateProgressByStampSheetRequest request
        )
		{
			return new CreateProgressByStampSheetTask(
                Gs2WebSocketSession,
			    request
            );
        }
    #endif
#else
		public async Task<Result.CreateProgressByStampSheetResult> CreateProgressByStampSheetAsync(
            Request.CreateProgressByStampSheetRequest request
        )
		{
		    var task = new CreateProgressByStampSheetTask(
		        Gs2WebSocketSession,
		        request
            );
			return await task.Invoke();
        }
#endif
	}
}