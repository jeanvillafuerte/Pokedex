using Pokedex.Repository.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Repository.Persistence
{
    public partial class Users : IObjectState
    {
        public Users()
        {
            Favorites = new HashSet<Favorites>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateReg { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Favorites> Favorites { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}