using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImSoPunny.Models
{
	public class Tag
	{

		public Tag()
		{
			PunTags = new HashSet<PunTag>();
		}

		[Key]
		public int TagId { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		public string Acronym { get; set; }

		public ICollection<PunTag> PunTags { get; set; }

	}

	public class TagDto
	{
		public string Text { get; set; }
		public string Acronym { get; set; }
	}

	public class TagDtoReturn
	{
		public int TagId { get; set; }
		public string Text { get; set; }
		public string Acronym { get; set; }
		public int Count { get; set; }
	}
}
