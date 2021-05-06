using System;
using System.Collections.Generic;
using System.Text;

namespace hw4
{
	class Gallery
	{
		public int Id { get; set; }

		public string GalleryName { get; set; }
		public string Description { get; set; }
		public DateTime DateTimeGalleryCreated { get; set; }

		public List<Art> Arts { get; set; }
	}
}
