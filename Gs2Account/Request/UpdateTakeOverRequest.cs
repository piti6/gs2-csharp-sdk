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
using Gs2.Gs2Account.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Account.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateTakeOverRequest : Gs2Request<UpdateTakeOverRequest>
	{
        public string NamespaceName { set; get; }
        public string AccessToken { set; get; }
        public int? Type { set; get; }
        public string OldPassword { set; get; }
        public string Password { set; get; }

        public UpdateTakeOverRequest WithNamespaceName(string namespaceName) {
            this.NamespaceName = namespaceName;
            return this;
        }

        public UpdateTakeOverRequest WithAccessToken(string accessToken) {
            this.AccessToken = accessToken;
            return this;
        }

        public UpdateTakeOverRequest WithType(int? type) {
            this.Type = type;
            return this;
        }

        public UpdateTakeOverRequest WithOldPassword(string oldPassword) {
            this.OldPassword = oldPassword;
            return this;
        }

        public UpdateTakeOverRequest WithPassword(string password) {
            this.Password = password;
            return this;
        }

    	[Preserve]
        public static UpdateTakeOverRequest FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new UpdateTakeOverRequest()
                .WithNamespaceName(!data.Keys.Contains("namespaceName") || data["namespaceName"] == null ? null : data["namespaceName"].ToString())
                .WithAccessToken(!data.Keys.Contains("accessToken") || data["accessToken"] == null ? null : data["accessToken"].ToString())
                .WithType(!data.Keys.Contains("type") || data["type"] == null ? null : (int?)int.Parse(data["type"].ToString()))
                .WithOldPassword(!data.Keys.Contains("oldPassword") || data["oldPassword"] == null ? null : data["oldPassword"].ToString())
                .WithPassword(!data.Keys.Contains("password") || data["password"] == null ? null : data["password"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["namespaceName"] = NamespaceName,
                ["accessToken"] = AccessToken,
                ["type"] = Type,
                ["oldPassword"] = OldPassword,
                ["password"] = Password,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (NamespaceName != null) {
                writer.WritePropertyName("namespaceName");
                writer.Write(NamespaceName.ToString());
            }
            if (AccessToken != null) {
                writer.WritePropertyName("accessToken");
                writer.Write(AccessToken.ToString());
            }
            if (Type != null) {
                writer.WritePropertyName("type");
                writer.Write(int.Parse(Type.ToString()));
            }
            if (OldPassword != null) {
                writer.WritePropertyName("oldPassword");
                writer.Write(OldPassword.ToString());
            }
            if (Password != null) {
                writer.WritePropertyName("password");
                writer.Write(Password.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}