using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImSoPunny.Models
{
	public class Pun
	{

		public Pun()
		{
			PunTags = new HashSet<PunTag>();
		}

		[Key]

		public int PunId { get; set; }

		[Required]
		public string Text { get; set; }
		public int Score { get; set; } = 0;

		public ICollection<PunTag> PunTags { get; set; }
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public AppUser User { get; set; }
	}

	public class PunTag
	{
		[Key]
		public int Id { get; set; }

		public int PunId { get; set; }
		public Pun Pun { get; set; }

		public int TagId { get; set; }
		public Tag Tag { get; set; }
	}

	public class PunDto
	{
		public int PunId { get; set; }
		public string Text { get; set; }
		public string[] Tags { get; set; }
		public string UserId { get; set; }
	}

	public class PunDtoReturn
	{
		public int PunId { get; set; }
		public string Text { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public int Score { get; set; }
		public AppUser User { get; set; }
	}
}
