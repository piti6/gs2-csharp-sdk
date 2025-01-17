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
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
#endif

namespace Gs2.Gs2Matchmaking.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	public class Player : IComparable
	{
        public string UserId { set; get; }
        public Gs2.Gs2Matchmaking.Model.Attribute_[] Attributes { set; get; }
        public string RoleName { set; get; }
        public string[] DenyUserIds { set; get; }
        public Player WithUserId(string userId) {
            this.UserId = userId;
            return this;
        }
        public Player WithAttributes(Gs2.Gs2Matchmaking.Model.Attribute_[] attributes) {
            this.Attributes = attributes;
            return this;
        }
        public Player WithRoleName(string roleName) {
            this.RoleName = roleName;
            return this;
        }
        public Player WithDenyUserIds(string[] denyUserIds) {
            this.DenyUserIds = denyUserIds;
            return this;
        }

#if UNITY_2017_1_OR_NEWER
    	[Preserve]
#endif
        public static Player FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new Player()
                .WithUserId(!data.Keys.Contains("userId") || data["userId"] == null ? null : data["userId"].ToString())
                .WithAttributes(!data.Keys.Contains("attributes") || data["attributes"] == null ? new Gs2.Gs2Matchmaking.Model.Attribute_[]{} : data["attributes"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Matchmaking.Model.Attribute_.FromJson(v);
                }).ToArray())
                .WithRoleName(!data.Keys.Contains("roleName") || data["roleName"] == null ? null : data["roleName"].ToString())
                .WithDenyUserIds(!data.Keys.Contains("denyUserIds") || data["denyUserIds"] == null ? new string[]{} : data["denyUserIds"].Cast<JsonData>().Select(v => {
                    return v.ToString();
                }).ToArray());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["userId"] = UserId,
                ["attributes"] = Attributes == null ? null : new JsonData(
                        Attributes.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["roleName"] = RoleName,
                ["denyUserIds"] = DenyUserIds == null ? null : new JsonData(
                        DenyUserIds.Select(v => {
                            return new JsonData(v.ToString());
                        }).ToArray()
                    ),
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (UserId != null) {
                writer.WritePropertyName("userId");
                writer.Write(UserId.ToString());
            }
            if (Attributes != null) {
                writer.WritePropertyName("attributes");
                writer.WriteArrayStart();
                foreach (var attribute in Attributes)
                {
                    if (attribute != null) {
                        attribute.WriteJson(writer);
                    }
                }
                writer.WriteArrayEnd();
            }
            if (RoleName != null) {
                writer.WritePropertyName("roleName");
                writer.Write(RoleName.ToString());
            }
            if (DenyUserIds != null) {
                writer.WritePropertyName("denyUserIds");
                writer.WriteArrayStart();
                foreach (var denyUserId in DenyUserIds)
                {
                    if (denyUserId != null) {
                        writer.Write(denyUserId.ToString());
                    }
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Player;
            var diff = 0;
            if (UserId == null && UserId == other.UserId)
            {
                // null and null
            }
            else
            {
                diff += UserId.CompareTo(other.UserId);
            }
            if (Attributes == null && Attributes == other.Attributes)
            {
                // null and null
            }
            else
            {
                diff += Attributes.Length - other.Attributes.Length;
                for (var i = 0; i < Attributes.Length; i++)
                {
                    diff += Attributes[i].CompareTo(other.Attributes[i]);
                }
            }
            if (RoleName == null && RoleName == other.RoleName)
            {
                // null and null
            }
            else
            {
                diff += RoleName.CompareTo(other.RoleName);
            }
            if (DenyUserIds == null && DenyUserIds == other.DenyUserIds)
            {
                // null and null
            }
            else
            {
                diff += DenyUserIds.Length - other.DenyUserIds.Length;
                for (var i = 0; i < DenyUserIds.Length; i++)
                {
                    diff += DenyUserIds[i].CompareTo(other.DenyUserIds[i]);
                }
            }
            return diff;
        }
    }
}