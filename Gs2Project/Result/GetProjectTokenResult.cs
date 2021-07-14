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
using Gs2.Gs2Project.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Result
{
	[Preserve]
	[System.Serializable]
	public class GetProjectTokenResult : IResult
	{
        public Gs2.Gs2Project.Model.Project Item { set; get; }
        public string OwnerId { set; get; }
        public string ProjectToken { set; get; }

        public GetProjectTokenResult WithItem(Gs2.Gs2Project.Model.Project item) {
            this.Item = item;
            return this;
        }

        public GetProjectTokenResult WithOwnerId(string ownerId) {
            this.OwnerId = ownerId;
            return this;
        }

        public GetProjectTokenResult WithProjectToken(string projectToken) {
            this.ProjectToken = projectToken;
            return this;
        }

    	[Preserve]
        public static GetProjectTokenResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new GetProjectTokenResult()
                .WithItem(!data.Keys.Contains("item") || data["item"] == null ? null : Gs2.Gs2Project.Model.Project.FromJson(data["item"]))
                .WithOwnerId(!data.Keys.Contains("ownerId") || data["ownerId"] == null ? null : data["ownerId"].ToString())
                .WithProjectToken(!data.Keys.Contains("projectToken") || data["projectToken"] == null ? null : data["projectToken"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["item"] = Item?.ToJson(),
                ["ownerId"] = OwnerId,
                ["projectToken"] = ProjectToken,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Item != null) {
                Item.WriteJson(writer);
            }
            if (OwnerId != null) {
                writer.WritePropertyName("ownerId");
                writer.Write(OwnerId.ToString());
            }
            if (ProjectToken != null) {
                writer.WritePropertyName("projectToken");
                writer.Write(ProjectToken.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}