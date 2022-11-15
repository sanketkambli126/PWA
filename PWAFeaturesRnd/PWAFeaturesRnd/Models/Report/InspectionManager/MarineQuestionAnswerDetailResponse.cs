using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Marine Question Answer Detail Response
    /// </summary>
    public class MarineQuestionAnswerDetailResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the user defined list answer.
        /// </summary>
        /// <value>
        /// The user defined list answer.
        /// </value>
        public List<MarineQuestionListAnswerResponse> UserDefinedListAnswer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is comment required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is comment required; otherwise, <c>false</c>.
        /// </value>
        public bool IsCommentRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finding required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is finding required; otherwise, <c>false</c>.
        /// </value>
        public bool IsFindingRequired { get; set; }

        /// <summary>
        /// Gets or sets the qal identifier.
        /// </summary>
        /// <value>
        /// The qal identifier.
        /// </value>
        public string QalId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the SCL identifier.
        /// </summary>
        /// <value>
        /// The SCL identifier.
        /// </value>
        public string SclId { get; set; }

        /// <summary>
        /// Gets or sets the scale description.
        /// </summary>
        /// <value>
        /// The scale description.
        /// </value>
        public string ScaleDescription { get; set; }

        /// <summary>
        /// Gets or sets the date answer.
        /// </summary>
        /// <value>
        /// The date answer.
        /// </value>
        public DateTime? DateAnswer { get; set; }

        /// <summary>
        /// Gets or sets the boolean answer.
        /// </summary>
        /// <value>
        /// The boolean answer.
        /// </value>
        public bool? BooleanAnswer { get; set; }

        /// <summary>
        /// Gets or sets the numeric answer.
        /// </summary>
        /// <value>
        /// The numeric answer.
        /// </value>
        public decimal? NumericAnswer { get; set; }

        /// <summary>
        /// Gets or sets the text answer.
        /// </summary>
        /// <value>
        /// The text answer.
        /// </value>
        public string TextAnswer { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int? Sequence { get; set; }

        /// <summary>
        /// Gets or sets the type of the answer.
        /// </summary>
        /// <value>
        /// The type of the answer.
        /// </value>
        public string AnswerType { get; set; }

        /// <summary>
        /// Gets or sets the type of the ial identifier answer.
        /// </summary>
        /// <value>
        /// The type of the ial identifier answer.
        /// </value>
        public string IalIdAnswerType { get; set; }

        /// <summary>
        /// Gets or sets the question reference number.
        /// </summary>
        /// <value>
        /// The question reference number.
        /// </value>
        public string QuestionReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the question description.
        /// </summary>
        /// <value>
        /// The question description.
        /// </value>
        public string QuestionDescription { get; set; }

        /// <summary>
        /// Gets or sets the VLK source description.
        /// </summary>
        /// <value>
        /// The VLK source description.
        /// </value>
        public string VlkSourceDescription { get; set; }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        public string SourceId { get; set; }

        /// <summary>
        /// Gets or sets the VLK identifier source.
        /// </summary>
        /// <value>
        /// The VLK identifier source.
        /// </value>
        public string VlkIdSource { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the mqa identifier.
        /// </summary>
        /// <value>
        /// The mqa identifier.
        /// </value>
        public string MqaId { get; set; }

        /// <summary>
        /// Gets or sets the qud identifier.
        /// </summary>
        /// <value>
        /// The qud identifier.
        /// </value>
        public string QudId { get; set; }

        /// <summary>
        /// Gets or sets the question remarks.
        /// </summary>
        /// <value>
        /// The question remarks.
        /// </value>
        public string QuestionRemarks { get; set; }
    }
}
