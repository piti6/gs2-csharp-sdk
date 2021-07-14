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
using Gs2.Gs2Inbox.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inbox.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateGlobalMessageMasterRequest : Gs2Request<CreateGlobalMessageMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string Name { set; get; }
        public string Metadata { set; get; }
        public Gs2.Gs2Inbox.Model.AcquireAction[] ReadAcquireActions { set; get; }
        public Gs2.Gs2Inbox.Model.TimeSpan_ ExpiresTimeSpan { set; get; }
        public long? ExpiresAt { set; get; }

        public CreateGlobalMessageMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public CreateGlobalMessageMasterRequest WithName(string name) {
            this.Name = name;
            return this;
        }

        public CreateGlobalMessageMasterRequest WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }

        public CreateGlobalMessageMasterRequest WithReadAcquireActions(Gs2.Gs2Inbox.Model.AcquireAction[] readAcquireActions) {
            this.ReadAcquireActions = readAcquireActions;
            return this;
        }

        public CreateGlobalMessageMasterRequest WithExpiresTimeSpan(Gs2.Gs2Inbox.Model.TimeSpan_ expiresTimeSpan) {
            this.ExpiresTimeSpan = expiresTimeSpan;
            return this;
        }

        public CreateGlobalMessageMasterRequest WithExpiresAt(long? expiresAt) {
            this.ExpiresAt = expiresAt;
            return this;
        }

    	[Preserve]
        public static CreateGlobalMessageMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CreateGlobalMessageMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithReadAcquireActions(!data.Keys.Contains("readAcquireActions") || data["readAcquireActions"] == null ? new Gs2.Gs2Inbox.Model.AcquireAction[]{} : data["readAcquireActions"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Inbox.Model.AcquireAction.FromJson(v);
                }).ToArray())
                .WithExpiresTimeSpan(!data.Keys.Contains("expiresTimeSpan") || data["expiresTimeSpan"] == null ? null : Gs2.Gs2Inbox.Model.TimeSpan_.FromJson(data["expiresTimeSpan"]))
                .WithExpiresAt(!data.Keys.Contains("expiresAt") || data["expiresAt"] == null ? null : (long?)long.Parse(data["expiresAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["name"] = Name,
                ["metadata"] = Metadata,
                ["readAcquireActions"] = new JsonData(ReadAcquireActions == null ? new JsonData[]{} :
                        ReadAcquireActions.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["expiresTimeSpan"] = ExpiresTimeSpan?.ToJson(),
                ["expiresAt"] = ExpiresAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            writer.WriteArrayStart();
            foreach (var readAcquireAction in ReadAcquireActions)
            {
                if (readAcquireAction != null) {
                    readAcquireAction.WriteJson(writer);
                }
            }
            writer.WriteArrayEnd();
            if (ExpiresTimeSpan != null) {
                ExpiresTimeSpan.WriteJson(writer);
            }
            if (ExpiresAt != null) {
                writer.WritePropertyName("expiresAt");
                writer.Write(long.Parse(ExpiresAt.ToString()));
            }
            writer.WriteObjectEnd();
        }
    }
}