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
using UnityEngine.Events;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Util.LitJson;namespace Gs2.Gs2Gateway
{
	public class Gs2GatewayRestClient : AbstractGs2Client
	{
		private readonly CertificateHandler _certificateHandler;

		public static string Endpoint = "gateway";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2GatewayRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="gs2RestSession">REST API 用セッション</param>
		/// <param name="certificateHandler"></param>
		public Gs2GatewayRestClient(Gs2RestSession gs2RestSession, CertificateHandler certificateHandler) : base(gs2RestSession)
		{
			_certificateHandler = certificateHandler;
		}

        private class DescribeNamespacesTask : Gs2RestSessionTask<Result.DescribeNamespacesResult>
        {
			private readonly Request.DescribeNamespacesRequest _request;

			public DescribeNamespacesTask(Request.DescribeNamespacesRequest request, UnityAction<AsyncResult<Result.DescribeNamespacesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/";

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                if (_request.PageToken != null) {
                    queryStrings.Add(string.Format("{0}={1}", "pageToken", UnityWebRequest.EscapeURL(_request.PageToken)));
                }
                if (_request.Limit != null) {
                    queryStrings.Add(string.Format("{0}={1}", "limit", _request.Limit));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeNamespaces(
                Request.DescribeNamespacesRequest request,
                UnityAction<AsyncResult<Result.DescribeNamespacesResult>> callback
        )
		{
			var task = new DescribeNamespacesTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class CreateNamespaceTask : Gs2RestSessionTask<Result.CreateNamespaceResult>
        {
			private readonly Request.CreateNamespaceRequest _request;

			public CreateNamespaceTask(Request.CreateNamespaceRequest request, UnityAction<AsyncResult<Result.CreateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/";

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.Name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.Name.ToString());
                }
                if (_request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.Description.ToString());
                }
                if (_request.FirebaseSecret != null)
                {
                    jsonWriter.WritePropertyName("firebaseSecret");
                    jsonWriter.Write(_request.FirebaseSecret.ToString());
                }
                if (_request.LogSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    _request.LogSetting.WriteJson(jsonWriter);
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateNamespace(
                Request.CreateNamespaceRequest request,
                UnityAction<AsyncResult<Result.CreateNamespaceResult>> callback
        )
		{
			var task = new CreateNamespaceTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetNamespaceStatusTask : Gs2RestSessionTask<Result.GetNamespaceStatusResult>
        {
			private readonly Request.GetNamespaceStatusRequest _request;

			public GetNamespaceStatusTask(Request.GetNamespaceStatusRequest request, UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/status";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetNamespaceStatus(
                Request.GetNamespaceStatusRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> callback
        )
		{
			var task = new GetNamespaceStatusTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetNamespaceTask : Gs2RestSessionTask<Result.GetNamespaceResult>
        {
			private readonly Request.GetNamespaceRequest _request;

			public GetNamespaceTask(Request.GetNamespaceRequest request, UnityAction<AsyncResult<Result.GetNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetNamespace(
                Request.GetNamespaceRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceResult>> callback
        )
		{
			var task = new GetNamespaceTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class UpdateNamespaceTask : Gs2RestSessionTask<Result.UpdateNamespaceResult>
        {
			private readonly Request.UpdateNamespaceRequest _request;

			public UpdateNamespaceTask(Request.UpdateNamespaceRequest request, UnityAction<AsyncResult<Result.UpdateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.Description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.Description.ToString());
                }
                if (_request.FirebaseSecret != null)
                {
                    jsonWriter.WritePropertyName("firebaseSecret");
                    jsonWriter.Write(_request.FirebaseSecret.ToString());
                }
                if (_request.LogSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    _request.LogSetting.WriteJson(jsonWriter);
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateNamespace(
                Request.UpdateNamespaceRequest request,
                UnityAction<AsyncResult<Result.UpdateNamespaceResult>> callback
        )
		{
			var task = new UpdateNamespaceTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteNamespaceTask : Gs2RestSessionTask<Result.DeleteNamespaceResult>
        {
			private readonly Request.DeleteNamespaceRequest _request;

			public DeleteNamespaceTask(Request.DeleteNamespaceRequest request, UnityAction<AsyncResult<Result.DeleteNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteNamespace(
                Request.DeleteNamespaceRequest request,
                UnityAction<AsyncResult<Result.DeleteNamespaceResult>> callback
        )
		{
			var task = new DeleteNamespaceTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeWebSocketSessionsTask : Gs2RestSessionTask<Result.DescribeWebSocketSessionsResult>
        {
			private readonly Request.DescribeWebSocketSessionsRequest _request;

			public DescribeWebSocketSessionsTask(Request.DescribeWebSocketSessionsRequest request, UnityAction<AsyncResult<Result.DescribeWebSocketSessionsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/session/user/me";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                if (_request.PageToken != null) {
                    queryStrings.Add(string.Format("{0}={1}", "pageToken", UnityWebRequest.EscapeURL(_request.PageToken)));
                }
                if (_request.Limit != null) {
                    queryStrings.Add(string.Format("{0}={1}", "limit", _request.Limit));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }
                if (_request.AccessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.AccessToken);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeWebSocketSessions(
                Request.DescribeWebSocketSessionsRequest request,
                UnityAction<AsyncResult<Result.DescribeWebSocketSessionsResult>> callback
        )
		{
			var task = new DescribeWebSocketSessionsTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeWebSocketSessionsByUserIdTask : Gs2RestSessionTask<Result.DescribeWebSocketSessionsByUserIdResult>
        {
			private readonly Request.DescribeWebSocketSessionsByUserIdRequest _request;

			public DescribeWebSocketSessionsByUserIdTask(Request.DescribeWebSocketSessionsByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeWebSocketSessionsByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/session/user/{userId}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                if (_request.PageToken != null) {
                    queryStrings.Add(string.Format("{0}={1}", "pageToken", UnityWebRequest.EscapeURL(_request.PageToken)));
                }
                if (_request.Limit != null) {
                    queryStrings.Add(string.Format("{0}={1}", "limit", _request.Limit));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeWebSocketSessionsByUserId(
                Request.DescribeWebSocketSessionsByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeWebSocketSessionsByUserIdResult>> callback
        )
		{
			var task = new DescribeWebSocketSessionsByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SetUserIdTask : Gs2RestSessionTask<Result.SetUserIdResult>
        {
			private readonly Request.SetUserIdRequest _request;

			public SetUserIdTask(Request.SetUserIdRequest request, UnityAction<AsyncResult<Result.SetUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/session/user/me/user";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.AllowConcurrentAccess != null)
                {
                    jsonWriter.WritePropertyName("allowConcurrentAccess");
                    jsonWriter.Write(_request.AllowConcurrentAccess.ToString());
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }
                if (_request.AccessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.AccessToken);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetUserId(
                Request.SetUserIdRequest request,
                UnityAction<AsyncResult<Result.SetUserIdResult>> callback
        )
		{
			var task = new SetUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SetUserIdByUserIdTask : Gs2RestSessionTask<Result.SetUserIdByUserIdResult>
        {
			private readonly Request.SetUserIdByUserIdRequest _request;

			public SetUserIdByUserIdTask(Request.SetUserIdByUserIdRequest request, UnityAction<AsyncResult<Result.SetUserIdByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/session/user/{userId}/user";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.AllowConcurrentAccess != null)
                {
                    jsonWriter.WritePropertyName("allowConcurrentAccess");
                    jsonWriter.Write(_request.AllowConcurrentAccess.ToString());
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetUserIdByUserId(
                Request.SetUserIdByUserIdRequest request,
                UnityAction<AsyncResult<Result.SetUserIdByUserIdResult>> callback
        )
		{
			var task = new SetUserIdByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SendNotificationTask : Gs2RestSessionTask<Result.SendNotificationResult>
        {
			private readonly Request.SendNotificationRequest _request;

			public SendNotificationTask(Request.SendNotificationRequest request, UnityAction<AsyncResult<Result.SendNotificationResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/session/user/{userId}/notification";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.Subject != null)
                {
                    jsonWriter.WritePropertyName("subject");
                    jsonWriter.Write(_request.Subject.ToString());
                }
                if (_request.Payload != null)
                {
                    jsonWriter.WritePropertyName("payload");
                    jsonWriter.Write(_request.Payload.ToString());
                }
                if (_request.EnableTransferMobileNotification != null)
                {
                    jsonWriter.WritePropertyName("enableTransferMobileNotification");
                    jsonWriter.Write(_request.EnableTransferMobileNotification.ToString());
                }
                if (_request.Sound != null)
                {
                    jsonWriter.WritePropertyName("sound");
                    jsonWriter.Write(_request.Sound.ToString());
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SendNotification(
                Request.SendNotificationRequest request,
                UnityAction<AsyncResult<Result.SendNotificationResult>> callback
        )
		{
			var task = new SendNotificationTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SetFirebaseTokenTask : Gs2RestSessionTask<Result.SetFirebaseTokenResult>
        {
			private readonly Request.SetFirebaseTokenRequest _request;

			public SetFirebaseTokenTask(Request.SetFirebaseTokenRequest request, UnityAction<AsyncResult<Result.SetFirebaseTokenResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/firebase/token";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.Token != null)
                {
                    jsonWriter.WritePropertyName("token");
                    jsonWriter.Write(_request.Token.ToString());
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }
                if (_request.AccessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.AccessToken);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetFirebaseToken(
                Request.SetFirebaseTokenRequest request,
                UnityAction<AsyncResult<Result.SetFirebaseTokenResult>> callback
        )
		{
			var task = new SetFirebaseTokenTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SetFirebaseTokenByUserIdTask : Gs2RestSessionTask<Result.SetFirebaseTokenByUserIdResult>
        {
			private readonly Request.SetFirebaseTokenByUserIdRequest _request;

			public SetFirebaseTokenByUserIdTask(Request.SetFirebaseTokenByUserIdRequest request, UnityAction<AsyncResult<Result.SetFirebaseTokenByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/firebase/token";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.Token != null)
                {
                    jsonWriter.WritePropertyName("token");
                    jsonWriter.Write(_request.Token.ToString());
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetFirebaseTokenByUserId(
                Request.SetFirebaseTokenByUserIdRequest request,
                UnityAction<AsyncResult<Result.SetFirebaseTokenByUserIdResult>> callback
        )
		{
			var task = new SetFirebaseTokenByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetFirebaseTokenTask : Gs2RestSessionTask<Result.GetFirebaseTokenResult>
        {
			private readonly Request.GetFirebaseTokenRequest _request;

			public GetFirebaseTokenTask(Request.GetFirebaseTokenRequest request, UnityAction<AsyncResult<Result.GetFirebaseTokenResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/firebase/token";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }
                if (_request.AccessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.AccessToken);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetFirebaseToken(
                Request.GetFirebaseTokenRequest request,
                UnityAction<AsyncResult<Result.GetFirebaseTokenResult>> callback
        )
		{
			var task = new GetFirebaseTokenTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetFirebaseTokenByUserIdTask : Gs2RestSessionTask<Result.GetFirebaseTokenByUserIdResult>
        {
			private readonly Request.GetFirebaseTokenByUserIdRequest _request;

			public GetFirebaseTokenByUserIdTask(Request.GetFirebaseTokenByUserIdRequest request, UnityAction<AsyncResult<Result.GetFirebaseTokenByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/firebase/token";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetFirebaseTokenByUserId(
                Request.GetFirebaseTokenByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetFirebaseTokenByUserIdResult>> callback
        )
		{
			var task = new GetFirebaseTokenByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteFirebaseTokenTask : Gs2RestSessionTask<Result.DeleteFirebaseTokenResult>
        {
			private readonly Request.DeleteFirebaseTokenRequest _request;

			public DeleteFirebaseTokenTask(Request.DeleteFirebaseTokenRequest request, UnityAction<AsyncResult<Result.DeleteFirebaseTokenResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/firebase/token";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }
                if (_request.AccessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.AccessToken);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteFirebaseToken(
                Request.DeleteFirebaseTokenRequest request,
                UnityAction<AsyncResult<Result.DeleteFirebaseTokenResult>> callback
        )
		{
			var task = new DeleteFirebaseTokenTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteFirebaseTokenByUserIdTask : Gs2RestSessionTask<Result.DeleteFirebaseTokenByUserIdResult>
        {
			private readonly Request.DeleteFirebaseTokenByUserIdRequest _request;

			public DeleteFirebaseTokenByUserIdTask(Request.DeleteFirebaseTokenByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteFirebaseTokenByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/firebase/token";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.ContextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.ContextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteFirebaseTokenByUserId(
                Request.DeleteFirebaseTokenByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteFirebaseTokenByUserIdResult>> callback
        )
		{
			var task = new DeleteFirebaseTokenByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SendMobileNotificationByUserIdTask : Gs2RestSessionTask<Result.SendMobileNotificationByUserIdResult>
        {
			private readonly Request.SendMobileNotificationByUserIdRequest _request;

			public SendMobileNotificationByUserIdTask(Request.SendMobileNotificationByUserIdRequest request, UnityAction<AsyncResult<Result.SendMobileNotificationByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "gateway")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/firebase/token/notification";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.UserId) ? _request.UserId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.Subject != null)
                {
                    jsonWriter.WritePropertyName("subject");
                    jsonWriter.Write(_request.Subject.ToString());
                }
                if (_request.Payload != null)
                {
                    jsonWriter.WritePropertyName("payload");
                    jsonWriter.Write(_request.Payload.ToString());
                }
                if (_request.Sound != null)
                {
                    jsonWriter.WritePropertyName("sound");
                    jsonWriter.Write(_request.Sound.ToString());
                }
                if (_request.ContextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.ContextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.RequestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.RequestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SendMobileNotificationByUserId(
                Request.SendMobileNotificationByUserIdRequest request,
                UnityAction<AsyncResult<Result.SendMobileNotificationByUserIdResult>> callback
        )
		{
			var task = new SendMobileNotificationByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }
	}
}