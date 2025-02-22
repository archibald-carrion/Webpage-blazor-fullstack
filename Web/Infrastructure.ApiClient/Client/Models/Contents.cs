// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.Models {
    public class Contents : IParsable 
    {
        /// <summary>The career property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.Models.Career? Career { get; set; }
#nullable restore
#else
        public UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.Models.Career Career { get; set; }
#endif
        /// <summary>The careerAcronym property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Acronym? CareerAcronym { get; set; }
#nullable restore
#else
        public Acronym CareerAcronym { get; set; }
#endif
        /// <summary>The contentName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public MediumName? ContentName { get; set; }
#nullable restore
#else
        public MediumName ContentName { get; set; }
#endif
        /// <summary>The contentType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public MediumName? ContentType { get; set; }
#nullable restore
#else
        public MediumName ContentType { get; set; }
#endif
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public LongName? Description { get; set; }
#nullable restore
#else
        public LongName Description { get; set; }
#endif
        /// <summary>The id property</summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="Contents"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Contents CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Contents();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"career", n => { Career = n.GetObjectValue<UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.Models.Career>(UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.Models.Career.CreateFromDiscriminatorValue); } },
                {"careerAcronym", n => { CareerAcronym = n.GetObjectValue<Acronym>(Acronym.CreateFromDiscriminatorValue); } },
                {"contentName", n => { ContentName = n.GetObjectValue<MediumName>(MediumName.CreateFromDiscriminatorValue); } },
                {"contentType", n => { ContentType = n.GetObjectValue<MediumName>(MediumName.CreateFromDiscriminatorValue); } },
                {"description", n => { Description = n.GetObjectValue<LongName>(LongName.CreateFromDiscriminatorValue); } },
                {"id", n => { Id = n.GetGuidValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<UCR.ECCI.IS.TECH_EVALUATION_1.Infrastructure.ApiClient.Client.Models.Career>("career", Career);
            writer.WriteObjectValue<Acronym>("careerAcronym", CareerAcronym);
            writer.WriteObjectValue<MediumName>("contentName", ContentName);
            writer.WriteObjectValue<MediumName>("contentType", ContentType);
            writer.WriteObjectValue<LongName>("description", Description);
            writer.WriteGuidValue("id", Id);
        }
    }
}
