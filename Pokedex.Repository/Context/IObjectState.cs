using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pokedex.Repository.Context
{
	public interface IObjectState
	{
		[NotMapped]
		ObjectState ObjectState { get; set; }
	}
	public enum ObjectState
	{
		Unchanged,
		Added,
		Modified,
		Deleted
	}
}
