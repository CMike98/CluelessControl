﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using CluelessControl.Converters;

namespace CluelessControl.Constants
{
    public static class GameConstants
    {
        #region Strings
        /// <summary>
        /// The title of the program.
        /// </summary>
        public const string PROGRAM_TITLE = "Gra w Ciemno - Reżyserka";

        /// <summary>
        /// Closing message - closing the program should be done via the director form.
        /// </summary>
        public const string CLOSE_ON_DIRECTOR_FORM_MESSAGE = "Zamknij reżyserkę, by zamknąć program.";

        /// <summary>
        /// "Cash" in PL language
        /// </summary>
        public const string CASH_STRING = "GOTÓWKA";

        /// <summary>
        /// "Offer" in PL language
        /// </summary>
        public const string OFFER_STRING = "OFERTA";

        /// <summary>
        /// "Prize Money" in PL language
        /// </summary>
        public const string PRIZE_STRING = "WYGRANA";

        /// <summary>
        /// "Contestant" in PL language
        /// </summary>
        public const string CONTESTANT_STRING = "ZAWODNIK";

        /// <summary>
        /// "Host" in PL language
        /// </summary>
        public const string HOST_STRING = "PROWADZĄCY";

        #endregion

        #region Numbers
        /// <summary>
        /// The default number of decimal places
        /// </summary>
        public const int DEFAULT_DECIMAL_PLACES = 2;
        #endregion

        #region Envelopes and cheques
        /// <summary>
        /// How much envelopes does the contestant select by default?
        /// </summary>
        public const int ENVELOPE_DEFAULT_COUNT = 5;

        /// <summary>
        /// How much envelopes can the system handle currently?
        /// </summary>
        public const int MAX_ENVELOPE_COUNT_PERSON = 10;

        /// <summary>
        /// Minimum number of the envelope
        /// </summary>
        public const int MIN_ENVELOPE_NUMBER = 1;

        /// <summary>
        /// Maximum number of the envelope
        /// </summary>
        public const int MAX_ENVELOPE_NUMBER = 50;

        /// <summary>
        /// How much maximum envelopes there are?
        /// </summary>
        public const int MAX_ENVELOPES_COUNT = 50;

        /// <summary>
        /// Minimum percentage cheque
        /// </summary>
        public const decimal MIN_PERCENTAGE = -100;
        #endregion

        #region Questions
        /// <summary>
        /// Number representing a first answer.
        /// </summary>
        public const int MIN_ANSWER_NUMBER = 1;

        /// <summary>
        /// Number representing the last answer.
        /// </summary>
        public const int MAX_ANSWER_NUMBER = 4;
        #endregion

        #region Json Serializer Options
        /// <summary>
        /// The serializer options for all JSON serializations/deserializations
        /// </summary>
        public static readonly JsonSerializerOptions JSON_SERIALIZER_OPTIONS = new()
        {
            Converters = {
                new JsonStringEnumConverter(),
                new JsonColorConverter(),
                new JsonGameSettingsConverter(),
                new JsonChequeConverter(),
                new JsonChequeSettingsConverter(),
                new JsonQuestionConverter(),
                new JsonQuestionSetConverter()
            },
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        #endregion
    }
}
