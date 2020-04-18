﻿using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ICQ.Bot.Helpers;
using ICQ.Bot.Requests.Abstractions;
using ICQ.Bot.Types;
using ICQ.Bot.Types.Enums;
using ICQ.Bot.Types.InputFiles;
using ICQ.Bot.Types.ReplyMarkups;

namespace ICQ.Bot.Requests
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SendDocumentRequest : FileRequestBase<MessagesResponse>,
                                       INotifiableMessage,
                                       IReplyMessage,
                                       IReplyMarkupMessage<IReplyMarkup>
    {
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        [JsonProperty(Required = Required.Always)]
        public InputOnlineFile Document { get; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InputMedia Thumb { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ParseMode ParseMode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        public SendDocumentRequest(ChatId chatId, InputOnlineFile document, string caption, IReplyMarkup replyMarkup)
            : base("/messages/sendFile", HttpMethod.Post)
        {
            ChatId = chatId;
            Document = document;
            Caption = caption;
            ReplyMarkup = replyMarkup;

            Parameters.Add("chatId", ChatId);
            Parameters.Add("fileId", Document.FileId);

            if (!string.IsNullOrWhiteSpace(Caption))
            {
                Parameters.Add("caption", Caption);
            }

            if (ReplyMarkup != null)
            {
                string markup = JsonConvert.SerializeObject(ReplyMarkup);
                Parameters.Add("inlineKeyboardMarkup", markup);
            }
        }
    }
}