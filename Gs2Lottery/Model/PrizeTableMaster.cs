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

namespace Gs2.Gs2Lottery.Model
{

	[Preserve]
	public class PrizeTableMaster : IComparable
	{
        public string PrizeTableId { set; get; }
        public string Name { set; get; }
        public string Metadata { set; get; }
        public string Description { set; get; }
        public Gs2.Gs2Lottery.Model.Prize[] Prizes { set; get; }
        public long? CreatedAt { set; get; }
        public long? UpdatedAt { set; get; }

        public PrizeTableMaster WithPrizeTableId(string prizeTableId) {
            this.PrizeTableId = prizeTableId;
            return this;
        }

        public PrizeTableMaster WithName(string name) {
            this.Name = name;
            return this;
        }

        public PrizeTableMaster WithMetadata(string metadata) {
            this.Metadata = metadata;
            return this;
        }

        public PrizeTableMaster WithDescription(string description) {
            this.Description = description;
            return this;
        }

        public PrizeTableMaster WithPrizes(Gs2.Gs2Lottery.Model.Prize[] prizes) {
            this.Prizes = prizes;
            return this;
        }

        public PrizeTableMaster WithCreatedAt(long? createdAt) {
            this.CreatedAt = createdAt;
            return this;
        }

        public PrizeTableMaster WithUpdatedAt(long? updatedAt) {
            this.UpdatedAt = updatedAt;
            return this;
        }

    	[Preserve]
        public static PrizeTableMaster FromJson(JsonData data)
        {
            if (data == null) {
                return null;
            }
            return new PrizeTableMaster()
                .WithPrizeTableId(!data.Keys.Contains("prizeTableId") || data["prizeTableId"] == null ? null : data["prizeTableId"].ToString())
                .WithName(!data.Keys.Contains("name") || data["name"] == null ? null : data["name"].ToString())
                .WithMetadata(!data.Keys.Contains("metadata") || data["metadata"] == null ? null : data["metadata"].ToString())
                .WithDescription(!data.Keys.Contains("description") || data["description"] == null ? null : data["description"].ToString())
                .WithPrizes(!data.Keys.Contains("prizes") || data["prizes"] == null ? new Gs2.Gs2Lottery.Model.Prize[]{} : data["prizes"].Cast<JsonData>().Select(v => {
                    return Gs2.Gs2Lottery.Model.Prize.FromJson(v);
                }).ToArray())
                .WithCreatedAt(!data.Keys.Contains("createdAt") || data["createdAt"] == null ? null : (long?)long.Parse(data["createdAt"].ToString()))
                .WithUpdatedAt(!data.Keys.Contains("updatedAt") || data["updatedAt"] == null ? null : (long?)long.Parse(data["updatedAt"].ToString()));
        }

        public JsonData ToJson()
        {
            return new JsonData {
                ["prizeTableId"] = PrizeTableId,
                ["name"] = Name,
                ["metadata"] = Metadata,
                ["description"] = Description,
                ["prizes"] = new JsonData(Prizes == null ? new JsonData[]{} :
                        Prizes.Select(v => {
                            //noinspection Convert2MethodRef
                            return v.ToJson();
                        }).ToArray()
                    ),
                ["createdAt"] = CreatedAt,
                ["updatedAt"] = UpdatedAt,
            };
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if (PrizeTableId != null) {
                writer.WritePropertyName("prizeTableId");
                writer.Write(PrizeTableId.ToString());
            }
            if (Name != null) {
                writer.WritePropertyName("name");
                writer.Write(Name.ToString());
            }
            if (Metadata != null) {
                writer.WritePropertyName("metadata");
                writer.Write(Metadata.ToString());
            }
            if (Description != null) {
                writer.WritePropertyName("description");
                writer.Write(Description.ToString());
            }
            if (Prizes != null) {
                writer.WritePropertyName("prizes");
                writer.WriteArrayStart();
                foreach (var prize in Prizes)
                {
                    if (prize != null) {
                        prize.WriteJson(writer);
                    }
                }
                writer.WriteArrayEnd();
            }
            if (CreatedAt != null) {
                writer.WritePropertyName("createdAt");
                writer.Write(long.Parse(CreatedAt.ToString()));
            }
            if (UpdatedAt != null) {
                writer.WritePropertyName("updatedAt");
                writer.Write(long.Parse(UpdatedAt.ToString()));
            }
            writer.WriteObjectEnd();
        }

        public int CompareTo(object obj)
        {
            var other = obj as PrizeTableMaster;
            var diff = 0;
            if (PrizeTableId == null && PrizeTableId == other.PrizeTableId)
            {
                // null and null
            }
            else
            {
                diff += PrizeTableId.CompareTo(other.PrizeTableId);
            }
            if (Name == null && Name == other.Name)
            {
                // null and null
            }
            else
            {
                diff += Name.CompareTo(other.Name);
            }
            if (Metadata == null && Metadata == other.Metadata)
            {
                // null and null
            }
            else
            {
                diff += Metadata.CompareTo(other.Metadata);
            }
            if (Description == null && Description == other.Description)
            {
                // null and null
            }
            else
            {
                diff += Description.CompareTo(other.Description);
            }
            if (Prizes == null && Prizes == other.Prizes)
            {
                // null and null
            }
            else
            {
                diff += Prizes.Length - other.Prizes.Length;
                for (var i = 0; i < Prizes.Length; i++)
                {
                    diff += Prizes[i].CompareTo(other.Prizes[i]);
                }
            }
            if (CreatedAt == null && CreatedAt == other.CreatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(CreatedAt - other.CreatedAt);
            }
            if (UpdatedAt == null && UpdatedAt == other.UpdatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(UpdatedAt - other.UpdatedAt);
            }
            return diff;
        }
    }
}