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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Log.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Log.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class CountIssueStampSheetLogRequest : Gs2Request<CountIssueStampSheetLogRequest>
	{
        public string NamespaceName { set; get; }
        public bool? Service { set; get; }
        public bool? Method { set; get; }
        public bool? UserId { set; get; }
        public bool? Action { set; get; }
        public long? Begin { set; get; }
        public long? End { set; get; }
        public bool? LongTerm { set; get; }
        public string PageToken { set; get; }
        public int? Limit { set; get; }
        public CountIssueStampSheetLogRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public CountIssueStampSheetLogRequest WithService(bool? service) {
            this.Service = service;
            return this;
        }
        public CountIssueStampSheetLogRequest WithMethod(bool? method) {
            this.Method = method;
            return this;
        }
        public CountIssueStampSheetLogRequest WithUserId(bool? userId) {
            this.UserId = userId;
            return this;
        }
        public CountIssueStampSheetLogRequest WithAction(bool? action) {
            this.Action = action;
            return this;
        }
        public CountIssueStampSheetLogRequest WithBegin(long? begin) {
            this.Begin = begin;
            return this;
        }
        public CountIssueStampSheetLogRequest WithEnd(long? end) {
            this.End = end;
            return this;
        }
        public CountIssueStampSheetLogRequest WithLongTerm(bool? longTerm) {
            this.LongTerm = longTerm;
            return this;
        }
        public CountIssueStampSheetLogRequest WithPageToken(string pageToken) {
            this.PageToken = pageToken;
            return this;
        }
        public CountIssueStampSheetLogRequest WithLimit(int? limit) {
            this.Limit = limit;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static CountIssueStampSheetLogRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CountIssueStampSheetLogRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithService(!data.Keys.Contains("service") || data["service"] == null ? null : (bool?)bool.Parse(data["service"].ToString()))
                .WithMethod(!data.Keys.Contains("method") || data["method"] == null ? null : (bool?)bool.Parse(data["method"].ToString()))
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : (bool?)bool.Parse(data["userId"].ToString()))
                .WithAction(!data.Keys.Contains("action") || data["action"] == null ? null : (bool?)bool.Parse(data["action"].ToString()))
                .WithBegin(!data.Keys.Contains("begin") || data["begin"] == null ? null : (long?)long.Parse(data["begin"].ToString()))
                .WithEnd(!data.Keys.Contains("end") || data["end"] == null ? null : (long?)long.Parse(data["end"].ToString()))
                .WithLongTerm(!data.Keys.Contains("longTerm") || data["longTerm"] == null ? null : (bool?)bool.Parse(data["longTerm"].ToString()))
                .WithPageToken(!data.Keys.Contains("pageToken") || data["pageToken"] == null ? null : data["pageToken"].ToString())
                .WithLimit(!data.Keys.Contains("limit") || data["limit"] == null ? null : (int?)int.Parse(data["limit"].ToString()));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["service"] = Service,
                ["method"] = Method,
                ["userId"] = UserId,
                ["action"] = Action,
                ["begin"] = Begin,
                ["end"] = End,
                ["longTerm"] = LongTerm,
                ["pageToken"] = PageToken,
                ["limit"] = Limit,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (Service != null) {
                writer.WritePropertyName("service");
                writer.Write(bool.Parse(Service.ToString()));
            }
            if (Method != null) {
                writer.WritePropertyName("method");
                writer.Write(bool.Parse(Method.ToString()));
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(bool.Parse(UserId.ToString()));
            }
            if (Action != null) {
                writer.WritePropertyName("action");
                writer.Write(bool.Parse(Action.ToString()));
            }
            if (Begin != null) {
                writer.WritePropertyName("begin");
                writer.Write(long.Parse(Begin.ToString()));
            }
            if (End != null) {
                writer.WritePropertyName("end");
                writer.Write(long.Parse(End.ToString()));
            }
            if (LongTerm != null) {
                writer.WritePropertyName("longTerm");
                writer.Write(bool.Parse(LongTerm.ToString()));
            }
            if (PageToken != null) {
                writer.WritePropertyName("pageToken");
                writer.Write(PageToken.ToString());
            }
            if (Limit != null) {
                writer.WritePropertyName("limit");
                writer.Write(int.Parse(Limit.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += Service + ":";
            key += Method + ":";
            key += UserId + ":";
            key += Action + ":";
            key += Begin + ":";
            key += End + ":";
            key += LongTerm + ":";
            key += PageToken + ":";
            key += Limit + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply CountIssueStampSheetLogRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (CountIssueStampSheetLogRequest)x;
            return this;
        }
    }
}