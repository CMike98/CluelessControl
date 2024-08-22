﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CluelessControl
{
    public static class Constants
    {
        /// <summary>
        /// The title of the program.
        /// </summary>
        public const string PROGRAM_TITLE = "Gra w Ciemno - Reżyserka";

        /// <summary>
        /// Closing message - closing the program should be done via the director form.
        /// </summary>
        public const string CLOSE_ON_DIRECTOR_FORM_MESSAGE = "Zamknij reżyserkę, by zamknąć program.";

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
        /// How much envelopes to pick?
        /// </summary>
        public const int HOW_MUCH_ENVELOPES_TO_PICK = 5;

        /// <summary>
        /// Number of answers to each question in the game.
        /// </summary>
        public const int ANSWERS_PER_QUESTION = 4;
        
        /// <summary>
        /// Minimum percentage cheque
        /// </summary>
        public const decimal MIN_PERCENTAGE = -100;

        #region Drawing
        /// <summary>
        /// Font family used when drawing the envelopes
        /// </summary>
        public static readonly FontFamily DRAWING_FONT_FAMILY = new("Arial");

        /// <summary>
        /// Font size used when drawing the envelopes
        /// </summary>
        public const float DRAWING_FONT_SIZE = 24.0f;

        /// <summary>
        /// Font used when drawing the envelopes
        /// </summary>
        public static readonly Font DRAWING_FONT = new(DRAWING_FONT_FAMILY, DRAWING_FONT_SIZE);
        #endregion

        #region Json Serializer Options
        public static readonly JsonSerializerOptions JSON_SERIALIZER_OPTIONS = new()
        {
            Converters = {
                new JsonStringEnumConverter(),
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
