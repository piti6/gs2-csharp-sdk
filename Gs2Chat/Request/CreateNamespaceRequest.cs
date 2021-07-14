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
using Gs2.Gs2Chat.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Chat.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{
        public string Name { set; get; }
        public string Description { set; get; }
        public bool? AllowCreateRoom { set; get; }
        public Gs2.Gs2Chat.Model.ScriptSetting PostMessageScript { set; get; }
        public Gs2.Gs2Chat.Model.ScriptSetting CreateRoomScript { set; get; }
        public Gs2.Gs2Chat.Model.ScriptSetting DeleteRoomScript { set; get; }
        public Gs2.Gs2Chat.Model.ScriptSetting SubscribeRoomScript { set; get; }
        public Gs2.Gs2Chat.Model.ScriptSetting UnsubscribeRoomScript { set; get; }
        public Gs2.Gs2Chat.Model.NotificationSetting PostNotification { set; get; }
        public Gs2.Gs2Chat.Model.LogSetting LogSetting { set; get; }

        public CreateNamespaceRequest WithName(string name) {
            this.Name = name;
            return this;
        }

        public CreateNamespaceRequest WithDescription(string description) {
            this.Description = description;
            return this;
        }

        public CreateNamespaceRequest WithAllowCreateRoom(bool? allowCreateRoom) {
            this.AllowCreateRoom = allowCreateRoom;
            return this;
        }

        public CreateNamespaceRequest WithPostMessageScript(Gs2.Gs2Chat.Model.ScriptSetting postMessageScript) {
            this.PostMessageScript = postMessageScript;
            return this;
        }

        public CreateNamespaceRequest WithCreateRoomScript(Gs2.Gs2Chat.Model.ScriptSetting createRoomScript) {
            this.CreateRoomScript = createRoomScript;
            return this;
        }

        public CreateNamespaceRequest WithDeleteRoomScript(Gs2.Gs2Chat.Model.ScriptSetting deleteRoomScript) {
            this.DeleteRoomScript = deleteRoomScript;
            return this;
        }

        public CreateNamespaceRequest WithSubscribeRoomScript(Gs2.Gs2Chat.Model.ScriptSetting subscribeRoomScript) {
            this.SubscribeRoomScript = subscribeRoomScript;
            return this;
        }

        public CreateNamespaceRequest WithUnsubscribeRoomScript(Gs2.Gs2Chat.Model.ScriptSetting unsubscribeRoomScript) {
            this.UnsubscribeRoomScript = unsubscribeRoomScript;
            return this;
        }

        public CreateNamespaceRequest WithPostNotification(Gs2.Gs2Chat.Model.NotificationSetting postNotification) {
            this.PostNotification = postNotification;
            return this;
        }

        public CreateNamespaceRequest WithLogSetting(Gs2.Gs2Chat.Model.LogSetting logSetting) {
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
                .WithAllowCreateRoom(!data.Keys.Contains("allowCreateRoom") || data["allowCreateRoom"] == null ? null : (bool?)bool.Parse(data["allowCreateRoom"].ToString()))
                .WithPostMessageScript(!data.Keys.Contains("postMessageScript") || data["postMessageScript"] == null ? null : Gs2.Gs2Chat.Model.ScriptSetting.FromJson(data["postMessageScript"]))
                .WithCreateRoomScript(!data.Keys.Contains("createRoomScript") || data["createRoomScript"] == null ? null : Gs2.Gs2Chat.Model.ScriptSetting.FromJson(data["createRoomScript"]))
                .WithDeleteRoomScript(!data.Keys.Contains("deleteRoomScript") || data["deleteRoomScript"] == null ? null : Gs2.Gs2Chat.Model.ScriptSetting.FromJson(data["deleteRoomScript"]))
                .WithSubscribeRoomScript(!data.Keys.Contains("subscribeRoomScript") || data["subscribeRoomScript"] == null ? null : Gs2.Gs2Chat.Model.ScriptSetting.FromJson(data["subscribeRoomScript"]))
                .WithUnsubscribeRoomScript(!data.Keys.Contains("unsubscribeRoomScript") || data["unsubscribeRoomScript"] == null ? null : Gs2.Gs2Chat.Model.ScriptSetting.FromJson(data["unsubscribeRoomScript"]))
                .WithPostNotification(!data.Keys.Contains("postNotification") || data["postNotification"] == null ? null : Gs2.Gs2Chat.Model.NotificationSetting.FromJson(data["postNotification"]))
                .WithLogSetting(!data.Keys.Contains("logSetting") || data["logSetting"] == null ? null : Gs2.Gs2Chat.Model.LogSetting.FromJson(data["logSetting"]));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["name"] = Name,
                ["description"] = Description,
                ["allowCreateRoom"] = AllowCreateRoom,
                ["postMessageScript"] = PostMessageScript?.ToJson(),
                ["createRoomScript"] = CreateRoomScript?.ToJson(),
                ["deleteRoomScript"] = DeleteRoomScript?.ToJson(),
                ["subscribeRoomScript"] = SubscribeRoomScript?.ToJson(),
                ["unsubscribeRoomScript"] = UnsubscribeRoomScript?.ToJson(),
                ["postNotification"] = PostNotification?.ToJson(),
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
            if (AllowCreateRoom != null) {
                writer.WritePropertyName("allowCreateRoom");
                writer.Write(bool.Parse(AllowCreateRoom.ToString()));
            }
            if (PostMessageScript != null) {
                PostMessageScript.WriteJson(writer);
            }
            if (CreateRoomScript != null) {
                CreateRoomScript.WriteJson(writer);
            }
            if (DeleteRoomScript != null) {
                DeleteRoomScript.WriteJson(writer);
            }
            if (SubscribeRoomScript != null) {
                SubscribeRoomScript.WriteJson(writer);
            }
            if (UnsubscribeRoomScript != null) {
                UnsubscribeRoomScript.WriteJson(writer);
            }
            if (PostNotification != null) {
                PostNotification.WriteJson(writer);
            }
            if (LogSetting != null) {
                LogSetting.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }
    }
}