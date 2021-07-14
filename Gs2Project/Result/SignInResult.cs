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
	public class SignInResult : IResult
	{
        public Gs2.Gs2Project.Model.Account Item { set; get; }
        public string AccountToken { set; get; }

        public SignInResult WithItem(Gs2.Gs2Project.Model.Account item) {
            this.Item = item;
            return this;
        }

        public SignInResult WithAccountToken(string accountToken) {
            this.AccountToken = accountToken;
            return this;
        }

    	[Preserve]
        public static SignInResult FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new SignInResult()
                .WithItem(!data.Keys.Contains("item") || data["item"] == null ? null : Gs2.Gs2Project.Model.Account.FromJson(data["item"]))
                .WithAccountToken(!data.Keys.Contains("accountToken") || data["accountToken"] == null ? null : data["accountToken"].ToString());
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["item"] = Item?.ToJson(),
                ["accountToken"] = AccountToken,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (Item != null) {
                Item.WriteJson(writer);
            }
            if (AccountToken != null) {
                writer.WritePropertyName("accountToken");
                writer.Write(AccountToken.ToString());
            }
            writer.WriteObjectEnd();
        }
    }
}