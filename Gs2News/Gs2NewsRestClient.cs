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
using Gs2.Util.LitJson;namespace Gs2.Gs2News
{
	public class Gs2NewsRestClient : AbstractGs2Client
	{
		private readonly CertificateHandler _certificateHandler;

		public static string Endpoint = "news";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2NewsRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="gs2RestSession">REST API 用セッション</param>
		/// <param name="certificateHandler"></param>
		public Gs2NewsRestClient(Gs2RestSession gs2RestSession, CertificateHandler certificateHandler) : base(gs2RestSession)
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
                    .Replace("{service}", "news")
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
                    .Replace("{service}", "news")
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
                    .Replace("{service}", "news")
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
                    .Replace("{service}", "news")
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
                    .Replace("{service}", "news")
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
                    .Replace("{service}", "news")
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

        private class PrepareUpdateCurrentNewsMasterTask : Gs2RestSessionTask<Result.PrepareUpdateCurrentNewsMasterResult>
        {
			private readonly Request.PrepareUpdateCurrentNewsMasterRequest _request;

			public PrepareUpdateCurrentNewsMasterTask(Request.PrepareUpdateCurrentNewsMasterRequest request, UnityAction<AsyncResult<Result.PrepareUpdateCurrentNewsMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/prepare";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
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
		public IEnumerator PrepareUpdateCurrentNewsMaster(
                Request.PrepareUpdateCurrentNewsMasterRequest request,
                UnityAction<AsyncResult<Result.PrepareUpdateCurrentNewsMasterResult>> callback
        )
		{
			var task = new PrepareUpdateCurrentNewsMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentNewsMasterTask : Gs2RestSessionTask<Result.UpdateCurrentNewsMasterResult>
        {
			private readonly Request.UpdateCurrentNewsMasterRequest _request;

			public UpdateCurrentNewsMasterTask(Request.UpdateCurrentNewsMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentNewsMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.UploadToken != null)
                {
                    jsonWriter.WritePropertyName("uploadToken");
                    jsonWriter.Write(_request.UploadToken.ToString());
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
		public IEnumerator UpdateCurrentNewsMaster(
                Request.UpdateCurrentNewsMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentNewsMasterResult>> callback
        )
		{
			var task = new UpdateCurrentNewsMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class PrepareUpdateCurrentNewsMasterFromGitHubTask : Gs2RestSessionTask<Result.PrepareUpdateCurrentNewsMasterFromGitHubResult>
        {
			private readonly Request.PrepareUpdateCurrentNewsMasterFromGitHubRequest _request;

			public PrepareUpdateCurrentNewsMasterFromGitHubTask(Request.PrepareUpdateCurrentNewsMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.PrepareUpdateCurrentNewsMasterFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/from_git_hub";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.NamespaceName) ? _request.NamespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.CheckoutSetting != null)
                {
                    jsonWriter.WritePropertyName("checkoutSetting");
                    _request.CheckoutSetting.WriteJson(jsonWriter);
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
		public IEnumerator PrepareUpdateCurrentNewsMasterFromGitHub(
                Request.PrepareUpdateCurrentNewsMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.PrepareUpdateCurrentNewsMasterFromGitHubResult>> callback
        )
		{
			var task = new PrepareUpdateCurrentNewsMasterFromGitHubTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeNewsTask : Gs2RestSessionTask<Result.DescribeNewsResult>
        {
			private readonly Request.DescribeNewsRequest _request;

			public DescribeNewsTask(Request.DescribeNewsRequest request, UnityAction<AsyncResult<Result.DescribeNewsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/news";

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
		public IEnumerator DescribeNews(
                Request.DescribeNewsRequest request,
                UnityAction<AsyncResult<Result.DescribeNewsResult>> callback
        )
		{
			var task = new DescribeNewsTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeNewsByUserIdTask : Gs2RestSessionTask<Result.DescribeNewsByUserIdResult>
        {
			private readonly Request.DescribeNewsByUserIdRequest _request;

			public DescribeNewsByUserIdTask(Request.DescribeNewsByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeNewsByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/news";

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
		public IEnumerator DescribeNewsByUserId(
                Request.DescribeNewsByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeNewsByUserIdResult>> callback
        )
		{
			var task = new DescribeNewsByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class WantGrantTask : Gs2RestSessionTask<Result.WantGrantResult>
        {
			private readonly Request.WantGrantRequest _request;

			public WantGrantTask(Request.WantGrantRequest request, UnityAction<AsyncResult<Result.WantGrantResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/news/grant";

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
		public IEnumerator WantGrant(
                Request.WantGrantRequest request,
                UnityAction<AsyncResult<Result.WantGrantResult>> callback
        )
		{
			var task = new WantGrantTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class WantGrantByUserIdTask : Gs2RestSessionTask<Result.WantGrantByUserIdResult>
        {
			private readonly Request.WantGrantByUserIdRequest _request;

			public WantGrantByUserIdTask(Request.WantGrantByUserIdRequest request, UnityAction<AsyncResult<Result.WantGrantByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "news")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/news/grant";

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
		public IEnumerator WantGrantByUserId(
                Request.WantGrantByUserIdRequest request,
                UnityAction<AsyncResult<Result.WantGrantByUserIdResult>> callback
        )
		{
			var task = new WantGrantByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }
	}
}