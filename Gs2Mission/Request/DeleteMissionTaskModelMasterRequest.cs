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
using Gs2.Gs2Mission.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Request
{
	[Preserve]
	[System.Serializable]
	public class DeleteMissionTaskModelMasterRequest : Gs2Request<DeleteMissionTaskModelMasterRequest>
	{
        public string NamespaceName { set; get; }
        public string MissionGroupName { set; get; }
        public string MissionTaskName { set; get; }

        public DeleteMissionTaskModelMasterRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public DeleteMissionTaskModelMasterRequest WithMissionGroupName(string missionGroupName) {
            this.MissionGroupName = missionGroupName;
            return this;
        }

        public DeleteMissionTaskModelMasterRequest WithMissionTaskName(string missionTaskName) {
            this.MissionTaskName = missionTaskName;
            return this;
        }

    	[Preserve]
        public static DeleteMissionTaskModelMasterRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new DeleteMissionTaskModelMasterRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithMissionGroupName(!data.Keys.Contains("missionGroupName") || data["missionGroupName"] == null ? null : data["missionGroupName"].ToString())
                .WithMissionTaskName(!data.Keys.Contains("missionTaskName") || data["missionTaskName"] == null ? null : data["missionTaskName"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["missionGroupName"] = MissionGroupName,
                ["missionTaskName"] = MissionTaskName,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (MissionGroupName != null) {
                writer.WritePropertyName("missionGroupName");
                writer.Write(MissionGroupName.ToString());
            }
            if (MissionTaskName != null) {
                writer.WritePropertyName("missionTaskName");
                writer.Write(MissionTaskName.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}