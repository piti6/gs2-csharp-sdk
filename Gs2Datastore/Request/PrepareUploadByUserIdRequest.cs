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
using Gs2.Gs2Datastore.Model;
using Gs2.Util.LitJson;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Datastore.Request
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	public class PrepareUploadByUserIdRequest : Gs2Request<PrepareUploadByUserIdRequest>
	{
        public string NamespaceName { set; get; }
        public string UserId { set; get; }
        public string Name { set; get; }
        public string ContentType { set; get; }
        public string Scope { set; get; }
        public string[] AllowUserIds { set; get; }
        public bool? UpdateIfExists { set; get; }
        public string DuplicationAvoider { set; get; }
        public PrepareUploadByUserIdRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }
        public PrepareUploadByUserIdRequest WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public PrepareUploadByUserIdRequest WithName(string name) {
            this.Name = name;
            return this;
        }
        public PrepareUploadByUserIdRequest WithContentType(string contentType) {
            this.ContentType = contentType;
            return this;
        }
        public PrepareUploadByUserIdRequest WithScope(string scope) {
            this.Scope = scope;
            return this;
        }
        public PrepareUploadByUserIdRequest WithAllowUserIds(string[] allowUserIds) {
            this.AllowUserIds = allowUserIds;
            return this;
        }
        public PrepareUploadByUserIdRequest WithUpdateIfExists(bool? updateIfExists) {
            this.UpdateIfExists = updateIfExists;
            return this;
        }

        public PrepareUploadByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.DuplicationAvoider = duplicationAvoider;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static PrepareUploadByUserIdRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new PrepareUploadByUserIdRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithContentType(!data.Keys.Contains("contentType") || data["contentType"] == null ? null : data["contentType"].ToString())
                .WithScope(!data.Keys.Contains("scope") || data["scope"] == null ? null : data["scope"].ToString())
                .WithAllowUserIds(!data.Keys.Contains("allowUserIds") || data["allowUserIds"] == null ? new string[]{} : data["allowUserIds"].Cast<JsonData>().Select(v => {
                    return v.ToString();
                }).ToArray())
                .WithUpdateIfExists(!data.Keys.Contains("updateIfExists") || data["updateIfExists"] == null ? null : (bool?)bool.Parse(data["updateIfExists"].ToString()));
        }

        public override JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["userId"] = UserId,
                ["name"] = Name,
                ["contentType"] = ContentType,
                ["scope"] = Scope,
                ["allowUserIds"] = AllowUserIds == null ? null : new JsonData(
                        AllowUserIds.Select(v => {
                            return new JsonData(v.ToString());
                        }).ToArray()
                    ),
                ["updateIfExists"] = UpdateIfExists,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (ContentType != null) {
                writer.WritePropertyName("contentType");
                writer.Write(ContentType.ToString());
            }
            if (Scope != null) {
                writer.WritePropertyName("scope");
                writer.Write(Scope.ToString());
            }
            writer.WriteArrayStart();
            foreach (var allowUserId in AllowUserIds)
            {
                writer.Write(allowUserId.ToString());
            }
            writer.WriteArrayEnd();
            if (UpdateIfExists != null) {
                writer.WritePropertyName("updateIfExists");
                writer.Write(bool.Parse(UpdateIfExists.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public override string UniqueKey() {
            var key = "";
            key += NamespaceName + ":";
            key += UserId + ":";
            key += Name + ":";
            key += ContentType + ":";
            key += Scope + ":";
            key += AllowUserIds + ":";
            key += UpdateIfExists + ":";
            return key;
        }

        protected override Gs2Request DoMultiple(int x) {
            if (x != 1) {
                throw new ArithmeticException("Unsupported multiply PrepareUploadByUserIdRequest");
            }
            return this;
        }

        protected override Gs2Request DoAdd(Gs2Request x) {
            var y = (PrepareUploadByUserIdRequest)x;
            return this;
        }
    }
}