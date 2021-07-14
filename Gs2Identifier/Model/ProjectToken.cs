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

namespace Gs2.Gs2Identifier.Model
{

	[Preserve]
	public class ProjectToken : IComparable
	{
        public string Token { set; get; }

        public ProjectToken WithToken(string token) {
            this.Token = token;
            return this;
        }

    	[Preserve]
        public static ProjectToken FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new ProjectToken()
                .WithToken(!data.Keys.Contains("token") || data["token"] == null ? null : data["token"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["token"] = Token,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Token != null) {
                writer.WritePropertyName("token");
                writer.Write(Token.ToString());
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as ProjectToken;
            var diff = 0;
            if (Token == null && Token == other.Token)
            {
                // null and null
            }
            else
            {
                diff += Token.CompareTo(other.Token);
            }
            return diff;
        }
    }
}