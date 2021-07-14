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
using Gs2.Gs2Realtime.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Realtime.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{
        public string Name { set; get; }
        public string Description { set; get; }
        public string ServerType { set; get; }
        public string ServerSpec { set; get; }
        public Gs2.Gs2Realtime.Model.NotificationSetting CreateNotification { set; get; }
        public Gs2.Gs2Realtime.Model.LogSetting LogSetting { set; get; }

        public CreateNamespaceRequest WithName(string name) {
            this.Name = name;
            return this;
        }

        public CreateNamespaceRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }

        public CreateNamespaceRequest WithServerType(string serverType) {
            this.ServerType = serverType;
            return this;
        }

        public CreateNamespaceRequest WithServerSpec(string serverSpec) {
            this.ServerSpec = serverSpec;
            return this;
        }

        public CreateNamespaceRequest WithCreateNotification(Gs2.Gs2Realtime.Model.NotificationSetting createNotification) {
            this.CreateNotification = createNotification;
            return this;
        }

        public CreateNamespaceRequest WithLogSetting(Gs2.Gs2Realtime.Model.LogSetting logSetting) {
            this.LogSetting = logSetting;
            return this;
        }

    	[Preserve]
        public static CreateNamespaceRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new CreateNamespaceRequest()
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithServerType(!data.Keys.Contains("serverType") || data["serverType"] == null ? null : data["serverType"].ToString())
                .WithServerSpec(!data.Keys.Contains("serverSpec") || data["serverSpec"] == null ? null : data["serverSpec"].ToString())
                .WithCreateNotification(!data.Keys.Contains("createNotification") || data["createNotification"] == null ? null : Gs2.Gs2Realtime.Model.NotificationSetting.FromJson(data["createNotification"]))
                .WithLogSetting(!data.Keys.Contains("logSetting") || data["logSetting"] == null ? null : Gs2.Gs2Realtime.Model.LogSetting.FromJson(data["logSetting"]));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["name"] = Name,
                ["description"] = Description,
                ["serverType"] = ServerType,
                ["serverSpec"] = ServerSpec,
                ["createNotification"] = CreateNotification?.ToJson(),
                ["logSetting"] = LogSetting?.ToJson(),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (ServerType != null) {
                writer.WritePropertyName("serverType");
                writer.Write(ServerType.ToString());
            }
            if (ServerSpec != null) {
                writer.WritePropertyName("serverSpec");
                writer.Write(ServerSpec.ToString());
            }
            if (CreateNotification != null) {
                CreateNotification.WriteJson(writer);
            }
            if (LogSetting != null) {
                LogSetting.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }
    }
}