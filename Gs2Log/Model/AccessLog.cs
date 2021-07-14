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
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Log.Model
{

	[Preserve]
	public class AccessLog : IComparable
	{
        public long? Timestamp { set; get; }
        public string RequestId { set; get; }
        public string Service { set; get; }
        public string Method { set; get; }
        public string UserId { set; get; }
        public string Request { set; get; }
        public string Result { set; get; }

        public AccessLog WithTimestamp(long? timestamp) {
            this.Timestamp = timestamp;
            return this;
        }

        public AccessLog WithRequestId(string requestId) {
            this.RequestId = requestId;
            return this;
        }

        public AccessLog WithService(string service) {
            this.Service = service;
            return this;
        }

        public AccessLog WithMethod(string method) {
            this.Method = method;
            return this;
        }

        public AccessLog WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }

        public AccessLog WithRequest(string request) {
            this.Request = request;
            return this;
        }

        public AccessLog WithResult(string result) {
            this.Result = result;
            return this;
        }

    	[Preserve]
        public static AccessLog FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new AccessLog()
                .WithTimestamp(!data.Keys.Contains("timestamp") || data["timestamp"] == null ? null : (long?)long.Parse(data["timestamp"].ToString()))
                .WithRequestId(!data.Keys.Contains("requestId") || data["requestId"] == null ? null : data["requestId"].ToString())
                .WithService(!data.Keys.Contains("service") || data["service"] == null ? null : data["service"].ToString())
                .WithMethod(!data.Keys.Contains("method") || data["method"] == null ? null : data["method"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithRequest(!data.Keys.Contains("request") || data["request"] == null ? null : data["request"].ToString())
                .WithResult(!data.Keys.Contains("result") || data["result"] == null ? null : data["result"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["timestamp"] = Timestamp,
                ["requestId"] = RequestId,
                ["service"] = Service,
                ["method"] = Method,
                ["userId"] = UserId,
                ["request"] = Request,
                ["result"] = Result,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Timestamp != null) {
                writer.WritePropertyName("timestamp");
                writer.Write(long.Parse(Timestamp.ToString()));
            }
            if (RequestId != null) {
                writer.WritePropertyName("requestId");
                writer.Write(RequestId.ToString());
            }
            if (Service != null) {
                writer.WritePropertyName("service");
                writer.Write(Service.ToString());
            }
            if (Method != null) {
                writer.WritePropertyName("method");
                writer.Write(Method.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Request != null) {
                writer.WritePropertyName("request");
                writer.Write(Request.ToString());
            }
            if (Result != null) {
                writer.WritePropertyName("result");
                writer.Write(Result.ToString());
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as AccessLog;
            var diff = 0;
            if (Timestamp == null && Timestamp == other.Timestamp)
            {
                // null and null
            }
            else
            {
                diff += (int)(Timestamp - other.Timestamp);
            }
            if (RequestId == null && RequestId == other.RequestId)
            {
                // null and null
            }
            else
            {
                diff += RequestId.CompareTo(other.RequestId);
            }
            if (Service == null && Service == other.Service)
            {
                // null and null
            }
            else
            {
                diff += Service.CompareTo(other.Service);
            }
            if (Method == null && Method == other.Method)
            {
                // null and null
            }
            else
            {
                diff += Method.CompareTo(other.Method);
            }
            if (UserId == null && UserId == other.UserId)
            {
                // null and null
            }
            else
            {
                diff += UserId.CompareTo(other.UserId);
            }
            if (Request == null && Request == other.Request)
            {
                // null and null
            }
            else
            {
                diff += Request.CompareTo(other.Request);
            }
            if (Result == null && Result == other.Result)
            {
                // null and null
            }
            else
            {
                diff += Result.CompareTo(other.Result);
            }
            return diff;
        }
    }
}